using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Fighter.HeroSpecific.Troll
{
    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Orbwalker;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Extensions;

    //[AbilityHeroModuleMetadata((uint)HeroId.npc_dota_hero_troll_warlord)]
    public class TrollOrbwalker : UnitOrbwalkerBase
    {
        public TrollOrbwalker(IAbilityUnit unit, uint key, string name = null, bool toggle = false, string description = null)
            : base(unit, key, name, toggle, description)
        {
        }

        public override void Dispose()
        {
        }

        public override void Initialize()
        {
        }

        public override bool BeforeAttack()
        {
            if (
                this.Target.Position.Predict(
                        (float)(Game.Ping + this.Unit.SourceUnit.GetTurnTime(this.Target.SourceUnit) * 1000f))
                    .Distance2D(this.Unit.Position.Current) <= this.Unit.SourceUnit.GetAttackRange())
            {
                Console.WriteLine("beforeattack");
                this.Unit.SourceUnit.Attack(this.Target.SourceUnit);
                return true;
            }
            else
            {
                Console.WriteLine("asd");
                this.MoveToMouse();
                return false;
            }
        }

        public override bool AfterAttack()
        {
            Console.WriteLine("after");
            this.MoveToMouse();
            return true;
        }

        public override bool NoTarget()
        {
            Console.WriteLine("NoTarget");
            this.Unit.Target = this.Unit.TargetSelector.GetTarget();
            this.MoveToMouse();
            return true;
        }

        public override bool CantAttack()
        {
            return false;
        }

        public override bool Meanwhile()
        {
            Console.WriteLine("meanwhile");
            this.Unit.Target = this.Unit.TargetSelector.GetTarget();
            this.MoveToMouse();
            return true;
        }

        public override bool Attack()
        {
            throw new NotImplementedException();
        }
    }
}
