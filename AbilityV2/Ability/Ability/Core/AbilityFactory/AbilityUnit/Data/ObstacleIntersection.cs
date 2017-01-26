// <copyright file="ObstacleIntersection.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Data
{
    using Ability.Core.AbilityFactory.AbilitySkill;

    using SharpDX;

    /// <summary>
    ///     The obstacle intersection.
    /// </summary>
    public class ObstacleIntersection
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the impact delay.
        /// </summary>
        public float ImpactDelay { get; set; }

        /// <summary>
        ///     Gets or sets the impact position.
        /// </summary>
        public Vector3 ImpactPosition { get; set; }

        /// <summary>
        ///     Gets or sets the intersecting unit.
        /// </summary>
        public IAbilityUnit IntersectingUnit { get; set; }

        /// <summary>
        ///     Gets or sets the obstacle source skill.
        /// </summary>
        public IAbilitySkill ObstacleSourceSkill { get; set; }

        #endregion
    }
}