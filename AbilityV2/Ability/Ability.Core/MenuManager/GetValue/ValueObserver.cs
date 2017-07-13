// <copyright file="ValueObserver.cs" company="EnsageSharp">
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
namespace Ability.Core.MenuManager.GetValue
{
    using System;

    /// <summary>
    ///     The value observer.
    /// </summary>
    /// <typeparam name="T">
    ///     The menu item type
    /// </typeparam>
    /// <typeparam name="TD">
    ///     The menu item value
    /// </typeparam>
    public class ValueObserver<T, TD> : IObserver<Tuple<T, TD>>
    {
        #region Constructors and Destructors

        public ValueObserver(Action<Tuple<T, TD>> onNextAction)
        {
            this.OnNextAction = onNextAction;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the on next action.
        /// </summary>
        public Action<Tuple<T, TD>> OnNextAction { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>Notifies the observer that the provider has finished sending push-based notifications.</summary>
        public void OnCompleted()
        {
        }

        /// <summary>Notifies the observer that the provider has experienced an error condition.</summary>
        /// <param name="error">An object that provides additional information about the error.</param>
        public void OnError(Exception error)
        {
        }

        /// <summary>Provides the observer with new data.</summary>
        /// <param name="value">The current notification information.</param>
        public void OnNext(Tuple<T, TD> value)
        {
            this.OnNextAction.Invoke(value);
        }

        #endregion
    }
}