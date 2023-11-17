using FluentValidation;
using Infeco.Api.Commands.EtatDesLieux;
using Infeco.Api.Commands.Locataire.Validations;

namespace Infeco.Api.Commands.Bilan.Validations
{
    public class CreerEvtBilanCommandValidation : EvtBilanCommandValidation<CreerEvtBilanCommand>
    {

        public CreerEvtBilanCommandValidation()
        {
            ValideMail();
            ValideDateEnvoi();
        }
    }
}
