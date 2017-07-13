using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.LocalHero.ControllableUnits
{
    using Ability.Core.AbilityFactory.Utilities;

    public class ControllableUnits : IControllableUnits
    {
        public ControllableUnits(IAbilityUnit unit)
        {
            this.Unit = unit;
        }

        public void Dispose()
        {
        }

        public IAbilityUnit Unit { get; set; }

        public void Initialize()
        {

        }

        public DataProvider<IAbilityUnit> ControllableUnitAdded { get; }

        public DataProvider<IAbilityUnit> ControllableUnitRemoved { get; }
    }
}
