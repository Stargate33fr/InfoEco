namespace Infeco.Api.Commands.Locataire.Validations
{
    public class SupprimerLocataireCommandValidation : ActionLocataireCommandValidation<SupprimerLocataireCommand>
    {
        public SupprimerLocataireCommandValidation()
        {
            ValideId();
        }
    }
}
