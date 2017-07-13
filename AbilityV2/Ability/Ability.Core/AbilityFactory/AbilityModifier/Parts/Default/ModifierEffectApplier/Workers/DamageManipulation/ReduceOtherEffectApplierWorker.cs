namespace Ability.Core.AbilityFactory.AbilityModifier.Parts.Default.ModifierEffectApplier.Workers.DamageManipulation
{
    using System;

    internal class ReduceOtherEffectApplierWorker : EffectApplierWorker
    {
        internal ReduceOtherEffectApplierWorker(
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
                            unit.DamageManipulation.ReduceOther += this.Value;
                            unit.DamageManipulation.ReduceOtherMinusEvents.Add(
                                this.Modifier.ModifierHandle,
                                new Tuple<float, double>(this.Modifier.SourceModifier.DieTime, this.Value));
                        };
                };
            this.RemoveEffectActionGetter = () => unit =>
                {
                    unit.DamageManipulation.ReduceOtherMinusEvents.Remove(this.Modifier.ModifierHandle);
                    unit.DamageManipulation.ReduceOther -= this.Value;
                };
        }


        public IAbilityModifier Modifier { get; }

        public double Value { get; set; }

        public Func<IAbilityModifier, double> ValueGetter { get; }
    }
}
