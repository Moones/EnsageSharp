namespace Ability.Core.AbilityModule.ModuleBase
{
    using Ability.Core.AbilityData.AbilityMapDataProvider.AbilityMapData;
    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.MenuManager.Menus.AbilityMenu;
    using Ability.Core.MenuManager.Menus.AbilityMenu.Items;

    /// <summary>The AbilityModule interface.</summary>
    public interface IAbilityModule : IAbilityService
    {
        bool LoadOnGameStart { get; }

        IAbilityUnit LocalHero { get; set; }

        IAbilityMapData MapData { get; set; }

        string ModuleName { get; }

        string Description { get; }

        AbilityMenu Menu { get; }

        AbilityMenuItem<bool> EnableSwitchMenuItem { get; }
    }
}
