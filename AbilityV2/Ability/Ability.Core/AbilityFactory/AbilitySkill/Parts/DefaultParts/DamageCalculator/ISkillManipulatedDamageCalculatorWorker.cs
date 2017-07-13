using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.DamageCalculator
{
    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.Utilities;

    public interface ISkillManipulatedDamageCalculatorWorker : IDisposable
    {
        /// <summary>The target.</summary>
        IAbilityUnit Target { get; set; }

        /// <summary>The skill.</summary>
        IAbilitySkill Skill { get; set; }

        float DamageValue { get; set; }
        Notifier DamageChanged { get; }
        void UpdateDamage(float rawDamage);
    }
}
