// <copyright file="ISkillDamageCalculator.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.DamageCalculator
{
    using Ensage;

    /// <summary>
    ///     The DamageCalculator interface.
    /// </summary>
    public interface ISkillDamageCalculator : IAbilitySkillPart
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The dealt damage.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <param name="minusMagicResistancePerc">
        ///     The minus Magic Resistance Percentage.
        /// </param>
        /// <param name="minusArmor">
        ///     The minus Armor.
        /// </param>
        /// <param name="minusHealth">
        ///     The minus Health.
        /// </param>
        /// <param name="minusDamageResistancePerc">
        ///     The minus Damage Resistance Percentage.
        /// </param>
        /// <returns>
        ///     The <see cref="float" />.
        /// </returns>
        float DealtDamage(
            Unit unit,
            float minusMagicResistancePerc = 0,
            float minusArmor = 0,
            float minusHealth = 0,
            float minusDamageResistancePerc = 0);

        #endregion
    }
}