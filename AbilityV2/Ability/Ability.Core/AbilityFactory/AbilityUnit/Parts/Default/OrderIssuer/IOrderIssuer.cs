using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderIssuer
{
    public interface IOrderIssuer : IAbilityUnitPart
    {
        bool Enabled { get; set; }

        uint Id { get; set; }

        bool Issue();

        bool PreciseIssue();
    }
}
