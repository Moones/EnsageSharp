// <copyright file="AbilityDataProvider.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityData
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Ability.Core.AbilityFactory.AbilitySkill;
    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityManager;
    using Ability.Core.MenuManager.GetValue;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Menu;
    using Ensage.Common.Objects.UtilityObjects;

    using SharpDX;

    /// <summary>The ability data provider.</summary>
    [Export(typeof(IAbilityService))]
    internal class AbilityDataProvider : IAbilityService
    {
        #region Fields

        /// <summary>The menu.</summary>
        private readonly Menu menu;

        /// <summary>
        ///     The update sleeper.
        /// </summary>
        private readonly Sleeper updateSleeper = new Sleeper();

        private Vector3 lastBlinkPosition;

        private Vector3 lastCameraPosition;

        /// <summary>
        ///     The particle skills.
        /// </summary>
        private Dictionary<double, IAbilitySkill> particleSkills = new Dictionary<double, IAbilitySkill>();

        /// <summary>
        ///     The projectile skills.
        /// </summary>
        private Dictionary<double, IAbilitySkill> projectileSkills = new Dictionary<double, IAbilitySkill>();

        /// <summary>The update interval.</summary>
        private GetValue<Slider, float> updateInterval;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="AbilityDataProvider" /> class.</summary>
        internal AbilityDataProvider()
        {
            this.menu = new Menu(nameof(AbilityDataProvider), Constants.AssemblyName + nameof(AbilityDataProvider));
            this.updateInterval =
                new GetValue<Slider, float>(
                    this.menu.AddItem(
                        new MenuItem(this.menu.Name + nameof(this.updateInterval), "Data update interval (ms)").SetValue
                            (new Slider(25, 0, 300))
                            .SetTooltip(
                                "If you are experiencing FPS drops while ONLY THIS ASSEMBLY is loaded, increase the interval")),
                    slider => slider.Value);
        }

        #endregion

        #region Public Properties

        /// <summary>Gets a value indicating whether generate menu.</summary>
        public bool GenerateMenu { get; } = true;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the ability unit manager.
        /// </summary>
        [Import(typeof(IAbilityManager))]
        internal Lazy<IAbilityManager> AbilityUnitManager { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>The menu.</summary>
        /// <returns>The <see cref="Menu" />.</returns>
        public Menu Menu()
        {
            return this.menu;
        }

        /// <summary>
        ///     The on close.
        /// </summary>
        public void OnClose()
        {
            Game.OnUpdate -= this.Game_OnUpdate;

            // Entity.OnFloatPropertyChange -= this.Entity_OnFloatPropertyChange;
            Entity.OnBoolPropertyChange -= this.Entity_OnBoolPropertyChange;
            Entity.OnInt32PropertyChange -= this.Entity_OnInt32PropertyChange;
            Entity.OnParticleEffectAdded -= this.Entity_OnParticleEffectAdded;

            // ObjectManager.OnAddTrackingProjectile -= this.ObjectManager_OnAddTrackingProjectile;
            // ObjectManager.OnAddLinearProjectile -= this.ObjectManager_OnAddLinearProjectile;
            Unit.OnModifierAdded -= this.Unit_OnModifierAdded;
            Unit.OnModifierRemoved -= this.Unit_OnModifierRemoved;

            Drawing.OnDraw -= this.Drawing_OnDraw;

            ObjectManager.OnAddEntity -= this.ObjectManager_OnAddEntity;
        }

        /// <summary>
        ///     The on load.
        /// </summary>
        public void OnLoad()
        {
            this.StartUp();
            DelayAction.Add(
                300,
                () =>
                    {
                        if (!Game.IsInGame)
                        {
                            return;
                        }

                        // foreach (var keyValuePair in this.AbilityUnitManager.Value.Units)
                        // {
                        // foreach (var valuePair in keyValuePair.Value.SkillBook.AllSkills)
                        // {
                        // if (valuePair.Value.Json.ParticleNames != null
                        // && valuePair.Value.Json.ParticleNames.Length > 0)
                        // {
                        // this.particleSkills.Add(valuePair.Key, valuePair.Value);
                        // }
                        // else if (valuePair.Value.Json.HasProjectile)
                        // {
                        // this.projectileSkills.Add(valuePair.Key, valuePair.Value);
                        // }
                        // }
                        // }

                        // this.AbilityUnitManager.Value.SkillAdded += this.Value_SkillAdded;
                        // this.AbilityUnitManager.Value.SkillRemoved += this.Value_SkillRemoved;

                        // Game.OnMessage += this.Game_OnMessage;
                        Game.OnUpdate += this.Game_OnUpdate;

                        // Entity.OnFloatPropertyChange += this.Entity_OnFloatPropertyChange;
                        Entity.OnBoolPropertyChange += this.Entity_OnBoolPropertyChange;
                        Entity.OnParticleEffectAdded += this.Entity_OnParticleEffectAdded;
                        Entity.OnInt32PropertyChange += this.Entity_OnInt32PropertyChange;

                        // ObjectManager.OnAddTrackingProjectile += this.ObjectManager_OnAddTrackingProjectile;
                        // ObjectManager.OnAddLinearProjectile += this.ObjectManager_OnAddLinearProjectile;
                        Unit.OnModifierAdded += this.Unit_OnModifierAdded;
                        Unit.OnModifierRemoved += this.Unit_OnModifierRemoved;

                        DelayAction.Add(500, () => Drawing.OnDraw += this.Drawing_OnDraw);

                        // Drawing.OnEndScene += Drawing_OnEndScene;

                        // Game.OnWndProc += this.Game_OnWndProc;
                        ObjectManager.OnAddEntity += this.ObjectManager_OnAddEntity;
                    });
        }

        #endregion

        #region Methods

        private static void Drawing_OnEndScene(EventArgs args)
        {
            // string installpath = null;
            ///*
            // * To find steam installation
            // */
            // RegistryKey regKey = Registry.CurrentUser;
            // regKey = regKey.OpenSubKey(@"Software\Valve\Steam");
            // if (regKey != null)
            // installpath = regKey.GetValue("SteamPath").ToString();

            ///*
            // * Get the texture you want to draw!
            // */
            // string filename = installpath + @"\file.png";
            // var mTexture = SharpDX.Direct3D9.Texture.FromFile(Drawing.Direct3DDevice9, filename);
            ///*
            // * Get the sprite in current direct 3d device
            // */
            // var mSprite = new SharpDX.Direct3D9.Sprite(Drawing.Direct3DDevice9);

            ///*
            // * Set your position
            // */
            // var mTexturePosition = new SharpDX.Vector3(mTexturePositionX, mTexturePositionY, 0);

            ///*
            // * Set color for the texture
            // */
            // var color = new SharpDX.ColorBGRA(0xBLACK, 0xGREEN, 0xRED, 0xALPHA);

            ///*
            // * Draw the texture
            // */
            // mSprite.Begin(SharpDX.Direct3D9.SpriteFlags.AlphaBlend);

            // mSprite.Draw(mTexture, color, null, null, mTexturePosition);
            // mSprite.End();
        }

        /// <summary>
        ///     The calculate path.
        /// </summary>
        /// <param name="enemy">
        ///     The enemy.
        /// </param>
        /// <returns>
        ///     The <see cref="Collection{T}" />.
        /// </returns>
        private void Drawing_OnDraw(EventArgs args)
        {
            if (!Game.IsInGame)
            {
                // || Game.IsPaused)
                return;
            }

            foreach (var keyValuePair in this.AbilityUnitManager.Value.Units)
            {
                keyValuePair.Value.Visibility.Visible = keyValuePair.Value.SourceUnit.IsVisible;
                keyValuePair.Value.Position.Update();
                keyValuePair.Value.ScreenInfo.Update();
                keyValuePair.Value.DataReceiver.Drawing_OnDraw();
            }
        }

        /// <summary>
        ///     The entity_ on boolean property change.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="args">
        ///     The args.
        /// </param>
        private void Entity_OnBoolPropertyChange(Entity sender, BoolPropertyChangeEventArgs args)
        {
            if (args.NewValue == args.OldValue)
            {
                return;
            }

            if (sender is Creep)
            {
                return;
            }

            var propertyName = args.PropertyName;
            switch (propertyName)
            {
                default:

                    // foreach (var keyValuePair in this.AbilityUnitManager.Value.Units)
                    // {
                    // keyValuePair.Value.DataReceiver.Entity_OnBoolPropertyChange(sender, args);
                    // }
                    return;
                case GlobalVariables.PropertyAbilityPhase:
                    var ability = sender as Ability;
                    var owner = ability?.Owner;
                    if (ability == null || owner == null)
                    {
                        return;
                    }

                    if (args.NewValue)
                    {
                        this.AbilityUnitManager.Value.Units.FirstOrDefault(x => x.Key == owner.Handle)
                            .Value?.SkillBook.CastPointSpells.FirstOrDefault(x => x.Key == ability.Handle)
                            .Value?.AbilityPhase.Start();
                    }
                    else
                    {
                        this.AbilityUnitManager.Value.Units.FirstOrDefault(x => x.Key == owner.Handle)
                            .Value?.SkillBook.CastPointSpells.FirstOrDefault(x => x.Key == ability.Handle)
                            .Value?.AbilityPhase.Stop();
                    }

                    return;

                // case GlobalVariables.PropertyVisible:
                // if (!args.NewValue)
                // {
                // return;
                // }

                // Console.WriteLine("visble: " + sender.Name);
                // var unit = this.AbilityUnitManager.Value.Units.FirstOrDefault(x => x.Key == sender.Handle);
                // unit.Value?.Position.Update();
                // if (unit.Value != null)
                // {
                // unit.Value.Visibility.Visible = args.NewValue;
                // }

                // return;
                // case "m_flStartSequenceCycle":
                // return;
            }
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
        private void Entity_OnFloatPropertyChange(Entity sender, FloatPropertyChangeEventArgs args)
        {
            return;
            if (args.NewValue.Equals(args.OldValue) || args.PropertyName == "m_fGameTime"
                || args.PropertyName == "m_flStartSequenceCycle")
            {
                return;
            }

            if (sender is Creep)
            {
                return;
            }

            var propertyName = args.PropertyName;
            switch (propertyName)
            {
                default:

                    // foreach (var keyValuePair in this.AbilityUnitManager.Value.Units)
                    // {
                    // keyValuePair.Value.DataReceiver.Entity_OnFloatPropertyChange(sender, args);
                    // }
                    return;
                case GlobalVariables.PropertyX:
                    this.AbilityUnitManager.Value.Units.FirstOrDefault(x => x.Value.UnitHandle.Equals(sender.Handle))
                        .Value?.DataReceiver.PositionXChange(sender.Position.X);
                    return;
                case GlobalVariables.PropertyY:
                    this.AbilityUnitManager.Value.Units.FirstOrDefault(x => x.Value.UnitHandle.Equals(sender.Handle))
                        .Value?.DataReceiver.PositionYChange(sender.Position.Y);
                    return;
            }
        }

        private void Entity_OnInt32PropertyChange(Entity sender, Int32PropertyChangeEventArgs args)
        {
            if (args.NewValue.Equals(args.OldValue) || args.PropertyName == "m_flStartSequenceCycle"
                || args.PropertyName == "m_iFoWFrameNumber" || args.PropertyName == "m_iNetTimeOfDay")
            {
                return;
            }

            if (sender is Creep)
            {
                return;
            }

            // Console.WriteLine(args.PropertyName + " " + args.NewValue);
            var propertyName = args.PropertyName;
            switch (propertyName)
            {
                default:

                    // foreach (var keyValuePair in this.AbilityUnitManager.Value.Units)
                    // {
                    // keyValuePair.Value.DataReceiver.Entity_OnInt32PropertyChange(sender, args);
                    // }
                    return;
                case GlobalVariables.PropertyHealth:
                    this.AbilityUnitManager.Value.Units.FirstOrDefault(x => x.Value.UnitHandle.Equals(sender.Handle))
                        .Value?.DataReceiver.HealthChange(args.NewValue);
                    return;
            }
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
        private void Entity_OnParticleEffectAdded(Entity sender, ParticleEffectAddedEventArgs args)
        {
            if (args.Name.StartsWith(@"particles/ui_mouseactions") || args.Name.StartsWith(@"particles/base_attacks")
                || args.Name.StartsWith(@"materials/ensage_ui") || args.Name.StartsWith(@"particles/generic_gameplay")
                || args.Name.StartsWith(@"particles/siege_fx"))
            {
                return;
            }

            var isHeroParticle = args.Name.Contains("units/heroes");
            var info = new ParticleEffectMoreInfo(
                sender is Hero,
                isHeroParticle,
                isHeroParticle ? args.Name.Substring("particles/units/heroes".Length) : null,
                args.ParticleEffect.GetControlPoint(0) == Vector3.Zero);

            DelayAction.Add(
                -1,
                () =>
                    {
                        // Console.WriteLine(
                        // args.Name + " " + args.ParticleEffect.GetControlPoint(2) + " "
                        // + args.ParticleEffect.GetControlPoint(3) + " " + args.ParticleEffect.GetControlPoint(4)
                        // + " " + args.ParticleEffect.Owner?.Name);
                        if (args.Name == "particles/items_fx/blink_dagger_start.vpcf")
                        {
                            this.lastBlinkPosition = args.ParticleEffect.GetControlPoint(0);
                            return;
                        }
                        else if (args.Name == "particles/items_fx/blink_dagger_end.vpcf")
                        {
                            if (args.ParticleEffect.Owner.IsVisible)
                            {
                                return;
                            }

                            var unit =
                                this.AbilityUnitManager.Value.Enemies.FirstOrDefault(
                                    x => x.Key.Equals(args.ParticleEffect.Owner.Handle));
                            unit.Value?.PositionTracker.PositionUpdated(this.lastBlinkPosition);
                            return;
                        }

                        if (info.PositionIsKnown)
                        {
                            foreach (var keyValuePair in this.AbilityUnitManager.Value.Units)
                            {
                                keyValuePair.Value.DataReceiver.Entity_OnParticleEffectAdded(sender, args, info);
                            }
                        }

                        foreach (var particleSkill in this.particleSkills)
                        {
                            particleSkill.Value.DataReceiver.Entity_OnParticleEffectAdded(sender, args);
                        }
                    });
        }

        /// <summary>
        ///     The game_ on update.
        /// </summary>
        /// <param name="args">
        ///     The args.
        /// </param>
        private void Game_OnUpdate(EventArgs args)
        {
            if (!Game.IsInGame)
            {
                return;
            }

            if (this.updateSleeper.Sleeping)
            {
                return;
            }

            this.updateSleeper.Sleep(this.updateInterval.Value);

            foreach (var keyValuePair in this.AbilityUnitManager.Value.Units)
            {
                keyValuePair.Value.DataReceiver.Game_OnUpdate();
            }
        }

        /// <summary>The object manager_ on add entity.</summary>
        /// <param name="args">The args.</param>
        private void ObjectManager_OnAddEntity(EntityEventArgs args)
        {
            DelayAction.Add(
                -1,
                () =>
                    {
                        if (!args.Entity.IsValid || args.Entity.NetworkPosition.Equals(Vector3.Zero))
                        {
                            return;
                        }

                        // if (!args.Entity.Name.Contains("npc_dota_creep")
                        // && !args.Entity.Name.Contains("npc_dota_neutral")
                        // && !args.Entity.Name.Contains("npc_dota_badguys")
                        // && !args.Entity.Name.Contains("npc_dota_goodguys")
                        // && !args.Entity.Name.Contains("dota_scene_entity")
                        // && !args.Entity.Name.Contains("point_hmd") && !args.Entity.Name.Contains("dota_item")
                        // && !args.Entity.Name.Contains("ent_dota_tree") && !args.Entity.Name.Contains("dota_world")
                        // && !args.Entity.Name.Contains("ambient_")
                        // && !args.Entity.Name.Contains("npc_dota_hero_") && !args.Entity.Name.Contains("effigy")
                        // && !args.Entity.Name.Contains("prop_dynamic") && !args.Entity.Name.Contains("world_layer")
                        // && !args.Entity.NetworkName.Contains("Spawner") && !args.Entity.Name.Contains("dota_shop")
                        // && !args.Entity.Name.Contains("fountain") && !args.Entity.Name.Contains("halloffame")
                        // && !args.Entity.Name.Contains("info_player") && !args.Entity.Name.Contains("info_particle")
                        // && !args.Entity.Name.Contains("Camera") && !args.Entity.Name.Contains("tonemap")
                        // && args.Entity.NetworkName != "CDOTAPlayer"
                        // && !args.Entity.Name.Contains("Item_Physical"))
                        // {
                        // Console.WriteLine(
                        // args.Entity.Name + " position:" + args.Entity.NetworkPosition + " owner: "
                        // + args.Entity.Owner?.Name);
                        // var unit = args.Entity as Unit;

                        // if (unit != null)
                        // {
                        // Console.WriteLine(
                        // unit.DayVision + " " + unit.NightVision + " " + unit.HullRadius);
                        // foreach (var unitModifier in unit.Modifiers)
                        // {
                        // Console.WriteLine(
                        // unitModifier.Name + " caster: " + unitModifier.Caster?.Name + " parent:"
                        // + unitModifier.Parent?.Name + " " + unitModifier.TextureName + " "
                        // + unitModifier.RemainingTime);
                        // }
                        // }
                        // }
                        if (args.Entity.Owner is Hero)
                        {
                            if (
                                this.AbilityUnitManager.Value.Enemies.Any(
                                    keyValuePair => keyValuePair.Value.PositionTracker.Entity(args.Entity)))
                            {
                                return;
                            }
                        }

                        if (args.Entity.Name == "npc_dota_base")
                        {
                            var unit = args.Entity as Unit;
                            if (unit == null)
                            {
                                return;
                            }

                            if (
                                this.AbilityUnitManager.Value.Enemies.Any(
                                    keyValuePair => keyValuePair.Value.PositionTracker.DotaBase(unit)))
                            {
                                return;
                            }

                            return;
                        }

                        if (args.Entity.Name == "npc_dota_thinker")
                        {
                            if (args.Entity.Owner == null
                                || args.Entity.Owner.ClassId == ClassId.CDOTA_Unit_Hero_Invoker
                                || args.Entity.Owner.ClassId == ClassId.CDOTA_Unit_Hero_Kunkka
                                || args.Entity.Owner.IsVisible)
                            {
                                return;
                            }

                            var unit =
                                this.AbilityUnitManager.Value.Enemies.FirstOrDefault(
                                    x => x.Key.Equals(args.Entity.Owner.Handle));
                            unit.Value?.PositionTracker.PositionUpdated(args.Entity.NetworkPosition);
                            return;
                        }
                    });
        }

        private void ObjectManager_OnAddTrackingProjectile(TrackingProjectileEventArgs args)
        {
            DelayAction.Add(
                -1,
                () =>
                    {
                        if (args.Projectile.Source == null
                            || args.Projectile.Target == null && args.Projectile.TargetPosition == Vector3.Zero)
                        {
                            return;
                        }

                        foreach (var projectileSkill in this.projectileSkills)
                        {
                            projectileSkill.Value.DataReceiver.ObjectManager_OnAddTrackingProjectile(args);
                        }
                    });
        }

        /// <summary>The start up.</summary>
        private void StartUp()
        {
            var list = new Dictionary<string, float>
                           {
                               { "dota_use_particle_fow", 0 }, { "fog_enable", 0 }, { "fog_override", 1 },
                               { "fog_end", 3000 }, { "dota_height_fog_scale", 0 }, { "cam_showangles", 0 },
                               { "cl_disable_ragdolls", 0 }, { "dota_camera_fog_end_zoomed_in", 4500 },
                               { "dota_camera_fog_end_zoomed_out", 6000 }, { "dota_camera_fog_start_zoomed_in", 2000 },
                               { "dota_camera_fog_start_zoomed_out", 4500 }, { "fow_client_nofiltering", 1 },
                               { "dota_use_particle_fow_unbloated", 0 }
                           };

            foreach (var data in list)
            {
                var var = Game.GetConsoleVar(data.Key);
                var.RemoveFlags(ConVarFlags.Cheat);
                var.RemoveFlags(ConVarFlags.DevelopmentOnly);
                var.RemoveFlags(ConVarFlags.Hidden);
                var.RemoveFlags(ConVarFlags.Protected);
                var.RemoveFlags(ConVarFlags.ServerCanExecute);
                var.RemoveFlags(ConVarFlags.Unlogged);
                var.SetValue(data.Value);
            }
        }

        /// <summary>
        ///     The unit_ on modifier added.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="args">
        ///     The args.
        /// </param>
        private void Unit_OnModifierAdded(Unit sender, ModifierChangedEventArgs args)
        {
            foreach (var keyValuePair in this.AbilityUnitManager.Value.Units)
            {
                keyValuePair.Value.DataReceiver.Unit_OnModifierChanged(sender, args);
            }
        }

        /// <summary>
        ///     The unit_ on modifier removed.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="args">
        ///     The args.
        /// </param>
        private void Unit_OnModifierRemoved(Unit sender, ModifierChangedEventArgs args)
        {
            foreach (var keyValuePair in this.AbilityUnitManager.Value.Units)
            {
                keyValuePair.Value.DataReceiver.Unit_OnModifierChanged(sender, args, false);
            }
        }

        /// <summary>
        ///     The calculate damage by local player.
        /// </summary>
        /// <param name="enemy">
        ///     The enemy.
        /// </param>
        private void UpdateSkillValues(IAbilityUnit enemy)
        {
            // enemy.TotalDamageByLocalPlayer = 0f;
            // enemy.TotalDoableDamageByLocalPlayer = 0f;
            // enemy.MinusMagicResistancePerc = 0f;
            // enemy.MinusDamageResistancePerc = 0f;
            // enemy.MinusArmor = 0f;
            // foreach (var keyValuePair in this.AbilityUnitManager.Value.Units)
            // {
            // if (keyValuePair.Value.UnitHandle.Equals(enemy.UnitHandle))
            // {
            // continue;
            // }

            // keyValuePair.Value.DamageDealtDictionary[enemy.UnitHandle] = 0;
            // if (keyValuePair.Value.SkillBook.DamageAmpsOrderedForCast == null)
            // {
            // continue;
            // }

            // foreach (var damageAmp in keyValuePair.Value.SkillBook.DamageAmpsOrderedForCast)
            // {
            // if (!damageAmp.SourceAbility.IsValid)
            // {
            // continue;
            // }

            // if (damageAmp.Updater.ShouldUpdate())
            // {
            // damageAmp.Updater.Update();
            // }

            // if (damageAmp.Updater.ShouldUpdate(enemy))
            // {
            // damageAmp.Updater.Update(enemy);
            // }
            // }
            // }

            // foreach (var keyValuePair in this.AbilityUnitManager.Value.Units)
            // {
            // if (keyValuePair.Value.UnitHandle.Equals(enemy.UnitHandle))
            // {
            // continue;
            // }

            // if (keyValuePair.Value.SkillBook.SkillsOrderedForDamageDealt == null)
            // {
            // continue;
            // }

            // foreach (var abilitySkill in keyValuePair.Value.SkillBook.SkillsOrderedForDamageDealt)
            // {
            // if (!abilitySkill.SourceAbility.IsValid)
            // {
            // continue;
            // }

            // if (abilitySkill.Updater.ShouldUpdate())
            // {
            // abilitySkill.Updater.Update();
            // }

            // if (abilitySkill.Updater.ShouldUpdate(enemy))
            // {
            // abilitySkill.Updater.Update(enemy);
            // }
            // }
            // }
        }

        /// <summary>
        ///     The value_ skill added.
        /// </summary>
        /// <param name="args">
        ///     The args.
        /// </param>
        private void Value_SkillAdded(SkillEventArgs args)
        {
            if (args.AbilitySkill.Json.ParticleNames != null && args.AbilitySkill.Json.ParticleNames.Length > 0)
            {
                Entity.OnParticleEffectAdded -= this.Entity_OnParticleEffectAdded;
                this.particleSkills.Add(args.AbilitySkill.SkillHandle, args.AbilitySkill);
                Entity.OnParticleEffectAdded += this.Entity_OnParticleEffectAdded;
            }
            else if (args.AbilitySkill.Json.HasProjectile)
            {
                ObjectManager.OnAddTrackingProjectile -= this.ObjectManager_OnAddTrackingProjectile;
                this.projectileSkills.Add(args.AbilitySkill.SkillHandle, args.AbilitySkill);
                ObjectManager.OnAddTrackingProjectile += this.ObjectManager_OnAddTrackingProjectile;
            }
        }

        /// <summary>
        ///     The value_ skill removed.
        /// </summary>
        /// <param name="args">
        ///     The args.
        /// </param>
        private void Value_SkillRemoved(SkillEventArgs args)
        {
            if (args.AbilitySkill.Json.ParticleNames != null && args.AbilitySkill.Json.ParticleNames.Length > 0)
            {
                Entity.OnParticleEffectAdded -= this.Entity_OnParticleEffectAdded;
                this.particleSkills.Remove(args.AbilitySkill.SkillHandle);
                Entity.OnParticleEffectAdded += this.Entity_OnParticleEffectAdded;
            }
            else if (args.AbilitySkill.Json.HasProjectile)
            {
                ObjectManager.OnAddTrackingProjectile -= this.ObjectManager_OnAddTrackingProjectile;
                this.projectileSkills.Remove(args.AbilitySkill.SkillHandle);
                ObjectManager.OnAddTrackingProjectile += this.ObjectManager_OnAddTrackingProjectile;
            }
        }

        #endregion
    }
}