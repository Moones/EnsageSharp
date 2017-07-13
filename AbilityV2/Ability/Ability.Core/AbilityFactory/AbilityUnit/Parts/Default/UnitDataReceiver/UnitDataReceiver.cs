// <copyright file="UnitDataReceiver.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.UnitDataReceiver
{
    using System;
    using System.Collections.Generic;

    using Ability.Core.AbilityData.AbilityDataCollector;
    using Ability.Core.AbilityFactory.AbilityModifier;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.ModifierGenerator;
    using Ability.Core.AbilityFactory.Utilities;

    using Ensage;

    using SharpDX;

    /// <summary>
    ///     The unit data receiver.
    /// </summary>
    internal class UnitDataReceiver : IUnitDataReceiver
    {
        #region Constructors and Destructors

        internal UnitDataReceiver(IAbilityUnit unit)
        {
            this.Unit = unit;
        }

        #endregion

        #region Public Properties

        /// <summary>Gets the drawings.</summary>
        public ActionManager Drawings { get; } = new ActionManager();

        /// <summary>Gets the modifier added checks.</summary>
        public FunctionManager<IAbilityModifier> ModifierAddedChecks { get; } = new FunctionManager<IAbilityModifier>();

        public FunctionManager<IAbilityModifier> ModifierRemovedChecks { get; } = new FunctionManager<IAbilityModifier>();

        /// <summary>Gets the self modifier generators.</summary>
        public Dictionary<double, IModifierGenerator> SelfModifierGenerators { get; } = new Dictionary<double, IModifierGenerator>();

        /// <summary>
        ///     Gets or sets the unit.
        /// </summary>
        public IAbilityUnit Unit { get; set; }

        /// <summary>Gets the updates.</summary>
        public ActionManager Updates { get; } = new ActionManager();

        #endregion

        #region Public Methods and Operators

        public virtual void Dispose()
        {
            this.SelfModifierGenerators.Clear();
            this.Updates.Dispose();
            this.Drawings.Dispose();
            this.ModifierAddedChecks.Dispose();
            this.ModifierRemovedChecks.Dispose();
        }

        /// <summary>The drawing_ on draw.</summary>
        public void Drawing_OnDraw()
        {
            this.Drawings.InvokeActions();
        }

        /// <summary>
        ///     The entity_ on bool property change.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="args">
        ///     The args.
        /// </param>
        public void Entity_OnBoolPropertyChange(Entity sender, BoolPropertyChangeEventArgs args)
        {
            // if (sender.Handle == this.Unit.UnitHandle && args.PropertyName == GlobalVariables.PropertyVisible)
            // {
            // if (args.NewValue == false)
            // {
            // return;
            // }

            // this.Unit.Position.Update();
            // }
            // else if (args.PropertyName == GlobalVariables.PropertyAbilityPhase)
            // {
            // var ability = sender as Ability;
            // var owner = ability?.Owner;
            // if (ability == null || owner?.Handle != this.Unit.UnitHandle)
            // {
            // return;
            // }

            // foreach (var keyValuePair in this.Unit.SkillBook.CastPointSpells)
            // {
            // if (!ability.Handle.Equals(keyValuePair.Value.SkillHandle))
            // {
            // continue;
            // }

            // if (args.NewValue && !args.OldValue)
            // {
            // keyValuePair.Value.AbilityPhase.Start();
            // }
            // else
            // {
            // keyValuePair.Value.AbilityPhase.Stop();
            // }
            // }
            // }
        }

        /// <summary>
        ///     The entity_ on float property change.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="args">
        ///     The args.
        /// </param>
        public void Entity_OnFloatPropertyChange(Entity sender, FloatPropertyChangeEventArgs args)
        {
            // if ((sender as Ability) != null)
            // {
            // Console.WriteLine(sender.Name + " " + args.PropertyName + " " + args.NewValue);
            // }

            // else if (args.PropertyName == GlobalVariables.PropertyCooldown)
            // {
            // var ability = sender as Ability;
            // var owner = ability?.Owner;
            // if (ability == null || owner?.Handle != this.Unit.UnitHandle)
            // {
            // return;
            // }
            // foreach (var keyValuePair in this.Unit.SkillBook.CastPointSpells)
            // {
            // if (!ability.Handle.Equals(keyValuePair.Value.SkillHandle))
            // {
            // continue;
            // }

            // keyValuePair.Value.Cooldown.Current = args.NewValue;
            // }
            // }
        }

        /// <summary>
        ///     The entity_ on int 32 property change.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="args">
        ///     The args.
        /// </param>
        public void Entity_OnInt32PropertyChange(Entity sender, Int32PropertyChangeEventArgs args)
        {
            // if (sender.Handle == this.Unit.UnitHandle && args.PropertyName == GlobalVariables.PropertyHealth)
            // {
            // this.Unit.Health.Current = args.NewValue;
            // }
        }

        /// <summary>
        ///     The entity_ on particle effect added.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="args">
        ///     The args.
        /// </param>
        public void Entity_OnParticleEffectAdded(
            Entity sender,
            ParticleEffectAddedEventArgs args,
            ParticleEffectMoreInfo info)
        {
            if (this.Unit.SourceUnit.IsVisible)
            {
                return;
            }

            if (info.SenderIsHero && !sender.IsVisible && sender.Handle.Equals(this.Unit.UnitHandle))
            {
                //this.Unit.PositionTracker.SenderIsHero(sender, args, info);
                return;
            }

            if (info.IsHeroParticle
                && info.StringContainingHeroName.Contains(this.Unit.Name.Substring("npc_dota_hero_".Length)))
            {
                //this.Unit.PositionTracker.ParticleIsFromHero(sender, args, info);
            }
        }

        /// <summary>
        ///     The on update.
        /// </summary>
        public void Game_OnUpdate()
        {
            //foreach (var keyValuePair in this.Unit.SkillBook.AllSkills)
            //{
            //    // if (!keyValuePair.Value.SourceAbility.IsValid)
            //    // {
            //    // this.Unit.SkillBook.RemoveSkill(keyValuePair.Value);
            //    // keyValuePair.Value.Dispose();
            //    // return;
            //    // }
            //    keyValuePair.Value.DataReceiver.Game_OnUpdate();
            //}
            
            this.Updates.InvokeActions();

            this.Unit.Mana.Current = this.Unit.SourceUnit.Mana;

            // this.Unit.Position.Update();
            // this.Unit.Visibility.Visible = this.Unit.SourceUnit.IsVisible;
            this.Unit.Level.Current = this.Unit.SourceUnit.Level;
        }

        public void HealthChange(float value)
        {
            //Console.WriteLine(value);
            this.Unit.Health.Current = value;
        }

        public virtual void Initialize()
        {
        }

        /// <summary>
        ///     The position x change.
        /// </summary>
        /// <param name="value">
        ///     The value.
        /// </param>
        public void PositionXChange(float value)
        {
            // Console.WriteLine(value + " " + this.Unit.SourceUnit.Position.X);
            this.Unit.Position.Update(
                new Vector3(value, this.Unit.SourceUnit.Position.Y, this.Unit.SourceUnit.Position.Z));
        }

        /// <summary>
        ///     The position y change.
        /// </summary>
        /// <param name="value">
        ///     The value.
        /// </param>
        public void PositionYChange(float value)
        {
            // Console.WriteLine(value + " " + this.Unit.SourceUnit.Position.Y);
            this.Unit.Position.Update(
                new Vector3(this.Unit.SourceUnit.Position.X, value, this.Unit.SourceUnit.Position.Z));
        }

        /// <summary>
        ///     The unit_ on modifier changed.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="args">
        ///     The args.
        /// </param>
        /// <param name="added">
        ///     The added.
        /// </param>
        public void Unit_OnModifierChanged(Unit sender, ModifierChangedEventArgs args, bool added = true)
        {
            if (sender.Handle.Equals(this.Unit.UnitHandle))
            {
                if (added)
                {
                    this.Unit.Modifiers.AddModifier(args.Modifier);
                }
                else
                {
                    this.Unit.Modifiers.RemoveModifier(args.Modifier);
                }
            }
        }

        public bool ModifierAdded(IAbilityModifier modifier)
        {
            return this.ModifierAddedChecks.AnyFunctionPasses(modifier);
        }

        public bool ModifierRemoved(IAbilityModifier modifier)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}