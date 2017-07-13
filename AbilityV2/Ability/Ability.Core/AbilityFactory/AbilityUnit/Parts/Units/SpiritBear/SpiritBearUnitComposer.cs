using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Units.SpiritBear
{
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Primitives;

    using Ability.Core.AbilityFactory.AbilitySkill;
    using Ability.Core.AbilityFactory.AbilityUnit.Metadata;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Composer;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.SkillBook;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Units.SpiritBear.SkillBook;

    [Export(typeof(IAbilityUnitComposer))]
    [AbilityUnitMetadata(
        "npc_dota_lone_druid_bear1", 
        "npc_dota_lone_druid_bear2", 
        "npc_dota_lone_druid_bear3", 
        "npc_dota_lone_druid_bear4")]
    internal class SpiritBearUnitComposer : AbilityUnitComposer
    {
        public SpiritBearUnitComposer()
        {
            this.AssignPart<ISkillBook<IAbilitySkill>>(unit => new SpiritBearSkillBook(unit));
        }
    }
}
