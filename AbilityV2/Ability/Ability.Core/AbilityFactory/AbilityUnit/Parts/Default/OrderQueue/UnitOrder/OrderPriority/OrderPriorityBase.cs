using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderQueue.UnitOrder.OrderPriority
{
    public abstract class OrderPriorityBase : IOrderPriority
    {
        /// <summary>The get priority.</summary>
        /// <returns>The <see cref="uint"/>.</returns>
        public abstract uint GetPriority();
    }
}
