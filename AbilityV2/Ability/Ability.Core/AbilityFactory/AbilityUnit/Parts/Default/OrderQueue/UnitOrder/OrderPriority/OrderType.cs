using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderQueue.UnitOrder.OrderPriority
{
    public enum OrderType
    {
        MoveToPosition,

        FarmGold,

        DropItem,

        HealTower,

        DealDamageToEnemy,

        HealAlly,

        HealSelf,

        Escape,

        HelpAlly,

        PickUpFromGround,

        Evade
    }
}
