using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Bodyblocker
{
    using Ability.Core.Utilities;

    using Ensage;
    using Ensage.Common.Extensions;
    using Ensage.Common.Extensions.SharpDX;

    using SharpDX;

    public class UnitBodyblocker : IUnitBodyblocker
    {
        public UnitBodyblocker(IAbilityUnit unit)
        {
            this.Unit = unit;
        }

        public UnitBodyblocker()
        {
        }

        public void Dispose()
        {
        }

        public IAbilityUnit Unit { get; set; }

        public float MaxTargetDistance { get; set; } = 2000;

        public void Initialize()
        {
        }

        public bool Enabled { get; set; }

        public uint Id { get; set; }

        private Sleeper issueSleeper = new Sleeper();

        public float IssueSleep { get; set; } = 120;

        public bool Issue()
        {
            if (!this.Enabled || this.issueSleeper.Sleeping)
            {
                return false;
            }

            this.issueSleeper.Sleep(this.IssueSleep);

            if (!this.Unit.TargetSelector.TargetIsSet || !this.Target.Visibility.Visible || !this.Target.SourceUnit.IsAlive
                || this.Target.Position.Current.Distance2D(this.Unit.Position.Current) > this.MaxTargetDistance)
            {
                //this.Unit.TargetSelector.GetTarget();
                return this.NoTarget();
            }

            return this.Bodyblock();
        }

        public bool PreciseIssue()
        {
            if (!this.Enabled || !this.issueSleeper.Sleeping)
            {
                return false;
            }

            if (!this.Unit.TargetSelector.TargetIsSet || !this.Target.Visibility.Visible || !this.Target.SourceUnit.IsAlive
                || this.Target.Position.Current.Distance2D(this.Unit.Position.Current) > this.MaxTargetDistance)
            {
                //this.Unit.TargetSelector.GetTarget();
                return false;
            }

            if ((!this.wasMoving && this.Target.SourceUnit.NetworkActivity == NetworkActivity.Move)
                || (Math.Abs(this.lastRad - this.Target.SourceUnit.RotationRad) > Math.PI / 5))
            {
                return this.Bodyblock();
            }

            return false;
        }

        public IAbilityUnit Target => this.Unit.TargetSelector.Target;

        private bool moveOrderWasSent;

        public virtual bool NoTarget()
        {
            return this.MoveToMouse();
        }

        public bool MoveToMouse()
        {
            return this.Unit.SourceUnit.Move(Game.MousePosition);
        }

        private ProjectionInfo projectionInfo;
        private ProjectionInfo projectionInfo2;

        private Vector3 infront;

        private Vector3 unitPosition;

        private Vector3 targetPosition;

        private bool isCloserToFront;

        private bool canBlock;

        private Vector3 direction;

        private Vector3 direction1;

        private Vector3 direction2;

        private Vector3 position;

        private Vector3 position2;

        private float distance;

        private float distance2;

        public bool PreciseBodyBlock()
        {
            this.unitPosition = this.Unit.Position.PredictedByLatency;
            this.targetPosition = this.Target.Position.PredictedByLatency;
            this.infront = this.Target.SourceUnit.InFront(500);
            //infront =
            //    this.Target.SourceUnit.InFront(
            //        (this.Unit.Position.Current.Distance2D(infront) / this.Unit.SourceUnit.MovementSpeed)
            //        * this.Target.SourceUnit.MovementSpeed);
            this.projectionInfo = Vector3Extensions.ProjectOn(
                this.targetPosition,
                this.unitPosition,
                this.infront);
            this.projectionInfo2 = Vector3Extensions.ProjectOn(
                this.unitPosition,
                this.targetPosition,
                this.infront);
            this.isCloserToFront = this.unitPosition.Distance2D(this.infront) + this.Unit.SourceUnit.HullRadius
                                  + this.Target.SourceUnit.HullRadius < this.targetPosition.Distance2D(this.infront);
            this.canBlock = (this.Unit.SourceUnit.NetworkActivity == NetworkActivity.Move || this.moveOrderWasSent)
                           && (this.projectionInfo2.IsOnSegment
                               || this.unitPosition.Distance2D(
                                   Vector2Extensions.ToVector3(this.projectionInfo2.SegmentPoint))
                               < this.Target.SourceUnit.HullRadius / 2)
                           && this.isCloserToFront;
            if (!this.canBlock && (this.projectionInfo.IsOnSegment
                || this.targetPosition.Distance2D(Vector2Extensions.ToVector3(this.projectionInfo.SegmentPoint))
                < this.Target.SourceUnit.HullRadius + this.Unit.SourceUnit.HullRadius + 20))
            {
                this.direction = (this.infront - this.targetPosition).Normalized();
                this.direction1 = (this.infront - this.targetPosition).Perpendicular().Normalized();
                this.direction2 = (this.targetPosition - this.infront).Perpendicular().Normalized();
                //Console.WriteLine(direction1 + " " + direction2);

                this.position = Pathfinding.ExtendUntilWall(
                    this.targetPosition,
                    this.direction1,
                    this.Target.SourceUnit.HullRadius + this.Unit.SourceUnit.HullRadius + 50,
                    this.Unit.Pathfinder);

                this.position2 = Pathfinding.ExtendUntilWall(
                    this.targetPosition,
                    this.direction2,
                    this.Target.SourceUnit.HullRadius + this.Unit.SourceUnit.HullRadius + 50,
                    this.Unit.Pathfinder);


                this.distance = this.unitPosition.Distance2D(this.position);
                this.distance2 = this.unitPosition.Distance2D(this.position2);

                var pos = this.position;
                var dist = this.distance;

                if (this.distance2 < this.distance)
                {
                    dist = this.distance2;
                    pos = this.position2;
                }

                if (!this.isCloserToFront)
                {
                    if (dist > 100)
                    {
                        this.infront = Pathfinding.ExtendUntilWall(
                            this.unitPosition,
                            (pos - this.unitPosition).Normalized(),
                            dist + 500,
                            this.Unit.Pathfinder);
                    }
                    else
                    {
                        this.infront = Pathfinding.ExtendUntilWall(this.unitPosition, this.direction, 300, this.Unit.Pathfinder);
                    }
                }
                //else
                //{
                //    infront = Pathfinding.ExtendUntilWall(unitPosition, direction, 300, this.Pathfinder);
                //}
            }
            else
            {
                if (!this.Target.SourceUnit.CanMove())// || this.Target.SourceUnit.NetworkActivity != NetworkActivity.Move)
                {
                    return false;
                }

                if (this.canBlock)
                {
                    this.Unit.SourceUnit.Stop();
                    this.moveOrderWasSent = false;
                    //this.Unit.SourceUnit.Move(infront);
                    return true;
                }
                else
                {
                    this.infront = this.Target.SourceUnit.InFront(250);
                }
            }

            this.Unit.SourceUnit.Move(this.infront);
            this.moveOrderWasSent = true;
            return true;
        }

        private double lastRad;

        private bool wasMoving;

        public bool Bodyblock()
        {
            var unitPosition = this.Unit.Position.PredictedByLatency;
            var targetPosition = this.Target.Position.PredictedByLatency;
            var infront = this.Target.SourceUnit.InFront(500);
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
                                  + this.Target.SourceUnit.HullRadius + 100 < targetPosition.Distance2D(infront);
            var distanceFromSegment2 = unitPosition.Distance2D(
                 Vector2Extensions.ToVector3(projectionInfo2.SegmentPoint));
            this.wasMoving = this.Unit.SourceUnit.NetworkActivity == NetworkActivity.Move;
            var canBlock = (this.wasMoving || this.moveOrderWasSent)
                           && (projectionInfo2.IsOnSegment
                               || distanceFromSegment2 < this.Target.SourceUnit.HullRadius / 2) && isCloserToFront;
            if (!canBlock && (projectionInfo.IsOnSegment
                || targetPosition.Distance2D(Vector2Extensions.ToVector3(projectionInfo.SegmentPoint))
                < this.Target.SourceUnit.HullRadius + this.Unit.SourceUnit.HullRadius + 20))
            {
                var direction = (infront - targetPosition).Normalized();
                var direction1 = (infront - targetPosition).Perpendicular().Normalized();
                var direction2 = (targetPosition - infront).Perpendicular().Normalized();
                //Console.WriteLine(direction1 + " " + direction2);

                var position = Pathfinding.ExtendUntilWall(
                    targetPosition,
                    direction1,
                    Math.Max(this.Target.SourceUnit.HullRadius + this.Unit.SourceUnit.HullRadius + 100, distanceFromSegment2),
                    this.Unit.Pathfinder);

                var position2 = Pathfinding.ExtendUntilWall(
                    targetPosition,
                    direction2,
                    Math.Max(this.Target.SourceUnit.HullRadius + this.Unit.SourceUnit.HullRadius + 100, distanceFromSegment2),
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
                    if (distanceFromSegment2 < this.Target.SourceUnit.HullRadius + this.Unit.SourceUnit.HullRadius + 50)
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
                if (!this.Target.SourceUnit.CanMove() || !this.wasMoving)
                {
                    this.moveOrderWasSent = false;
                    return false;
                }

                if (canBlock)
                {
                    this.Unit.SourceUnit.Stop();
                    //this.Unit.SourceUnit.Move(infront);
                    return true;
                }
                else
                {
                    infront = this.Target.SourceUnit.InFront(200);
                }
            }

            this.Unit.SourceUnit.Move(infront);
            this.moveOrderWasSent = true;
            return true;
        }
    }
}
