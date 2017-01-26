// <copyright file="ControllableHero.cs" company="EnsageSharp">
//    Copyright (c) 2017 Moones.
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see http://www.gnu.org/licenses/
// </copyright>
namespace Ability.Core.AbilityFactory.AbilityUnit.Types
{
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.UnitControl;
    using Ability.Core.ActionQueue.AbilityAction;

    using Ensage;
    using Ensage.Common.Extensions;

    /// <summary>
    ///     The unit hero.
    /// </summary>
    internal class ControllableHero : ControllableUnit
    {
        #region Fields

        private IUnitControl unitControl;

        #endregion

        #region Constructors and Destructors

        public ControllableHero(Unit unit)
            : base(unit)
        {
        }

        #endregion

        #region Public Properties

        public new IUnitControl UnitControl
        {
            get
            {
                return this.unitControl;
            }

            set
            {
                this.unitControl = value;
                this.unitControl.AttackAction = new AbilityAction(this, AbilityActionType.Attack)
                                                    {
                                                        ExecutionDuration =
                                                            () => (float)(this.SourceUnit.AttackPoint() * 300),
                                                        ExecutionIntervalSleep = 50, TryExecute = () =>
                                                            {
                                                                this.unitControl.Orbwalker.Attack(
                                                                    this.unitControl.Target.SourceUnit,
                                                                    true);
                                                                return true;
                                                            },
                                                        ExecutionId = this.UnitHandle ^ 3,
                                                        ConfirmExecutionFunction =
                                                            () =>
                                                                this.unitControl.Orbwalker.IsAttackOnCoolDown(
                                                                    this.unitControl.Target?.SourceUnit)
                                                    };

                // this.unitControl.MoveAction = new AbilityAction(this, AbilityActionType.Move)
                // {
                // ExecutionDuration = () => 0, ExecutionIntervalSleep = 250,
                // TryExecute = () =>
                // {
                // if (this.unitControl.Target != null
                // && VectorExtensions.Distance(
                // Game.MousePosition,
                // this.unitControl.Target.Position.Current) < 200
                // && (this.unitControl.Target.SourceUnit.IsStunned()
                // || this.unitControl.Target.SourceUnit.IsRooted()
                // || this.unitControl.Target.SourceUnit.IsHexed()
                // || this.unitControl.Target.SourceUnit
                // .MovementSpeed < 200
                // || this.unitControl.Target.SourceUnit
                // .NetworkActivity != NetworkActivity.Move))
                // {
                // this.SourceUnit.Attack(
                // this.unitControl.Target.SourceUnit);
                // }
                // else
                // {
                // var movePosition =
                // global::Ability.Utilities.Pathfinding
                // .GetValidMovePosition(
                // Game.MousePosition,
                // this,
                // this.Pathfinding);

                // // var obstaclesIDs =
                // // this.Pathfinding.GetIntersectingObstacleIDs(
                // // this.SourceUnit.BasePredict(Game.Ping + 150),
                // // this.SourceUnit.HullRadius + 150);
                // // Console.WriteLine(movePosition);
                // bool completed;

                // // if (obstaclesIDs.Any())
                // // {
                // // var path =
                // // this.Pathfinding.CalculatePathFromObstacle(
                // // unit.Position,
                // // movePosition,
                // // (float)
                // // (this.SourceUnit.RotationRad
                // // + Utils.DegreeToRadian(
                // // this.SourceUnit.RotationDifference)),
                // // this.SourceUnit.MovementSpeed,
                // // (float)this.SourceUnit.GetTurnRate(),
                // // 2,
                // // true,
                // // out completed).ToList();
                // // if (path.Any())
                // // {
                // // Console.WriteLine("moving away to: " + path.First());
                // // this.SourceUnit.Move(path.First());
                // // return true;
                // // }
                // // }
                // var maxDistance =
                // (movePosition - this.Position.Current).Length()
                // * 10;
                // this.Path =
                // this.Pathfinding.CalculateLongPath(
                // this.Position.Current,
                // movePosition,
                // maxDistance,
                // true,
                // out completed).ToList();

                // if (this.Path.Any())
                // {
                // var first = this.Path[0];
                // this.SourceUnit.Move(first);
                // for (var i = 1; i < this.Path.Count; ++i)
                // {
                // var pos = this.Path[i];
                // this.SourceUnit.Move(pos, true);
                // }
                // }
                // else
                // {
                // this.SourceUnit.Move(movePosition);
                // }
                // }

                // return true;
                // },
                // ExecutionId = this.UnitHandle ^ 2,
                // ConfirmExecutionFunction =
                // () => this.SourceUnit.NetworkActivity == NetworkActivity.Move
                // };
            }
        }

        #endregion
    }
}