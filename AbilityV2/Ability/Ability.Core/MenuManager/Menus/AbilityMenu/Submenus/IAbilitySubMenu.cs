using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.MenuManager.Menus.AbilityMenu.Submenus
{
    internal interface IAbilitySubMenu : IAbilityMenu
    {
        AbilityMenu ParentMenu { get; }
        void AddToMenu(AbilityMenu menu);
    }
}
