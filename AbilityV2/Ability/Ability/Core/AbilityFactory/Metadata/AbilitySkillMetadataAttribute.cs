// <copyright file="AbilitySkillMetadataAttribute.cs" company="EnsageSharp">
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
    using System;
    using System.ComponentModel.Composition;

    using Ensage;

    using Attribute = System.Attribute;

    /// <summary>
    ///     The skills attribute.
    /// </summary>
    [MetadataAttribute, AttributeUsage(AttributeTargets.Class)]
    public sealed class AbilitySkillMetadataAttribute : Attribute, IAbilitySkillMetadata
    {
        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="AbilitySkillMetadataAttribute" /> class.</summary>
        /// <param name="ownerClassId">The owner class id.</param>
        /// <param name="abilityIds">The skill ids.</param>
        public AbilitySkillMetadataAttribute(ClassId ownerClassId, params uint[] abilityIds)
        {
            this.OwnerClassId = ownerClassId;
            this.AbilityIds = abilityIds;
            this.Owner = true;
        }

        #endregion

        #region Public Properties

        /// <summary>Gets the ability ids.</summary>
        public uint[] AbilityIds { get; }

        /// <summary>Gets a value indicating whether owner.</summary>
        public bool Owner { get; }

        /// <summary>Gets the owner class id.</summary>
        public ClassId OwnerClassId { get; }

        #endregion
    }
}