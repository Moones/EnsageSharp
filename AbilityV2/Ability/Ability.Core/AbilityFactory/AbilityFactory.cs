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

    using Ability.Core.AbilityFactory.AbilityModifier;
    using Ability.Core.AbilityFactory.AbilitySkill;
    using Ability.Core.AbilityFactory.AbilitySkill.Data;
    using Ability.Core.AbilityFactory.AbilitySkill.Metadata;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillComposer;
    using Ability.Core.AbilityFactory.AbilitySkill.Types;
    using Ability.Core.AbilityFactory.AbilityTalent;
    using Ability.Core.AbilityFactory.AbilityTeam;
    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.AbilityUnit.Metadata;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Composer;
    using Ability.Core.AbilityFactory.AbilityUnit.Types;
    using Ability.Core.AbilityFactory.AbilityUnit.Types.Meepo;
    using Ability.Core.AbilityFactory.Database;
    using Ability.Core.Utilities;

    using Ensage;
    using Ensage.Common.Extensions;
    using Ensage.Common.Menu;
    using Ensage.Common.Objects;
    using Ensage.Heroes;

    /// <summary>
    ///     The ability skill factory.
    /// </summary>
    [Export(typeof(IAbilityFactory))]
    internal class AbilityFactory : IAbilityFactory
    {
        #region Fields

        private IAbilitySkillComposer defaultSkillComposer;

        private IAbilityUnitHeroComposer unitComposer;

        #endregion

        #region Constructors and Destructors

        internal AbilityFactory()
        {
            // AbilityBootstrapper.ComposeParts(this);

            Logging.Write()(LogLevel.Info, "AbilityFactory Initialized");
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the ability priority database.
        /// </summary>
        [Import(typeof(IAbilityDatabase))]
        protected Lazy<IAbilityDatabase> AbilityDatabase { get; set; }

        [ImportMany]
        protected IEnumerable<Lazy<IAbilitySkillComposer, IAbilitySkillMetadata>> SkillComposers { get; set; }

        /// <summary>Gets or sets the skill item composers.</summary>
        [ImportMany]
        protected IEnumerable<Lazy<IAbilitySkillItemComposer, IAbilitySkillItemMetadata>> SkillItemComposers { get; set; }

        /// <summary>Gets or sets the unit composers.</summary>
        [ImportMany]
        protected IEnumerable<Lazy<IAbilityUnitHeroComposer, IAbilityUnitHeroMetadata>> UnitHeroComposers { get; set; }

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
                                       Json =
                                           this.AbilityDatabase.Value.GetSkillData(skill.StoredName())
                                           ?? new SkillJson(),
                                       Owner = owner
                                   };

            // abilitySkill.Json.CastPriority = this.AbilityDatabase.Value.GetCastPriority(
            // skill.StoredName(),
            // skill.Owner?.StoredName());
            // abilitySkill.Json.GlobalCastPriority = this.AbilityDatabase.Value.GetCastPriority(skill.StoredName());
            // abilitySkill.Json.DamageDealtPriority = this.AbilityDatabase.Value.GetDamageDealtPriority(
            // skill.StoredName());
            if (abilitySkill.IsItem)
            {
                var itemComposer =
                    this.SkillItemComposers.FirstOrDefault(
                        x =>
                            (!x.Metadata.Owner || x.Metadata.OwnerClassId.Equals(owner.SourceUnit.ClassId))
                            && x.Metadata.AbilityIds.Contains((uint)skill.Id));
                if (itemComposer?.Value != null)
                {
                    itemComposer.Value.Compose(abilitySkill);
                }
                else
                {
                    this.defaultSkillComposer.Compose(abilitySkill);
                }
            }
            else
            {
                var skillComposer =
                    this.SkillComposers.FirstOrDefault(
                        x =>
                            (!x.Metadata.Owner || x.Metadata.OwnerClassId.Equals(owner.SourceUnit.ClassId))
                            && x.Metadata.AbilityIds.Contains((uint)skill.Id));
                if (skillComposer?.Value != null)
                {
                    skillComposer.Value.Compose(abilitySkill);
                }
                else
                {
                    this.defaultSkillComposer.Compose(abilitySkill);
                }
            }

            return abilitySkill;
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
                                       Json =
                                           this.AbilityDatabase.Value.GetSkillData(skill.StoredName())
                                           ?? new SkillJson(),
                                       Owner = owner
                                   };

            // abilitySkill.Json.CastPriority = this.AbilityDatabase.Value.GetCastPriority(
            // skill.StoredName(),
            // skill.Owner?.StoredName());
            // abilitySkill.Json.GlobalCastPriority = this.AbilityDatabase.Value.GetCastPriority(skill.StoredName());
            // abilitySkill.Json.DamageDealtPriority = this.AbilityDatabase.Value.GetDamageDealtPriority(
            // skill.StoredName());

            if (abilitySkill.IsItem)
            {
                var itemComposer =
                    this.SkillItemComposers.FirstOrDefault(
                        x =>
                            (!x.Metadata.Owner || x.Metadata.OwnerClassId.Equals(owner.SourceUnit.ClassId))
                            && x.Metadata.AbilityIds.Contains((uint)skill.Id));
                if (itemComposer?.Value != null)
                {
                    itemComposer.Value.Compose(abilitySkill);
                }
                else
                {
                    this.defaultSkillComposer.Compose(abilitySkill);
                }
            }
            else
            {
                var skillComposer =
                    this.SkillComposers.FirstOrDefault(
                        x =>
                            (!x.Metadata.Owner || x.Metadata.OwnerClassId.Equals(owner.SourceUnit.ClassId))
                            && x.Metadata.AbilityIds.Contains((uint)skill.Id));
                if (skillComposer?.Value != null)
                {
                    skillComposer.Value.Compose(abilitySkill);
                }
                else
                {
                    this.defaultSkillComposer.Compose(abilitySkill);
                }
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
        public IAbilityUnit CreateNewUnit(Unit unit, IAbilityTeam team)
        {
            IAbilityUnit abilityUnit;
            if (unit is Hero)
            {
                if (unit.Handle.Equals(GlobalVariables.LocalHero.Handle))
                {
                    abilityUnit = new LocalHero(unit);
                }
                else if (unit is Meepo)
                {
                    abilityUnit = new AbilityUnitMeepo(unit as Meepo);
                }
                else
                {
                    abilityUnit = new AbilityHero(unit);
                }
            }
            else
            {
                abilityUnit = new AbilityCreep(unit);
            }

            team.UnitManager.AddUnit(abilityUnit);
            
            if (abilityUnit.IsHero)
            {
                var composer = this.UnitHeroComposers.FirstOrDefault(x => x.Metadata.HeroIds.Contains((uint)(abilityUnit.SourceUnit as Hero).HeroId));

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

            var unitcomposer = this.UnitComposers.FirstOrDefault(x => x.Metadata.UnitNames.Contains(abilityUnit.Name));

            if (unitcomposer != null)
            {
                unitcomposer.Value.Compose(abilityUnit);
            }
            else
            {
                this.unitComposer.Compose(abilityUnit);
            }

            return abilityUnit;
        }

        /// <summary>The create new modifier.</summary>
        /// <param name="modifier">The modifier.</param>
        /// <param name="affectedUnit">The affectedUnit.</param>
        /// <returns>The <see cref="IAbilityModifier"/>.</returns>
        public IAbilityModifier CreateNewModifier(Modifier modifier, IAbilityUnit affectedUnit)
        {
            var abilityModifier = new AbilityModifier.AbilityModifier(modifier) { AffectedUnit = affectedUnit };
            return abilityModifier;
        }

        /// <summary>The create new talent.</summary>
        /// <param name="ability">The ability.</param>
        /// <param name="owner">The owner.</param>
        /// <returns>The <see cref="IAbilityTalent"/>.</returns>
        public IAbilityTalent CreateNewTalent(Ability ability, IAbilityUnit owner)
        {
            var abilityTalent = new AbilityTalent.AbilityTalent(ability, owner);
            return abilityTalent;
        }

        public bool GenerateMenu { get; }

        public Menu GetMenu()
        {
            return null;
        }

        public void OnClose()
        {
        }

        public void OnLoad()
        {
            this.defaultSkillComposer =
                this.SkillComposers.FirstOrDefault(x => x.Metadata.OwnerClassId == ClassId.CBaseEntity).Value;

            this.unitComposer =
                this.UnitHeroComposers.FirstOrDefault(x => x.Metadata.HeroIds.Contains(uint.MaxValue)).Value;
        }

        #endregion
    }
}