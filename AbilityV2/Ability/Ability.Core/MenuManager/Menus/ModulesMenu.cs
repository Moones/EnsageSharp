using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.MenuManager.Menus
{
    using Ensage.Common.Menu;

    internal class ModulesMenu : Menu
    {
        internal ModulesMenu()
            : base("Modules", "Ability#.Modules")
        {
            this.UtilityModules = new Menu("Utility", this.Name + "Utility");
            this.AddSubMenu(this.UtilityModules);
        }

        public Menu UtilityModules { get; }
    }
}
