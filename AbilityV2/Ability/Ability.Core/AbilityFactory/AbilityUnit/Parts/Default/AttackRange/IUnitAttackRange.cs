using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.AttackRange
{
    public interface IUnitAttackRange : IAbilityUnitPart
    {
        float Value { get; set; }

        bool IsInAttackRange(IAbilityUnit target);
    }
}
