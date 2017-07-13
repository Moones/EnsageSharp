namespace Ability.Core.AbilityModule.Metadata
{
    using System;
    using System.ComponentModel.Composition;

    [MetadataAttribute, AttributeUsage(AttributeTargets.Class)]
    public class AbilityUnitModuleMetadata : Attribute, IAbilityUnitModuleMetadata
    {
        public AbilityUnitModuleMetadata(params string[] unitNames)
        {
            this.UnitNames = unitNames;
        }

        public string[] UnitNames { get; }
    }
}
