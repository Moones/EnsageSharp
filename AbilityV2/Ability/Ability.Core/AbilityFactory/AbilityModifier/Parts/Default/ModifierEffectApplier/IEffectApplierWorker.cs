using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityModifier.Parts.Default.ModifierEffectApplier
{
    using Ability.Core.AbilityFactory.AbilityUnit;

    public interface IEffectApplierWorker
    {
        bool UpdateWithLevel { get; }
        Func<Action<IAbilityUnit>> ApplyEffectActionGetter { get; set; }
        Func<Action<IAbilityUnit>> RemoveEffectActionGetter { get; set; }
        Func<Action<IAbilityUnit>> UpdateEffectActionGetter { get; set; }
        bool EffectWasApplied { get; }

        void ApplyEffect(IAbilityUnit affectedUnit);

        void UpdateEffect();

        void RemoveEffect();
    }
}
