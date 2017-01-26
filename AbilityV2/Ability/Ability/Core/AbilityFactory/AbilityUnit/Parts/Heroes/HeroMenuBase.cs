// <copyright file="HeroMenuBase.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes
{
    using System.ComponentModel.Composition;

    using Ability.Core.MenuManager.Menus.Submenus.UnitMenu;

    using Ensage.Common.Menu;

    /// <summary>The hero menu base.</summary>
    [InheritedExport(typeof(IHeroMenu))]
    public abstract class HeroMenuBase : IHeroMenu
    {
        #region Fields

        /// <summary>The added.</summary>
        private bool added;

        #endregion

        #region Public Properties

        /// <summary>Gets the hero name.</summary>
        public abstract string HeroName { get; }

        /// <summary>Gets or sets the menu.</summary>
        public Menu Menu { get; set; }

        /// <summary>Gets the menu display name.</summary>
        public abstract string MenuDisplayName { get; }

        /// <summary>Gets or sets the parent menu.</summary>
        public Menu ParentMenu { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>The add to menu.</summary>
        /// <param name="parentMenu">The parent menu.</param>
        public void AddToMenu(Menu parentMenu)
        {
            if (this.Menu == null)
            {
                this.Menu = new Menu(
                    this.MenuDisplayName,
                    parentMenu.Name + this.MenuDisplayName,
                    textureName: this.HeroName,
                    showTextWithTexture: true);
            }

            this.ParentMenu = parentMenu;
        }

        /// <summary>The connect to unit.</summary>
        /// <param name="unit">The unit.</param>
        public void ConnectToUnit(IAbilityUnit unit)
        {
            if (this.added || unit.Name != this.HeroName)
            {
                return;
            }

            this.ParentMenu.AddSubMenu(this.Menu);
            this.ConnectUnit(unit);
            this.added = true;
        }

        /// <summary>The connect unit.</summary>
        /// <param name="unit">The unit.</param>
        public abstract void ConnectUnit(IAbilityUnit unit);

        #endregion
    }
}