using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.AttackAnimationTracker
{
    using Ensage;
    using Ensage.Common.Extensions;

    public class AttackAnimationTracker : IAttackAnimationTracker
    {
        public AttackAnimationTracker(IAbilityUnit unit)
        {
            this.Unit = unit;
        }

        public void AttackStarted()
        {
            var time = Game.RawGameTime * 1000 - 0.01f;
            this.CancelAnimationTime = (this.Unit.AttackAnimation.GetAttackPoint() * 1000f) + time;
            this.NextAttackTime = (this.Unit.AttackAnimation.GetAttackRate() * 1000f) + time;
        }

        public float NextAttackTime { get; set; }

        public float CancelAnimationTime { get; set; }

        public void Dispose()
        {
        }

        public IAbilityUnit Unit { get; set; }

        public void Initialize()
        {
        }
    }
}
