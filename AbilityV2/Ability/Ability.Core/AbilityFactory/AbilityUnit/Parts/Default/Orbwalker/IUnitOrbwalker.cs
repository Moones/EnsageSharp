using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Orbwalker
{
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.DamageCalculator.Workers.Abstract;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderIssuer;
    using Ability.Core.AbilityFactory.Utilities;
    using Ability.Core.MenuManager.Menus.AbilityMenu.Items;

    using Ensage.Common.Menu;

    public interface IUnitOrbwalker : IOrderIssuer
    {
        IAbilityUnit Target { get; }

        bool Enabled { get; set; }

        bool BeforeAttack();

        bool AfterAttack();

        bool NoTarget();

        bool CantAttack();

        bool Meanwhile();
    }
}
