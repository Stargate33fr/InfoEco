using FluentValidation.Results;
using MediatR;

namespace Infeco.Api.Infrastructure.MediatR
{
    public abstract class Command : IRequest
    {
        public int Id { get; set; }

        public abstract ValidationResult Valide();
    }
}
