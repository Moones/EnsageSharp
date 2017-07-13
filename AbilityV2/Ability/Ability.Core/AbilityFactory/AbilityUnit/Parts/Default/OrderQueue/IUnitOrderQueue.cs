using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderQueue
{
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderIssuer;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderQueue.UnitOrder;
    using Ability.Core.AbilityFactory.Utilities;

    public interface IUnitOrderQueue : IOrderIssuer
    {
        DataProvider<IUnitOrder> NewOrderQueued { get; }

        Notifier QueueEmpty { get; }

        DataProvider<IUnitOrder> StartedExecution { get; }

        bool IsWorking { get; set; }

        void EnqueueOrder(IUnitOrder order);

        //IUnitOrder DequeueFirstOrder();
    }
}
