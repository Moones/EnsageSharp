using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderQueue.UnitOrder.OrderPriority
{
    using System.Diagnostics.Contracts;
    
    public interface IOrderPriority
    {
        /// <summary>The get priority.</summary>
        /// <returns>The <see cref="uint"/>.</returns>
        uint GetPriority();
    }
}
