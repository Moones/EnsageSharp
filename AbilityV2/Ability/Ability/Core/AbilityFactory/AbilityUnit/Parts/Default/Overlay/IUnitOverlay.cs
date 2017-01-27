// <copyright file="IUnitOverlay.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Overlay
{
    using System.Collections.Generic;

    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Overlay.Bars;
    using Ability.Core.MenuManager.GetValue;

    using Ensage.Common.Menu;

    using SharpDX;

    /// <summary>
    ///     The UnitOverlay interface.
    /// </summary>
    public interface IUnitOverlay : IAbilityUnitPart
    {
        #region Public Properties

        /// <summary>
        ///     Gets the bot panel.
        /// </summary>
        PanelField BotPanel { get; }

        /// <summary>Gets or sets the distance from local hero.</summary>
        GetValue<Slider, float> DistanceFromLocalHero { get; set; }

        /// <summary>
        ///     Gets or sets the elements.
        /// </summary>
        ICollection<IUnitOverlayElement> Elements { get; }

        /// <summary>
        ///     Gets or sets the health bar.
        /// </summary>
        HealthBar HealthBar { get; set; }

        /// <summary>
        ///     Gets or sets the health bar position.
        /// </summary>
        Vector2 HealthBarPosition { get; set; }

        /// <summary>
        ///     Gets the left panel.
        /// </summary>
        PanelField LeftPanel { get; }

        /// <summary>
        ///     Gets or sets the mana bar.
        /// </summary>
        ManaBar ManaBar { get; set; }

        /// <summary>
        ///     Gets the panels.
        /// </summary>
        ICollection<IUnitOverlayElement> Panels { get; }

        /// <summary>
        ///     Gets the right panel.
        /// </summary>
        PanelField RightPanel { get; }

        /// <summary>Gets or sets the stick to screen.</summary>
        GetValue<bool, bool> StickToScreen { get; set; }

        /// <summary>
        ///     Gets the top panel.
        /// </summary>
        PanelField TopPanel { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The on draw.
        /// </summary>
        void OnDraw();

        #endregion
    }
}