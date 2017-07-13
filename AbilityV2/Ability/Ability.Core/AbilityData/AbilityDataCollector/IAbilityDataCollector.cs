using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityData.AbilityDataCollector
{
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Orbwalker;
    using Ability.Core.AbilityModule.Combo;

    public interface IAbilityDataCollector : IAbilityService
    {
        void AddOrbwalker(IUnitOrbwalker orbwalker);
    }
}
