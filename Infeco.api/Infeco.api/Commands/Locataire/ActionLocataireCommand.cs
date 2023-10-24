using Infeco.Api.Infrastructure.MediatR;
using System.ComponentModel.DataAnnotations;

namespace Infeco.Api.Commands.Locataire
{
    public abstract class ActionLocataireCommand: Command
    {
        public string? Nom { get; set; } 
        public string? Prenom { get; set; } 
        public string? Mail { get; set; } 
        public string? Telephone { get; set; } 
        public DateTime? DateNaissance { get; set; }
        public string? IBAN { get; set; }
    }
}
