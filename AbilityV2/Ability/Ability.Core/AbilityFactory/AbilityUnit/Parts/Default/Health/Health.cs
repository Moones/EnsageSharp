// <copyright file="Health.cs" company="EnsageSharp">
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
    using System.Collections.ObjectModel;

    using Ability.Core.AbilityFactory.Utilities;

    using Ensage.Common;

    /// <summary>
    ///     The health.
    /// </summary>
    internal class Health : DataProvider<IHealth>, IHealth
    {
        #region Fields

        private float current;

        private Collection<IncomingDamage> incomingDamages = new Collection<IncomingDamage>();

        private float maximum;

        #endregion

        #region Constructors and Destructors

        internal Health(IAbilityUnit abilityUnit)
        {
            this.Unit = abilityUnit;
            this.Maximum = this.Unit.SourceUnit.MaximumHealth;
            this.Current = this.Unit.SourceUnit.Health;
            this.Percentage = 100;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the current.
        /// </summary>
        public float Current
        {
            get
            {
                return this.current;
            }

            set
            {
                var previous = this.current;
                this.current = value;
                if (this.current.Equals(previous))
                {
                    return;
                }

                if (this.current <= 0)
                {
                    this.ZeroHealth.Notify();
                    return;
                }

                DelayAction.Add(-1, this.NewValueReceived);
            }
        }

        /// <summary>
        ///     Gets the maximum.
        /// </summary>
        public float Maximum
        {
            get
            {
                return this.maximum;
            }

            set
            {
                if (this.maximum != value)
                {
                    this.maximum = value;
                    this.MaximumHealthChange.Notify();
                }
                else
                {
                    this.maximum = value;
                }
            }
        }

        /// <summary>Gets the maximum health change.</summary>
        public Notifier MaximumHealthChange { get; } = new Notifier();

        public Notifier ZeroHealth { get; } = new Notifier();

        /// <summary>
        ///     Gets or sets the percentage.
        /// </summary>
        public float Percentage { get; set; }

        /// <summary>
        ///     Gets or sets the unit.
        /// </summary>
        public IAbilityUnit Unit { get; set; }

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
        public void AddIncomingDamage(float damage, float delay)
        {
            var incomingDamage = new IncomingDamage(damage, delay);
            this.incomingDamages.Add(incomingDamage);
            DelayAction.Add(delay, () => { this.incomingDamages.Remove(incomingDamage); });
        }

        private ActionExecutor maximumHealthUpdater;

        public virtual void Dispose()
        {
            this.maximumHealthUpdater.Dispose();
        }

        /// <summary>The initialize.</summary>
        public virtual void Initialize()
        {
            this.maximumHealthUpdater = new ActionExecutor(() => this.Maximum = this.Unit.SourceUnit.MaximumHealth);
            this.maximumHealthUpdater.Subscribe(this.Unit.DataReceiver.Updates);
        }

        /// <summary>
        ///     The predict.
        /// </summary>
        /// <param name="delay">
        ///     The delay.
        /// </param>
        /// <returns>
        ///     The <see cref="float" />.
        /// </returns>
        public float Predict(float delay)
        {
            throw new NotImplementedException();
        }

        /// <summary>Notifies the provider that an observer is to receive notifications.</summary>
        /// <returns>
        ///     A reference to an interface that allows observers to stop receiving notifications before the provider has
        ///     finished sending them.
        /// </returns>
        /// <param name="observer">The object that is to receive notifications.</param>
        public override IDisposable Subscribe(IObserver<IHealth> observer)
        {
            observer.OnNext(this);
            return base.Subscribe(observer);
        }

        #endregion

        #region Methods

        private void NewValueReceived()
        {
            this.Maximum = this.Unit.SourceUnit.MaximumHealth;
            this.Percentage = this.current * 100 / this.Maximum;
            this.Next(this);
        }

        #endregion
    }
}