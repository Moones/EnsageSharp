// <copyright file="SkillEventArgs.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityManager
{
    using System;

    using Ability.Core.AbilityFactory.AbilitySkill;

    /// <summary>
    ///     The skill added event args.
    /// </summary>
    public class SkillEventArgs : EventArgs
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the ability skill.
        /// </summary>
        public IAbilitySkill AbilitySkill { get; set; }

        #endregion
    }
}