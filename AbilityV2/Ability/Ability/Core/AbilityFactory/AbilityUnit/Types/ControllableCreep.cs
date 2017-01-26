// <copyright file="ControllableCreep.cs" company="EnsageSharp">
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

    class ControllableCreep : ControllableUnit
    {
        #region Fields

        private IUnitControl unitControl;

        #endregion

        #region Constructors and Destructors

        public ControllableCreep(Unit unit)
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

                this.UnitControl.AttackAction = new AbilityAction(this, AbilityActionType.Attack)
                                                    {
                                                        ExecutionDuration =
                                                            () => (float)(this.SourceUnit.AttackPoint() * 300),
                                                        ExecutionIntervalSleep = 100, TryExecute = () =>
                                                            {
                                                                this.UnitControl.Orbwalker.Attack(
                                                                    this.UnitControl.Target.SourceUnit,
                                                                    true);
                                                                return true;
                                                            },
                                                        ExecutionId = this.UnitHandle + 10,
                                                        ConfirmExecutionFunction =
                                                            () =>
                                                                this.UnitControl.Orbwalker.IsAttackOnCoolDown(
                                                                    this.UnitControl.Target?.SourceUnit)
                                                    };
                this.UnitControl.MoveAction = new AbilityAction(this, AbilityActionType.Move)
                                                  {
                                                      ExecutionDuration =
                                                          () => (float)(this.SourceUnit.AttackBackswing() * 300),
                                                      ExecutionIntervalSleep = 100, TryExecute = () =>
                                                          {
                                                              if (this.UnitControl.Target != null
                                                                  && Game.MousePosition.Distance(
                                                                      this.UnitControl.Target.Position.Current) < 200
                                                                  && (this.UnitControl.Target.SourceUnit.IsStunned()
                                                                      || this.UnitControl.Target.SourceUnit.IsRooted()
                                                                      || this.UnitControl.Target.SourceUnit.IsHexed()
                                                                      || this.UnitControl.Target.SourceUnit
                                                                          .MovementSpeed < 200
                                                                      || this.UnitControl.Target.SourceUnit
                                                                          .NetworkActivity != NetworkActivity.Move))
                                                              {
                                                                  this.SourceUnit.Attack(
                                                                      this.UnitControl.Target.SourceUnit);
                                                              }
                                                              else
                                                              {
                                                                  this.SourceUnit.Move(Game.MousePosition);
                                                              }

                                                              return true;
                                                          },
                                                      ExecutionId = this.UnitHandle + 20,
                                                      ConfirmExecutionFunction =
                                                          () => this.SourceUnit.NetworkActivity == NetworkActivity.Move
                                                  };
            }
        }

        #endregion
    }
}