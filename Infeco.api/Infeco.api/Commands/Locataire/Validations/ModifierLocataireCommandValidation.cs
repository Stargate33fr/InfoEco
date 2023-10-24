using FluentValidation;
using Infeco.Api.Commands.Locataire;
using Infeco.Api.Commands.Locataire.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infeco.Api.Commands.Locataire.Validations
{
    public class ModifierLocataireCommandValidation : ActionLocataireCommandValidation<ModifierLocataireCommand>
    {
        public ModifierLocataireCommandValidation()
        {
            ValideId();
        }
    }
}
