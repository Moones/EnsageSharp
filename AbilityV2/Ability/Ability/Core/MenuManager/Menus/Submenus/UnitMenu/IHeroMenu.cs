namespace Ability.Core.MenuManager.Menus.Submenus.UnitMenu
{
    /// <summary>The HeroMenu interface.</summary>
    public interface IHeroMenu : IUnitMenu
    {
        /// <summary>Gets the hero name.</summary>
        string HeroName { get; }

        /// <summary>Gets the menu display name.</summary>
        string MenuDisplayName { get; }
    }
}
