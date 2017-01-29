// <copyright file="DotaBasePositionTracker.cs" company="EnsageSharp">
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

    /// <summary>The dota base position tracker.</summary>
    public class DotaBasePositionTracker : PositionTracker
    {
        #region Fields

        /// <summary>The day vision.</summary>
        private float dayVision;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="DotaBasePositionTracker" /> class.</summary>
        /// <param name="unit">The unit.</param>
        /// <param name="dayVision">The day Vision.</param>
        public DotaBasePositionTracker(IAbilityUnit unit, float dayVision)
            : base(unit)
        {
            this.dayVision = dayVision;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>The dota base.</summary>
        /// <param name="dotaBase">The dota base.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public override bool DotaBase(Unit dotaBase)
        {
            if (this.Unit.Visibility.Visible || dotaBase.DayVision != this.dayVision)
            {
                return false;
            }

            this.PositionUpdated(dotaBase.NetworkPosition);
            return true;
        }

        #endregion
    }
}