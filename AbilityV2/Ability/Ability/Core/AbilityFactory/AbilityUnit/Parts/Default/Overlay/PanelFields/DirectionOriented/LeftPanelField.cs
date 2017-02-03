// <copyright file="LeftPanelField.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Overlay.PanelFields.DirectionOriented
{
    using System;

    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Overlay.Panels.ObjectPanel;

    using SharpDX;

    /// <summary>The left panel field.</summary>
    public class LeftPanelField : PanelField
    {
        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="LeftPanelField" /> class.</summary>
        /// <param name="unit">The unit.</param>
        /// <param name="parent">The parent.</param>
        public LeftPanelField(IAbilityUnit unit, IUnitOverlayElement parent)
            : base(
                unit,
                parent,
                () => unit.Overlay.HealthBar.Position,
                PanelDirection.Left,
                element => -new Vector2(element.Size.X, 0))
        {
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>The element priority.</summary>
        /// <param name="element">The element.</param>
        /// <returns>The <see cref="float" />.</returns>
        public override float ElementPriority(IUnitOverlayElement element)
        {
            return element.Size.Y + element.Size.X / 2;
        }

        /// <summary>The update panel field size.</summary>
        public override void UpdatePanelFieldSize()
        {
            this.Size = new Vector2(0, this.ParentElement.Size.Y);
            foreach (var unitOverlayElement in this.StoredElements)
            {
                if (!unitOverlayElement.Enabled)
                {
                    continue;
                }

                this.Size = new Vector2(
                    this.Size.X + unitOverlayElement.Size.X,
                    Math.Max(this.Size.Y, unitOverlayElement.Size.Y));
            }
        }

        #endregion
    }
}