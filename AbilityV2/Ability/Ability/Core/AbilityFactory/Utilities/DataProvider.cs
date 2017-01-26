// <copyright file="DataProvider.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.Utilities
{
    using System;
    using System.Collections.ObjectModel;

    using Ability.Utilities;

    /// <summary>
    ///     The DataProvider.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of data
    /// </typeparam>
    public class DataProvider<T> : IObservable<T>
    {
        #region Fields

        private bool onSubscribe;

        private Action<IObserver<T>> onSubscribeAction;

        #endregion

        #region Public Properties

        public Collection<IObserver<T>> Observers { get; set; } = new Collection<IObserver<T>>();

        public Action<IObserver<T>> OnSubscribeAction
        {
            get
            {
                return this.onSubscribeAction;
            }

            set
            {
                this.onSubscribeAction = value;
                this.onSubscribe = this.onSubscribeAction != null;
            }
        }

        #endregion

        #region Public Methods and Operators

        public void Next(T value)
        {
            foreach (var observer in this.Observers)
            {
                observer?.OnNext(value);
            }
        }

        /// <summary>Notifies the provider that an observer is to receive notifications.</summary>
        /// <returns>
        ///     A reference to an interface that allows observers to stop receiving notifications before the provider has
        ///     finished sending them.
        /// </returns>
        /// <param name="observer">The object that is to receive notifications.</param>
        public virtual IDisposable Subscribe(IObserver<T> observer)
        {
            var newObservers = new Collection<IObserver<T>>(this.Observers) { observer };
            if (this.onSubscribe)
            {
                this.onSubscribeAction.Invoke(observer);
            }

            this.Observers = newObservers;
            return new Unsubscriber<T>(this.Observers, observer);
        }

        #endregion
    }
}