namespace Ability.Core.AbilityModule.Metadata
{
    using System;
    using System.ComponentModel.Composition;

    using Attribute = System.Attribute;

    [MetadataAttribute, AttributeUsage(AttributeTargets.Class)]
    public class AbilityHeroModuleMetadata : Attribute, IAbilityHeroModuleMetadata
    {
        public AbilityHeroModuleMetadata(params uint[] classIds)
        {
            this.HeroIds = classIds;
        }

        public uint[] HeroIds { get; }
    }
}
