using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityModule
{
    using Ability.Core.AbilityFactory.Utilities;
    using Ability.Core.AbilityModule.ModuleBase;

    public interface IAbilityModuleManager : IAbilityService
    {
        DataProvider<IAbilityModule> ModuleActivated { get; set; }
    }
}
