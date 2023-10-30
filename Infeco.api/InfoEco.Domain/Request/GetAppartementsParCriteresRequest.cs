namespace InfoEco.Domain.Request
{
    public class GetAppartementsParCriteresRequest
    {   
        public int? Id { get; set; }
        public int? AgenceImmobiliereId { get; set; }

        public int? TypeAppartementId { get; set; }

        public string? Adresse { get; set; }
       
        public string? NomVille { get; set; }

        public string? NomLocataire { get; set; }

        public PaginationRequest? Pagination { get; set; }
        public TriRequest? Tri { get; set; }
    }
}
