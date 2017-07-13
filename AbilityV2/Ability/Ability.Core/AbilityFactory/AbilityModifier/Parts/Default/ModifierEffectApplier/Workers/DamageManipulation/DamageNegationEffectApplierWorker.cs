namespace Ability.Core.AbilityFactory.AbilityModifier.Parts.Default.ModifierEffectApplier.Workers.DamageManipulation
{
    using System;

    internal class DamageNegationEffectApplierWorker : EffectApplierWorker
    {
        internal DamageNegationEffectApplierWorker(IAbilityModifier modifier,
            bool updateWithLevel, Func<IAbilityModifier, double> valueGetter)
            : base(updateWithLevel)
        {
            this.Modifier = modifier;
            this.ValueGetter = valueGetter;
            this.ApplyEffectActionGetter = () =>
            {
                return unit =>
                {
                    this.Value = this.ValueGetter.Invoke(this.Modifier);
                    unit.DamageManipulation.DamageNegation.AddModifierValue(modifier, this.Value, true);
                        //unit.DamageManipulation.DamageNegationMinusEvents.Add(
                        //    this.Modifier.ModifierHandle,
                        //    new Tuple<float, float>(this.Modifier.SourceModifier.DieTime, this.Value));
                    };
            };
            this.RemoveEffectActionGetter = () => unit =>
            {
                //unit.DamageManipulation.DamageNegationMinusEvents.Remove(this.Modifier.ModifierHandle);
                unit.DamageManipulation.DamageNegation.RemoveModifierValue(modifier, this.Value);
            };
            this.UpdateEffectActionGetter = () =>
            {
                return unit =>
                {
                    this.Value = this.ValueGetter.Invoke(this.Modifier);
                    unit.DamageManipulation.DamageNegation.UpdateModifierValue(modifier, this.Value);
                    //unit.DamageManipulation.DamageNegationMinusEvents.Add(
                    //    this.Modifier.ModifierHandle,
                    //    new Tuple<float, float>(this.Modifier.SourceModifier.DieTime, this.Value));
                };
            };
        }


        public IAbilityModifier Modifier { get; }

        public double Value { get; set; }

        public Func<IAbilityModifier, double> ValueGetter { get; }
    }
}
