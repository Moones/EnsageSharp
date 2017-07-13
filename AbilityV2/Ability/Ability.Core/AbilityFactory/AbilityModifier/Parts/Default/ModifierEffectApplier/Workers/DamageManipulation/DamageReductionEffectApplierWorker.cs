namespace Ability.Core.AbilityFactory.AbilityModifier.Parts.Default.ModifierEffectApplier.Workers.DamageManipulation
{
    using System;

    using Ability.Core.AbilityFactory.AbilitySkill;

    internal class DamageReductionEffectApplierWorker : EffectApplierWorker
    {
        internal DamageReductionEffectApplierWorker(IAbilityModifier modifier,
            bool updateWithLevel, Func<IAbilityModifier, double> modifierValueGetter)
            : base(updateWithLevel)
        {
            this.Modifier = modifier;
            this.ModifierValueGetter = modifierValueGetter;
            this.ApplyEffectActionGetter = () =>
            {
                return unit =>
                {
                    this.Value = this.ModifierValueGetter.Invoke(this.Modifier);
                    unit.DamageManipulation.DamageReduction.AddModifierValue(modifier, this.Value, true);
                };
            };
            this.RemoveEffectActionGetter = () => unit =>
            {
                unit.DamageManipulation.DamageReduction.RemoveModifierValue(modifier, this.Value);
            };

            this.UpdateEffectActionGetter = () =>
            {
                return unit =>
                {
                    this.Value = this.ModifierValueGetter.Invoke(this.Modifier);
                    unit.DamageManipulation.DamageReduction.UpdateModifierValue(modifier, this.Value);
                };
            };
        }

        public DamageReductionEffectApplierWorker(
            IAbilitySkill skill,
            bool updateWithLevel,
            Func<IAbilitySkill, double> valueGetter)
            : base(updateWithLevel)
        {
            this.Skill = skill;
            this.ValueGetter = valueGetter;
            this.ApplyEffectActionGetter = () =>
                {
                    return unit =>
                    {
                        this.Value = this.ValueGetter.Invoke(this.Skill);
                        unit.DamageManipulation.DamageReduction.AddSkillValue(skill, this.Value);
                        };
                };
            this.RemoveEffectActionGetter = () => unit =>
                {
                    unit.DamageManipulation.DamageReduction.RemoveSkillValue(skill, this.Value);
                };

            this.UpdateEffectActionGetter = () =>
                {
                    return unit =>
                    {
                        this.Value = this.ValueGetter.Invoke(this.Skill);
                        unit.DamageManipulation.DamageReduction.UpdateSkillValue(skill, this.Value);
                        };
                };
        }

        public IAbilitySkill Skill { get; }

        public IAbilityModifier Modifier { get; }

        public double Value { get; set; }

        public Func<IAbilityModifier, double> ModifierValueGetter { get; }
        public Func<IAbilitySkill, double> ValueGetter { get; }
    }
}
