// <copyright file="SkillDynamic.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilitySkill.Data
{
    /// <summary>
    ///     The skill dynamic.
    /// </summary>
    public class SkillDynamic
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets a value indicating whether applies buff on self.
        /// </summary>
        public bool AppliesBuffOnSelf { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether applies buff on target.
        /// </summary>
        public bool AppliesBuffOnTarget { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether applies disable on target.
        /// </summary>
        public bool AppliesDisableOnTarget { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether applies slow on self.
        /// </summary>
        public bool AppliesSlowOnSelf { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether applies slow on target.
        /// </summary>
        public bool AppliesSlowOnTarget { get; set; }

        /// <summary>
        ///     Gets or sets the bonus mana.
        /// </summary>
        public float BonusMana { get; set; }

        /// <summary>
        ///     Gets or sets the cast range.
        /// </summary>
        public float CastRange { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether deals damage to self.
        /// </summary>
        public bool DealsDamageToSelf { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether deals damage to target.
        /// </summary>
        public bool DealsDamageToTarget { get; set; }

        /// <summary>
        ///     Gets or sets the hex duration.
        /// </summary>
        public float HexDuration { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether is damage amp.
        /// </summary>
        public bool IsDamageAmp { get; set; }

        /// <summary>
        ///     Gets or sets the minus armor.
        /// </summary>
        public float MinusArmor { get; set; }

        /// <summary>
        ///     Gets or sets the minus damage resistance percentage.
        /// </summary>
        public float MinusDamageResistancePercentage { get; set; }

        /// <summary>
        ///     Gets or sets the minus magic resistance percentage.
        /// </summary>
        public float MinusMagicResistancePercentage { get; set; }

        /// <summary>
        ///     Gets or sets the minus slow.
        /// </summary>
        public float MinusSlow { get; set; }

        /// <summary>
        ///     Gets or sets the minus slow percentage.
        /// </summary>
        public float MinusSlowPercentage { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether prevents target from casting.
        /// </summary>
        public bool PreventsTargetFromCasting { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether prevents target from casting items.
        /// </summary>
        public bool PreventsTargetFromCastingItems { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether prevents target from casting spells.
        /// </summary>
        public bool PreventsTargetFromCastingSpells { get; set; }

        /// <summary>
        ///     Gets or sets the refills mana.
        /// </summary>
        public bool RefillsMana { get; set; }

        /// <summary>
        ///     Gets or sets the slow duration.
        /// </summary>
        public float SlowDuration { get; set; }

        /// <summary>
        ///     Gets or sets the stun duration.
        /// </summary>
        public float StunDuration { get; set; }

        #endregion
    }
}