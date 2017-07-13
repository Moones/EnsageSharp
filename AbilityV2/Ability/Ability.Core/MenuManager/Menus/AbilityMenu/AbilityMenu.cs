using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.MenuManager.Menus.AbilityMenu
{
    using System.CodeDom;

    using Ability.Core.MenuManager.Menus.AbilityMenu.Items;
    using Ability.Core.MenuManager.Menus.AbilityMenu.Submenus;

    using Ensage.Common.Menu;

    public class AbilityMenu : IAbilityMenu
    {
        private readonly Dictionary<string, IAbilityMenuItem> menuItems = new Dictionary<string, IAbilityMenuItem>();

        private readonly Dictionary<string, AbilitySubMenu> submenus = new Dictionary<string, AbilitySubMenu>();

        public AbilityMenu(string name, string textureName = null)
        {
            this.Name = name;
            this.Menu = textureName != null
                            ? new Menu(name, Constants.AssemblyName + name, false, textureName, true)
                            : new Menu(name, Constants.AssemblyName + name);
        }

        public IReadOnlyDictionary<string, IAbilityMenuItem> MenuItems => this.menuItems;

        public IReadOnlyDictionary<string, AbilitySubMenu> Submenus => this.submenus;

        public string Name { get; }

        internal Menu Menu { get; set; }

        public void AddDescription(string description)
        {
            this.Menu.AddItem(
                new MenuItem(this.Menu.Name + "Description", "Description (hover mouse)").SetTooltip(description));
        }
    }
}
