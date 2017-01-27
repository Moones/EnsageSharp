// <copyright file="DefaultSkillComposer.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillComposer
{
    using System;
    using System.Collections.Generic;

    using Ability.Core.AbilityFactory.AbilitySkill.Data;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.Charges;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.Cooldown;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillCastData;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillDataReceiver;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillLevel;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillOverlay;
    using Ability.Core.AbilityFactory.AbilitySkill.Types;

    using Ensage;
    using Ensage.Common.Extensions;

    /// <summary>
    ///     The unit target skill composer.
    /// </summary>
    internal class DefaultSkillComposer : IAbilitySkillComposer
    {
        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="DefaultSkillComposer" /> class.</summary>
        public DefaultSkillComposer()
        {
            this.AssignPart<ISkillDataReceiver>(skill => new SkillDataReceiver(skill));
            this.AssignPart<ISkillLevel>(skill => skill.IsItem ? new ItemLevel(skill) : new SkillLevel(skill));
            this.AssignPart<ICooldown>(skill => skill.SourceAbility.GetCooldown(2) <= 0 ? null : new Cooldown(skill));
            this.AssignPart<ISkillCastData>(skill => new SkillCastData(skill));
            this.AssignPart<ICharges>(
                skill =>
                    {
                        if (skill.IsItem
                            && (skill.SourceItem.IsRequiringCharges || skill.SourceItem.IsDisplayingCharges
                                || skill.SourceItem.CurrentCharges > 0 || skill.SourceItem.SecondaryCharges > 0))
                        {
                            return new Charges(skill);
                        }

                        return null;
                    });
            this.AssignPart<ISkillOverlayProvider>(skill => new SkillOverlayProvider(skill));
        }

        #endregion

        #region Public Properties

        /// <summary>Gets the assignments.</summary>
        public IDictionary<Type, Action<IAbilitySkill>> Assignments { get; } =
            new Dictionary<Type, Action<IAbilitySkill>>();

        #endregion

        #region Public Methods and Operators

        /// <summary>The assign part.</summary>
        /// <param name="factory">The factory.</param>
        /// <typeparam name="T">The type of part</typeparam>
        public void AssignPart<T>(Func<IAbilitySkill, T> factory) where T : IAbilitySkillPart
        {
            var type = typeof(T);
            this.Assignments[type] = unit => unit.AddPart(factory);
        }

        /// <summary>
        ///     The compose.
        /// </summary>
        /// <param name="skill">
        ///     The skill.
        /// </param>
        /// <exception cref="Exception">A delegate callback throws an exception.</exception>
        public void Compose(IAbilitySkill skill)
        {
            if (skill == null)
            {
                return;
            }

            foreach (var keyValuePair in this.Assignments)
            {
                keyValuePair.Value.Invoke(skill);
            }

            if (skill.AbilityPhase == null && skill.CastData.CastPoint > 0)
            {
                skill.AbilityPhase = new AbilityPhase(skill);
            }

            if (skill.AbilityParticle == null && skill.Json.ParticleNames != null && skill.Json.ParticleNames.Length > 0)
            {
                skill.AbilityParticle = new AbilityParticle(skill);
            }

            if (skill.AbilityProjectile == null && skill.Json.HasProjectile)
            {
                skill.AbilityProjectile = new AbilityProjectile(skill);
            }

            if (skill.AbilityCast == null && !skill.SourceAbility.IsAbilityBehavior(AbilityBehavior.Passive))
            {
                skill.AbilityCast = new AbilityCast(skill);
            }

            foreach (var keyValuePair in skill.Parts)
            {
                keyValuePair.Value.Initialize();
            }

            // if (skill.DamageCalculator == null)
            // {
            // skill.DamageCalculator = new DefaultSkillDamageCalculator(skill);
            // }

            // if (skill.Drawer == null)
            // {
            // skill.Drawer = new DefaultSkillDrawer(skill);
            // }

            // if (skill.Updater == null)
            // {
            // skill.Updater = new DefaultSkillUpdater(skill);
            // }
            // if (!skill.IsControllable)
            // {
            // this.ComposeUncontrollableSkill(skill);
            // return;
            // }

            // var controllableSkill = skill as IControllableSkill;
            // if (controllableSkill == null)
            // {
            // Logging.Write()(LogLevel.Error, "ControllableSkill was null");
            // return;
            // }

            // this.ComposeControllableSkill(controllableSkill);
        }

        /// <summary>
        ///     The compose controllable skill.
        /// </summary>
        /// <param name="controllableSkill">
        ///     The controllable skill.
        /// </param>
        public virtual void ComposeControllableSkill(IControllableSkill controllableSkill)
        {
            // controllableSkill.SkillControl = new SkillControl(controllableSkill);
            // Func<IControllableSkill, ISkillCastingFunction> castingFunctionInitialize;
            // if (this.CastingFunctions != null
            // && this.CastingFunctions.TryGetValue(controllableSkill.Name, out castingFunctionInitialize))
            // {
            // controllableSkill.SkillControl.SkillCastingFunction = castingFunctionInitialize.Invoke(
            // controllableSkill);
            // }

            // if (controllableSkill.SourceAbility.IsAbilityBehavior(AbilityBehavior.UnitTarget))
            // {
            // if (controllableSkill.SkillControl.SkillCastingFunction == null)
            // {
            // controllableSkill.SkillControl.SkillCastingFunction = new UnitTargetCast(controllableSkill);
            // }

            // if (controllableSkill.AreaOfEffect == null)
            // {
            // controllableSkill.AreaOfEffect = new ProjectileAreaOfEffect(controllableSkill);
            // }

            // if (controllableSkill.SkillTargetFind == null)
            // {
            // controllableSkill.SkillTargetFind = new ProjectileAreaOfEffectTargetFind(controllableSkill);
            // }
            // }
            // else if (controllableSkill.SourceAbility.IsAbilityBehavior(AbilityBehavior.Point)
            // || controllableSkill.SourceAbility.IsAbilityBehavior(AbilityBehavior.AreaOfEffect))
            // {
            // if (controllableSkill.SkillControl.SkillCastingFunction == null)
            // {
            // controllableSkill.SkillControl.SkillCastingFunction = new SkillShotCast(controllableSkill);
            // }

            // if (controllableSkill.AreaOfEffect == null)
            // {
            // var properties = controllableSkill.SourceAbility.CommonProperties();
            // if (!string.IsNullOrEmpty(properties?.Width))
            // {
            // controllableSkill.AreaOfEffect = new LinearAreaOfEffect(controllableSkill);
            // }
            // else
            // {
            // controllableSkill.AreaOfEffect = new RadialAreaOfEffect(controllableSkill);
            // }
            // }

            // if (controllableSkill.SkillTargetFind == null)
            // {
            // controllableSkill.SkillTargetFind = new LinearAreaOfEffectTargetFind(controllableSkill);
            // }
            // }
            // else
            // {
            // if (controllableSkill.SkillControl.SkillCastingFunction == null)
            // {
            // controllableSkill.SkillControl.SkillCastingFunction = new NoTargetCast(controllableSkill);
            // }

            // if (controllableSkill.AreaOfEffect == null)
            // {
            // controllableSkill.AreaOfEffect = new RadialAreaOfEffect(controllableSkill);
            // }

            // if (controllableSkill.SkillTargetFind == null)
            // {
            // controllableSkill.SkillTargetFind = new RadialAreaOfEffectTargetFind(controllableSkill);
            // }
            // }
        }

        /// <summary>
        ///     The compose uncontrollable skill.
        /// </summary>
        /// <param name="skill">
        ///     The skill.
        /// </param>
        public virtual void ComposeUncontrollableSkill(IAbilitySkill skill)
        {
            // if (skill.SourceAbility.IsAbilityBehavior(AbilityBehavior.UnitTarget))
            // {
            // if (skill.AreaOfEffect == null && skill.Json.HasProjectile)
            // {
            // skill.AreaOfEffect = new ProjectileAreaOfEffect(skill);
            // }

            // if (skill.SkillTargetFind == null)
            // {
            // skill.SkillTargetFind = new ProjectileAreaOfEffectTargetFind(skill);
            // }
            // }
            // else if (skill.SourceAbility.IsAbilityBehavior(AbilityBehavior.AreaOfEffect)
            // || skill.SourceAbility.IsAbilityBehavior(AbilityBehavior.Point))
            // {
            // if (skill.AreaOfEffect == null)
            // {
            // var properties = skill.SourceAbility.CommonProperties();
            // if (!string.IsNullOrEmpty(properties?.Width))
            // {
            // skill.AreaOfEffect = new LinearAreaOfEffect(skill);
            // }
            // else
            // {
            // skill.AreaOfEffect = new RadialAreaOfEffect(skill);
            // }
            // }

            // if (skill.SkillTargetFind == null)
            // {
            // skill.SkillTargetFind = new LinearAreaOfEffectTargetFind(skill);
            // }
            // }
            // else
            // {
            // if (skill.AreaOfEffect == null)
            // {
            // skill.AreaOfEffect = new RadialAreaOfEffect(skill);
            // }

            // if (skill.SkillTargetFind == null)
            // {
            // skill.SkillTargetFind = new RadialAreaOfEffectTargetFind(skill);
            // }
            // }
        }

        #endregion
    }
}