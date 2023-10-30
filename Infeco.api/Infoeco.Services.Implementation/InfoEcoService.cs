using Infoeco.infrastructure;
using Infoeco.infrastructure.Entities;
using InfoEco.Domain.Abstractions.Queries;
using InfoEco.Domain.Request;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading;

namespace Infoeco.Services.Implementation
{
    public class InfoEcoService : IInfoEcoService
    {
        private readonly InfoEcoDbContext _context;
        private const int _nombreMaximumResultats = 20;


        private Func<GetLocatairesAppartementRequest, Expression<Func<LocataireAppartementEntite, bool>>> Recherche
       => (criteres)
           => (demande)
               => (string.IsNullOrEmpty(criteres.MailLocataire) || (!string.IsNullOrWhiteSpace(criteres.MailLocataire) && demande!=null && demande.Locataire !=null && demande.Locataire.Mail.Contains(criteres.MailLocataire))) &&
                      (!criteres.AppartementId.HasValue || demande.AppartementId == criteres.AppartementId) &&
                      (!criteres.Id.HasValue || demande.Id == criteres.Id) &&
                      (!criteres.LocataireId.HasValue || demande.LocataireId == criteres.LocataireId) &&
                      (!criteres.VilleId.HasValue || demande.Appartement.VilleId == criteres.VilleId);


        private Func<GetAppartementsParCriteresRequest, Expression<Func<LocataireAppartementEntite, bool>>> RechercheAppartements
     => (criteres)
         => (demande)
             => (string.IsNullOrEmpty(criteres.Adresse) || (!string.IsNullOrWhiteSpace(criteres.Adresse) && demande != null && demande.Appartement != null && demande.Appartement.Adresse != null && demande.Appartement.Adresse.Contains(criteres.Adresse))) &&
                    (!criteres.Id.HasValue || (demande != null && demande.Id == criteres.Id)) &&
                    (!criteres.AgenceImmobiliereId.HasValue || (demande.Appartement != null && demande.Appartement.AgenceImmobiliereId == criteres.AgenceImmobiliereId)) &&
                    (string.IsNullOrEmpty(criteres.NomLocataire) || (demande.Locataire != null && demande.Locataire.Nom.ToLower().Contains(criteres.NomLocataire.ToLower()))) &&
                    (string.IsNullOrEmpty(criteres.NomVille) || (!string.IsNullOrEmpty(criteres.NomVille) && demande.Appartement.Ville != null && demande.Appartement.Ville.Nom.ToLower().Contains(criteres.NomVille.ToLower()))) &&
                    (!criteres.TypeAppartementId.HasValue || (demande.Appartement != null && demande.Appartement.TypeAppartementId == criteres.TypeAppartementId));

        private Func<GetAppartementsParCriteresRequest, Expression<Func<AppartementEntite, bool>>> RechercheAppartementsDansAppartements
        => (criteres)
            => (demande)
              => (string.IsNullOrEmpty(criteres.Adresse) || (!string.IsNullOrWhiteSpace(criteres.Adresse) && demande != null && demande != null && demande.Adresse != null && demande.Adresse.Contains(criteres.Adresse))) &&
                     (!criteres.AgenceImmobiliereId.HasValue || (demande != null && demande.AgenceImmobiliereId == criteres.AgenceImmobiliereId)) &&
                     (string.IsNullOrEmpty(criteres.NomVille) || (!string.IsNullOrEmpty(criteres.NomVille) && demande.Ville != null && demande.Ville.Nom.ToLower().Contains(criteres.NomVille.ToLower()))) &&
                     (!criteres.TypeAppartementId.HasValue || (demande != null && demande.TypeAppartementId == criteres.TypeAppartementId));


        public InfoEcoService(InfoEcoDbContext context)
        {
            _context = context;
        }
        public async Task<UtilisateurEntite?> ObtientParIdentifiantEtMotDePasseAsync(string identifiant, string motDePasse, CancellationToken cancellationToken)
        {
            if (_context.Utilisateur != null)
            {
                return await _context.Utilisateur.FirstOrDefaultAsync(u => u.Courriel == identifiant && u.Passe == motDePasse, cancellationToken: cancellationToken);
            }
            return null;
        }

