namespace Ability.Core.AbilityFactory.AbilityModifier.Parts.Default.ModifierEffectApplier
{
    using System.Collections.Generic;

    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillLevel;
    using Ability.Core.AbilityFactory.Utilities;

    /// <summary>The modifier effect applier.</summary>
    public class ModifierEffectApplier : IModifierEffectApplier
    {
        private DataObserver<ISkillLevel> levelObserver;

        public ModifierEffectApplier(IAbilityModifier modifier)
        {
            this.Modifier = modifier;
        }

        public void Dispose()
        {
            foreach (var modifierEffectApplierWorker in this.Workers)
            {
                modifierEffectApplierWorker.RemoveEffect();
            }

            this.levelObserver.Dispose();
        }

        public IAbilityModifier Modifier { get; set; }

        public ICollection<IEffectApplierWorker> Workers { get; set; } = new List<IEffectApplierWorker>();

        public virtual void Initialize()
        {
            this.ApplyEffect();

            this.levelObserver = new DataObserver<ISkillLevel>(level => this.UpdateEffect());
            this.levelObserver.Subscribe(this.Modifier.SourceSkill.Level);
        }

        public void ApplyEffect()
        {
            foreach (var modifierEffectApplierWorker in this.Workers)
            {
                modifierEffectApplierWorker.ApplyEffect(this.Modifier.AffectedUnit);
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
