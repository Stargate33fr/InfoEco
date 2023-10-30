namespace Infoeco.infrastructure.Entities
{
    public class AppartementEntite : TrackedEntity
    {
        public int Id { get; set; }
        public string? Adresse { get; set; }
        public string? ComplementAdresse { get; set; }
        public int VilleId { get; set; }
        public double PrixDesCharges { get; set; }
        public double Loyer { get; set; }
        public double DepotDeGarantie { get; set; }
        public double NombreDeMetre2 { get; set; }
        public int AgenceImmobiliereId { get; set; }
        public virtual VilleEntite? Ville {  get; set; }
        public virtual AgenceImmobiliereEntite? AgenceImmobiliere {  get; set; }
        public int? TypeAppartementId { get; set; }
        public virtual TypeAppartementEntite? TypeAppartement { get; set; }
        public virtual ICollection<LocataireAppartementEntite>? LocatairesAppartements { get; set; }
        public string? NomResidence { get; set; }
        public int? NumeroAppartement { get; set; }
    }
}
