namespace Ability.Core.AbilityFactory.Metadata
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
    public sealed class AbilityUnitMetadataAttribute : Attribute, IAbilityUnitMetadata
    {
        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="AbilityUnitMetadataAttribute"/> class.</summary>
        /// <param name="classIds">The class Ids.</param>
        public AbilityUnitMetadataAttribute(params ClassID[] classIds)
        {
            this.ClassIds = classIds;
        }

        #endregion

        #region Public Properties

        /// <summary>Gets the class ids.</summary>
        public ICollection<ClassID> ClassIds { get; }

        #endregion
    }
}
