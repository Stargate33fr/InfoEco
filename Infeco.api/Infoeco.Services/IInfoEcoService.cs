using Infoeco.infrastructure.Entities;
using InfoEco.Domain.Request;

namespace Infoeco.Services
{
    public interface IInfoEcoService
    {
        Task<List<CiviliteEntite>?> ObtientCivilites(CancellationToken cancellationToken);
        Task<List<TypeAppartementEntite>> ObtientTypeAppartements(CancellationToken cancellationToken);
        Task<List<TypePaiementEntite>> ObtientTypePaiements(CancellationToken cancellationToken);
        Task<List<VilleEntite>?> ObtientVilleParNom(string nomRecherche, CancellationToken cancellationToken);
        Task<VilleEntite?> ObtientVilleParId(int id);
        Task<EtatDesLieuxEntite?> ObtientEtatDesLieuxAsync(int locataireAppartementId, CancellationToken cancellationToken);
        Task<PaiementEntite?> ObtientPaiementParIdEtlocataireIdAsync(int id, int locataireAppartementId, CancellationToken cancellationToken);
        Task<List<PaiementEntite>?> ObtientPaiementTousAsync(int locataireAppartementId, CancellationToken cancellationToken);
        Task<List<QuittanceLoyerEntite>?> ObtientQuittanceLoyersAsync(int locataireAppartementId, CancellationToken cancellationToken);
        Task<QuittanceLoyerEntite?> ObtientQuittanceLoyersParIdAsync(int id, int locataireAppartementId, CancellationToken cancellationToken);
        Task<UtilisateurEntite?> ObtientParIdentifiantEtMotDePasseAsync(string identifiant, string motDePasse, CancellationToken cancellationToken);
        Task<UtilisateurEntite?> ObtientUtilisateurParEmailAsync(string email, CancellationToken cancellationToken);
        Task<AppartementEntite?> ObtientAppartementParIdAsync(int id, CancellationToken cancellationToken);
        Task<List<AppartementEntite>?> RechercheAppartementsAsync(GetAppartementsParCriteresRequest request, CancellationToken cancellationToken);
        Task<int?> GetCountAppartementsAsync(GetAppartementsParCriteresRequest request, CancellationToken cancellation);
        Task<AppartementEntite?> CreateAppartementAsync(AppartementEntite appartement);
        Task ModifierAppartementAsync(AppartementEntite appartement);
        Task SupprimerAppartementAsync(AppartementEntite appartement);
        Task<LocataireEntite?> ObtientLocataireParIdAsync(int id, CancellationToken cancellationToken);
        Task<LocataireEntite?> CreateLocataireAsync(LocataireEntite locataire);
        Task<IReadOnlyCollection<LocataireAppartementEntite>?> RechercheLocataireAppartementAsync(GetLocatairesAppartementRequest request);
        Task ModifierLocataireAsync(LocataireEntite locataire);
        Task SupprimerLocataireAsync(LocataireEntite locataire);
        Task ModifierPaiementAsync(PaiementEntite paiement);
        Task SupprimerPaiementAsync(PaiementEntite paiement);
        Task SupprimerQuittanceLoyerAsync(QuittanceLoyerEntite quittance);
        Task<LocataireAppartementEntite?> AssigneLocataireAUnAppartement(LocataireAppartementEntite locataireAppartement);
        Task<EtatDesLieuxEntite?> AjoutEtatDesLieux(EtatDesLieuxEntite etatsDesLieux);
        Task<QuittanceLoyerEntite?> AjoutQuittanceLoyer(QuittanceLoyerEntite quittanceLoyer);
        Task<PaiementEntite?> AjoutPaiement(PaiementEntite paiement);
    }
}