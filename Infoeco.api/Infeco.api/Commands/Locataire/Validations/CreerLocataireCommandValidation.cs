using FluentValidation;
using Infeco.Api.Commands.Locataire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infeco.Api.Commands.Locataire.Validations
{
    public class CreerLocataireCommandValidation : ActionLocataireCommandValidation<CreerLocataireCommand>
    {
        public CreerLocataireCommandValidation()
        {
            ValidePrenom();
            ValideNom();
            ValideIBAN();
            ValideTelephone();
            ValideEmail();
            ValidDateNaissance();
        }
    }
}
