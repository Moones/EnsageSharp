// <copyright file="Modifiers.cs" company="EnsageSharp">
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
    using Ability.Core.AbilityFactory.Utilities;

    using Ensage;
    using Ensage.Common.Extensions;

    /// <summary>
    ///     The modifiers.
    /// </summary>
    public class Modifiers : IModifiers
    {
        #region Constructors and Destructors

        internal Modifiers(IAbilityUnit unit)
        {
            this.Unit = unit;
        }

        #endregion

        #region Public Properties

        public DataProvider<Modifier> ModifierAdded { get; } = new DataProvider<Modifier>();

        public DataProvider<Modifier> ModifierRemoved { get; } = new DataProvider<Modifier>();

        /// <summary>
        ///     Gets or sets the unit.
        /// </summary>
        public IAbilityUnit Unit { get; set; }

        public bool ConsumedAghanim { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The new modifier received.
        /// </summary>
        /// <param name="modifier">
        ///     The modifier.
        /// </param>
        public virtual void AddModifier(Modifier modifier)
        {
            if (modifier.Name == "modifier_item_ultimate_scepter_consumed")
            {
                this.ConsumedAghanim = true;
            }

            this.ModifierAdded.Next(modifier);
        }

        public virtual void Dispose()
        {
        }

        public virtual void Initialize()
        {
        }

        /// <summary>
        ///     The modifier removed.
        /// </summary>
        /// <param name="modifier">
        ///     The modifier.
        /// </param>
        public virtual void RemoveModifier(Modifier modifier)
        {
            if (modifier.Name == "modifier_item_ultimate_scepter_consumed")
            {
                this.ConsumedAghanim = false;
            }

            this.ModifierRemoved.Next(modifier);
        }

        #endregion
    }
}