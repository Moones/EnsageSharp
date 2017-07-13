// <copyright file="GlobalVariables.cs" company="EnsageSharp">
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
namespace Ability.Core
{
    using Ensage;
    using Ensage.Common.Extensions;

    using SharpDX;

    /// <summary>
    ///     The variables.
    /// </summary>
    public static class GlobalVariables
    {
        #region Constants

        public const string PropertyAbilityLevel = "m_iLevel";

        /// <summary>
        ///     The property ability phase.
        /// </summary>
        public const string PropertyAbilityPhase = "m_bInAbilityPhase";

        public const string PropertyCooldown = "m_fCooldown";

        /// <summary>
        ///     The property health.
        /// </summary>
        public const string PropertyHealth = "m_iHealth";

        /// <summary>
        ///     The property visible.
        /// </summary>
        public const string PropertyVisible = "m_bVisible";

        /// <summary>
        ///     The property x.
        /// </summary>
        public const string PropertyX = "m_vecX";

        /// <summary>
        ///     The property y.
        /// </summary>
        public const string PropertyY = "m_vecY";

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the enemy team.
        /// </summary>
        public static Team EnemyTeam { get; set; }

        /// <summary>
        ///     Gets or sets the local hero.
        /// </summary>
        public static Hero LocalHero { get; set; }

        /// <summary>
        ///     Gets the local hero predicted position.
        /// </summary>
        public static Vector3 LocalHeroPredictedPosition => LocalHero?.BasePredict(Game.Ping) ?? Vector3.Zero;

        /// <summary>
        ///     Gets or sets the team.
        /// </summary>
        public static Team Team { get; set; }

        #endregion
    }
}