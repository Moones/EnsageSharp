namespace Ability.Fighter.LoneDruid.BodyblockCombo
{
    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Bodyblocker;

    public class BearBodyblocker : UnitBodyblocker
    {
        public BearBodyblocker(IAbilityUnit unit)
            : base(unit)
        {
            this.IssueSleep = 200;
        }
    }
}
