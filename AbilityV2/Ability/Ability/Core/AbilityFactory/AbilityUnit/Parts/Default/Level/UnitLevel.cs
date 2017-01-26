// <copyright file="UnitLevel.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Level
{
    using System;

    using Ability.Core.AbilityFactory.Utilities;

    /// <summary>
    ///     The level.
    /// </summary>
    public class UnitLevel : DataProvider<IUnitLevel>, IUnitLevel
    {
        #region Fields

        /// <summary>
        ///     The current.
        /// </summary>
        private uint current;

        #endregion

        #region Constructors and Destructors

        public UnitLevel(IAbilityUnit unit)
        {
            this.Unit = unit;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the current.
        /// </summary>
        public uint Current
        {
            get
            {
                return this.current;
            }

            set
            {
                this.current = value;
                this.Next(this);
            }
        }

        /// <summary>
        ///     Gets or sets the unit.
        /// </summary>
        public IAbilityUnit Unit { get; set; }

        public virtual void Initialize()
        {
        }

        public virtual void Dispose()
        {
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>Notifies the provider that an observer is to receive notifications.</summary>
        /// <returns>
        ///     A reference to an interface that allows observers to stop receiving notifications before the provider has
        ///     finished sending them.
        /// </returns>
        /// <param name="observer">The object that is to receive notifications.</param>
        public override IDisposable Subscribe(IObserver<IUnitLevel> observer)
        {
            observer.OnNext(this);
            return base.Subscribe(observer);
        }

        #endregion
    }
}