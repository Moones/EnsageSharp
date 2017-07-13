using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.DamageCalculator
{
    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.Utilities;

    public interface ISkillRawDamageCalculatorWorker : IDisposable
    {
        ISkillManipulatedDamageCalculatorWorker ManipulatedDamageWorker { get; set; }

        IAbilityUnit Target { get; set; }
        
        IAbilitySkill Skill { get; set; }

        float RawDamageValue { get; set; }
        Notifier DamageChanged { get; }
        void UpdateRawDamage();
    }
}
