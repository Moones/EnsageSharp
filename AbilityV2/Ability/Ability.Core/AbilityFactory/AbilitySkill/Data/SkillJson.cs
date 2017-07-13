// <copyright file="SkillJson.cs" company="EnsageSharp">
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
    ///     The skill data.
    /// </summary>
    public class SkillJson
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the cast priority.
        /// </summary>
        public uint CastPriority { get; set; }

        /// <summary>
        ///     Gets or sets the damage amp modifier.
        /// </summary>
        public string DamageAmpModifier { get; set; }

        /// <summary>
        ///     Gets or sets the damage dealt priority.
        /// </summary>
        public uint DamageDealtPriority { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether deals damage to self.
        /// </summary>
        public bool DealsDamageToSelf { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether deals damage to target.
        /// </summary>
        public bool DealsDamageToTarget { get; set; }

        /// <summary>
        ///     Gets or sets the global cast priority.
        /// </summary>
        public uint GlobalCastPriority { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether has projectile.
        /// </summary>
        public bool HasProjectile { get; set; }

        /// <summary>
        ///     Gets or sets the hex duration data.
        /// </summary>
        public string HexDurationData { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether is damage amp.
        /// </summary>
        public bool IsDamageAmp { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether is hide.
        /// </summary>
        public bool IsHide { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether is leap.
        /// </summary>
        public bool IsLeap { get; set; }

        /// <summary>
        ///     Gets or sets the minus armor.
        /// </summary>
        public float MinusArmor { get; set; }

        /// <summary>
        ///     Gets or sets the minus damage resistance percentage.
        /// </summary>
        public float MinusDamageResistancePerc { get; set; }

        /// <summary>
        ///     Gets or sets the minus magic resistance percentage.
        /// </summary>
        public float MinusMagicResistancePerc { get; set; }

        /// <summary>
        ///     Gets or sets the minus slow data.
        /// </summary>
        public string MinusSlowData { get; set; }

        /// <summary>
        ///     Gets or sets the minus slow percentage data.
        /// </summary>
        public string MinusSlowPercentageData { get; set; }

        /// <summary>
        ///     Gets or sets the particle names.
        /// </summary>
        public string[] ParticleNames { get; set; }

        /// <summary>
        ///     Gets or sets the slow duration data.
        /// </summary>
        public string SlowDurationData { get; set; }

        /// <summary>
        ///     Gets or sets the stun duration data.
        /// </summary>
        public string StunDurationData { get; set; }

        #endregion
    }
}