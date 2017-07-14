using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.AttackAnimation
{
    public interface IAttackAnimation : IAbilityUnitPart
    {
        float GetAttackSpeed();
        float GetAttackPoint();

        float GetAttackRate();
    }
}
