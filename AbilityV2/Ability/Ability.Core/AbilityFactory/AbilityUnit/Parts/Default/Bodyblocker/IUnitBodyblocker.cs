using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Bodyblocker
{
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderIssuer;

    public interface IUnitBodyblocker : IOrderIssuer
    {
        IAbilityUnit Target { get; }
        bool Bodyblock();
    }
}
