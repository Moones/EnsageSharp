using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderQueue.UnitOrder.Orders
{
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderQueue.UnitOrder.OrderPriority;

    using Ensage;

    public class MoveToMouse : UnitOrderBase
    {
        public MoveToMouse(OrderType orderType, IAbilityUnit unit)
            : base(orderType, unit)
        {
        }

        public override bool CanExecute()
        {
            return true;
        }

        public override float Execute()
        {
            this.Unit.SourceUnit.Move(Game.MousePosition);
            return 100;
        }
    }
}
