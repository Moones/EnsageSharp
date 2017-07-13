namespace Ability.Core.AbilityFactory.AbilityModifier.Parts.Default.ModifierEffectApplier.Workers.DamageManipulation
{
    using System;

    internal class AmpFromMeEffectApplierWorker : EffectApplierWorker
    {
        internal AmpFromMeEffectApplierWorker(
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
                        unit.DamageManipulation.AmpFromMe.AddModifierValue(modifier, this.Value, true);
                        };
                };
            this.RemoveEffectActionGetter = () => unit =>
                {
                    unit.DamageManipulation.AmpFromMe.RemoveModifierValue(modifier, this.Value);
                };

            this.UpdateEffectActionGetter = () =>
                {
                    return unit =>
                    {
                        this.Value = this.ValueGetter.Invoke(this.Modifier);
                        unit.DamageManipulation.AmpFromMe.UpdateModifierValue(modifier, this.Value);
                        };
                };
        }



        public IAbilityModifier Modifier { get; }

        public double Value { get; set; }

        public Func<IAbilityModifier, double> ValueGetter { get; }
    }
}
