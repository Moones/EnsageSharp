// <copyright file="LocalHeroScreenInfo.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.LocalHero.ScreenPosition
{
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.ScreenInfo;

    using Ensage.Common;

    using SharpDX;

    /// <summary>
    ///     The local hero screen position.
    /// </summary>
    internal class LocalHeroScreenInfo : ScreenInfo
    {
        #region Constructors and Destructors

        internal LocalHeroScreenInfo(IAbilityUnit unit)
            : base(unit)
        {
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The update health bar position.
        /// </summary>
        /// <returns>
        ///     The <see cref="Vector2" />.
        /// </returns>
        public override Vector2 UpdateHealthBarPosition()
        {
            return this.Position
                   + new Vector2(
                       (float)(-HUDInfo.HpBarX * 1.11 * HUDInfo.Monitor),
                       (float)(-HUDInfo.HpBarY * 1.38 * HUDInfo.Monitor));
        }

        #endregion
    }
}