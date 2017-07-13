// <copyright file="DataObserver.cs" company="EnsageSharp">
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

    /// <summary>
    ///     The data observer.
    /// </summary>
    /// <typeparam name="T">
    ///     The data type
    /// </typeparam>
    public class DataObserver<T> : IObserver<T>, IDisposable
    {
        #region Fields

        /// <summary>
        ///     The on completed.
        /// </summary>
        private bool onCompleted;

        /// <summary>
        ///     The on completed action.
        /// </summary>
        private Action onCompletedAction;

        /// <summary>
        ///     The on error.
        /// </summary>
        private bool onError;

        /// <summary>
        ///     The on error action.
        /// </summary>
        private Action onErrorAction;

        #endregion

        #region Constructors and Destructors

        public DataObserver(Action<T> onNext, Action onCompleted = null, Action onError = null)
        {
            this.OnNextAction = onNext;
            this.OnCompletedAction = onCompleted;
            this.OnErrorAction = onError;
        }

        public DataObserver()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the on completed action.
        /// </summary>
        public Action OnCompletedAction
        {
            get
            {
                return this.onCompletedAction;
            }

            set
            {
                this.onCompletedAction = value;
                this.onCompleted = this.onCompletedAction != null;
            }
        }

        /// <summary>
        ///     Gets or sets the on error action.
        /// </summary>
        public Action OnErrorAction
        {
            get
            {
                return this.onErrorAction;
            }

            set
            {
                this.onErrorAction = value;
                this.onError = this.onErrorAction != null;
            }
        }

        /// <summary>
        ///     Gets or sets the on next action.
        /// </summary>
        public Action<T> OnNextAction { get; set; }

        public Collection<IDisposable> Unsubscribers { get; } = new Collection<IDisposable>();

        #endregion

        #region Public Methods and Operators

        public void Dispose()
        {
            foreach (var unsubscriber in this.Unsubscribers)
            {
                unsubscriber.Dispose();
            }
        }

        /// <summary>Notifies the observer that the provider has finished sending push-based notifications.</summary>
        public void OnCompleted()
        {
            if (!this.onCompleted)
            {
                return;
            }

            this.OnCompletedAction.Invoke();
        }

        /// <summary>Notifies the observer that the provider has experienced an error condition.</summary>
        /// <param name="error">An object that provides additional information about the error.</param>
        public void OnError(Exception error)
        {
            if (!this.onError)
            {
                return;
            }

            this.OnErrorAction.Invoke();
        }

        /// <summary>Provides the observer with new data.</summary>
        /// <param name="value">The current notification information.</param>
        public void OnNext(T value)
        {
            this.OnNextAction.Invoke(value);
        }

        public void Subscribe(IObservable<T> provider)
        {
            this.Unsubscribers.Add(provider.Subscribe(this));
        }

        #endregion
    }

    public class DataObserver<T1, T2> : DataObserver<T1>, IObserver<T2>
    {
        #region Constructors and Destructors

        public DataObserver(Action<T1> onNext, Action<T2> onNext2, Action onCompleted = null, Action onError = null)
            : base(onNext, onCompleted, onError)
        {
            this.OnNextAction2 = onNext2;
        }

        #endregion

        #region Public Properties

        public Action<T2> OnNextAction2 { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>Provides the observer with new data.</summary>
        /// <param name="value">The current notification information.</param>
        public void OnNext(T2 value)
        {
            this.OnNextAction2.Invoke(value);
        }

        #endregion
    }

    public class DataObserver<T1, T2, T3> : DataObserver<T1, T2>, IObserver<T3>
    {
        #region Constructors and Destructors

        public DataObserver(
            Action<T1> onNext,
            Action<T2> onNext2,
            Action<T3> onNext3,
            Action onCompleted = null,
            Action onError = null)
            : base(onNext, onNext2, onError)
        {
            this.OnNextAction3 = onNext3;
        }

        #endregion

        #region Public Properties

        public Action<T3> OnNextAction3 { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>Provides the observer with new data.</summary>
        /// <param name="value">The current notification information.</param>
        public void OnNext(T3 value)
        {
            this.OnNextAction3.Invoke(value);
        }

        #endregion
    }
}