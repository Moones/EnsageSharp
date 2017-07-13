using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.LoneDruid.SkillBook
{
    using Ability.Core.AbilityFactory.AbilitySkill;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillComposer;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.SkillBook;

    using Ensage;

    public class LoneDruidSkillBook : SkillBook<IAbilitySkill>
    {
        public LoneDruidSkillBook(IAbilityUnit unit)
            : base(unit)
        {
        }

        public IAbilitySkill Rabid { get; set; }

        public IAbilitySkill SavageRoar { get; set; }
        public IAbilitySkill TrueForm { get; set; }
        public IAbilitySkill TrueFormDruid { get; set; }
        public IAbilitySkill BattleCry { get; set; }

        public override void AddSkill(IAbilitySkill skill)
        {
            base.AddSkill(skill);

            switch (skill.SourceAbility.Id)
            {
                case AbilityId.lone_druid_rabid:
                    this.Rabid = skill;
                    return;
                case AbilityId.lone_druid_savage_roar:
                    this.SavageRoar = skill;
                    return;
                case AbilityId.lone_druid_true_form:
                    this.TrueForm = skill;
                    return;
                case AbilityId.lone_druid_true_form_druid:
                    this.TrueFormDruid = skill;
                    return;
                case AbilityId.lone_druid_true_form_battle_cry:
                    this.BattleCry = skill;
                    return;
            }
        }
    }
}
