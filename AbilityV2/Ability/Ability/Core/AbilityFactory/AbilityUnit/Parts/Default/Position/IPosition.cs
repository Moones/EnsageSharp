// <copyright file="IPosition.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Position
{
    using System;

    using SharpDX;

    /// <summary>
    ///     The Position interface.
    /// </summary>
    public interface IPosition : IObservable<IPosition>, IAbilityUnitPart
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the current.
        /// </summary>
        Vector3 Current { get; set; }

        /// <summary>
        ///     Gets or sets the direction.
        /// </summary>
        Vector3 Direction { get; set; }

        /// <summary>
        ///     Gets or sets the distance.
        /// </summary>
        float Distance { get; set; }

        /// <summary>
        ///     Gets or sets the predicted by latency.
        /// </summary>
        Vector3 PredictedByLatency { get; set; }

        /// <summary>
        ///     Gets or sets the predicted direction.
        /// </summary>
        Vector3 PredictedDirection { get; set; }

        /// <summary>
        ///     Gets or sets the previous.
        /// </summary>
        Vector3 Previous { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The predict.
        /// </summary>
        /// <param name="delay">
        ///     The delay.
        /// </param>
        /// <returns>
        ///     The <see cref="Vector3" />.
        /// </returns>
        Vector3 Predict(float delay);

        /// <summary>
        ///     The update.
        /// </summary>
        void Update();

        /// <summary>
        ///     The update.
        /// </summary>
        /// <param name="position">
        ///     The position.
        /// </param>
        void Update(Vector3 position);

        #endregion
    }
}