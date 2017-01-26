// <copyright file="ISkillInteraction.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillInteraction
{
    /// <summary>
    ///     The SkillInteraction interface.
    /// </summary>
    public interface ISkillInteraction : IAbilitySkillPart
    {
        /// <summary>
        ///     Gets or sets the ability cast observer.
        ///// </summary>
        // DataObserver<Tuple<UnitInteractionData, AbilityCast>> AbilityCastObserver { get; set; }

        ///// <summary>
        /////     Gets or sets the ability particle observer.
        ///// </summary>
        // DataObserver<Tuple<UnitInteractionData, AbilityParticle>> AbilityParticleObserver { get; set; }

        ///// <summary>
        /////     Gets or sets the ability phase observer.
        ///// </summary>
        // DataObserver<Tuple<UnitInteractionData, AbilityPhase>> AbilityPhaseObserver { get; set; }

        ///// <summary>
        /////     Gets or sets the ability projectile observer.
        ///// </summary>
        // DataObserver<Tuple<UnitInteractionData, AbilityProjectile>> AbilityProjectileObserver { get; set; }
    }
}