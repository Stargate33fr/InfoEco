using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Infeco.Api.Controllers
{
    public abstract class AppControllerBase : ControllerBase
    {
        protected readonly IMediator Mediator;

        protected AppControllerBase(IMediator mediator)
        {
            Mediator = mediator;
        }
    }
}
