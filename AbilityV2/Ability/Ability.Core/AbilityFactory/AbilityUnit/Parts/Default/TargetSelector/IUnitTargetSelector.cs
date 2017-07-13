using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.TargetSelector
{
    using Ability.Core.AbilityFactory.Utilities;

    public interface IUnitTargetSelector : IAbilityUnitPart
    {
        void ResetTarget();

        Notifier TargetChanged { get; }

        Notifier TargetDistanceChanged { get; }

        float LastDistanceToTarget { get; set; }

        bool TargetIsSet { get; set; }

        IAbilityUnit Target { get; set; }

        IAbilityUnit GetTarget();

        IAbilityUnit[] GetTargets();
    }
}
