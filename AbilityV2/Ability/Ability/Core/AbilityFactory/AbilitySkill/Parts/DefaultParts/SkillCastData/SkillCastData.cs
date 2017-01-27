// <copyright file="SkillCastData.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillCastData
{
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.Cooldown;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Mana;
    using Ability.Core.AbilityFactory.Utilities;

    using Ensage.Common.Extensions;

    /// <summary>
    ///     The skill cast data.
    /// </summary>
    public class SkillCastData : ISkillCastData
    {
        #region Fields

        private LevelUpdater<IAbilitySkill, double> castPointUpdate;

        private LevelUpdater<IAbilitySkill, float> castRangeUpdate;

        private DataObserver<ICooldown> cooldownObserver;

        private DataObserver<IMana> manaObserver;

        private LevelUpdater<IAbilitySkill, float> speedUpdate;

        #endregion

        #region Constructors and Destructors

        public SkillCastData(IAbilitySkill skill)
        {
            this.Skill = skill;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the cast point.
        /// </summary>
        public double CastPoint => this.castPointUpdate?.Value ?? this.Skill.SourceAbility.FindCastPoint();

        /// <summary>
        ///     Gets the cast range.
        /// </summary>
        public float CastRange => this.castRangeUpdate?.Value ?? this.Skill.SourceAbility.GetCastRange();

        /// <summary>
        ///     Gets or sets the cooldown.
        /// </summary>
        public float Cooldown { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether enough mana.
        /// </summary>
        public bool EnoughMana { get; set; } = true;

        /// <summary>
        ///     Gets or sets a value indicating whether is on cooldown.
        /// </summary>
        public bool IsOnCooldown { get; set; }

        /// <summary>
        ///     Gets or sets the skill.
        /// </summary>
        public IAbilitySkill Skill { get; set; }

        /// <summary>
        ///     Gets the speed.
        /// </summary>
        public float Speed => this.speedUpdate?.Value ?? this.Skill.SourceAbility.GetProjectileSpeed();

        #endregion

        #region Public Methods and Operators

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            this.manaObserver.Dispose();
            this.cooldownObserver?.Dispose();
        }

        public virtual void Initialize()
        {
            this.castPointUpdate = new LevelUpdater<IAbilitySkill, double>(
                this.Skill,
                () => this.Skill.SourceAbility.FindCastPoint());
            this.speedUpdate = new LevelUpdater<IAbilitySkill, float>(
                this.Skill,
                () => this.Skill.SourceAbility.GetProjectileSpeed());
            this.castRangeUpdate = new LevelUpdater<IAbilitySkill, float>(
                this.Skill,
                () => this.Skill.SourceAbility.GetCastRange());

            this.manaObserver =
                new DataObserver<IMana>(
                    mana => { this.EnoughMana = mana.Current >= this.Skill.SourceAbility.ManaCost; });
            this.manaObserver.Subscribe(this.Skill.Owner.Mana);

            if (this.Skill.Cooldown != null)
            {
                this.cooldownObserver = new DataObserver<ICooldown>(
                    cooldown =>
                        {
                            this.Cooldown = cooldown.Current;
                            this.IsOnCooldown = this.Cooldown > 0;
                        });
                this.cooldownObserver.Subscribe(this.Skill.Cooldown);
            }
        }

        #endregion
    }
}