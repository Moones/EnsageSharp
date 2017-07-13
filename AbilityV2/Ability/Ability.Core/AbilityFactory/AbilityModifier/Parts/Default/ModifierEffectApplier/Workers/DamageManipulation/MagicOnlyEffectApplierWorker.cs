namespace Ability.Core.AbilityFactory.AbilityModifier.Parts.Default.ModifierEffectApplier.Workers.DamageManipulation
{
    using System;

    internal class MagicOnlyEffectApplierWorker : EffectApplierWorker
    {
        internal MagicOnlyEffectApplierWorker(
            IAbilityModifier modifier,
            bool updateWithLevel,
            Func<IAbilityModifier, float> valueGetter)
            : base(updateWithLevel)
        {
            this.Modifier = modifier;
            this.ValueGetter = valueGetter;
            this.ApplyEffectActionGetter = () =>
                {
                    return unit =>
                    {
                        this.Value = this.ValueGetter.Invoke(this.Modifier);
                        unit.DamageManipulation.MagicalDamageAbsorb.AddModifierValue(modifier, this.Value, true);
                        };
                };
            this.RemoveEffectActionGetter = () => unit =>
                {
                    unit.DamageManipulation.MagicalDamageAbsorb.RemoveModifierValue(modifier, this.Value);
                };

            this.UpdateEffectActionGetter = () =>
                {
                    return unit =>
                    {
                        this.Value = this.ValueGetter.Invoke(this.Modifier);
                        unit.DamageManipulation.MagicalDamageAbsorb.UpdateModifierValue(modifier, this.Value);
                        };
                };
        }


        public IAbilityModifier Modifier { get; }

        public float Value { get; set; }

        public Func<IAbilityModifier, float> ValueGetter { get; }
    }
}
