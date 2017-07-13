using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.LocalHero.ControllableUnits
{
    using Ability.Core.AbilityFactory.Utilities;

    public interface IControllableUnits : IAbilityUnitPart
    {
        DataProvider<IAbilityUnit> ControllableUnitAdded { get;}
        
        DataProvider<IAbilityUnit> ControllableUnitRemoved { get; }
    }
}
