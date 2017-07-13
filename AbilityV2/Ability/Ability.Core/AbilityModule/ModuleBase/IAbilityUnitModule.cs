namespace Ability.Core.AbilityModule.ModuleBase
{
    using Ability.Core.AbilityFactory.AbilityUnit;

    public interface IAbilityUnitModule
    {
        void UnitAdded(IAbilityUnit unit);

        void UnitRemoved(IAbilityUnit unit);
    }
}
