// <copyright file="TopPanelField.cs" company="EnsageSharp">
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

    /// <summary>The top panel field.</summary>
    public class TopPanelField : PanelField
    {
        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="TopPanelField" /> class.</summary>
        /// <param name="unit">The unit.</param>
        /// <param name="parent">The parent.</param>
        public TopPanelField(IAbilityUnit unit, IUnitOverlayElement parent)
            : base(unit, parent, () => parent.Position, PanelDirection.Top, element => -new Vector2(0, element.Size.Y))
        {
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>The element priority.</summary>
        /// <param name="element">The element.</param>
        /// <returns>The <see cref="float" />.</returns>
        public override float ElementPriority(IUnitOverlayElement element)
        {
            return element.Size.X + element.Size.Y / 2;
        }

        /// <summary>The update element position.</summary>
        /// <param name="unitOverlayElement">The unit overlay element.</param>
        /// <param name="basePosition">The base position.</param>
        /// <returns>The <see cref="Vector2" />.</returns>
        public override Vector2 UpdateElementPosition(IUnitOverlayElement unitOverlayElement, Vector2 basePosition)
        {
            if (!unitOverlayElement.Enabled)
            {
                return Vector2.Zero;
            }

            var addition = Vector2.Zero;
            if (this.PositionFromElementHealthbarFunc != null)
            {
                addition += this.PositionFromElementHealthbarFunc.Invoke(unitOverlayElement);
            }

            unitOverlayElement.Position = addition + basePosition;

            unitOverlayElement.Position +=
                new Vector2(this.Unit.ScreenInfo.HealthBarSize.X / 2 - unitOverlayElement.Size.X / 2, 0);

            if (this.PositionFromLastElementFunc != null)
            {
                addition += this.PositionFromLastElementFunc.Invoke(unitOverlayElement);
            }

            return addition;
        }

        /// <summary>The update panel field size.</summary>
        public override void UpdatePanelFieldSize()
        {
            this.Size = new Vector2(this.ParentElement.Size.X, 0);
            foreach (var unitOverlayElement in this.StoredElements)
            {
                if (!unitOverlayElement.Enabled)
                {
                    continue;
                }

                this.Size = new Vector2(
                    Math.Max(this.Size.X, unitOverlayElement.Size.X),
                    this.Size.Y + unitOverlayElement.Size.Y);
            }
        }

        #endregion
    }
}