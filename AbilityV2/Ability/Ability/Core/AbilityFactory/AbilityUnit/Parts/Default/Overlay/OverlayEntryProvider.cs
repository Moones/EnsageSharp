// <copyright file="OverlayEntryProvider.cs" company="EnsageSharp">
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
    using Ability.Core.AbilityManager.UI.Elements;
    using Ability.Core.AbilityManager.UI.Elements.Body.Bodies;

    /// <summary>
    ///     The overlay entry provider.
    /// </summary>
    public class OverlayEntryProvider : IOverlayEntryProvider
    {
        #region Constructors and Destructors

        public OverlayEntryProvider(IAbilityUnit unit)
        {
            this.Unit = unit;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the unit.
        /// </summary>
        public IAbilityUnit Unit { get; set; }

        #endregion

        #region Public Methods and Operators

        public virtual void Dispose()
        {
        }

        /// <summary>
        ///     The generate.
        /// </summary>
        /// <param name="parent">
        ///     The parent.
        /// </param>
        /// <returns>
        ///     The <see cref="IUnitOverlayEntry" />.
        /// </returns>
        public IUnitOverlayEntry Generate(IUserInterfaceElement parent)
        {
            return new UnitOverlayEntry(this.Unit, parent);
        }

        public virtual void Initialize()
        {
        }

        #endregion
    }
}