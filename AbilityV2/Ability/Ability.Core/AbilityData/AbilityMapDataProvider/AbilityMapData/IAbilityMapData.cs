using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityData.AbilityMapDataProvider.AbilityMapData
{
    using Ability.Core.AbilityData.AbilityMapDataProvider.AbilityMapData.Runes.AbilityRune.Types;

    public interface IAbilityMapData
    {
        /// <summary>Gets the bounty rune spawner.</summary>
        RuneSpawner<BountyRune> BountyRuneSpawner { get; }

        /// <summary>Gets the power up rune spawner.</summary>
        RuneSpawner<PowerUpRune> PowerUpRuneSpawner { get; }


    }
}
