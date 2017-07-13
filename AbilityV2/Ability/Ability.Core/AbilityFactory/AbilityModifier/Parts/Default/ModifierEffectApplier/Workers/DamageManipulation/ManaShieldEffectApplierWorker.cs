namespace Ability.Core.AbilityFactory.AbilityModifier.Parts.Default.ModifierEffectApplier.Workers.DamageManipulation
{
    using System;

    internal class ManaShieldEffectApplierWorker : EffectApplierWorker
    {
        internal ManaShieldEffectApplierWorker(
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
                        unit.DamageManipulation.ManaShield.AddSpecialModifierValue(
                                modifier,
                                (unit1, damageValue) =>
                                    {
                                        if (modifier.AffectedUnit.Mana.Current >= damageValue * .6 / this.Value)
                                        {
                                            return 0.6;
                                        }
                                        else
                                        {
                                            return modifier.AffectedUnit.Mana.Current * this.Value / damageValue;
                                        }
                                    });
                        };
                };
            this.RemoveEffectActionGetter = () => unit =>
            {
                unit.DamageManipulation.ManaShield.RemoveSpecialModifierValue(modifier);
            };
            this.UpdateEffectActionGetter = () =>
            {
                return unit =>
                {
                    this.Value = this.ValueGetter.Invoke(this.Modifier);
                    unit.DamageManipulation.ManaShield.UpdateSpecialModifierValue(
                            modifier,
                            (unit1, damageValue) =>
                                {
                                    if (modifier.AffectedUnit.Mana.Current >= damageValue * .6 / this.Value)
                                    {
                                        return 0.6;
                                    }
                                    else
                                    {
                                        return modifier.AffectedUnit.Mana.Current * this.Value / damageValue;
                                    }
                                });
                    };
            };
        }



        public IAbilityModifier Modifier { get; }

        public double Value { get; set; }

        public Func<IAbilityModifier, double> ValueGetter { get; }
    }
}
