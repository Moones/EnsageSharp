namespace Ability.Core.AbilityFactory.AbilityModifier.Parts.Default.ModifierEffectApplier.Workers.DamageManipulation
{
    using System;

    using Ability.Core.AbilityFactory.AbilitySkill;

    internal class DamageBlockEffectApplierWorker : EffectApplierWorker
    {
        internal DamageBlockEffectApplierWorker(
            IAbilityModifier modifier,
            bool updateWithLevel,
            Func<IAbilityModifier, float> modifierValueGetter)
            : base(updateWithLevel)
        {
            this.Modifier = modifier;
            this.ModifierValueGetter = modifierValueGetter;
            this.ApplyEffectActionGetter = () =>
                {
                    return unit =>
                    {
                        this.Value = this.ModifierValueGetter.Invoke(this.Modifier);
                        unit.DamageManipulation.AddDamageBlock(this.Modifier.ModifierHandle, this.Value);
                        };
                };
            this.RemoveEffectActionGetter = () => unit =>
                {
                    unit.DamageManipulation.RemoveDamageBlock(this.Modifier.ModifierHandle);
                };
            this.UpdateEffectActionGetter = () =>
                {
                    return unit =>
                    {
                        this.Value = this.ModifierValueGetter.Invoke(this.Modifier);
                        unit.DamageManipulation.UpdateDamageBlock(this.Modifier.ModifierHandle, this.Value);
                        };
                };
        }

        public DamageBlockEffectApplierWorker(
            IAbilitySkill skill,
            bool updateWithLevel,
            Func<IAbilitySkill, float> valueGetter)
            : base(updateWithLevel)
        {
            this.ValueGetter = valueGetter;
            this.Skill = skill;
            this.ApplyEffectActionGetter = () =>
                {
                    return unit =>
                    {
                        this.Value = this.ValueGetter.Invoke(this.Skill);
                        unit.DamageManipulation.AddDamageBlock(this.Skill.SkillHandle, this.Value);
                        };
                };
            this.RemoveEffectActionGetter = () => unit =>
                {
                    unit.DamageManipulation.RemoveDamageBlock(this.Skill.SkillHandle);
                };
            this.UpdateEffectActionGetter = () =>
                {
                    return unit =>
                    {
                        this.Value = this.ValueGetter.Invoke(this.Skill);
                        unit.DamageManipulation.UpdateDamageBlock(this.Skill.SkillHandle, this.Value);
                        };
                };
        }


        public IAbilityModifier Modifier { get; }

        public IAbilitySkill Skill { get; }

        public float Value { get; set; }

        public Func<IAbilityModifier, float> ModifierValueGetter { get; }
        public Func<IAbilitySkill, float> ValueGetter { get; }
    }
}
