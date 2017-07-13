using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.MenuManager.Menus.AbilityMenu
{
    using Ability.Core.MenuManager.Menus.AbilityMenu.Items;
    using Ability.Core.MenuManager.Menus.AbilityMenu.Submenus;

    /// <summary>The AbilityMenu interface.</summary>
    internal interface IAbilityMenu
    {
        IReadOnlyDictionary<string, IAbilityMenuItem> MenuItems { get; }
        IReadOnlyDictionary<string, AbilitySubMenu> Submenus { get; }

        string Name { get; }
    }
}
