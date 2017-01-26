// <copyright file="ModifierAdd.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Modifiers
{
    using System;

    using Ability.Core.AbilityFactory.Utilities;

    using Ensage;

    /// <summary>
    ///     The modifier change.
    /// </summary>
    public class ModifierAdd : DataProvider<ModifierAdd>
    {
        #region Fields

        /// <summary>
        ///     The modifier.
        /// </summary>
        private Modifier modifier;

        private IAbilityUnit unit;

        #endregion

        #region Constructors and Destructors

        public ModifierAdd(IAbilityUnit unit)
        {
            this.unit = unit;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the modifier.
        /// </summary>
        public Modifier Modifier
        {
            get
            {
                return this.modifier;
            }

            set
            {
                this.modifier = value;
                this.Next(this);
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>Notifies the provider that an observer is to receive notifications.</summary>
        /// <returns>
        ///     A reference to an interface that allows observers to stop receiving notifications before the provider has
        ///     finished sending them.
        /// </returns>
        /// <param name="observer">The object that is to receive notifications.</param>
        public override IDisposable Subscribe(IObserver<ModifierAdd> observer)
        {
            foreach (var modifier1 in this.unit.SourceUnit.Modifiers)
            {
                this.modifier = modifier1;
                this.Next(this);
            }

            return base.Subscribe(observer);
        }

        #endregion
    }
}