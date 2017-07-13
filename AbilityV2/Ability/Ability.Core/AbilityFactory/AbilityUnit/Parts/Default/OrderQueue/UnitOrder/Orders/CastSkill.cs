using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderQueue.UnitOrder.Orders
{
    using Ability.Core.AbilityFactory.AbilitySkill;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderQueue.UnitOrder.OrderPriority;

    public class CastSkill : UnitOrderBase
    {
        public CastSkill(OrderType orderType, IAbilitySkill skill, Func<float> execute)
            : base(orderType, skill.Owner)
        {
            this.ExecuteAction = execute;
            this.Skill = skill;
        }

        public Func<float> ExecuteAction { get; }

        public IAbilitySkill Skill { get; }

        public override bool CanExecute()
        {
            return !this.Skill.SourceAbility.IsInAbilityPhase && !this.Skill.CastData.IsOnCooldown
                   && this.Skill.CastData.EnoughMana;
        }

        public override float Execute()
        {
            return this.ExecuteAction();
        }

        public override void Enqueue()
        {
            this.Skill.CastData.Queued = true;
        }

        public override void Dequeue()
        {
            this.Skill.CastData.Queued = false;
        }
    }
}
