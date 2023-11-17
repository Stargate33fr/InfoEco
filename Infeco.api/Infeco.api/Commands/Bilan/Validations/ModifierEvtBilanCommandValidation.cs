using Infeco.Api.Commands.Locataire.Validations;

namespace Infeco.Api.Commands.Bilan.Validations
{
    public class ModifierEvtBilanCommandValidation : EvtBilanCommandValidation<ModifierEvtBilanCommand>
    {
        public ModifierEvtBilanCommandValidation()
        {
            ValideId();
            ValideMail();
            ValideDateEnvoi();
        }
    }
}