        public async Task<UtilisateurEntite?> ObtientUtilisateurParEmailAsync(string email, CancellationToken cancellationToken)
        {
            if (_context.Utilisateur != null)
            {
                return await _context.Utilisateur.FirstOrDefaultAsync(u => u.Courriel == email, cancellationToken: cancellationToken);
            }
            return null;
        }

        public async Task<List<CiviliteEntite>> ObtientCivilites(CancellationToken cancellationToken)
        {
            if (_context.Civilite != null)
            {
                return await _context.Civilite.ToListAsync(cancellationToken: cancellationToken);
            }
            return null;
        }

        public async Task<List<TypeAppartementEntite>> ObtientTypeAppartements(CancellationToken cancellationToken)
        {
            if (_context.TypeAppartement != null)
            {
                return await _context.TypeAppartement.ToListAsync(cancellationToken: cancellationToken);
            }
            return null;
        }

        public async Task<List<TypePaiementEntite>> ObtientTypePaiements(CancellationToken cancellationToken)
        {
            if (_context.TypePaiement != null)
            {
                return await _context.TypePaiement.ToListAsync(cancellationToken: cancellationToken);
            }
            return null;
        }

        public async Task<List<VilleEntite>?> ObtientVilleParNom(string nomRecherche, CancellationToken cancellationToken)
        {
            if (_context.Ville != null)
            {
                return await _context.Ville
                    .Where(u => u.Nom.ToLower().StartsWith(nomRecherche.ToLower()))
                    .OrderBy(u => u.Nom)
                    .ToListAsync(cancellationToken: cancellationToken);    
            }
            return null;
        }

        public async Task<VilleEntite?> ObtientVilleParId(int id)
        {
            if (_context.Ville != null)
            {
                return await _context.Ville.FirstOrDefaultAsync(u => u.Id == id);
            }
            return null;
        }

        public async Task<AppartementEntite?> ObtientAppartementParIdAsync(int id, CancellationToken cancellationToken)
        {
            if (_context.Appartement != null)
            {
                return await _context.Appartement
                    .Include(u => u.Ville)
                    .Include(u => u.TypeAppartement)
                    .AsNoTracking().FirstOrDefaultAsync(u => u.Id == id, cancellationToken: cancellationToken);
            }
            return null;
        }

       public async Task<QuittanceLoyerEntite?> ObtientQuittanceLoyersParIdAsync(int id, int locataireAppartementId, CancellationToken cancellationToken)
         {
            if (_context.QuittanceLoyer != null)
            {
                return await _context.QuittanceLoyer
                    .AsNoTracking().FirstOrDefaultAsync(u => u.Id == id && u.LocataireAppartementId == locataireAppartementId, cancellationToken: cancellationToken);
            }
            return null;
        }

        public async Task<PaiementEntite?> ObtientPaiementParIdEtlocataireIdAsync(int id, int locataireAppartementId,  CancellationToken cancellationToken)
        {
            if (_context.Paiement != null)
            {
                return await _context.Paiement
                    .Include(u => u.TypePaiement)
                    .AsNoTracking().FirstOrDefaultAsync(u => u.Id == id && u.LocataireAppartementId== locataireAppartementId, cancellationToken: cancellationToken);
            }
            return null;
        }

        public async Task<List<PaiementEntite>?> ObtientPaiementTousAsync(int locataireAppartementId, CancellationToken cancellationToken)
        {
            if (_context.Paiement != null)
            {
                return await _context.Paiement
                      .Include(u => u.TypePaiement)
                    .AsNoTracking().Where(u => u.LocataireAppartementId == locataireAppartementId).ToListAsync();
            }
            return null;
        }

