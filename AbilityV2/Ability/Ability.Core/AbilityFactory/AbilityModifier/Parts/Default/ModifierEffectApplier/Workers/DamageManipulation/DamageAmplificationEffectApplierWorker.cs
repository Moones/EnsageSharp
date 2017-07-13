namespace Ability.Core.AbilityFactory.AbilityModifier.Parts.Default.ModifierEffectApplier.Workers.DamageManipulation
{
    using System;

    internal class DamageAmplificationEffectApplierWorker : EffectApplierWorker
    {
        internal DamageAmplificationEffectApplierWorker(
            IAbilityModifier modifier,
            bool updateWithLevel,
            Func<IAbilityModifier, double> valueGetter)
            : base(updateWithLevel)
        {
            this.Modifier = modifier;
            this.ValueGetter = valueGetter;
            this.ApplyEffectActionGetter = () =>
                {
                    return unit =>
                    {
                        this.Value = this.ValueGetter.Invoke(this.Modifier);
                        unit.DamageManipulation.DamageAmplification.AddModifierValue(modifier, this.Value, true);
                        };
                };
            this.RemoveEffectActionGetter = () => unit =>
                {
                    unit.DamageManipulation.DamageAmplification.RemoveModifierValue(modifier, this.Value);
                };
            this.UpdateEffectActionGetter = () =>
                {
                    return unit =>
                    {
                        this.Value = this.ValueGetter.Invoke(this.Modifier);
                        unit.DamageManipulation.DamageAmplification.UpdateModifierValue(modifier, this.Value);
                        };
                };
        }



        public IAbilityModifier Modifier { get; }

        public double Value { get; set; }

        public Func<IAbilityModifier, double> ValueGetter { get; }
    }
}
