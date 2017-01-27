// <copyright file="IPositionTracker.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.PositionTracker
{
    using Ability.Core.AbilityData;
    using Ability.Core.MenuManager.GetValue;

    using Ensage;
    using Ensage.Common.Menu;

    using SharpDX;

    /// <summary>
    ///     The PositionTracker interface.
    /// </summary>
    public interface IPositionTracker : IAbilityUnitPart
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the draw on map.
        /// </summary>
        GetValue<bool, bool> DrawOnMap { get; set; }

        /// <summary>
        ///     Gets or sets the draw on minimap.
        /// </summary>
        GetValue<bool, bool> DrawOnMinimap { get; set; }

        /// <summary>
        ///     Gets or sets the menu.
        /// </summary>
        Menu Menu { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>The dota base.</summary>
        /// <param name="dotaBase">The dota base.</param>
        /// <returns>The <see cref="bool" />.</returns>
        bool DotaBase(Unit dotaBase);

        /// <summary>The entity.</summary>
        /// <param name="entity">The entity.</param>
        /// <returns>The <see cref="bool" />.</returns>
        bool Entity(Entity entity);

        /// <summary>
        ///     The particle is from hero.
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
        void ParticleIsFromHero(Entity sender, ParticleEffectAddedEventArgs args, ParticleEffectMoreInfo info);

        /// <summary>The position updated.</summary>
        /// <param name="position">The position.</param>
        void PositionUpdated(Vector3 position);

        /// <summary>
        ///     The sender is hero.
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
        void SenderIsHero(Entity sender, ParticleEffectAddedEventArgs args, ParticleEffectMoreInfo info);

        /// <summary>The thinker.</summary>
        /// <param name="thinker">The thinker.</param>
        /// <returns>The <see cref="bool" />.</returns>
        bool Thinker(Entity thinker);

        #endregion
    }
}