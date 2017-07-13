// <copyright file="Position.cs" company="EnsageSharp">
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

    using Ability.Core.AbilityFactory.Utilities;

    using Ensage;
    using Ensage.Common.Extensions;
    using Ensage.Common.Extensions.SharpDX;

    using SharpDX;

    /// <summary>
    ///     The position.
    /// </summary>
    internal class Position : IPosition
    {
        #region Fields

        /// <summary>
        ///     The provider.
        /// </summary>
        private DataProvider<IPosition> provider = new DataProvider<IPosition>();

        #endregion

        #region Constructors and Destructors

        internal Position(IAbilityUnit unit)
        {
            this.Unit = unit;
            this.Update();

            // var particle = new ParticleEffect(
            // @"particles\ui_mouseactions\drag_selected_ring.vpcf",
            // this.Current);
            // particle.SetControlPoint(1, new Vector3(255, 80, 50));
            // particle.SetControlPoint(3, new Vector3(20, 0, 0));
            // particle.SetControlPoint(2, new Vector3(100, 255, 0));
            // this.Subscribe(
            // new DataObserver<IPosition>(
            // position =>
            // {
            // particle.SetControlPoint(0, position.Current);
            // }));
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the current.
        /// </summary>
        public Vector3 Current { get; set; }

        /// <summary>
        ///     Gets or sets the direction.
        /// </summary>
        public Vector3 Direction { get; set; }

        /// <summary>
        ///     Gets or sets the distance.
        /// </summary>
        public float Distance { get; set; }

        /// <summary>
        ///     Gets or sets the predicted by latency.
        /// </summary>
        public Vector3 PredictedByLatency { get; set; }

        /// <summary>
        ///     Gets or sets the predicted direction.
        /// </summary>
        public Vector3 PredictedDirection { get; set; }

        /// <summary>
        ///     Gets or sets the previous.
        /// </summary>
        public Vector3 Previous { get; set; }

        /// <summary>
        ///     Gets or sets the unit.
        /// </summary>
        public IAbilityUnit Unit { get; set; }

        #endregion

        #region Public Methods and Operators

        public virtual void Dispose()
        {
        }

        public virtual void Initialize()
        {
        }

        /// <summary>
        ///     The predict.
        /// </summary>
        /// <param name="delay">
        ///     The delay.
        /// </param>
        /// <returns>
        ///     The <see cref="Vector2" />.
        /// </returns>
        public Vector3 Predict(float delay)
        {
            return this.Unit.SourceUnit.BasePredict(delay);
        }

        /// <summary>Notifies the provider that an observer is to receive notifications.</summary>
        /// <returns>
        ///     A reference to an interface that allows observers to stop receiving notifications before the provider has
        ///     finished sending them.
        /// </returns>
        /// <param name="observer">The object that is to receive notifications.</param>
        public IDisposable Subscribe(IObserver<IPosition> observer)
        {
            observer.OnNext(this);
            return this.provider.Subscribe(observer);
        }

        /// <summary>
        ///     The update.
        /// </summary>
        public void Update()
        {
            if (!this.Unit.SourceUnit.IsVisible)
            {
                return;
            }

            var current = this.Unit.SourceUnit.NetworkPosition;
            var distance = current.Distance2D(this.Current);
            if (distance < 1)
            {
                return;
            }

            this.Previous = this.Current;
            this.Current = current;
            this.Distance = distance;
            this.Direction = (this.Current - this.Previous).Normalized();
            this.PredictedByLatency = this.Unit.SourceUnit.NetworkActivity == NetworkActivity.Move
                                          ? this.Unit.SourceUnit.InFront(Game.AvgPing)
                                          : this.Current;
            this.PredictedDirection = (this.PredictedByLatency - this.Previous).Normalized();
            this.provider.Next(this);
        }

        /// <summary>
        ///     The update.
        /// </summary>
        /// <param name="position">
        ///     The position.
        /// </param>
        public void Update(Vector3 position)
        {
            this.Previous = this.Current;
            this.Current = position;

            // if (this.Previous == this.Current)
            // {
            // return;
            // }
            this.Distance = this.Previous.Distance2D(this.Current);
            this.Direction = (this.Current - this.Previous).Normalized();
            this.PredictedByLatency = this.Unit.SourceUnit.NetworkActivity == NetworkActivity.Move
                                          ? this.Unit.SourceUnit.InFront(Game.AvgPing)
                                          : this.Current;
            this.PredictedDirection = (this.PredictedByLatency - this.Previous).Normalized();
            this.provider.Next(this);
        }

        #endregion
    }
}