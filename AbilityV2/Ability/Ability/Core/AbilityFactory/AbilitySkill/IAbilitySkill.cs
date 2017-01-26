// <copyright file="IAbilitySkill.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilitySkill
{
    using System;
    using System.Collections.Generic;

    using Ability.Core.AbilityFactory.AbilitySkill.Data;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.Charges;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.Cooldown;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.DamageCalculator;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillAreaOfEffect;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillCastData;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillControl;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillDataReceiver;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillDrawer;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillInteraction;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillLevel;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillOverlay;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillUpdater;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.TargetFind;
    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.Utilities;

    using Ensage;

    using SharpDX;

    /// <summary>
    ///     The AbilitySkill interface.
    /// </summary>
    public interface IAbilitySkill : IDisposable
    {
        #region Public Properties

        /// <summary>Gets the parts.</summary>
        IReadOnlyDictionary<Type, IAbilitySkillPart> Parts { get; }

        /// <summary>
        ///     Gets or sets the ability cast.
        /// </summary>
        AbilityCast AbilityCast { get; set; }

        /// <summary>
        ///     Gets or sets the ability particle.
        /// </summary>
        AbilityParticle AbilityParticle { get; set; }

        /// <summary>
        ///     Gets or sets the cast start.
        /// </summary>
        AbilityPhase AbilityPhase { get; set; }

        /// <summary>
        ///     Gets or sets the ability projectile.
        /// </summary>
        AbilityProjectile AbilityProjectile { get; set; }

        /// <summary>
        ///     Gets or sets the skill interaction.
        /// </summary>
        ISkillAreaOfEffect AreaOfEffect { get; set; }

        /// <summary>
        ///     Gets or sets the cast data.
        /// </summary>
        ISkillCastData CastData { get; set; }

        /// <summary>
        ///     Gets or sets the charges.
        /// </summary>
        ICharges Charges { get; set; }

        /// <summary>
        ///     Gets or sets the cooldown.
        /// </summary>
        ICooldown Cooldown { get; set; }

        /// <summary>
        ///     Gets or sets the damage calculator.
        /// </summary>
        ISkillDamageCalculator DamageCalculator { get; set; }

        /// <summary>
        ///     Gets or sets the data receiver.
        /// </summary>
        ISkillDataReceiver DataReceiver { get; set; }

        /// <summary>
        ///     Gets the dispose notifier.
        /// </summary>
        Notifier DisposeNotifier { get; }

        /// <summary>
        ///     Gets or sets the drawer.
        /// </summary>
        ISkillDrawer Drawer { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether has charges.
        /// </summary>
        bool HasCharges { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether has cooldown.
        /// </summary>
        bool HasCooldown { get; set; }

        /// <summary>
        ///     Gets or sets the interaction.
        /// </summary>
        ISkillInteraction Interaction { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether is controllable.
        /// </summary>
        bool IsControllable { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether is item.
        /// </summary>
        bool IsItem { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether is temporary.
        /// </summary>
        bool IsTemporary { get; set; }

        /// <summary>
        ///     Gets or sets the skill data.
        /// </summary>
        SkillJson Json { get; set; }

        /// <summary>
        ///     Gets or sets the level.
        /// </summary>
        ISkillLevel Level { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        ///     Gets or sets the overlay provider.
        /// </summary>
        ISkillOverlayProvider OverlayProvider { get; set; }

        /// <summary>
        ///     Gets or sets the owner.
        /// </summary>
        IAbilityUnit Owner { get; set; }

        /// <summary>
        ///     Gets or sets the skill color.
        /// </summary>
        Color SkillColor { get; set; }

        /// <summary>
        ///     Gets or sets the skill control.
        /// </summary>
        ISkillControl SkillControl { get; set; }

        /// <summary>
        ///     Gets or sets the skill handle.
        /// </summary>
        uint SkillHandle { get; set; }

        /// <summary>
        ///     Gets or sets the skill handle string.
        /// </summary>
        string SkillHandleString { get; set; }

        /// <summary>
        ///     Gets or sets the cast range.
        /// </summary>
        ISkillTargetFind SkillTargetFind { get; set; }

        /// <summary>
        ///     Gets or sets the source.
        /// </summary>
        Unit Source { get; set; }

        /// <summary>
        ///     Gets or sets the ability.
        /// </summary>
        Ability SourceAbility { get; set; }

        /// <summary>
        ///     Gets or sets the source item.
        /// </summary>
        Item SourceItem { get; set; }

        /// <summary>
        ///     Gets or sets the texture.
        /// </summary>
        DotaTexture Texture { get; set; }

        /// <summary>
        ///     Gets or sets the unique name.
        /// </summary>
        string UniqueName { get; set; }

        /// <summary>
        ///     Gets or sets the updater.
        /// </summary>
        ISkillUpdater Updater { get; set; }

        /// <summary>
        ///     Gets or sets the variables.
        /// </summary>
        SkillDynamic Variables { get; set; }

        #endregion

        #region Public Methods and Operators
        
        /// <summary>The add part.</summary>
        /// <typeparam name="T">The type of part</typeparam>
        /// <param name="partFactory">The part Factory.</param>
        void AddPart<T>(Func<IAbilitySkill, T> partFactory) where T : IAbilitySkillPart;

        /// <summary>The get part.</summary>
        /// <typeparam name="T">The type of part</typeparam>
        /// <returns>The <see cref="T"/>.</returns>
        T GetPart<T>() where T : IAbilitySkillPart;

        /// <summary>
        ///     Checks if skill can be safely used on target and if target is in range
        /// </summary>
        /// <param name="target">
        ///     The target.
        /// </param>
        /// <param name="needToMoveCloser">
        ///     The need To Move Closer.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        bool CanCast(IAbilityUnit target, out bool needToMoveCloser);

        /// <summary>
        ///     The can cast.
        /// </summary>
        /// <param name="target">
        ///     The target.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        bool CanCast(IAbilityUnit target);

        /// <summary>
        ///     Checks if skill can be safely used
        /// </summary>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        bool CanCast();

        /// <summary>
        ///     The can hit.
        /// </summary>
        /// <param name="hero">
        ///     The hero.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        bool CanHit(Unit hero);

        /// <summary>
        ///     The damage dealt.
        /// </summary>
        /// <param name="hero">
        ///     The hero.
        /// </param>
        /// <returns>
        ///     The <see cref="float" />.
        /// </returns>
        float DamageDealt(Unit hero);

        /// <summary>
        ///     The hit delay.
        /// </summary>
        /// <param name="hero">
        ///     The hero.
        /// </param>
        /// <returns>
        ///     The <see cref="float" />.
        /// </returns>
        double HitDelay(Unit hero);

        /// <summary>
        ///     The set can hit.
        /// </summary>
        /// <param name="hero">
        ///     The hero.
        /// </param>
        /// <param name="canHit">
        ///     The can hit.
        /// </param>
        void SetCanHit(Unit hero, bool canHit);

        /// <summary>
        ///     The set damage dealt.
        /// </summary>
        /// <param name="hero">
        ///     The hero.
        /// </param>
        /// <param name="damage">
        ///     The damage.
        /// </param>
        void SetDamageDealt(Unit hero, float damage);

        /// <summary>
        ///     The set hit delay.
        /// </summary>
        /// <param name="hero">
        ///     The hero.
        /// </param>
        /// <param name="hitDelay">
        ///     The hit delay.
        /// </param>
        void SetHitDelay(Unit hero, double hitDelay);

        #endregion
    }
}