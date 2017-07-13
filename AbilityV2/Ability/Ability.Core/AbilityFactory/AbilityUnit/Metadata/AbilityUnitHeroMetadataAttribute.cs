// <copyright file="AbilityUnitMetadataAttribute.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Metadata
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    using Ensage;

    using Attribute = System.Attribute;

    /// <summary>
    ///     The skills attribute.
    /// </summary>
    [MetadataAttribute, AttributeUsage(AttributeTargets.Class)]
    public sealed class AbilityUnitHeroMetadataAttribute : Attribute, IAbilityUnitHeroMetadata
    {
        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="AbilityUnitHeroMetadataAttribute" /> class.</summary>
        /// <param name="classIds">The class Ids.</param>
        public AbilityUnitHeroMetadataAttribute(params uint[] heroIds)
        {
            this.HeroIds = heroIds;
        }

        #endregion

        #region Public Properties

        /// <summary>Gets the class ids.</summary>
        public uint[] HeroIds { get; }

        #endregion
    }
}