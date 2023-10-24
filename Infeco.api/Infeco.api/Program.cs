using FluentValidation;
using Infeco.api.Infrastructure.MediatR;
using Infeco.Api.Common.SwashBuckle;
using Infeco.Api.Infrastructure.Authentication;
using Infeco.Api.Infrastructure.AutoMapper;
using Infeco.Api.Infrastructure.MediatR;
using Infoeco.infrastructure;
using Infoeco.infrastructure.Configuration;
using Infoeco.Services;
using Infoeco.Services.Implementation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Text;

var configuration = GetConfiguration(args);

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor()
                .AddMemoryCache()
                .AddCustomMvc(builder.Configuration)
                .AddAutoMapper(typeof(AutoMapperProfile));

var appConfiguration = builder.Configuration.Get<AppConfiguration>();
if (appConfiguration != null && appConfiguration?.AuthentificationSettings!=null)
{
    builder.Services.AddSingleton<IAppConfiguration>(appConfiguration);

    Log.Logger = new LoggerConfiguration()
               .ReadFrom.Configuration(configuration)
               .CreateLogger();

    builder.Host.UseSerilog((hostContext, loggerConfig) =>
      {
          loggerConfig
              .ReadFrom.Configuration(hostContext.Configuration)
              .Enrich.FromLogContext();
      });

    var domainAssembly = typeof(CommandHandlerBase<>).GetTypeInfo().Assembly;
    builder.Services.AddMediatR(domainAssembly);
    builder.Services.AddValidatorsFromAssembly(domainAssembly);
    builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

    var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appConfiguration.AuthentificationSettings.SecretKey));

    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "InfoEco API", Version = "v1" });
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please insert JWT with Bearer into field",
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey
        });
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
        c.OperationFilter<AttachOperationNameFilter>();

        c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "apiKey",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
    });


    var tokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = appConfiguration?.JwtOptions?.Issuer,

        ValidateAudience = true,
        ValidAudience = appConfiguration?.JwtOptions?.Audience,

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = signingKey,

        RequireExpirationTime = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };

    // https://github.com/AzureAD/azure-activedirectory-identitymodel-extensions-for-dotnet/issues/415
    // Pour que le "sub" ne soit pas interprété en "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"
    JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
                    {
                        options.ClaimsIssuer = appConfiguration?.JwtOptions?.Issuer;
                        options.TokenValidationParameters = tokenValidationParameters;
                        options.SaveToken = true;

                        options.Events = new JwtBearerEvents
                        {
                            OnAuthenticationFailed = context =>
                            {
                                if (context.Exception.GetType() != typeof(SecurityTokenExpiredException))
                                    return Task.CompletedTask;

                                context.Response.Headers.Add("Token-Expired", "true");
                                var refererHeader = context.Request.GetTypedHeaders().Referer;
                                if (refererHeader == null)
                                    return Task.CompletedTask;
                                var origin = refererHeader.GetComponents(UriComponents.Scheme | UriComponents.Host | UriComponents.Port, UriFormat.UriEscaped);
                                context.Response.Headers.Add("Access-Control-Allow-Origin", origin);

                                return Task.CompletedTask;
                            }
                        };
                    });
}

var connectionString = builder.Configuration?.GetSection("DatabaseSettings:DefaultConnectionString");

if (connectionString != null && connectionString.Value != null)
{

    builder.Services.AddDbContext<InfoEcoDbContext>(opt => opt.UseNpgsql(connectionString.Value).LogTo(Console.WriteLine));
}


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IVerifieurReferencesService, VerifieurReferencesService>();
builder.Services.AddScoped<IInfoEcoService, InfoEcoService>();

var app = builder.Build();

app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseCors("CorsPolicy");

app.MapControllers().RequireAuthorization();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "InfoEco API v1");
    c.RoutePrefix = string.Empty;
});

using (var serviceScope = app.Services.CreateScope())
{
    var context = serviceScope.ServiceProvider.GetService<InfoEcoDbContext>();
    if (context != null)
    {
        context.Database.Migrate();
    }
}

app.Run();

static IConfiguration GetConfiguration(string[] args)
{
    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    var isDevelopment = environment == Environments.Development;

    var configurationBuilder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true);

    configurationBuilder.AddCommandLine(args);
    configurationBuilder.AddEnvironmentVariables();

    return configurationBuilder.Build();
}

internal static class CustomExtensions
{
    internal static IServiceCollection AddCustomMvc(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddControllers()
            .AddNewtonsoftJson();

        return services.AddCors(options =>
        {
#pragma warning disable CS8604 // Existence possible d'un argument de référence null.
            options.AddPolicy("CorsPolicy",
                builder => builder
                   //.AllowAnyOrigin()
                   .WithOrigins(configuration.GetSection("CORS:WithOrigins").Get<string[]>())
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    );
#pragma warning restore CS8604 // Existence possible d'un argument de référence null.
        });
    }
}