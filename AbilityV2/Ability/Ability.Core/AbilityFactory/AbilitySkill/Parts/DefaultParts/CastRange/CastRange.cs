using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.CastRange
{
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillLevel;
    using Ability.Core.AbilityFactory.Utilities;

    public class CastRange : ICastRange
    {
        private float baseValue;

        private float bonusValue;

        public CastRange(IAbilitySkill skill)
        {
            this.Skill = skill;
        }

        public void Dispose()
        {
        }

        public IAbilitySkill Skill { get; set; }

        public virtual void Initialize()
        {
            this.UpdateValue();

            this.Skill.Level.Subscribe(
                new DataObserver<ISkillLevel>(
                    level =>
                    {
                        this.UpdateValue();
                    }));
            
            this.Skill.Owner.TargetSelector.TargetDistanceChanged.Subscribe(
                () =>
                    {
                        this.IsTargetInRange = this.Skill.Owner.TargetSelector.LastDistanceToTarget <= this.Value;
                    });
        }

        public virtual void UpdateValue()
        {
            this.BaseValue = this.Skill.SourceAbility.CastRange;
        }

        public float BaseValue
        {
            get
            {
                return this.baseValue;
            }

            set
            {
                this.baseValue = value;
                this.Value = this.baseValue + this.bonusValue;
            }
        }

        public float BonusValue
        {
            get
            {
                return this.bonusValue;
            }

            set
            {
                this.bonusValue = value;
                this.Value = this.bonusValue + this.baseValue;
            }
        }

        public bool IsTargetInRange { get; set; }

        public float Value { get; set; }
    }
}
