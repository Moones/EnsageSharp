using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Units.SpiritBear.SkillBook
{
    using Ability.Core.AbilityFactory.AbilitySkill;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.SkillBook;

    using Ensage;

    public class SpiritBearSkillBook : SkillBook<IAbilitySkill>
    {
        public SpiritBearSkillBook(IAbilityUnit unit)
            : base(unit)
        {
        }

        public IAbilitySkill Return { get; set; }

        public override void AddSkill(IAbilitySkill skill)
        {
            base.AddSkill(skill);

            if (skill.SourceAbility.Id == AbilityId.lone_druid_spirit_bear_return)
            {
                this.Return = skill;
            }
        }
    }
}
