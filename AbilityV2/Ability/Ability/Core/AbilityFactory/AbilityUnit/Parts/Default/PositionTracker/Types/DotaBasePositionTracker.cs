using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.PositionTracker.Types
{
    using Ensage;

    /// <summary>The dota base position tracker.</summary>
    public class DotaBasePositionTracker : PositionTracker
    {
        /// <summary>The day vision.</summary>
        private float dayVision;

        /// <summary>Initializes a new instance of the <see cref="DotaBasePositionTracker"/> class.</summary>
        /// <param name="unit">The unit.</param>
        /// <param name="dayVision">The day Vision.</param>
        public DotaBasePositionTracker(IAbilityUnit unit, float dayVision)
            : base(unit)
        {
            this.dayVision = dayVision;
        }

        /// <summary>The dota base.</summary>
        /// <param name="dotaBase">The dota base.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public override bool DotaBase(Unit dotaBase)
        {
            if (dotaBase.DayVision != this.dayVision)
            {
                return false;
            }

            this.PositionUpdated(dotaBase.NetworkPosition);
            return true;
        }
    }
}
