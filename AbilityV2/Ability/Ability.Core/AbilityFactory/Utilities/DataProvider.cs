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
    using System.Collections.Generic;

    using Ability.Core.Utilities;

    /// <summary>
    ///     The DataProvider.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of data
    /// </typeparam>
    public class DataProvider<T> : IObservable<T>, IDisposable
    {
        #region Fields

        private bool onSubscribe;

        private Action<IObserver<T>> onSubscribeAction;

        private Dictionary<int, IObserver<T>> observers = new Dictionary<int, IObserver<T>>();

        private int count;

        #endregion

        #region Public Properties

        public IReadOnlyDictionary<int, IObserver<T>> Observers => this.observers;

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
                observer.Value.OnNext(value);
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
            this.count++;
            //var newObservers = new Collection<IObserver<T>>(this.Observers) { observer };
            this.observers.Add(this.count, observer);
            if (this.onSubscribe)
            {
                this.onSubscribeAction.Invoke(observer);
            }

            return new Unsubscriber<T>(this.observers, this.count);
        }

        public void Dispose()
        {
            this.observers.Clear();
        }

        #endregion
    }
}