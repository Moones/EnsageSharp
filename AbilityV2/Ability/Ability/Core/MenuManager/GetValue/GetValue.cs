// <copyright file="GetValue.cs" company="EnsageSharp">
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
    using System.Collections.ObjectModel;

    using Ability.Utilities;

    using Ensage.Common.Menu;

    /// <summary>
    ///     The get value.
    /// </summary>
    /// <typeparam name="T">
    ///     The menuItem type
    /// </typeparam>
    /// <typeparam name="TD">
    ///     The menuItem value
    /// </typeparam>
    public class GetValue<T, TD> : IObservable<Tuple<T, TD>>
    {
        #region Fields

        /// <summary>
        ///     The observers.
        /// </summary>
        private readonly Collection<IObserver<Tuple<T, TD>>> observers = new Collection<IObserver<Tuple<T, TD>>>();

        #endregion

        #region Constructors and Destructors

        public GetValue(MenuItem menuItem, Func<T, TD> getValueFunction)
        {
            this.GetValueFunction = getValueFunction;
            this.ValueType = menuItem.GetValue<T>();
            this.Value = getValueFunction.Invoke(this.ValueType);
            menuItem.ValueChanged += (sender, args) =>
                {
                    this.ValueType = args.GetNewValue<T>();
                    this.Value = getValueFunction.Invoke(this.ValueType);
                };
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the get value function.
        /// </summary>
        public Func<T, TD> GetValueFunction { get; set; }

        /// <summary>
        ///     Gets or sets the value.
        /// </summary>
        public TD Value { get; set; }

        /// <summary>
        ///     Gets or sets the value type.
        /// </summary>
        public T ValueType { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>Notifies the provider that an observer is to receive notifications.</summary>
        /// <returns>
        ///     A reference to an interface that allows observers to stop receiving notifications before the provider has
        ///     finished sending them.
        /// </returns>
        /// <param name="observer">The object that is to receive notifications.</param>
        public IDisposable Subscribe(IObserver<Tuple<T, TD>> observer)
        {
            this.observers.Add(observer);
            return new Unsubscriber<Tuple<T, TD>>(this.observers, observer);
        }

        #endregion
    }
}