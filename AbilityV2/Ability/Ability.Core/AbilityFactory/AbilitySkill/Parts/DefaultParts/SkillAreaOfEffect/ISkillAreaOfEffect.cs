// <copyright file="ISkillAreaOfEffect.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillAreaOfEffect
{
    using Ability.Core.AbilityFactory.AbilityUnit;

    using Ensage;

    using SharpDX;

    /// <summary>
    ///     The SkillInteraction interface.
    /// </summary>
    public interface ISkillAreaOfEffect : IAbilitySkillPart
    {
        #region Public Properties

        /// <summary>
        ///     Gets a value indicating whether creates obstacle.
        /// </summary>
        bool CreatesObstacle { get; }

        /// <summary>
        ///     Gets or sets a value indicating whether draw.
        /// </summary>
        bool Draw { get; set; }

        /// <summary>
        ///     Gets or sets the particle effect.
        /// </summary>
        ParticleEffect ParticleEffect { get; set; }

        /// <summary>
        ///     Gets or sets the projectile.
        /// </summary>
        TrackingProjectile Projectile { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The ability particle effect.
        /// </summary>
        /// <param name="particleEffect">
        ///     The particle effect.
        /// </param>
        void AbilityParticleEffect(ParticleEffect particleEffect);

        /// <summary>
        ///     The ability phase start.
        /// </summary>
        /// <param name="value">
        ///     The value.
        /// </param>
        void AbilityPhase(bool value);

        /// <summary>
        ///     The ability projectile.
        /// </summary>
        /// <param name="projectile">
        ///     The projectile.
        /// </param>
        void AbilityProjectile(TrackingProjectile projectile);

        /// <summary>
        ///     The add obstacle.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <returns>
        ///     The <see cref="uint" />.
        /// </returns>
        uint AddObstacle(IAbilityUnit unit);

        /// <summary>
        ///     The calculate impact delay.
        /// </summary>
        /// <param name="abilityUnit">
        ///     The ability unit.
        /// </param>
        /// <param name="impactPosition">
        ///     The impact position.
        /// </param>
        /// <returns>
        ///     The <see cref="float" />.
        /// </returns>
        float CalculateImpactDelay(IAbilityUnit abilityUnit, out Vector3 impactPosition);

        /// <summary>
        ///     The is intersecting.
        /// </summary>
        /// <param name="abilityUnit">
        ///     The ability unit.
        /// </param>
        /// <param name="impactDelay">
        ///     The impact Delay.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        bool IsIntersecting(IAbilityUnit abilityUnit, float impactDelay);

        /// <summary>
        ///     The update.
        /// </summary>
        void Update();

        /// <summary>
        ///     The update obstacle.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <param name="obstacleId">
        ///     The obstacle id.
        /// </param>
        void UpdateObstacle(IAbilityUnit unit, uint obstacleId);

        #endregion
    }
}