namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.EffectApplier
{
    using System;
    using System.Collections.Generic;

    using Ability.Core.AbilityFactory.AbilityModifier.Parts.Default.ModifierEffectApplier;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillLevel;
    using Ability.Core.AbilityFactory.Utilities;

    public class EffectApplier : IEffectApplier
    {
        private DataObserver<ISkillLevel> levelObserver;

        public EffectApplier(IAbilitySkill skill)
        {
            this.Skill = skill;
        }

        public void Dispose()
        {
            //foreach (var modifierEffectApplierWorker in this.Workers)
            //{
            //    modifierEffectApplierWorker.RemoveEffect();
            //}

            //this.levelObserver.Dispose();
        }

        public IAbilitySkill Skill { get; set; }

        public void Initialize()
        {
            //this.ApplyEffect();
            //Console.WriteLine("applying effect " + this.Skill.Name);
            //this.levelObserver = new DataObserver<ISkillLevel>(level => this.UpdateEffect());
            //this.levelObserver.Subscribe(this.Skill.Level);
        }

        public ICollection<IEffectApplierWorker> Workers { get; set; } = new List<IEffectApplierWorker>();

        public void ApplyEffect()
        {
            foreach (var modifierEffectApplierWorker in this.Workers)
            {
                modifierEffectApplierWorker.ApplyEffect(this.Skill.Owner);
            }
        }

        public void UpdateEffect()
        {
            foreach (var modifierEffectApplierWorker in this.Workers)
            {
                if (modifierEffectApplierWorker.UpdateWithLevel)
                {
                    modifierEffectApplierWorker.UpdateEffect();
                }
            }
        }
    }
}
