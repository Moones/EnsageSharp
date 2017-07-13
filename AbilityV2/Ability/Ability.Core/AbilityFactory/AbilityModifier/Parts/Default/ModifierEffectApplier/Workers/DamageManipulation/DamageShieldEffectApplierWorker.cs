namespace Ability.Core.AbilityFactory.AbilityModifier.Parts.Default.ModifierEffectApplier.Workers.DamageManipulation
{
    internal class DamageShieldEffectApplierWorker : EffectApplierWorker
    {
        internal DamageShieldEffectApplierWorker(
            IAbilityModifier modifier,
            bool updateWithLevel,
            bool magical,
            bool physical,
            bool pure)
            : base(updateWithLevel)
        {
            this.Modifier = modifier;
            this.ApplyEffectActionGetter = () =>
                {
                    return unit =>
                        {
                            unit.DamageManipulation.AddDamageShield(
                                this.Modifier.ModifierHandle,
                                magical,
                                physical,
                                pure);
                        };
                };
            this.RemoveEffectActionGetter = () => unit =>
                {
                    unit.DamageManipulation.RemoveDamageShield(this.Modifier.ModifierHandle, magical, physical, pure);
                };
        }

        public IAbilityModifier Modifier { get; }
    }
}
