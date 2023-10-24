using AutoMapper;
using MediatR;

namespace Infeco.Api.Infrastructure.MediatR
{
    public abstract class QueryHandlerBase<TRequest, TResult> : IRequestHandler<TRequest, TResult>
        where TRequest : IRequest<TResult>
    {
        protected readonly IMapper Mapper;
        protected readonly IHttpContextAccessor HttpContextAccessor;

        protected QueryHandlerBase(IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            HttpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public abstract Task<TResult> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
