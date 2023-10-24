using AutoMapper;
using FluentValidation.Results;
using Infeco.Api.Infrastructure.MediatR;
using Infoeco.infrastructure.Entities;
using Infoeco.Services;
using InfoEco.Domain.Exceptions;
using System.Linq.Expressions;

namespace Infeco.Api.Commands.LocataireAppartement
{
    public class AssignerAppartementCommandHandler : CommandHandlerBase<AssignerAppartementCommand>
    {
        private readonly IInfoEcoService _iInfoEcoService;
        public AssignerAppartementCommandHandler(IInfoEcoService iInfoEcoService, IMapper mapper, IHttpContextAccessor httpContextAccessor, IVerifieurReferencesService verifieurReferenceService, ILoggerFactory loggerFactory) : base(mapper, httpContextAccessor, verifieurReferenceService, loggerFactory)
        {
            _iInfoEcoService = iInfoEcoService ?? throw new ArgumentNullException(nameof(iInfoEcoService));
        }

        protected override List<Func<Task<ValidationFailure>>>? DefinitLesVerifieurs(AssignerAppartementCommand commande, CancellationToken cancellationToken)
        {
            return null;
        }

        protected override async Task ExecuteCommandeAsync(AssignerAppartementCommand commande, CancellationToken cancellationToken)
        {
           var appartement = await _iInfoEcoService.ObtientAppartementParIdAsync(commande.AppartementId, cancellationToken);
           if (appartement == null) 
           { 
                throw new Exception($"L'appartement avec l'id {commande.AppartementId} n'existe pas");
           }
           var locataire = await _iInfoEcoService.ObtientLocataireParIdAsync(commande.LocataireId, cancellationToken);
           if (locataire == null)
           {
                throw new Exception($"Le locataire avec l'id {commande.LocataireId} n'existe pas");
           }
          
            var locataireAppartement = Mapper.Map<LocataireAppartementEntite>(commande);
            try
            {
                var resultat = await _iInfoEcoService.AssigneLocataireAUnAppartement(locataireAppartement);
                if (resultat != null)
                {
                    commande.Id = resultat.AppartementId;
                }
            }
            catch (Exception ex)
            {
                throw new GlobalException(ex.Message);
            }
        }
    }
}
