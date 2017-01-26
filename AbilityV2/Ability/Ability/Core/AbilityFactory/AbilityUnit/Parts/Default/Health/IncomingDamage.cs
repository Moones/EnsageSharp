// <copyright file="IncomingDamage.cs" company="EnsageSharp">
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
    using Ensage;

    /// <summary>
    ///     The incoming damage.
    /// </summary>
    public class IncomingDamage
    {
        #region Constructors and Destructors

        public IncomingDamage(float damage, float delay)
        {
            this.Damage = damage;
            this.ProcTime = Game.RawGameTime + delay;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the damage.
        /// </summary>
        public float Damage { get; }

        /// <summary>
        ///     Gets the proc time.
        /// </summary>
        public float ProcTime { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The passed.
        /// </summary>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public bool Passed()
        {
            return this.ProcTime <= Game.RawGameTime;
        }

        #endregion
    }
}