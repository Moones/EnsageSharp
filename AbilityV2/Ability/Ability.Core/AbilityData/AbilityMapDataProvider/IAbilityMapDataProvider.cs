using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityData.AbilityMapDataProvider
{
    using Ability.Core.AbilityData.AbilityMapDataProvider.AbilityMapData;
    using Ability.Core.AbilityData.AbilityMapDataProvider.AbilityMapData.Runes.AbilityRune.Types;
    using Ability.Core.AbilityFactory.Utilities;

    using Ensage;

    internal interface IAbilityMapDataProvider : IAbilityService
    {
        void OnUpdate();

        void OnDraw();

        void EntityAdded(Entity entity);

        void EntityRemoved(Entity entity);
    }
}
