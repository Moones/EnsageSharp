// <copyright file="Unsubscriber.cs" company="EnsageSharp">
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
namespace Ability.Core.Utilities
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///     The unsubscriber.
    /// </summary>
    /// <typeparam name="TD">
    ///     The type
    /// </typeparam>
    public class Unsubscriber<TD> : IDisposable
    {
        #region Fields
        
        private readonly int id;

        /// <summary>
        ///     The _observers.
        /// </summary>
        private readonly Dictionary<int, IObserver<TD>> observers;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="Unsubscriber{TD}"/> class.
        ///     The unsubscriber.</summary>
        /// <param name="observers">The observers.</param>
        /// <param name="id">The id.</param>
        public Unsubscriber(Dictionary<int, IObserver<TD>> observers, int id)
        {
            this.observers = observers;
            this.id = id;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The dispose.
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);

            this.observers.Remove(this.id);
        }

        #endregion
    }
}