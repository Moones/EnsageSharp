// <copyright file="AbilitySubMenu.cs" company="EnsageSharp">
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
namespace Ability.Core.MenuManager.Menus.AbilityMenu.Submenus
{
    using Ensage.Common.Menu;

    public class AbilitySubMenu : AbilityMenu, IAbilitySubMenu
    {
        #region Fields

        private AbilityMenu parentMenu;

        #endregion

        #region Constructors and Destructors

        public AbilitySubMenu(string name)
            : base(name)
        {
        }

        #endregion

        #region Public Properties

        public AbilityMenu ParentMenu
        {
            get
            {
                return this.parentMenu;
            }

            private set
            {
                this.parentMenu = value;
                this.Menu = new Menu(this.Name, this.parentMenu.Name + this.Name);
            }
        }

        #endregion

        #region Public Methods and Operators

        public void AddToMenu(AbilityMenu menu)
        {
            this.ParentMenu = menu;
            this.ParentMenu.Menu.AddSubMenu(this.Menu);
        }

        #endregion
    }
}