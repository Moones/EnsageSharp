using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.PositionTracker.Types
{
    using Ensage;

    /// <summary>The entity owner position tracker.</summary>
    public class EntityOwnerPositionTracker : PositionTracker
    {

        /// <summary>Initializes a new instance of the <see cref="EntityOwnerPositionTracker"/> class.</summary>
        /// <param name="unit">The unit.</param>
        public EntityOwnerPositionTracker(IAbilityUnit unit)
            : base(unit)
        {
        }

        /// <summary>The entity.</summary>
        /// <param name="entity">The entity.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public override bool Entity(Entity entity)
        {
            if (this.Unit.Visibility.Visible || !entity.Owner.Handle.Equals(this.Unit.UnitHandle))
            {
                return false;
            }

            this.PositionUpdated(entity.NetworkPosition);
            return true;
        }
    }
}
