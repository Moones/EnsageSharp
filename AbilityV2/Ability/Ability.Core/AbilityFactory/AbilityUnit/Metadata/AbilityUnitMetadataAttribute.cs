using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Metadata
{
    using System.ComponentModel.Composition;

    [MetadataAttribute, AttributeUsage(AttributeTargets.Class)]
    public class AbilityUnitMetadataAttribute : Attribute, IAbilityUnitMetadata
    {
        public AbilityUnitMetadataAttribute(params string[] unitNames)
        {
            this.UnitNames = unitNames;
        }

        public string[] UnitNames { get; }
    }
}
