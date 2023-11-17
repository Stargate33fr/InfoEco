using FluentValidation.Results;
using Infeco.Api.Commands.Bilan.Validations;
using Infeco.Api.Infrastructure.MediatR;

namespace Infeco.Api.Commands.Bilan
{
    public class CreerEvtBilanCommand : EvtBilanCommand
    {
        public override ValidationResult Valide()
        {
            return new CreerEvtBilanCommandValidation().Validate(this);
        }
    }
}
