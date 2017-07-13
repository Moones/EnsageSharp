namespace Ability.Core.AbilityFactory.AbilityTalent.Parts.Default.TalentEffectApllier
{
    using Ability.Core.AbilityFactory.Utilities;

    public abstract class TalentEffectApplier : ITalentEffectApplier
    {
        protected TalentEffectApplier(IAbilityTalent talent)
        {
            this.Talent = talent;
        }

        public void Dispose()
        {
            if (!this.EffectWasApplied)
            {
                this.levelUpdater.Dispose();
            }
        }

        public IAbilityTalent Talent { get; set; }

        private ActionExecutor levelUpdater;

        public void Initialize()
        {
            this.levelUpdater = new ActionExecutor(this.Update);
            this.levelUpdater.Subscribe(this.Talent.Owner.DataReceiver.Updates);
        }

        public bool EffectWasApplied { get; set; }

        public abstract void ApplyEffect();

        private uint lastLevel;

        private void Update()
        {
            if (this.EffectWasApplied || this.lastLevel == this.Talent.SourceAbility.Level)
            {
                return;
            }

            this.lastLevel = this.Talent.SourceAbility.Level;
            if (this.lastLevel > 0)
            {
                this.ApplyEffect();
                this.EffectWasApplied = true;
                this.levelUpdater.Dispose();
            }
        }
    }
}
