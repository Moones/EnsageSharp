using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.MenuManager.Menus.AbilityMenu.Items
{
    using Ability.Core.AbilityFactory.Utilities;

    /// <summary>The AbilityMenuItem interface.</summary>
    public interface IAbilityMenuItem : IDisposable
    {
        Type ValueType { get; }

        string Name { get; }

        AbilityMenu ParentMenu { get; }

        void AddToMenu(AbilityMenu menu);
    }
}
