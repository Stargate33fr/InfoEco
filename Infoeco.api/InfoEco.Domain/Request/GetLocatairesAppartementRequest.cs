namespace InfoEco.Domain.Request
{
    public class GetLocatairesAppartementRequest
    {
        public int? AppartementId { get; set; }
        public string? MailLocataire { get; set; }
        public int? LocataireId { get; set; }
        public int? VilleId { get; set; }
        public PaginationRequest? Pagination { get; set; }
        public TriRequest? Tri { get; set; }
    }
}
