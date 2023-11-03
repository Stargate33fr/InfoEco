using System;
using System.Globalization;

namespace Infeco.Api.ViewModels.Habilitations
{
    public class UtilisateurViewModel
    {
        public int Id { get; set; }
        public string Courriel { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Nom { get; set; } = string.Empty;
        public string Prenom { get; set; } = string.Empty;
        public int AgenceImmobiliereId { get; set; }
        public bool EstActif { get; set; }
    }
}
