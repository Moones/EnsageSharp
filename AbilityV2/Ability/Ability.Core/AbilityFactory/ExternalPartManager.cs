using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory
{
    using System.ComponentModel.Composition;

    using Ability.Core.AbilityFactory.AbilitySkill;
    using Ability.Core.AbilityFactory.AbilitySkill.Metadata;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillComposer;
    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.AbilityUnit.Metadata;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Composer;
    using Ability.Core.AbilityManager;

    using Ensage;
    using Ensage.Common.Enums;

    [Export(typeof(IExternalPartManager))]
    internal class ExternalPartManager : IExternalPartManager
    {

        [Import(typeof(IAbilityManager))]
        protected Lazy<IAbilityManager> AbilityManager { get; set; }


        [ImportMany]
        protected IEnumerable<Lazy<IAbilitySkillComposer, IAbilitySkillMetadata>> SkillComposers { get; set; }

        /// <summary>Gets or sets the skill item composers.</summary>
        [ImportMany]
        protected IEnumerable<Lazy<IAbilitySkillItemComposer, IAbilitySkillItemMetadata>> SkillItemComposers { get; set; }

        /// <summary>Gets or sets the unit composers.</summary>
        [ImportMany]
        protected IEnumerable<Lazy<IAbilityUnitHeroComposer, IAbilityUnitHeroMetadata>> UnitComposers { get; set; }


        public void AddUnitPart<T>(HeroId unitClassId, Func<IAbilityUnit, T> factory) where T : IAbilityUnitPart
        {
            foreach (var keyValuePair in this.AbilityManager.Value.Units)
            {
                var unit = keyValuePair.Value;
                if ((unit.SourceUnit as Hero).HeroId == unitClassId)
                {
                    unit.AddPart(factory);
                }
            }

            foreach (var unitComposer in this.UnitComposers)
            {
                if (unitComposer.Metadata.HeroIds.Contains((uint)unitClassId))
                {
                    unitComposer.Value.AssignPart<T>(factory);
                }
            }
        }

        public void AddUnitPart<T>(Func<IAbilityUnit, T> factory) where T : IAbilityUnitPart
        {
            foreach (var keyValuePair in this.AbilityManager.Value.Units)
            {
                keyValuePair.Value.AddPart(factory);
            }

            foreach (var unitComposer in this.UnitComposers)
            {
                if (unitComposer.Metadata.HeroIds.Contains(uint.MaxValue))
                {
                    unitComposer.Value.AssignPart<T>(factory);
                }
            }
        }

        public void AddSkillPart<T>(Func<IAbilitySkill, T> factory) where T : IAbilitySkillPart
        {
            foreach (var keyValuePair in this.AbilityManager.Value.Units)
            {
                foreach (var skillBookAllSkill in keyValuePair.Value.SkillBook.AllSkills)
                {
                    skillBookAllSkill.Value.AddPart(factory);
                }
            }

            foreach (var skillComposer in this.SkillComposers)
            {
                if (skillComposer.Metadata.OwnerClassId == ClassId.CBaseEntity)
                {
                    skillComposer.Value.AssignPart(factory);
                }
            }
        }

        public void AddSkillPart<T>(Func<IAbilitySkill, T> factory, params uint[] abilityIds) where T : IAbilitySkillPart
        {
            foreach (var keyValuePair in this.AbilityManager.Value.Units)
            {
                foreach (var skillBookAllSkill in keyValuePair.Value.SkillBook.Spells)
                {
                    if (abilityIds.Contains((uint)skillBookAllSkill.Value.SourceAbility.Id))
                    {
                        skillBookAllSkill.Value.AddPart(factory);
                    }
                }
            }

            foreach (var skillComposer in this.SkillComposers)
            {
                foreach (var abilityId in abilityIds)
                {
                    if (skillComposer.Metadata.AbilityIds.Contains((uint)abilityId))
                    {
                        skillComposer.Value.AssignPart(factory);
                    }
                }
            }
        }

        public void AddSkillItemPart<T>(Func<IAbilitySkill, T> factory, params uint[] abilityIds) where T : IAbilitySkillPart
        {
            foreach (var keyValuePair in this.AbilityManager.Value.Units)
            {
                foreach (var skillBookAllSkill in keyValuePair.Value.SkillBook.Items)
                {
                    if (abilityIds.Contains((uint)skillBookAllSkill.Value.SourceItem.Id))
                    {
                        skillBookAllSkill.Value.AddPart(factory);
                    }
                }
            }

            foreach (var skillComposer in this.SkillItemComposers)
            {
                foreach (var abilityId in abilityIds)
                {
                    if (skillComposer.Metadata.AbilityIds.Contains((uint)abilityId))
                    {
                        skillComposer.Value.AssignPart(factory);
                    }
                }
            }
        }
    }
}
