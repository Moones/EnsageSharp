// <copyright file="ParticleEffectMoreInfo.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityData
{
    /// <summary>
    ///     The particle effect more info.
    /// </summary>
    public class ParticleEffectMoreInfo
    {
        #region Constructors and Destructors

        public ParticleEffectMoreInfo(
            bool senderIsHero,
            bool isHeroParticle,
            string stringContainingHeroName,
            bool positionIsKnown)
        {
            this.SenderIsHero = senderIsHero;
            this.IsHeroParticle = isHeroParticle;
            this.StringContainingHeroName = stringContainingHeroName;
            this.PositionIsKnown = positionIsKnown;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets a value indicating whether is hero particle.
        /// </summary>
        public bool IsHeroParticle { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether position is known.
        /// </summary>
        public bool PositionIsKnown { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether sender is hero.
        /// </summary>
        public bool SenderIsHero { get; set; }

        /// <summary>
        ///     Gets or sets the string containing hero name.
        /// </summary>
        public string StringContainingHeroName { get; set; }

        #endregion
    }
}