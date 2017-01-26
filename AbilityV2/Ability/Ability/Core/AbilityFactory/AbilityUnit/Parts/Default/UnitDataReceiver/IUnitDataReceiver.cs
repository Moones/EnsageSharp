// <copyright file="IUnitDataReceiver.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.UnitDataReceiver
{
    using Ability.Core.AbilityData;

    using Ensage;

    /// <summary>
    ///     The UnitDataReceiver interface.
    /// </summary>
    public interface IUnitDataReceiver : IAbilityUnitPart
    {
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
        ///     The entity_ on int 32 property change.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="args">
        ///     The args.
        /// </param>
        void Entity_OnInt32PropertyChange(Entity sender, Int32PropertyChangeEventArgs args);

        /// <summary>
        ///     The entity_ on particle effect added.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="args">
        ///     The args.
        /// </param>
        /// <param name="info">
        ///     The info.
        /// </param>
        void Entity_OnParticleEffectAdded(Entity sender, ParticleEffectAddedEventArgs args, ParticleEffectMoreInfo info);

        /// <summary>
        ///     The on update.
        /// </summary>
        void Game_OnUpdate();

        /// <summary>
        ///     The health change.
        /// </summary>
        /// <param name="value">
        ///     The value.
        /// </param>
        void HealthChange(float value);

        /// <summary>
        ///     The position x change.
        /// </summary>
        /// <param name="value">
        ///     The value.
        /// </param>
        void PositionXChange(float value);

        /// <summary>
        ///     The position y change.
        /// </summary>
        /// <param name="value">
        ///     The value.
        /// </param>
        void PositionYChange(float value);

        /// <summary>
        ///     The unit_ on modifier changed.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="args">
        ///     The args.
        /// </param>
        /// <param name="added">
        ///     The added.
        /// </param>
        void Unit_OnModifierChanged(Unit sender, ModifierChangedEventArgs args, bool added = true);

        #endregion
    }
}