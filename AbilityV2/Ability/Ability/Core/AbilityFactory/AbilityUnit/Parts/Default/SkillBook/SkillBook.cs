// <copyright file="SkillBook.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.SkillBook
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ability.Core.AbilityFactory.AbilitySkill;

    using Ensage;
    using Ensage.Common.Extensions;

    /// <summary>
    ///     The skill book.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of ability skill
    /// </typeparam>
    public class SkillBook<T> : ISkillBook<T>
        where T : IAbilitySkill
    {
        #region Fields

        /// <summary>
        ///     The all skills.
        /// </summary>
        private Dictionary<double, T> allSkills = new Dictionary<double, T>();

        /// <summary>
        ///     The cast point spells.
        /// </summary>
        private Dictionary<double, T> castPointSpells = new Dictionary<double, T>();

        /// <summary>
        ///     The damage amps.
        /// </summary>
        private Dictionary<double, T> damageAmps = new Dictionary<double, T>();

        /// <summary>
        ///     The items.
        /// </summary>
        private Dictionary<double, T> items = new Dictionary<double, T>();

        /// <summary>
        ///     The skills.
        /// </summary>
        private Dictionary<double, T> skills = new Dictionary<double, T>();

        /// <summary>
        ///     The spells.
        /// </summary>
        private Dictionary<double, T> spells = new Dictionary<double, T>();

        #endregion

        #region Constructors and Destructors

        public SkillBook(IAbilityUnit unit)
        {
            this.Unit = unit;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the all skills.
        /// </summary>
        public IReadOnlyDictionary<double, T> AllSkills
        {
            get
            {
                return this.allSkills;
            }

            set
            {
                this.allSkills = value.ToDictionary(x => x.Key, x => x.Value);
            }
        }

        /// <summary>
        ///     Gets or sets the cast point spells.
        /// </summary>
        public IReadOnlyDictionary<double, T> CastPointSpells
        {
            get
            {
                return this.castPointSpells;
            }

            set
            {
                this.castPointSpells = value.ToDictionary(x => x.Key, x => x.Value);
            }
        }

        /// <summary>
        ///     Gets or sets the damage amps ordered.
        /// </summary>
        public IReadOnlyDictionary<double, T> DamageAmps
        {
            get
            {
                return this.damageAmps;
            }

            set
            {
                this.damageAmps = value.ToDictionary(x => x.Key, x => x.Value);
            }
        }

        /// <summary>
        ///     Gets or sets the damage amps ordered for cast.
        /// </summary>
        public IOrderedEnumerable<T> DamageAmpsOrderedForCast { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether has blink.
        /// </summary>
        public bool HasBlink { get; set; }

        /// <summary>
        ///     Gets or sets the items.
        /// </summary>
        public IReadOnlyDictionary<double, T> Items
        {
            get
            {
                return this.items;
            }

            set
            {
                this.items = value.ToDictionary(x => x.Key, x => x.Value);
            }
        }

        /// <summary>
        ///     Gets or sets the skill add.
        /// </summary>
        public SkillAdd SkillAdd { get; set; } = new SkillAdd();

        /// <summary>
        ///     Gets or sets the skill remove.
        /// </summary>
        public SkillRemove SkillRemove { get; set; } = new SkillRemove();

        /// <summary>
        ///     Gets or sets the skills.
        /// </summary>
        public IReadOnlyDictionary<double, T> Skills
        {
            get
            {
                return this.skills;
            }

            set
            {
                this.skills = value.ToDictionary(x => x.Key, x => x.Value);
            }
        }

        /// <summary>
        ///     Gets or sets the skills ordered for cast.
        /// </summary>
        public IOrderedEnumerable<T> SkillsOrderedForCast { get; set; }

        /// <summary>
        ///     Gets or sets the skills ordered for damage dealt.
        /// </summary>
        public IOrderedEnumerable<T> SkillsOrderedForDamageDealt { get; set; }

        /// <summary>
        ///     Gets or sets the spells.
        /// </summary>
        public IReadOnlyDictionary<double, T> Spells
        {
            get
            {
                return this.spells;
            }

            set
            {
                this.spells = value.ToDictionary(x => x.Key, x => x.Value);
            }
        }

        /// <summary>
        ///     Gets or sets the unit.
        /// </summary>
        public IAbilityUnit Unit { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The add skill.
        /// </summary>
        /// <param name="skill">
        ///     The skill.
        /// </param>
        public virtual void AddSkill(T skill)
        {
            if (skill.Json.IsDamageAmp)
            {
                var newDamageAmps = this.damageAmps.ToDictionary(x => x.Key, x => x.Value);
                newDamageAmps.Add(skill.SkillHandle, skill);
                this.damageAmps = newDamageAmps;
                this.DamageAmpsOrderedForCast =
                    this.damageAmps.Select(x => x.Value).OrderByDescending(x => x.Json.CastPriority);
            }
            else
            {
                var newSkills = this.skills.ToDictionary(x => x.Key, x => x.Value);
                newSkills.Add(skill.SkillHandle, skill);
                this.skills = newSkills;
                this.SkillsOrderedForCast = this.skills.Select(x => x.Value).OrderByDescending(x => x.Json.CastPriority);
                this.SkillsOrderedForDamageDealt =
                    this.skills.Select(x => x.Value).OrderByDescending(x => x.Json.DamageDealtPriority);
            }

            if (skill.IsItem)
            {
                var newitems = this.items.ToDictionary(x => x.Key, x => x.Value);
                newitems.Add(skill.SkillHandle, skill);
                this.items = newitems;
            }
            else
            {
                var newspells = this.spells.ToDictionary(x => x.Key, x => x.Value);
                newspells.Add(skill.SkillHandle, skill);
                this.spells = newspells;
            }

            if (skill.CastData.CastPoint > 0)
            {
                var newcastPointSpells = this.castPointSpells.ToDictionary(x => x.Key, x => x.Value);
                newcastPointSpells.Add(skill.SkillHandle, skill);
                this.castPointSpells = newcastPointSpells;
            }

            var newallSkills = this.allSkills.ToDictionary(x => x.Key, x => x.Value);
            newallSkills.Add(skill.SkillHandle, skill);
            this.allSkills = newallSkills;

            if (skill.Name == "item_blink")
            {
                this.HasBlink = true;
            }

            this.SkillAdd.Skill = skill;
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);

            this.allSkills.ForEach(x => x.Value.Dispose());
            this.SkillAdd = null;
            this.SkillRemove = null;
            this.allSkills.Clear();
            this.castPointSpells.Clear();
            this.damageAmps.Clear();
            this.items.Clear();
            this.skills.Clear();
            this.spells.Clear();
        }

        public virtual void Initialize()
        {
        }

        /// <summary>The is valid.</summary>
        /// <param name="ability">The ability.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public virtual bool IsValid(Ability ability)
        {
            var item = ability as Item;

            return (item == null || !item.IsRecipe) && !ability.Name.Contains("recipe")
                   && !ability.Name.Contains("special_bonus") && !ability.Name.Contains("empty")
                   && !ability.Name.Contains("hidden");
        }

        /// <summary>
        ///     The remove skill.
        /// </summary>
        /// <param name="skill">
        ///     The skill.
        /// </param>
        public virtual void RemoveSkill(T skill)
        {
            // Console.WriteLine("remove: " + skill.Name);
            if (skill.Json.IsDamageAmp)
            {
                var newDamageAmps = this.damageAmps.ToDictionary(x => x.Key, x => x.Value);
                newDamageAmps.Remove(skill.SkillHandle);
                this.damageAmps = newDamageAmps;
                this.DamageAmpsOrderedForCast =
                    this.damageAmps.Select(x => x.Value).OrderByDescending(x => x.Json.CastPriority);
            }
            else
            {
                var newSkills = this.skills.ToDictionary(x => x.Key, x => x.Value);
                newSkills.Remove(skill.SkillHandle);
                this.skills = newSkills;
                this.SkillsOrderedForCast = this.skills.Select(x => x.Value).OrderByDescending(x => x.Json.CastPriority);
                this.SkillsOrderedForDamageDealt =
                    this.skills.Select(x => x.Value).OrderByDescending(x => x.Json.DamageDealtPriority);
            }

            if (skill.IsItem)
            {
                var newitems = this.items.ToDictionary(x => x.Key, x => x.Value);
                newitems.Remove(skill.SkillHandle);
                this.items = newitems;
            }
            else
            {
                var newspells = this.spells.ToDictionary(x => x.Key, x => x.Value);
                newspells.Remove(skill.SkillHandle);
                this.spells = newspells;
            }

            if (skill.CastData.CastPoint > 0)
            {
                var newcastPointSpells = this.castPointSpells.ToDictionary(x => x.Key, x => x.Value);
                newcastPointSpells.Remove(skill.SkillHandle);
                this.castPointSpells = newcastPointSpells;
            }

            var newallSkills = this.allSkills.ToDictionary(x => x.Key, x => x.Value);
            newallSkills.Remove(skill.SkillHandle);
            this.allSkills = newallSkills;

            if (skill.Name == "item_blink")
            {
                this.HasBlink = false;
            }

            skill.Dispose();
            this.SkillRemove.Skill = skill;
        }

        #endregion
    }
}