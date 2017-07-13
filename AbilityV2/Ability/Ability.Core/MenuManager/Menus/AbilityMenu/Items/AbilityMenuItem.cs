// <copyright file="AbilityMenuItem.cs" company="EnsageSharp">
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
namespace Ability.Core.MenuManager.Menus.AbilityMenu.Items
{
    using System;

    using Ability.Core.AbilityFactory.Utilities;
    using Ability.Core.MenuManager.GetValue;

    using Ensage.Common.Menu;

    public class AbilityMenuItem<T> : IAbilityMenuItem
    {
        #region Fields

        private AbilityMenu parentMenu;

        private T value;

        private string description;

        #endregion

        #region Constructors and Destructors

        public AbilityMenuItem(string name, T defaultValue, string description = null)
        {
            this.ValueType = typeof(T);
            this.Name = name;
            this.value = defaultValue;
            this.description = description;
        }

        #endregion

        #region Public Properties

        public string Name { get; }

        public DataProvider<T> NewValueProvider { get; } = new DataProvider<T>();

        public AbilityMenu ParentMenu
        {
            get
            {
                return this.parentMenu;
            }

            private set
            {
                this.parentMenu = value;
                this.MenuItem = new MenuItem(this.parentMenu.Name + this.Name, this.Name);
                if (this.description != null)
                {
                    this.MenuItem.SetTooltip(this.description);
                }

                this.MenuItem.SetValue(this.Value);
                this.MenuItem.ValueChanged += (sender, args) => this.Value = args.GetNewValue<T>();
                this.Value = this.MenuItem.GetValue<T>();
            }
        }

        public T Value
        {
            get
            {
                return this.value;
            }

            set
            {
                if (this.value.Equals(value))
                {
                    return;
                }

                this.value = value;
                this.NewValueProvider.Next(this.value);
            }
        }

        public Type ValueType { get; }

        #endregion

        #region Properties

        internal MenuItem MenuItem { get; set; }

        #endregion

        #region Public Methods and Operators

        public void AddToMenu(AbilityMenu menu)
        {
            this.ParentMenu = menu;
            menu.Menu.AddItem(this.MenuItem);
        }

        /// <summary>The value getter.</summary>
        /// <returns>The <see cref="GetValue" />.</returns>
        public GetValue<T, T> ValueGetter()
        {
            return new GetValue<T, T>(this.MenuItem, arg => arg);
        }

        public void Dispose()
        {
            this.NewValueProvider.Dispose();
        }

        #endregion
    }
}