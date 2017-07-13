// <copyright file="ObservableMenuItem.cs" company="EnsageSharp">
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
namespace Ability.Core.MenuManager.MenuItems
{
    using Ability.Core.AbilityFactory.Utilities;

    using Ensage.Common.Menu;

    /// <summary>
    ///     The observable menu item.
    /// </summary>
    /// <typeparam name="TMenuItem">
    ///     The menu item type
    /// </typeparam>
    public class ObservableMenuItem<TMenuItem> : MenuItem
    {
        #region Constructors and Destructors

        public ObservableMenuItem(string name, string displayName, bool makeChampionUniq = false)
            : base(name, displayName, makeChampionUniq)
        {
            this.Provider = new DataProvider<TMenuItem>
                                {
                                   OnSubscribeAction = observer => { observer.OnNext(this.GetValue<TMenuItem>()); } 
                                };
            this.ValueChanged += (sender, args) =>
                {
                    var newValue = args.GetNewValue<TMenuItem>();
                    this.Provider.Next(newValue);
                };
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the provider.
        /// </summary>
        public DataProvider<TMenuItem> Provider { get; }

        #endregion
    }
}