        public async Task<List<QuittanceLoyerEntite>?> ObtientQuittanceLoyersAsync(int locataireAppartementId, CancellationToken cancellationToken)
        {
            if (_context.QuittanceLoyer != null)
            {
                return await _context.QuittanceLoyer
                    .Include(u => u.LocataireAppartement)
                    .AsNoTracking().Where(u => u.LocataireAppartementId == locataireAppartementId).ToListAsync();
            }
            return null;
        }

        public async Task<EtatDesLieuxEntite?> ObtientEtatDesLieuxAsync(int locataireAppartementId, CancellationToken cancellationToken)
        {
            if (_context.EtatDesLieux != null)
            {
                return await _context.EtatDesLieux
                    .Include(u => u.LocataireAppartement)
                    .AsNoTracking().FirstOrDefaultAsync(u => u.LocataireAppartementId == locataireAppartementId, cancellationToken: cancellationToken);
            }
            return null;
        }

        public async Task<AppartementEntite?> CreateAppartementAsync(AppartementEntite appartement)
        {
            if (_context?.Appartement != null)
            {
                var inserted = await _context.Appartement.AddAsync(appartement);
                await _context.SaveChangesAsync();
                return inserted.Entity;
            }
            return null;
        }

        public async Task ModifierAppartementAsync(AppartementEntite appartement)
        {
            try
            {
                if (_context?.Appartement != null)
                {
                    _context.Appartement.Update(appartement);
                    await _context.SaveChangesAsync();
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<List<AppartementEntite>?> RechercheAppartementsAsync(GetAppartementsParCriteresRequest request, CancellationToken cancellationToken)
        {
            if (_context.Appartement!=null && _context.LocataireAppartement != null && request != null && request.Tri != null && request.Pagination != null)
            {
                var order = request.Tri.Ascendant.HasValue && request.Tri.Ascendant.Value ? string.Empty : "descending";

                var tousLesAppartements = new List<AppartementEntite>();

                var locataireAppartement = await _context.LocataireAppartement
                  .Include(u => u.Appartement)
                      .ThenInclude(v => v.TypeAppartement)
                   .Include(u => u.Appartement)
                      .ThenInclude(v => v.Ville)
                  .Include(u => u.Locataire)
                  .Where(RechercheAppartements(request)).ToListAsync();

                var regroupement = locataireAppartement.GroupBy(u => u.Appartement).ToList();
                tousLesAppartements = regroupement.Select(u => u.Key).ToList();

                if (string.IsNullOrEmpty(request.NomLocataire))
                {
                    var appartement = await _context.Appartement
                            .Include(v => v.TypeAppartement)
                            .Include(v => v.Ville)
                            .Where(u => !regroupement.Select(u => u.Key).Contains(u))
                             .Where(RechercheAppartementsDansAppartements(request)).ToListAsync();

                    tousLesAppartements.AddRange(appartement);
                }
              
                if (tousLesAppartements.Count>0)
                {
                    return tousLesAppartements.AsQueryable()
                        .OrderBy($"{request.Tri.Champ} {order}") 
                        .Skip(request.Pagination.Skip)
                        .Take(request.Pagination.Limite)
                        .ToList();
                }
            }
            return null;
        }


        public async Task<int?> GetCountAppartementsAsync(GetAppartementsParCriteresRequest request, CancellationToken cancellation)
        {
            if (_context.LocataireAppartement != null && request != null && request.Tri != null)
            {
                var order = request.Tri.Ascendant.HasValue && request.Tri.Ascendant.Value ? string.Empty : "descending";

                return await _context.LocataireAppartement
                    .Include(u => u.Appartement)
                   .Where(RechercheAppartements(request))
                   .OrderBy($"{request.Tri.Champ} {order}")
                   .Select(u => u.Appartement).Distinct()
                    .CountAsync();
            }

            return 0;
        }

        public async Task SupprimerAppartementAsync(AppartementEntite appartement)
        {
            if (_context.Appartement != null)
            {
               _context.Appartement.Remove(appartement);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<LocataireEntite?> ObtientLocataireParIdAsync(int id, CancellationToken cancellationToken)
        {
            if (_context.Locataire != null)
            {
                return await _context.Locataire
                        .Include(u => u.Civilite)
                    .FirstOrDefaultAsync(u => u.Id == id, cancellationToken: cancellationToken);
            }
            return null;
        }

        public async Task<LocataireEntite?> CreateLocataireAsync(LocataireEntite locataire)
        {
            if (_context?.Locataire != null)
            {
                var inserted = await _context.Locataire.AddAsync(locataire);
                await _context.SaveChangesAsync();
                return inserted.Entity;
            }
            return null;
        }

        public async Task ModifierLocataireAsync(LocataireEntite locataire)
        {
            try
            {
                if (_context?.Locataire != null)
                {
                    _context.Locataire.Update(locataire);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task SupprimerLocataireAsync(LocataireEntite locataire)
        {
            if (_context.Locataire != null)
            {
                _context.Locataire.Remove(locataire);
                await _context.SaveChangesAsync();
            }
        }

        public async Task SupprimerQuittanceLoyerAsync(QuittanceLoyerEntite quittance)
        {
            if (_context.QuittanceLoyer != null)
            {
                _context.QuittanceLoyer.Remove(quittance);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IReadOnlyCollection<LocataireAppartementEntite>?> RechercheLocataireAppartementAsync(GetLocatairesAppartementRequest request)
        {
            if (_context.LocataireAppartement != null)
            {
                return await _context.LocataireAppartement
                        .Include(u => u.Locataire)
                        .Include(u => u.Appartement)
                            .ThenInclude(u => u.Ville)
                        .Where(Recherche(request)).ToListAsync();
            }
            return null;
        }

        public async Task<EtatDesLieuxEntite?> AjoutEtatDesLieux(EtatDesLieuxEntite etatsDesLieux)
        {
            if (_context.EtatDesLieux != null)
            {
                var inserted = _context.EtatDesLieux.Add(etatsDesLieux);
                await _context.SaveChangesAsync();
                return inserted.Entity;
            }
            return null;
        }

      
        public async Task<QuittanceLoyerEntite?> AjoutQuittanceLoyer(QuittanceLoyerEntite quittanceLoyer)
        {
            if (_context.QuittanceLoyer != null)
            {
                var inserted = _context.QuittanceLoyer.Add(quittanceLoyer);
                await _context.SaveChangesAsync();
                return inserted.Entity;
            }
            return null;
        }

        public async Task<PaiementEntite?> AjoutPaiement(PaiementEntite paiement)
        {
            if (_context.Paiement != null)
            {
                var inserted = _context.Paiement.Add(paiement);
                await _context.SaveChangesAsync();
                return inserted.Entity;
            }
            return null;
        }


        public async Task<LocataireAppartementEntite?> AssigneLocataireAUnAppartement(LocataireAppartementEntite locataireAppartement)
        {
            if (_context.LocataireAppartement != null)
            {
                var testSiExiste = _context.LocataireAppartement
                                        .FirstOrDefault(u => u.LocataireId == locataireAppartement.LocataireId 
                                        && u.AppartementId == locataireAppartement.AppartementId);

                if (testSiExiste != null)
                {
                    throw new Exception("Ce locatairer est déjà assigné à cet appartement");
                }

                var inserted = await _context.LocataireAppartement.AddAsync(locataireAppartement);
                await _context.SaveChangesAsync();
                return inserted.Entity;
            }
            return null;
        }

        public async Task ModifierPaiementAsync(PaiementEntite paiement)
        {
            try
            {
                if (_context?.Paiement != null)
                {
                    _context.Paiement.Update(paiement);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task SupprimerPaiementAsync(PaiementEntite paiement)
        {
            if (_context.Paiement != null)
            {
                _context.Paiement.Remove(paiement);
                await _context.SaveChangesAsync();
            }
        }
    }
}