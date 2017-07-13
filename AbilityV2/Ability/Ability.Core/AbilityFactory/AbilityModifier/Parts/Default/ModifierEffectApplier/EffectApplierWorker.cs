namespace Ability.Core.AbilityFactory.AbilityModifier.Parts.Default.ModifierEffectApplier
{
    using System;

    using Ability.Core.AbilityFactory.AbilityUnit;

    /// <summary>The modifier effect applier worker.</summary>
    internal class EffectApplierWorker : IEffectApplierWorker
    {
        private Action<IAbilityUnit> removeEffectAction;

        private Action<IAbilityUnit> applyEffectAction;

        private Action<IAbilityUnit> applyEffectActionUpdate;

        private Action<IAbilityUnit> updateEffectAction;

        private bool useCustomUpdate;

        private IAbilityUnit abilityUnit;

        private Func<Action<IAbilityUnit>> applyEffectActionGetter;

        private Func<Action<IAbilityUnit>> removeEffectActionGetter;

        private Func<Action<IAbilityUnit>> updateEffectActionGetter;

        internal EffectApplierWorker(
            bool updateWithLevel,
            Func<Action<IAbilityUnit>> applyEffectActionGetter,
            Func<Action<IAbilityUnit>> removeEffectActionGetter,
            Func<Action<IAbilityUnit>> updateEffectActionGetter,
            Func<Action<IAbilityUnit>> applyEffectActionUpdateGetter = null)
        {
            this.UpdateWithLevel = updateWithLevel;
            this.ApplyEffectActionGetter = applyEffectActionGetter;
            this.RemoveEffectActionGetter = removeEffectActionGetter;
            this.UpdateEffectActionGetter = updateEffectActionGetter;
            this.ApplyEffectActionUpdateGetter = applyEffectActionUpdateGetter;
            this.useCustomUpdate = applyEffectActionGetter != null;
        }

        public EffectApplierWorker(bool updateWithLevel)
        {
            this.UpdateWithLevel = updateWithLevel;
        }

        public bool UpdateWithLevel { get; }

        public Func<Action<IAbilityUnit>> ApplyEffectActionGetter
        {
            get
            {
                return this.applyEffectActionGetter;
            }

            set
            {
                this.applyEffectActionGetter = value;
                this.applyEffectAction = this.applyEffectActionGetter.Invoke();
            }
        }

        public Func<Action<IAbilityUnit>> RemoveEffectActionGetter
        {
            get
            {
                return this.removeEffectActionGetter;
            }

            set
            {
                this.removeEffectActionGetter = value;
                this.removeEffectAction = this.removeEffectActionGetter.Invoke();
            }
        }

        public Func<Action<IAbilityUnit>> UpdateEffectActionGetter
        {
            get
            {
                return this.updateEffectActionGetter;
            }

            set
            {
                this.updateEffectActionGetter = value;
                this.updateEffectAction = this.updateEffectActionGetter?.Invoke();
            }
        }

        public Func<Action<IAbilityUnit>> ApplyEffectActionUpdateGetter { get; }

        public bool EffectWasApplied { get; private set; }

        public void ApplyEffect(IAbilityUnit affectedUnit)
        {
            this.applyEffectAction.Invoke(affectedUnit);
            this.abilityUnit = affectedUnit;
            this.EffectWasApplied = true;
        }

        public void UpdateEffect()
        {
            this.updateEffectAction.Invoke(this.abilityUnit);
        }

        public void RemoveEffect()
        {
            this.removeEffectAction.Invoke(this.abilityUnit);
            this.EffectWasApplied = false;
        }
    }
}
