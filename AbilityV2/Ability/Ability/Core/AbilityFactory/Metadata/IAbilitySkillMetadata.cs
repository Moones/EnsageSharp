// <copyright file="IAbilitySkillMetadata.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.Metadata
{
    using System.ComponentModel;

    using Ensage;

    /// <summary>
    ///     The AbilitySkillMetadata interface.
    /// </summary>
    public interface IAbilitySkillMetadata
    {
        #region Public Properties

        /// <summary>Gets the skill ids.</summary>
        [DefaultValue(new uint[0])]
        uint[] AbilityIds { get; }

        /// <summary>Gets a value indicating whether owner.</summary>
        bool Owner { get; }

        /// <summary>Gets the owner class id.</summary>
        ClassId OwnerClassId { get; }

        #endregion
    }
}