using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.LoneDruid
{
    using System.ComponentModel.Composition;

    using Ability.Core.AbilityFactory.AbilitySkill;
    using Ability.Core.AbilityFactory.AbilityUnit.Metadata;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.AttackRange;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Composer;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.SkillBook;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.LoneDruid.AttackRange;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.LoneDruid.SkillBook;

    using Ensage;

    [Export(typeof(IAbilityUnitHeroComposer))]
    [AbilityUnitHeroMetadata((uint)HeroId.npc_dota_hero_lone_druid)]
    public class LoneDruidComposer : AbilityUnitComposer
    {
        public LoneDruidComposer()
        {
            this.AssignPart<IUnitAttackRange>(unit => new LoneDruidAttackRange(unit));
            this.AssignPart<ISkillBook<IAbilitySkill>>(unit => new LoneDruidSkillBook(unit));
        }
    }
}
