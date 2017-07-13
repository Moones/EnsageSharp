using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderQueue.UnitOrder
{
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderQueue.UnitOrder.OrderPriority;
    using Ability.Core.AbilityFactory.Utilities;

    /// <summary>The UnitOrder interface.</summary>
    public interface IUnitOrder
    {
        OrderType OrderType { get; }

        IAbilityUnit Unit { get; }

        uint Id { get; set; }

        uint Priority { get; }

        bool ExecuteOnce { get; }

        bool CanExecute();

        float Execute();

        void Enqueue();

        void Dequeue();
    }
}
