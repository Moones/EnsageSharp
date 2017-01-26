// <copyright file="ISkillUpdater.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillUpdater
{
    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.AbilityUnit.Types;

    /// <summary>
    ///     The AbilityDataUpdater interface.
    /// </summary>
    public interface ISkillUpdater : IAbilitySkillPart
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The next level.
        /// </summary>
        void NextLevel();

        /// <summary>
        ///     The should update.
        /// </summary>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        bool ShouldUpdate();

        /// <summary>
        ///     The should update.
        /// </summary>
        /// <param name="target">
        ///     The target.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        bool ShouldUpdate(IAbilityUnit target);

        /// <summary>
        ///     The should update.
        /// </summary>
        /// <param name="target">
        ///     The target.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        bool ShouldUpdate(Ally target);

        /// <summary>
        ///     The update.
        /// </summary>
        void Update();

        /// <summary>
        ///     The update.
        /// </summary>
        /// <param name="target">
        ///     The target.
        /// </param>
        void Update(IAbilityUnit target);

        /// <summary>
        ///     The update.
        /// </summary>
        /// <param name="target">
        ///     The target.
        /// </param>
        void Update(Ally target);

        #endregion
    }
}