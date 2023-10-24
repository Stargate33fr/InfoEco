namespace Infeco.Api.ViewModel
{
    public class AppartementViewModel
    {
        public int Id { get; set; }
        public string? Adresse { get; set; }
        public string? ComplementAdresse { get; set; }
        public VilleViewModel? Ville { get; set; }
        public TypeAppartementViewModel? TypeAppartement { get; set; }
        public double PrixDesCharges { get; set; }
        public double Loyer { get; set; }
        public double DepotDeGarantie { get; set; }
        public double? NombreDeMetre2 { get; set; }
        public int AgenceImmobiliereId {get;set;}
        public AgenceImmobiliereViewModel? AgenceImmobiliere { get; set; }
        public string? NomResidence { get; set; }
        public int? NumeroAppartement { get; set; }
    }
}
