// <copyright file="IUnitDrawer.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Drawer
{
    using Ability.Utilities;

    using Ensage;

    using SharpDX;

    /// <summary>
    ///     The UnitDrawer interface.
    /// </summary>
    public interface IUnitDrawer : IAbilityUnitPart
    {
        #region Public Properties

        /// <summary>Gets the end scene icon.</summary>
        Render.Sprite EndSceneIcon { get; }

        /// <summary>
        ///     Gets or sets the icon.
        /// </summary>
        DotaTexture Icon { get; set; }

        /// <summary>Gets or sets the minimap icon.</summary>
        DotaTexture MinimapIcon { get; set; }

        /// <summary>
        ///     Gets or sets the minimap icon size.
        /// </summary>
        Vector2 MinimapIconSize { get; set; }

        /// <summary>
        ///     Gets or sets the world icon size.
        /// </summary>
        Vector2 WorldIconSize { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The draw icon on map.
        /// </summary>
        /// <param name="position">
        ///     The position.
        /// </param>
        void DrawIconOnMap(Vector3 position);

        /// <summary>
        ///     The draw icon on minimap.
        /// </summary>
        /// <param name="worldPosition">
        ///     The world position.
        /// </param>
        void DrawIconOnMinimap(Vector3 worldPosition);

        #endregion
    }
}