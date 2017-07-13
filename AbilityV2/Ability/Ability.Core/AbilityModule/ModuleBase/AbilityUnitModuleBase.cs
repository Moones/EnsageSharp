namespace Ability.Core.AbilityModule.ModuleBase
{
    using Ability.Core.AbilityFactory.AbilityUnit;

    public abstract class AbilityUnitModuleBase : AbilityHeroModuleBase, IAbilityUnitModule
    {
        protected AbilityUnitModuleBase(
            string name,
            string shortDescription,
            bool generateMenu,
            bool enabledByDefault,
            bool loadOnGameStart,
            string textureName = null)
            : base(name, shortDescription, generateMenu, enabledByDefault, loadOnGameStart, textureName)
        {
        }

        public abstract void UnitAdded(IAbilityUnit unit);

        public abstract void UnitRemoved(IAbilityUnit unit);
    }
}
