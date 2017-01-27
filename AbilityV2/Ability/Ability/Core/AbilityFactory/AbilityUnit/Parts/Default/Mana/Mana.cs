// <copyright file="Mana.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Mana
{
    using System;

    using Ability.Core.AbilityFactory.Utilities;

    /// <summary>
    ///     The mana.
    /// </summary>
    public class Mana : DataProvider<IMana>, IMana
    {
        #region Fields

        private float current;

        #endregion

        #region Constructors and Destructors

        public Mana(IAbilityUnit unit)
        {
            this.Unit = unit;
            this.Current = unit.SourceUnit.Mana;
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
                if (this.current.Equals(previous) || this.current <= 0)
                {
                    return;
                }

                this.Percentage = this.current * 100 / this.Maximum;
                this.Next(this);
            }
        }

        /// <summary>
        ///     Gets the maximum.
        /// </summary>
        public float Maximum => this.Unit.SourceUnit.MaximumMana;

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

        public virtual void Dispose()
        {
        }

        public virtual void Initialize()
        {
        }

        /// <summary>Notifies the provider that an observer is to receive notifications.</summary>
        /// <returns>
        ///     A reference to an interface that allows observers to stop receiving notifications before the provider has
        ///     finished sending them.
        /// </returns>
        /// <param name="observer">The object that is to receive notifications.</param>
        public override IDisposable Subscribe(IObserver<IMana> observer)
        {
            observer.OnNext(this);
            return base.Subscribe(observer);
        }

        #endregion
    }
}