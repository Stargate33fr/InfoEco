using Infeco.Api.Infrastructure.MediatR;

namespace Infeco.Api.Commands.Appartements
{
    public abstract class ActionAppartementCommand: Command
    {
        public string? Adresse { get; set; }
        public string? ComplementAdresse { get; set; }
        public int VilleId { get; set; }
        public double PrixDesCharges { get; set; }
        public double Loyer { get; set; }
        public double DepotDeGarantie { get; set; }
        public double NombreDeMetre2 { get; set; }
        public int TypeAppartementId { get; set; }
    }
}
