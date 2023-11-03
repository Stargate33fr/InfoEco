using FluentValidation;
using Infeco.Api.Commands.Appartements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infeco.Api.Commands.Appartements.Validations
{
    public class ModifierAppartementCommandValidation : ActionAppartementCommandValidation<ModifierAppartementCommand>
    {
        public ModifierAppartementCommandValidation()
        {
            ValideId();
        }
    }
}
