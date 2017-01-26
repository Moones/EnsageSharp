// <copyright file="IStatusPanel.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Overlay.Panels.StatusPanel
{
    using Ensage.Common.Objects.DrawObjects;

    using SharpDX;

    /// <summary>
    ///     The StatusPanel interface.
    /// </summary>
    public interface IStatusPanel : IUnitOverlayElement
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the background.
        /// </summary>
        DrawRect Background { get; set; }

        /// <summary>
        ///     Gets or sets the background color.
        /// </summary>
        Color BackgroundColor { get; set; }

        /// <summary>
        ///     Gets or sets the position.
        /// </summary>
        Vector2 Position { get; set; }

        /// <summary>
        ///     Gets or sets the size.
        /// </summary>
        Vector2 Size { get; set; }

        /// <summary>
        ///     Gets or sets the unit.
        /// </summary>
        IAbilityUnit Unit { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The draw.
        /// </summary>
        void Draw();

        #endregion
    }
}