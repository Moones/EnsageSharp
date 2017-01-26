using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.Invoker.PositionTracker
{
    using Ability.Core.AbilityData;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.PositionTracker;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.PositionTracker.Types;

    using Ensage;

    public class InvokerPositionTracker : EntityOwnerPositionTracker
    {
        public InvokerPositionTracker(IAbilityUnit unit)
            : base(unit)
        {
        }

        public override void ParticleIsFromHero(Entity sender, ParticleEffectAddedEventArgs args, ParticleEffectMoreInfo info)
        {
            if (info.StringContainingHeroName.Contains("forged_spirit_projectile"))
            {
                return;
            }

            base.ParticleIsFromHero(sender, args, info);
        }
    }
}
