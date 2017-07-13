// <copyright file="IUserInterfaceElement.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityManager.UI.Elements
{
    using SharpDX;
    using SharpDX.Mathematics.Interop;

    /// <summary>
    ///     The UserInterfaceElement interface.
    /// </summary>
    public interface IUserInterfaceElement
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the color.
        /// </summary>
        Color Color { get; set; }

        /// <summary>
        ///     Gets or sets the parent.
        /// </summary>
        IUserInterfaceElement Parent { get; set; }

        /// <summary>
        ///     Gets or sets the position.
        /// </summary>
        Vector2 Position { get; set; }

        /// <summary>
        ///     Gets or sets the size.
        /// </summary>
        Vector2 Size { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether visible.
        /// </summary>
        bool Visible { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The draw.
        /// </summary>
        void Draw();

        /// <summary>
        ///     The mouse down.
        /// </summary>
        /// <param name="mousePosition">
        ///     The mouse position.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        bool MouseDown(Vector2 mousePosition);

        /// <summary>
        ///     The mouse move.
        /// </summary>
        /// <param name="mousePosition">
        ///     The mouse position.
        /// </param>
        void MouseMove(Vector2 mousePosition);

        /// <summary>
        ///     The mouse up.
        /// </summary>
        /// <param name="mousePosition">
        ///     The mouse position.
        /// </param>
        void MouseUp(Vector2 mousePosition);

        /// <summary>
        ///     The update position.
        /// </summary>
        void UpdatePosition();

        /// <summary>
        ///     The update size.
        /// </summary>
        void UpdateSize();

        #endregion
    }
}