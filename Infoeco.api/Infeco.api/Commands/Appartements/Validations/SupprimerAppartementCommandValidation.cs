namespace Infeco.Api.Commands.Appartements.Validations
{
    public class SupprimerAppartementCommandValidation : ActionAppartementCommandValidation<SupprimerAppartementCommand>
    {
        public SupprimerAppartementCommandValidation()
        {
            ValideId();
        }
    }
}
