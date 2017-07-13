namespace Ability.Core.AbilityFactory.AbilitySkill.Metadata
{
    using System;
    using System.ComponentModel.Composition;

    using Ensage;

    using Attribute = System.Attribute;

    [MetadataAttribute, AttributeUsage(AttributeTargets.Class)]
    public sealed class AbilitySkillItemMetadataAttribute : Attribute, IAbilitySkillItemMetadata
    {
        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="AbilitySkillItemMetadataAttribute" /> class.</summary>
        /// <param name="ownerClassId">The owner class id.</param>
        /// <param name="itemIds">The skill ids.</param>
        public AbilitySkillItemMetadataAttribute(ClassId ownerClassId, params uint[] itemIds)
        {
            this.OwnerClassId = ownerClassId;
            this.AbilityIds = itemIds;
            this.Owner = true;
        }

        /// <summary>Initializes a new instance of the <see cref="AbilitySkillItemMetadataAttribute"/> class.</summary>
        /// <param name="itemIds">The ability ids.</param>
        public AbilitySkillItemMetadataAttribute(params uint[] itemIds)
        {
            this.AbilityIds = itemIds;
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
