// <copyright file="OrbsUpdate.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.Invoker.Modifiers
{
    using System;

    using Ability.Core.AbilityFactory.Utilities;

    /// <summary>
    ///     The orbs update.
    /// </summary>
    public class OrbsUpdate : DataProvider<OrbsUpdate>
    {
        #region Constructors and Destructors

        internal OrbsUpdate(InvokerModifiers modifiers)
        {
            this.Modifiers = modifiers;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the modifiers.
        /// </summary>
        public InvokerModifiers Modifiers { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>Notifies the provider that an observer is to receive notifications.</summary>
        /// <returns>
        ///     A reference to an interface that allows observers to stop receiving notifications before the provider has
        ///     finished sending them.
        /// </returns>
        /// <param name="observer">The object that is to receive notifications.</param>
        public override IDisposable Subscribe(IObserver<OrbsUpdate> observer)
        {
            if (this.Modifiers.QuasCount + this.Modifiers.WexCount + this.Modifiers.ExortCount >= 3)
            {
                observer.OnNext(this);
            }

            return base.Subscribe(observer);
        }

        /// <summary>
        ///     The update.
        /// </summary>
        public void Update()
        {
            this.Next(this);
        }

        #endregion
    }
}