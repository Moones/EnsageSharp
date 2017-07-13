namespace Ability.Core.AbilityFactory.AbilityTalent
{
    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.Utilities;

    using Ensage;

    public class AbilityTalent : IAbilityTalent
    {
        private ActionExecutor levelChecker;

        private uint lastLevel;
        public AbilityTalent(Ability sourceAbility, IAbilityUnit owner)
        {
            this.SourceAbility = sourceAbility;
            this.Owner = owner;

            this.levelChecker = new ActionExecutor(
                () =>
                    {
                        if (this.lastLevel > 0 || this.lastLevel == this.SourceAbility.Level)
                        {
                            return;
                        }

                        this.lastLevel = this.SourceAbility.Level;
                        if (this.lastLevel > 0)
                        {
                            this.TalentLeveledNotifier.Notify();
                            this.levelChecker.Dispose();
                        }
                    });
            //this.levelChecker.Subscribe(this.Owner.DataReceiver.Updates);
        }

        public void Dispose()
        {
            if (this.lastLevel == 0)
            {
                this.levelChecker.Dispose();
            }

            this.TalentLeveledNotifier.Dispose();
        }

        public IAbilityUnit Owner { get; set; }

        public Ability SourceAbility { get; set; }

        public Notifier TalentLeveledNotifier { get; } = new Notifier();
    }
}
