// <copyright file="IHealth.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Health
{
    using System;

    using Ability.Core.AbilityFactory.Utilities;

    /// <summary>
    ///     The Health interface.
    /// </summary>
    public interface IHealth : IObservable<IHealth>, IAbilityUnitPart
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the current.
        /// </summary>
        float Current { get; set; }

        /// <summary>
        ///     Gets the maximum.
        /// </summary>
        float Maximum { get; }

        /// <summary>Gets the maximum health change.</summary>
        Notifier MaximumHealthChange { get; }

        /// <summary>
        ///     Gets or sets the percentage.
        /// </summary>
        float Percentage { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The add incoming damage.
        /// </summary>
        /// <param name="damage">
        ///     The damage.
        /// </param>
        /// <param name="delay">
        ///     The delay.
        /// </param>
        void AddIncomingDamage(float damage, float delay);

        /// <summary>
        ///     The predict.
        /// </summary>
        /// <param name="delay">
        ///     The delay.
        /// </param>
        /// <returns>
        ///     The <see cref="float" />.
        /// </returns>
        float Predict(float delay);

        #endregion
    }
}