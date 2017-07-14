using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Orbwalker
{
    using System.ComponentModel.Composition;
    using System.Runtime.CompilerServices;

    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Bodyblocker;
    using Ability.Core.AbilityFactory.Utilities;
    using Ability.Core.MenuManager.Menus.AbilityMenu.Items;
    using Ability.Core.Utilities;

    using Ensage;
    using Ensage.Common.Extensions;
    using Ensage.Common.Extensions.SharpDX;
    using Ensage.Common.Menu;

    using SharpDX;

    //[InheritedExport(typeof(IUnitOrbwalker))]
    public abstract class UnitOrbwalkerBase : IUnitOrbwalker
    {
        protected UnitOrbwalkerBase(IAbilityUnit unit)
        {

            this.targetReset = new Reacter(
                () =>
                    {
                        this.TargetValid = this.IsTargetValid();
                        this.MeanWhile = false;
                        this.MoveToAttack = false;
                    });

            this.Bodyblocker = new UnitBodyblocker(unit);
            this.Unit = unit;
        }

        protected UnitOrbwalkerBase()
        {
            this.Bodyblocker = new UnitBodyblocker();
            this.targetReset = new Reacter(
                () =>
                {
                    this.TargetValid = this.IsTargetValid();
                    this.MeanWhile = false;
                    this.MoveToAttack = false;
                });
        }

        public UnitBodyblocker Bodyblocker { get; }

        public float MaxTargetDistance { get; set; } = 2000;

        public virtual void Dispose()
        {
        }

        private Reacter targetReset;

        public virtual IAbilityUnit Unit
        {
            get
            {
                return this.unit;
            }

            set
            {
                this.unit = value;
                this.Bodyblocker.Unit = this.unit;
                this.targetReset.Dispose();
                this.targetReset.Subscribe(this.unit.TargetSelector.TargetChanged);
                //this.unit.AddOrderIssuer(this);
            }
        }

        public abstract void Initialize();

        private bool afterAttackExecuted;

        private bool beforeAttackExecuted;

        private IAbilityUnit unit;

        private IAbilityUnit target;

        public uint Id { get; set; }

        public float Time { get; set; }

        public double NextAttack { get; set; }

        public bool TargetValid { get; set; }

        public bool MeanWhile { get; set; }

        public bool Attacking { get; set; }

        public bool MoveToAttack { get; set; }

        public virtual bool CastSpells()
        {
            return false;
        }

        public virtual bool IsTargetValid()
        {
            return this.Unit.TargetSelector.TargetIsSet && this.Target.SourceUnit.IsAlive
                               && this.Unit.TargetSelector.LastDistanceToTarget < this.MaxTargetDistance;
        }

        public virtual bool PreciseIssue()
        {
            if (!this.Enabled)
            {
                return false;
            }

            this.TargetValid = this.IsTargetValid();

            if (!this.TargetValid)
            {
                return false;
            }

            this.Attacking = this.Unit.SourceUnit.IsAttacking();

            if (!this.Attacking && this.CastSpells())
            {
                return true;
            }

            this.Time = (Game.RawGameTime) * 1000 + Game.Ping;
            this.NextAttack = this.Time - this.Unit.AttackAnimationTracker.NextAttackTime + (this.Unit.TurnRate.GetTurnTime(this.Target) * 1000);
            //Console.WriteLine(time + " " + Game.Ping + " " + this.NextAttackTime + " " + (this.Unit.SourceUnit.GetTurnTime(this.Target.SourceUnit) * 1000));

            if (this.NextAttack < 0)
            {
                this.MoveToAttack = false;
                this.beforeAttackExecuted = false;
                if (this.Time >= this.Unit.AttackAnimationTracker.CancelAnimationTime)
                {
                    if (this.afterAttackExecuted)
                    {
                        this.MeanWhile = true;
                        //this.Unit.Target = this.Unit.TargetSelector.GetTarget();
                        return false;
                    }
                    
                    this.afterAttackExecuted = true;
                    return this.CastSpells() || this.AfterAttack();
                }
            }
            else if (!this.Attacking)
            {
                //Console.WriteLine(
                //    this.Unit.TurnRate.GetTurnTime(this.Target) + " " + this.Unit.AttackAnimation.GetAttackRate() + " "
                //    + this.Unit.SourceUnit.AttackRate());
                this.afterAttackExecuted = false;
                if (this.beforeAttackExecuted)
                {
                    return false;
                }

                this.MeanWhile = false;
                if (this.BeforeAttack())
                {
                    this.MoveToAttack = false;
                    this.beforeAttackExecuted = true;
                    return true;
                }

                this.MoveToAttack = true;
                return false;
            }
            
            this.MeanWhile = false;
            return false;
        }

        private Sleeper issueSleeper = new Sleeper();

        public float IssueSleep { get; set; } = 120;

        public virtual bool Issue()
        {
            if (!this.Enabled || this.issueSleeper.Sleeping)
            {
                return false;
            }

            if (this.IssueMeanwhileActions())
            {
                this.issueSleeper.Sleep(this.IssueSleep);
                return true;
            }

            return false;
        }

        public virtual bool IssueMeanwhileActions()
        {
            if (!this.TargetValid)
            {
                return this.NoTarget();
            }

            if (this.beforeAttackExecuted && !this.Attacking)
            {
                return this.Attack();
            }

            if (this.MoveToAttack)
            {
                this.MoveBeforeAttack();
                return true;
            }

            return this.MeanWhile && this.Meanwhile();
        }

        public IAbilityUnit Target => this.Unit.TargetSelector.Target;

        public bool Enabled { get; set; }

        public virtual bool BeforeAttack()
        {
            if (this.Unit.AttackRange.IsInAttackRange(this.Target))
            {
                return this.Attack();
            }

            return false;
        }

        public bool RunAround(IAbilityUnit unit, IAbilityUnit targetUnit)
        {
            var targetPosition = unit.Position.PredictedByLatency;

            if (targetUnit.Position.Current.Distance2D(targetPosition) < 200)
            {
                return false;
            }

            return this.RunAround(unit, targetUnit.Position.PredictedByLatency);
        }

        public bool RunAround(IAbilityUnit unit, Vector3 target)
        {
            var unitPosition = this.Unit.Position.PredictedByLatency;
            var targetPosition = unit.Position.PredictedByLatency;

            if (target.Distance2D(targetPosition) < 200)
            {
                this.Unit.SourceUnit.Move(unit.SourceUnit.InFront(250));
                return true;
            }

            if (this.Unit.Position.PredictedByLatency.Distance2D(target) < unit.Position.PredictedByLatency.Distance2D(target)
                || this.Unit.Position.PredictedByLatency.Distance2D(unit.Position.PredictedByLatency) > 250)
            {
                return false;
            }

            var infront = unit.SourceUnit.InFront(500);
            //infront =
            //    this.Target.SourceUnit.InFront(
            //        (this.Unit.Position.Current.Distance2D(infront) / this.Unit.SourceUnit.MovementSpeed)
            //        * this.Target.SourceUnit.MovementSpeed);
            var backWardsdirection = (targetPosition - infront).Normalized();
            var projectionInfo = Vector3Extensions.ProjectOn(
                targetPosition,
                unitPosition,
                infront);
            var projectionInfo2 = Vector3Extensions.ProjectOn(
                unitPosition,
                targetPosition + (backWardsdirection * (unitPosition.Distance2D(targetPosition) + 200)),
                infront);
            var isCloserToFront = unitPosition.Distance2D(infront) + this.Unit.SourceUnit.HullRadius
                                  + unit.SourceUnit.HullRadius + 100 < targetPosition.Distance2D(infront);
            var distanceFromSegment2 = unitPosition.Distance2D(
                Vector2Extensions.ToVector3(projectionInfo2.SegmentPoint));
            var canBlock = (projectionInfo2.IsOnSegment || distanceFromSegment2 < unit.SourceUnit.HullRadius / 2)
                           && isCloserToFront;
            if (!canBlock && (projectionInfo.IsOnSegment
                || targetPosition.Distance2D(Vector2Extensions.ToVector3(projectionInfo.SegmentPoint))
                < unit.SourceUnit.HullRadius + this.Unit.SourceUnit.HullRadius + 20))
            {
                var direction = (infront - targetPosition).Normalized();
                var direction1 = (infront - targetPosition).Perpendicular().Normalized();
                var direction2 = (targetPosition - infront).Perpendicular().Normalized();
                //Console.WriteLine(direction1 + " " + direction2);

                var position = Pathfinding.ExtendUntilWall(
                    targetPosition,
                    direction1,
                    Math.Max(unit.SourceUnit.HullRadius + this.Unit.SourceUnit.HullRadius + 100, distanceFromSegment2),
                    this.Unit.Pathfinder);

                var position2 = Pathfinding.ExtendUntilWall(
                    targetPosition,
                    direction2,
                    Math.Max(unit.SourceUnit.HullRadius + this.Unit.SourceUnit.HullRadius + 100, distanceFromSegment2),
                    this.Unit.Pathfinder);


                var distance = unitPosition.Distance2D(position);
                var distance2 = unitPosition.Distance2D(position2);
                if (distance2 < distance)
                {
                    distance = distance2;
                    position = position2;
                }

                if (!isCloserToFront)
                {
                    if (distanceFromSegment2 < unit.SourceUnit.HullRadius + this.Unit.SourceUnit.HullRadius + 50)
                    {
                        infront = Pathfinding.ExtendUntilWall(position, direction, distance + 500, this.Unit.Pathfinder);
                    }
                    else
                    {
                        infront = Pathfinding.ExtendUntilWall(unitPosition, direction, 500, this.Unit.Pathfinder);
                    }
                }
                //else
                //{
                //    infront = Pathfinding.ExtendUntilWall(unitPosition, direction, 300, this.Pathfinder);
                //}
            }
            else
            {
                return false;
            }

            this.Unit.SourceUnit.Move(infront);
            return true;
        }

        public virtual void MoveBeforeAttack()
        {
            this.Move();
        }

        public virtual bool AfterAttack()
        {
            return this.Move();
        }

        public virtual bool Attack()
        {
            return this.Unit.SourceUnit.Attack(this.Target.SourceUnit);
        }

        public virtual bool Move()
        {
            return this.MoveToMouse();
        }

        public virtual bool Meanwhile()
        {
            return this.Move();
        }

        public virtual bool NoTarget()
        {
            return this.MoveToMouse();
        }

        public virtual bool CantAttack()
        {
            return this.MoveToMouse();
        }

        public bool MoveToMouse()
        {
            return this.Unit.SourceUnit.Move(Game.MousePosition);
        }

        public bool Bodyblock()
        {
            return this.Bodyblocker.Bodyblock() || this.Attack();
        }

        public bool KeepRange()
        {
            var difference = this.Unit.AttackRange.Value - this.Unit.TargetSelector.LastDistanceToTarget
                             + this.Unit.SourceUnit.HullRadius + this.Target.SourceUnit.HullRadius;
            var mousDistance = Game.MousePosition.Distance2D(this.Target.Position.Current);
            if (difference > 0)
            {
                //var targetDistance = this.Unit.TargetSelector.LastDistanceToTarget;
                if (mousDistance + 350
                    < this.Unit.AttackRange.Value + this.Unit.SourceUnit.HullRadius + this.Target.SourceUnit.HullRadius)
                {
                    if (this.Target.SourceUnit.NetworkActivity == NetworkActivity.Move)
                    {
                        if (this.Target.Position.PredictedByLatency.Distance2D(this.Unit.Position.Current)
                            > this.Target.Position.Current.Distance2D(this.Unit.Position.Current))
                        {
                            return this.Move();
                        }
                        else
                        {
                            return
                                this.Unit.SourceUnit.Move(
                                    this.Target.Position.PredictedByLatency.Extend(
                                        Game.MousePosition,
                                        Math.Max(this.Unit.AttackRange.Value * 0.8f, mousDistance)));
                        }
                    }

                    return this.Attack();
                }
                else
                {
                    return this.Unit.SourceUnit.Move(Game.MousePosition);
                }
            }

            return this.Unit.SourceUnit.Move(Game.MousePosition);
        }
    }
}
