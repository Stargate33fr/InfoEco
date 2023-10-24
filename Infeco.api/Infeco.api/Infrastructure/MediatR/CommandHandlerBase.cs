using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Infoeco.Services;
using MediatR;

namespace Infeco.Api.Infrastructure.MediatR
{
    public abstract class CommandHandlerBase<TCommande> : IRequestHandler<TCommande>
        where TCommande : Command
    {
        protected readonly IMapper Mapper;
        protected readonly IHttpContextAccessor HttpContextAccessor;
        protected readonly IVerifieurReferencesService VerifieurReferenceService;
        protected readonly ILoggerFactory LoggerFactory;
        protected readonly ILogger Logger;

        protected CommandHandlerBase(IMapper mapper, IHttpContextAccessor httpContextAccessor, IVerifieurReferencesService verifieurReferenceService, ILoggerFactory loggerFactory)
        {
            Mapper = mapper;
            HttpContextAccessor = httpContextAccessor;
            VerifieurReferenceService = verifieurReferenceService;
            LoggerFactory = loggerFactory;
            Logger = loggerFactory.CreateLogger(typeof(CommandHandlerBase<>));
        }

        protected abstract List<Func<Task<ValidationFailure>>>? DefinitLesVerifieurs(TCommande commande, CancellationToken cancellationToken);

        protected abstract Task ExecuteCommandeAsync(TCommande commande, CancellationToken cancellationToken);

        public async Task<Unit> Handle(TCommande commande, CancellationToken cancellationToken)
        {
            var verifieursIdentifiants = DefinitLesVerifieurs(commande, cancellationToken);
            if (verifieursIdentifiants != null)
            {
                var failures = await VerifieurReferenceService.VerifieAsync(verifieursIdentifiants.ToArray());
                if (failures.Any())
                    throw new ValidationException(failures);
            }

            await ExecuteCommandeAsync(commande, cancellationToken);
            return Unit.Value;
        }
    }
}
