// <copyright file="IObjectPanel.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Overlay.Panels.ObjectPanel
{
    using Ensage.Common.Objects.DrawObjects;

    /// <summary>
    ///     The panel direction.
    /// </summary>
    public enum PanelDirection
    {
        /// <summary>
        ///     The top.
        /// </summary>
        Top,

        /// <summary>
        ///     The bottom.
        /// </summary>
        Bottom,

        /// <summary>
        ///     The left.
        /// </summary>
        Left,

        /// <summary>
        ///     The right.
        /// </summary>
        Right
    }

    /// <summary>
    ///     The ObjectPanel interface.
    /// </summary>
    /// <typeparam name="TObject">
    ///     Type of the object
    /// </typeparam>
    public interface IObjectPanel<TObject> : IUnitOverlayElement
        where TObject : class, IUnitOverlayElement
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the background.
        /// </summary>
        DrawRect Background { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether draw background.
        /// </summary>
        bool DrawBackground { get; set; }

        /// <summary>
        ///     Gets the object manager.
        /// </summary>
        PanelObjectManager<TObject> ObjectManager { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The add object.
        /// </summary>
        /// <param name="panelObject">
        ///     The panel object.
        /// </param>
        void AddObject(TObject panelObject);

        /// <summary>
        ///     The remove object.
        /// </summary>
        /// <param name="panelObject">
        ///     The panel object.
        /// </param>
        void RemoveObject(TObject panelObject);

        #endregion
    }
}