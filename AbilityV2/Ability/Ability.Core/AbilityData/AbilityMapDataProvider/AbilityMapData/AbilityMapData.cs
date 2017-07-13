using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityData.AbilityMapDataProvider.AbilityMapData
{
    using System.ComponentModel.Composition;

    using Ability.Core.AbilityData.AbilityMapDataProvider.AbilityMapData.Runes.AbilityRune.Types;

    using SharpDX;

    [Export(typeof(IAbilityMapData))]
    public class AbilityMapData : IAbilityMapData
    {
        public RuneSpawner<BountyRune> BountyRuneSpawner { get; } =
            new RuneSpawner<BountyRune>(
                new List<RunePosition<BountyRune>>
                    {
                        new RunePosition<BountyRune>(new Vector3(-2827, 4144, 300)),
                        new RunePosition<BountyRune>(new Vector3(3482, 289, 300)),
                        new RunePosition<BountyRune>(new Vector3(1287, -4132, 300)),
                        new RunePosition<BountyRune>(new Vector3(-4352, 194, 300))
                    },
                0);

        public RuneSpawner<PowerUpRune> PowerUpRuneSpawner { get; } =
            new RuneSpawner<PowerUpRune>(
                new List<RunePosition<PowerUpRune>>
                    {
                        new RunePosition<PowerUpRune>(new Vector3(-1762, 1214, 150)),
                        new RunePosition<PowerUpRune>(new Vector3(2601, -2015, 150))
                    },
                120);
    }
}
