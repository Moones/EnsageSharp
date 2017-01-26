// <copyright file="IScreenInfo.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.ScreenInfo
{
    using Ability.Core.AbilityFactory.Utilities;

    using SharpDX;

    /// <summary>
    ///     The ScreenPosition interface.
    /// </summary>
    public interface IScreenInfo : IAbilityUnitPart
    {
        #region Public Properties

        /// <summary>
        ///     Gets the health bar position.
        /// </summary>
        Vector2 HealthBarPosition { get; }

        /// <summary>
        ///     Gets the health bar size.
        /// </summary>
        Vector2 HealthBarSize { get; }

        /// <summary>
        ///     Gets a value indicating whether is visible.
        /// </summary>
        bool IsVisible { get; }

        /// <summary>
        ///     Gets the minimap position.
        /// </summary>
        Vector2 MinimapPosition { get; }

        /// <summary>
        ///     Gets the position.
        /// </summary>
        Vector2 Position { get; }

        /// <summary>
        ///     Gets the top panel icon size.
        /// </summary>
        Vector2 TopPanelIconSize { get; }

        /// <summary>
        ///     Gets the top panel position.
        /// </summary>
        Vector2 TopPanelPosition { get; }

        /// <summary>
        ///     Gets the update provider.
        /// </summary>
        DataProvider<IScreenInfo> UpdateProvider { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The update.
        /// </summary>
        void Update();

        /// <summary>
        ///     The update health bar position.
        /// </summary>
        /// <returns>
        ///     The <see cref="Vector2" />.
        /// </returns>
        Vector2 UpdateHealthBarPosition();

        #endregion
    }
}