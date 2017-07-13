namespace Ability.Core.AbilityFactory.AbilityTalent.Metadata
{
    using System;
    using System.ComponentModel.Composition;

    [MetadataAttribute, AttributeUsage(AttributeTargets.Class)]
    public class AbilityTalentMetadataAttribute : Attribute, IAbilityTalentMetadata
    {
        /// <summary>Initializes a new instance of the <see cref="AbilityTalentMetadataAttribute"/> class.</summary>
        /// <param name="abilityIds">The ability ids.</param>
        public AbilityTalentMetadataAttribute(params uint[] abilityIds)
        {
            this.AbilityIds = abilityIds;
        }

        public uint[] AbilityIds { get; }
    }
}
