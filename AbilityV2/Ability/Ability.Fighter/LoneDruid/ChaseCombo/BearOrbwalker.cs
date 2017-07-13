namespace Ability.Fighter.LoneDruid.ChaseCombo
{
    using System;

    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Orbwalker;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Units.SpiritBear.SkillBook;

    using Ensage;
    using Ensage.Common.Extensions;

    public class BearOrbwalker : UnitOrbwalkerBase
    {
        private IAbilityUnit unit1;

        public BearOrbwalker()
        {
            this.IssueSleep = 200;
        }


        public override IAbilityUnit Unit
        {
            get
            {
                return this.unit1;
            }

            set
            {
                this.unit1 = value;
                this.Bodyblocker.Unit = this.unit1;
                this.SkillBook = this.unit1.SkillBook as SpiritBearSkillBook;
            }
        }

        public IAbilityUnit LocalHero { get; set; }

        public SpiritBearSkillBook SkillBook { get; set; }

        public override void Initialize()
        {
        }

        public override void MoveBeforeAttack()
        {
            if (this.Unit.TargetSelector.LastDistanceToTarget - 700 > this.LocalHero.TargetSelector.LastDistanceToTarget
                && this.LocalHero.TargetSelector.LastDistanceToTarget
                < this.Unit.Position.PredictedByLatency.Distance2D(this.LocalHero.Position.PredictedByLatency))
            {
                Console.WriteLine(("asd"));
                if (this.SkillBook.Return.CanCast())
                {
                    this.SkillBook.Return.CastFunction.Cast();
                    return;
                }
            }

            if (!this.RunAround(this.LocalHero, this.Target))
            {
                this.Attack();
            }
        }

        public override bool IsTargetValid()
        {
            return this.Unit.TargetSelector.TargetIsSet && this.Target.SourceUnit.IsAlive
                               && this.LocalHero.TargetSelector.LastDistanceToTarget < this.MaxTargetDistance;
        }

        public override bool NoTarget()
        {
            //if (this.Unit.TargetSelector.TargetIsSet
            //    && this.Unit.TargetSelector.LastDistanceToTarget - 700
            //    > this.LocalHero.TargetSelector.LastDistanceToTarget
            //    && this.LocalHero.TargetSelector.LastDistanceToTarget
            //    < this.Unit.Position.PredictedByLatency.Distance2D(this.LocalHero.Position.PredictedByLatency))
            //{
            //    if (this.SkillBook.Return.CanCast())
            //    {
            //        this.SkillBook.Return.CastFunction.Cast();
            //        return true;
            //    }
            //}

            if (!this.RunAround(this.LocalHero, Game.MousePosition))
            {
                this.Unit.SourceUnit.Move(Game.MousePosition);
            }

            return true;
        }

        public override bool Move()
        {
            if (!this.RunAround(this.LocalHero, this.Target))
            {
                this.Bodyblock();
            }

            return true;
        }

        public override bool Attack()
        {
            if (this.Unit.Position.PredictedByLatency.Distance2D(this.LocalHero.Position.PredictedByLatency) > 1100)
            {
                return this.Bodyblocker.Bodyblock();
            }

            return base.Attack();
        }
    }
}
