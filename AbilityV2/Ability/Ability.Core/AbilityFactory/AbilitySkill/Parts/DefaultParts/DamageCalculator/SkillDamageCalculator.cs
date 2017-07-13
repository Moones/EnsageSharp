// <copyright file="SkillDamageCalculator.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.DamageCalculator
{
    using System;
    using System.Collections.Generic;

    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.DamageCalculator.Workers;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.DamageCalculator.Workers.Abstract;
    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.SkillBook;
    using Ability.Core.AbilityFactory.Utilities;

    using Ensage;
    using Ensage.Common.Extensions;

    internal class SkillDamageCalculator : ISkillDamageCalculator
    {
        #region Fields

        private readonly Dictionary<double, ISkillRawDamageCalculatorWorker> workers =
            new Dictionary<double, ISkillRawDamageCalculatorWorker>();

        private Func<IAbilityUnit, ISkillManipulatedDamageCalculatorWorker> workerAssign;

        private Func<IAbilityUnit, ISkillRawDamageCalculatorWorker> rawDamageWorkerAssign;

        #endregion

        #region Constructors and Destructors

        internal SkillDamageCalculator(IAbilitySkill skill)
        {
            this.Skill = skill;
            this.DamageType = skill.SourceAbility.DamageType;
            if (this.DamageType == DamageType.None)
            {
                this.DamageType = DamageType.Magical;
            }
        }

        #endregion

        #region Public Properties

        public DamageType DamageType { get; set; }

        public IAbilitySkill Skill { get; set; }

        public DataObserver<IAbilityUnit> UnitObserver { get; set; }

        public Func<IAbilityUnit, ISkillManipulatedDamageCalculatorWorker> DamageWorkerAssign
        {
            get
            {
                return this.workerAssign;
            }

            set
            {
                this.workerAssign = value;
                this.ReAssignWorkers(true);
            }
        }



        public IReadOnlyDictionary<double, ISkillRawDamageCalculatorWorker> DamageWorkers => this.workers;

        #endregion

        #region Public Methods and Operators

        public void Dispose()
        {
            this.UnitObserver.Dispose();
            this.workers.ForEach(x => x.Value.Dispose());
        }

        public float GetDamage(IAbilityUnit target)
        {
            return this.workers[target.UnitHandle].ManipulatedDamageWorker.DamageValue;
        }

        public ISkillRawDamageCalculatorWorker GetDamageWorker(IAbilityUnit target)
        {
            return this.workers[target.UnitHandle];
        }

        public float GetRawDamage(IAbilityUnit target)
        {
            return this.workers[target.UnitHandle].RawDamageValue;
        }

        public virtual void Initialize()
        {
            if (this.Skill.AbilityInfo.DamageString != null)
            {
                if (this.Skill.AbilityInfo.DamageScepterString != null)
                {
                    if (this.Skill.Owner.SourceUnit.HasModifier("modifier_item_ultimate_scepter_consumed"))
                    {
                        this.RawDamageWorkerAssign =
                            unit =>
                                new StringDataAghaDamageCalculatorWorker(
                                    this.Skill,
                                    unit,
                                    this.DamageWorkerAssign(unit));
                    }
                    else if (this.Skill.Owner.SourceUnit.HasModifier("modifier_item_ultimate_scepter"))
                    {
                        this.RawDamageWorkerAssign =
                            unit =>
                                new StringDataAghaDamageCalculatorWorker(
                                    this.Skill,
                                    unit,
                                    this.DamageWorkerAssign(unit));
                        var observer = new DataObserver<IAbilitySkill>();
                        observer.OnNextAction = remove =>
                            {
                                if (remove.IsItem
                                    && remove.SourceItem.Id == AbilityId.item_ultimate_scepter)
                                {
                                    this.RawDamageWorkerAssign =
                                        unit =>
                                            new StringDataDamageCalculatorWorker(
                                                this.Skill,
                                                unit,
                                                this.DamageWorkerAssign(unit));
                                    observer.Dispose();
                                }
                            };

                        observer.Subscribe(this.Skill.Owner.SkillBook.SkillRemoved);
                        this.Skill.DisposeNotifier.Subscribe(() => observer.Dispose());
                    }
                }
                else
                {
                    this.RawDamageWorkerAssign =
                        unit => new StringDataDamageCalculatorWorker(this.Skill, unit, this.DamageWorkerAssign(unit));
                }
            }
            else
            {
                this.RawDamageWorkerAssign =
                    unit => new GetDamageCalculatorWorker(this.Skill, unit, this.DamageWorkerAssign(unit));
            }

            if (this.DamageType == DamageType.Magical)
            {
                this.DamageWorkerAssign = unit => new MagicalSkillDamageCalculatorWorker(this.Skill, unit);
            }
            else if (this.DamageType == DamageType.Physical)
            {
                this.DamageWorkerAssign =
                    unit => new PhysicalSkillDamageCalculatorWorker(this.Skill, unit);
            }
            else if (this.DamageType == DamageType.Pure)
            {
                this.DamageWorkerAssign = unit => new PureSkillDamageCalculatorWorker(this.Skill, unit);
            }
            else
            {
                this.DamageWorkerAssign = unit => new HealthRemovalSkillDamageCalculatorWorker(this.Skill, unit);
            }
        }

        public Func<IAbilityUnit, ISkillRawDamageCalculatorWorker> RawDamageWorkerAssign
        {
            get
            {
                return this.rawDamageWorkerAssign;
            }

            set
            {
                this.rawDamageWorkerAssign = value;
                if (this.workerAssign != null)
                {
                    this.ReAssignWorkers(false, true);
                }
            }
        }

        private void ReAssignWorkers(bool manipulatedChanged = false, bool rawChanged = false)
        {
            if (this.UnitObserver != null)
            {
                this.UnitObserver.Dispose();
                this.UnitObserver = null;
            }

            this.UnitObserver =
                new DataObserver<IAbilityUnit>(unit => this.workers[unit.UnitHandle] = this.RawDamageWorkerAssign(unit));

            if (!rawChanged && manipulatedChanged)
            {
                foreach (var skillRawDamageCalculatorWorker in this.workers)
                {
                    skillRawDamageCalculatorWorker.Value.ManipulatedDamageWorker =
                        this.DamageWorkerAssign(skillRawDamageCalculatorWorker.Value.Target);
                }
            }

            foreach (var teamOtherTeam in this.Skill.Owner.Team.OtherTeams)
            {
                if (rawChanged)
                {
                    foreach (var unitManagerUnit in teamOtherTeam.UnitManager.Units)
                    {
                        this.UnitObserver.OnNext(unitManagerUnit.Value);
                    }
                }

                this.UnitObserver.Subscribe(teamOtherTeam.UnitManager.UnitAdded);
            }
        }

        #endregion
    }
}