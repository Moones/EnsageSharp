// <copyright file="AbilitySkill.cs" company="EnsageSharp">
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
    using System.Globalization;
    using System.Linq;

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
    using Ensage.Common.Extensions;
    using Ensage.Common.Objects;

    using SharpDX;

    /// <summary>
    ///     The ability skill.
    /// </summary>
    public abstract class AbilitySkill : IAbilitySkill
    {
        #region Fields

        /// <summary>The parts.</summary>
        private readonly Dictionary<Type, IAbilitySkillPart> parts = new Dictionary<Type, IAbilitySkillPart>();

        /// <summary>The charges.</summary>
        private ICharges charges;

        /// <summary>The cooldown.</summary>
        private ICooldown cooldown;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AbilitySkill" /> class.
        /// </summary>
        /// <param name="sourceAbility">
        ///     The skill.
        /// </param>
        /// <param name="castingFunction">
        ///     The casting Function.
        /// </param>
        protected AbilitySkill(Ability sourceAbility)
        {
            if (sourceAbility == null)
            {
                throw new ArgumentException("Skill passed to AbilitySkill ctor was null");
            }

            this.Source = (Unit)sourceAbility.Owner;
            this.SourceAbility = sourceAbility;
            this.Name = sourceAbility.StoredName();

            // this.DamageCalculator = new DefaultSkillDamageCalculator(this);
            this.SourceItem = sourceAbility as Item;

            this.IsItem = this.SourceItem != null;
            this.SkillHandle = this.SourceAbility.Handle;
            this.SkillHandleString = this.SkillHandle.ToString(CultureInfo.CurrentCulture);

            // this.Variables.DealsDamageToTarget = this.SourceAbility.IsNuke();
            // this.Variables.AppliesDisableOnTarget = this.SourceAbility.IsDisable();
            // this.Variables.AppliesSlowOnTarget = this.SourceAbility.IsSlow();

            // if (this.Name == "item_soul_ring")
            // {
            // this.Variables.RefillsMana = true;
            // this.Variables.BonusMana = sourceAbility.GetAbilityData("mana_gain");
            // }
            if (this.Name.Contains("item_"))
            {
                this.Texture = Textures.GetItemTexture(this.Name);

                // this.IconSize = new Vector2(HUDInfo.GetHPBarSizeX() / 6, (float)(HUDInfo.GetHPBarSizeX() / 6 / 1.24));
            }
            else
            {
                this.Texture = Textures.GetSpellTexture(this.Name);

                // this.IconSize = new Vector2(HUDInfo.GetHPBarSizeX() / 6, HUDInfo.GetHPBarSizeX() / 6 + 1);
            }

            // this.AbilityPhase = new AbilityPhase(this);
            // this.AbilityParticle = new AbilityParticle(this);

            // DelayAction.Add(200,
            // () =>
            // {
            // if (this.Json.ParticleNames == null)
            // {
            // return;
            // }

            // Entity.OnParticleEffectAdded += this.Entity_OnParticleEffectAdded;
            // });

            // if (this.DealsDamageToTarget)
            // {
            // foreach (var hero in Heroes.All)
            // {
            // this.DamageDealtDictionary[hero.Handle] = 0;
            // this.CanUseOnDictionary[hero.Handle] = false;
            // }
            // }
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the ability cast.
        /// </summary>
        public AbilityCast AbilityCast { get; set; }

        /// <summary>Gets or sets the ability particle.</summary>
        public AbilityParticle AbilityParticle { get; set; }

        /// <summary>
        ///     Gets or sets the cast start.
        /// </summary>
        public AbilityPhase AbilityPhase { get; set; }

        /// <summary>
        ///     Gets or sets the ability projectile.
        /// </summary>
        public AbilityProjectile AbilityProjectile { get; set; }

        /// <summary>
        ///     Gets or sets the skill interaction.
        /// </summary>
        public ISkillAreaOfEffect AreaOfEffect { get; set; }

        /// <summary>
        ///     Gets or sets the cast data.
        /// </summary>
        public ISkillCastData CastData { get; set; }

        /// <summary>
        ///     Gets or sets the charges.
        /// </summary>
        public ICharges Charges
        {
            get
            {
                return this.charges;
            }

            set
            {
                this.charges = value;
                this.HasCharges = this.charges != null;
            }
        }

        /// <summary>
        ///     Gets or sets the cooldown.
        /// </summary>
        public ICooldown Cooldown
        {
            get
            {
                return this.cooldown;
            }

            set
            {
                this.cooldown = value;
                this.HasCooldown = this.cooldown != null;
            }
        }

        /// <summary>
        ///     Gets or sets the damage calculator.
        /// </summary>
        public ISkillDamageCalculator DamageCalculator { get; set; }

        /// <summary>
        ///     Gets or sets the data receiver.
        /// </summary>
        public ISkillDataReceiver DataReceiver { get; set; }

        /// <summary>
        ///     Gets the dispose notifier.
        /// </summary>
        public Notifier DisposeNotifier { get; } = new Notifier();

        /// <summary>
        ///     Gets or sets the drawer.
        /// </summary>
        public ISkillDrawer Drawer { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether has charges.
        /// </summary>
        public bool HasCharges { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether has cooldown.
        /// </summary>
        public bool HasCooldown { get; set; }

        /// <summary>
        ///     Gets or sets the interaction.
        /// </summary>
        public ISkillInteraction Interaction { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether is controllable.
        /// </summary>
        public bool IsControllable { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether is item.
        /// </summary>
        public bool IsItem { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether is temporary.
        /// </summary>
        public bool IsTemporary { get; set; }

        /// <summary>
        ///     Gets or sets the skill data.
        /// </summary>
        public SkillJson Json { get; set; }

        /// <summary>
        ///     Gets or sets the skill level.
        /// </summary>
        public ISkillLevel Level { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the overlay provider.
        /// </summary>
        public ISkillOverlayProvider OverlayProvider { get; set; }

        /// <summary>
        ///     Gets or sets the owner.
        /// </summary>
        public IAbilityUnit Owner { get; set; }

        /// <summary>The parts.</summary>
        public IReadOnlyDictionary<Type, IAbilitySkillPart> Parts => this.parts;

        /// <summary>
        ///     Gets or sets the skill color.
        /// </summary>
        public Color SkillColor { get; set; }

        /// <summary>
        ///     Gets or sets the skill control.
        /// </summary>
        public ISkillControl SkillControl { get; set; }

        /// <summary>
        ///     Gets or sets the skill handle.
        /// </summary>
        public uint SkillHandle { get; set; }

        /// <summary>
        ///     Gets or sets the skill handle string.
        /// </summary>
        public string SkillHandleString { get; set; }

        /// <summary>
        ///     Gets or sets the cast range.
        /// </summary>
        public ISkillTargetFind SkillTargetFind { get; set; }

        /// <summary>
        ///     Gets or sets the source.
        /// </summary>
        public Unit Source { get; set; }

        /// <summary>
        ///     Gets or sets the skill.
        /// </summary>
        public Ability SourceAbility { get; set; }

        /// <summary>
        ///     Gets or sets the source item.
        /// </summary>
        public Item SourceItem { get; set; }

        /// <summary>
        ///     Gets or sets the texture.
        /// </summary>
        public DotaTexture Texture { get; set; }

        /// <summary>
        ///     Gets or sets the unique name.
        /// </summary>
        public string UniqueName { get; set; }

        /// <summary>
        ///     Gets or sets the updater.
        /// </summary>
        public ISkillUpdater Updater { get; set; }

        /// <summary>
        ///     Gets or sets the variables.
        /// </summary>
        public SkillDynamic Variables { get; set; } = new SkillDynamic();

        #endregion

        #region Public Methods and Operators

        /// <summary>The add part.</summary>
        /// <param name="partFactory">The part factory.</param>
        /// <typeparam name="T">The type of part</typeparam>
        public void AddPart<T>(Func<IAbilitySkill, T> partFactory) where T : IAbilitySkillPart
        {
            var type = typeof(T);
            if (this.Parts.ContainsKey(type))
            {
                return;
            }

            var part = partFactory.Invoke(this);
            if (part == null)
            {
                return;
            }

            this.GetType().GetProperties().FirstOrDefault(x => x.PropertyType == type)?.SetValue(this, part);
            this.parts.Add(type, part);
        }

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
        public bool CanCast(IAbilityUnit target, out bool needToMoveCloser)
        {
            if (target == null)
            {
                needToMoveCloser = false;
                return false;
            }

            if (target?.SourceUnit == null)
            {
                needToMoveCloser = false;
                return false;
            }

            needToMoveCloser = false;
            if (target.SourceUnit.IsMagicImmune()
                && this.SourceAbility.SpellPierceImmunityType
                != (target.IsEnemy ? SpellPierceImmunityType.EnemiesYes : SpellPierceImmunityType.AlliesYes))
            {
                return false;
            }

            needToMoveCloser = !this.CanHit(target.SourceUnit);
            return !needToMoveCloser;
        }

        /// <summary>
        ///     The can cast.
        /// </summary>
        /// <param name="target">
        ///     The target.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public bool CanCast(IAbilityUnit target)
        {
            if (target == null)
            {
                return false;
            }

            return target.SourceUnit.IsAlive && target.SourceUnit.IsVisible && this.CanHit(target.SourceUnit);
        }

        /// <summary>
        ///     Checks if skill can be safely used
        /// </summary>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public bool CanCast()
        {
            return this.SourceAbility != null && this.Source != null && this.SourceAbility.IsValid
                   && this.Source.IsValid && this.SourceAbility.CanBeCasted(float.MaxValue)
                   && !this.SourceAbility.IsInAbilityPhase
                   && (this.IsItem ? this.Source.CanUseItems() : this.Source.CanCast());
        }

        /// <summary>
        ///     The can hit.
        /// </summary>
        /// <param name="hero">
        ///     The hero.
        /// </param>
        public bool CanHit(Unit hero)
        {
            // if (hero == null || !hero.IsValid)
            // {
            // return false;
            // }

            // return this.CastData.CanUseOnDictionary[hero.Handle];
            return false;
        }

        /// <summary>
        ///     The damage dealt.
        /// </summary>
        /// <param name="hero">
        ///     The hero.
        /// </param>
        public float DamageDealt(Unit hero)
        {
            // if (hero == null || !hero.IsValid)
            // {
            // return 0;
            // }

            // return this.CastData.DamageDealtDictionary[hero.Handle];
            return 0;
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            this.DisposeNotifier.Notify();
            foreach (var keyValuePair in this.Parts)
            {
                keyValuePair.Value.Dispose();
            }

            // this.CastData.Dispose();
            this.CastData = null;
            this.DataReceiver = null;
            this.Cooldown = null;
            this.AbilityPhase = null;
            this.AbilityParticle = null;
            this.AbilityProjectile = null;
            this.AbilityCast = null;
            this.SkillControl = null;
            this.AreaOfEffect = null;
            this.Updater = null;
            this.Drawer = null;
            this.DamageCalculator = null;
            this.SkillTargetFind = null;
        }

        /// <summary>The get part.</summary>
        /// <typeparam name="T">The type of part</typeparam>
        /// <returns>The <see cref="T" />.</returns>
        public T GetPart<T>() where T : IAbilitySkillPart
        {
            return (T)this.Parts[typeof(T)];
        }

        /// <summary>
        ///     The hit delay.
        /// </summary>
        /// <param name="hero">
        ///     The hero.
        /// </param>
        public double HitDelay(Unit hero)
        {
            // if (hero == null || !hero.IsValid)
            // {
            // return 0;
            // }

            // return this.CastData.HitDelayDictionary[hero.Handle];
            return 0;
        }

        /// <summary>
        ///     The set can hit.
        /// </summary>
        /// <param name="hero">
        ///     The hero.
        /// </param>
        /// <param name="canHit">
        ///     The can hit.
        /// </param>
        public void SetCanHit(Unit hero, bool canHit)
        {
            // if (hero == null || !hero.IsValid)
            // {
            // return;
            // }

            // this.CastData.CanUseOnDictionary[hero.Handle] = canHit;
        }

        /// <summary>
        ///     The set damage dealt.
        /// </summary>
        /// <param name="hero">
        ///     The hero.
        /// </param>
        /// <param name="damage">
        ///     The damage.
        /// </param>
        public void SetDamageDealt(Unit hero, float damage)
        {
            // if (hero == null || !hero.IsValid)
            // {
            // return;
            // }

            // this.CastData.DamageDealtDictionary[hero.Handle] = damage;
        }

        /// <summary>
        ///     The set hit delay.
        /// </summary>
        /// <param name="hero">
        ///     The hero.
        /// </param>
        /// <param name="hitDelay">
        ///     The hit delay.
        /// </param>
        public void SetHitDelay(Unit hero, double hitDelay)
        {
            // if (hero == null || !hero.IsValid)
            // {
            // return;
            // }

            // this.CastData.HitDelayDictionary[hero.Handle] = hitDelay;
        }

        #endregion
    }
}