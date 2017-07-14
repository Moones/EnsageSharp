using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.AttackAnimationTracker
{
    public interface IAttackAnimationTracker : IAbilityUnitPart
    {
        void AttackStarted();

        float NextAttackTime { get; set; }

        float CancelAnimationTime { get; set; }
    }
}
