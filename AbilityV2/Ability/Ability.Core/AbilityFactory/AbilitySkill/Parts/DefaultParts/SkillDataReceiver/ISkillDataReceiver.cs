// <copyright file="ISkillDataReceiver.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillDataReceiver
{
    using Ensage;

    /// <summary>
    ///     The SkillDataReceiver interface.
    /// </summary>
    public interface ISkillDataReceiver : IAbilitySkillPart
    {
        #region Public Properties

        /// <summary>Gets the updates.</summary>
        //Collection<Action> Updates { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The entity_ on bool property change.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="args">
        ///     The args.
        /// </param>
        void Entity_OnBoolPropertyChange(Entity sender, BoolPropertyChangeEventArgs args);

        /// <summary>
        ///     The entity_ on float property change.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="args">
        ///     The args.
        /// </param>
        void Entity_OnFloatPropertyChange(Entity sender, FloatPropertyChangeEventArgs args);

        /// <summary>
        ///     The entity_ on particle effect added.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="args">
        ///     The args.
        /// </param>
        void Entity_OnParticleEffectAdded(Entity sender, ParticleEffectAddedEventArgs args);

        /// <summary>
        ///     The game_ on update.
        /// </summary>
        void Game_OnUpdate();

        /// <summary>
        ///     The object manager_ on add tracking projectile.
        /// </summary>
        /// <param name="args">
        ///     The args.
        /// </param>
        void ObjectManager_OnAddTrackingProjectile(TrackingProjectileEventArgs args);

        #endregion
    }
}