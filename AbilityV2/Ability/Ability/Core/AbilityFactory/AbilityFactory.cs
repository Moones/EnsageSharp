// <copyright file="AbilityFactory.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Ability.Core.AbilityFactory.AbilitySkill;
    using Ability.Core.AbilityFactory.AbilitySkill.Data;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillComposer;
    using Ability.Core.AbilityFactory.AbilitySkill.Types;
    using Ability.Core.AbilityFactory.AbilityTeam;
    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Composer;
    using Ability.Core.AbilityFactory.AbilityUnit.Types;
    using Ability.Core.AbilityFactory.Metadata;
    using Ability.Utilities;

    using Ensage;
    using Ensage.Common.Extensions;
    using Ensage.Heroes;

    /// <summary>
    ///     The ability skill factory.
    /// </summary>
    [Export(typeof(IAbilityFactory))]
    public class AbilityFactory : IAbilityFactory
    {
        #region Fields

        private readonly IAbilitySkillComposer defaultSkillComposer;

        private readonly IAbilityUnitComposer unitComposer = new AbilityUnitComposer();

        #endregion

        #region Constructors and Destructors

        public AbilityFactory()
        {
            // AbilityBootstrapper.ComposeParts(this);
            this.defaultSkillComposer = new DefaultSkillComposer();

            Logging.Write()(LogLevel.Info, "AbilityFactory Initialized");
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the ability priority database.
        /// </summary>
        // [Import(typeof(IAbilityDatabase))]
        // protected Lazy<IAbilityDatabase> AbilityDatabase { get; set; }
        [ImportMany]
        protected IEnumerable<Lazy<IAbilitySkillComposer, IAbilitySkillMetadata>> SkillComposers { get; set; }

        /// <summary>Gets or sets the unit composers.</summary>
        [ImportMany]
        protected IEnumerable<Lazy<IAbilityUnitComposer, IAbilityUnitMetadata>> UnitComposers { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The compose new skill.
        /// </summary>
        /// <param name="skill">
        ///     The skill.
        /// </param>
        /// <returns>
        ///     The <see cref="IControllableSkill" />.
        /// </returns>
        public IControllableSkill CreateNewControllableSkill(Ability skill, IAbilityUnit owner)
        {
            var abilitySkill = new ControllableSkill(skill)
                                   {
                                       // Json =
                                       // this.AbilityDatabase.Value.GetSkillData(skill.StoredName())
                                       // ?? new SkillJson(),
                                       Json = new SkillJson(), Owner = owner
                                   };

            // abilitySkill.Json.CastPriority = this.AbilityDatabase.Value.GetCastPriority(
            // skill.StoredName(),
            // skill.Owner?.StoredName());
            // abilitySkill.Json.GlobalCastPriority = this.AbilityDatabase.Value.GetCastPriority(skill.StoredName());
            // abilitySkill.Json.DamageDealtPriority = this.AbilityDatabase.Value.GetDamageDealtPriority(
            // skill.StoredName());
            var composer =
                this.SkillComposers.FirstOrDefault(
                    x =>
                        (!x.Metadata.Owner || x.Metadata.OwnerClassId.Equals(owner.SourceUnit.ClassID))
                        && x.Metadata.AbilityIds.Contains((uint)skill.GetAbilityId()));
            if (composer != null)
            {
                composer.Value.Compose(abilitySkill);
            }
            else
            {
                this.defaultSkillComposer.Compose(abilitySkill);
            }

            return abilitySkill;
        }

        /// <summary>
        ///     The create new controllable unit.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <returns>
        ///     The <see cref="IControllableUnit" />.
        /// </returns>
        public IControllableUnit CreateNewControllableUnit(Unit unit, IAbilityTeam team)
        {
            IControllableUnit controllableUnit;
            if (unit is Hero)
            {
                if (unit.Handle.Equals(GlobalVariables.LocalHero.Handle))
                {
                    controllableUnit = new LocalHero(unit);
                }
                else if (unit is Meepo)
                {
                    controllableUnit = new ControllableMeepo(unit);
                }
                else
                {
                    controllableUnit = new ControllableHero(unit);
                }
            }
            else
            {
                controllableUnit = new ControllableCreep(unit);
            }

            team.UnitManager.AddUnit(controllableUnit);
            var composer =
                this.UnitComposers.FirstOrDefault(
                    x => x.Metadata.ClassIds.Contains(controllableUnit.SourceUnit.ClassID));
            if (composer != null)
            {
                composer.Value.Compose(controllableUnit);
            }
            else
            {
                this.unitComposer.Compose(controllableUnit);
            }

            return controllableUnit;
        }

        /// <summary>
        ///     The compose new skill.
        /// </summary>
        /// <param name="skill">
        ///     The skill.
        /// </param>
        /// <returns>
        ///     The <see cref="IAbilitySkill" />.
        /// </returns>
        public IUncontrollableSkill CreateNewSkill(Ability skill, IAbilityUnit owner)
        {
            var abilitySkill = new UncontrollableSkill(skill)
                                   {
                                       // Json =
                                       // this.AbilityDatabase.Value.GetSkillData(skill.StoredName())
                                       // ?? new SkillJson(),
                                       Json = new SkillJson(), Owner = owner
                                   };

            // abilitySkill.Json.CastPriority = this.AbilityDatabase.Value.GetCastPriority(
            // skill.StoredName(),
            // skill.Owner?.StoredName());
            // abilitySkill.Json.GlobalCastPriority = this.AbilityDatabase.Value.GetCastPriority(skill.StoredName());
            // abilitySkill.Json.DamageDealtPriority = this.AbilityDatabase.Value.GetDamageDealtPriority(
            // skill.StoredName());
            var composer =
                this.SkillComposers.FirstOrDefault(
                    x =>
                        (!x.Metadata.Owner || x.Metadata.OwnerClassId.Equals(owner.SourceUnit.ClassID))
                        && x.Metadata.AbilityIds.Contains((uint)skill.GetAbilityId()));
            if (composer != null)
            {
                composer.Value.Compose(abilitySkill);
            }
            else
            {
                this.defaultSkillComposer.Compose(abilitySkill);
            }

            return abilitySkill;
        }

        /// <summary>
        ///     The create new unit.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <returns>
        ///     The <see cref="IAbilityUnit" />.
        /// </returns>
        public IUncontrollableUnit CreateNewUnit(Unit unit, IAbilityTeam team)
        {
            IUncontrollableUnit abilityUnit;
            if (unit.Team == GlobalVariables.EnemyTeam)
            {
                abilityUnit = new Enemy(unit);
            }
            else
            {
                abilityUnit = new Ally(unit);
            }

            team.UnitManager.AddUnit(abilityUnit);
            var composer =
                this.UnitComposers.FirstOrDefault(x => x.Metadata.ClassIds.Contains(abilityUnit.SourceUnit.ClassID));
            if (composer != null)
            {
                composer.Value.Compose(abilityUnit);
            }
            else
            {
                this.unitComposer.Compose(abilityUnit);
            }

            return abilityUnit;
        }

        #endregion
    }
}