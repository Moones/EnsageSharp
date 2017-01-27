// <copyright file="EntityOwnerPositionTracker.cs" company="EnsageSharp">
//    Copyright (c) 2017 Moones.
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see http://www.gnu.org/licenses/
// </copyright>
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.PositionTracker.Types
{
    using Ensage;

    /// <summary>The entity owner position tracker.</summary>
    public class EntityOwnerPositionTracker : PositionTracker
    {
        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="EntityOwnerPositionTracker" /> class.</summary>
        /// <param name="unit">The unit.</param>
        public EntityOwnerPositionTracker(IAbilityUnit unit)
            : base(unit)
        {
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>The entity.</summary>
        /// <param name="entity">The entity.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public override bool Entity(Entity entity)
        {
            if (this.Unit.Visibility.Visible || !entity.Owner.Handle.Equals(this.Unit.UnitHandle))
            {
                return false;
            }

            this.PositionUpdated(entity.NetworkPosition);
            return true;
        }

        #endregion
    }
}