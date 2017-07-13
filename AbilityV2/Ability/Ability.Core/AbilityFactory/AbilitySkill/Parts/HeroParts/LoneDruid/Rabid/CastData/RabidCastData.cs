using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.HeroParts.LoneDruid.Rabid.CastData
{
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillCastData;
    using Ability.Core.AbilityFactory.Utilities;

    using Ensage;
    using Ensage.Common.Extensions;

    public class RabidCastData : SkillCastData
    {
        public RabidCastData(IAbilitySkill skill)
            : base(skill)
        {
        }

        public bool LocalHeroAffected { get; set; }

        public bool BearAffected { get; set; }

        public override void Initialize()
        {
            this.Skill.Owner.Modifiers.ModifierRemoved.Subscribe(
                new DataObserver<Modifier>(
                    modifier =>
                        {
                            if (modifier.Name == "modifier_lone_druid_rabid")
                            {
                                this.LocalHeroAffected = false;
                            }
                        }));

            this.LocalHeroAffected = this.Skill.Owner.SourceUnit.HasModifier("modifier_lone_druid_rabid");

            this.Skill.Owner.Modifiers.ModifierAdded.Subscribe(
                new DataObserver<Modifier>(
                    modifier =>
                        {
                            if (modifier.Name == "modifier_lone_druid_rabid")
                            {
                                this.LocalHeroAffected = true;
                            }
                        }));

            base.Initialize();
        }
    }
}
