// <copyright file="AbilityManager.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityManager
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Ability.Core.AbilityFactory;
    using Ability.Core.AbilityFactory.AbilityModifier;
    using Ability.Core.AbilityFactory.AbilitySkill;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.ModifierGenerator;
    using Ability.Core.AbilityFactory.AbilitySkill.Types;
    using Ability.Core.AbilityFactory.AbilityTeam;
    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.AbilityUnit.Types;
    using Ability.Core.AbilityFactory.Utilities;
    using Ability.Core.AbilityManager.UI;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Menu;
    using Ensage.Common.Objects;

    using SharpDX;

    /// <summary>
    ///     The ability unit manager.
    /// </summary>
    [Export(typeof(IAbilityManager))]
    internal class AbilityManager : IAbilityManager
    {
        #region Fields

        private Dictionary<double, IAbilityUnit> allies = new Dictionary<double, IAbilityUnit>();

        /// <summary>
        ///     The units.
        /// </summary>
        private Dictionary<double, IAbilityUnit> controllableUnits = new Dictionary<double, IAbilityUnit>();

        private Dictionary<double, IAbilityUnit> enemies = new Dictionary<double, IAbilityUnit>();

        private Dictionary<double, IAbilitySkill> skills = new Dictionary<double, IAbilitySkill>();

        private Dictionary<string, IControllableSkill> temporarySkills = new Dictionary<string, IControllableSkill>();

        private AbilityManagerUserInterface ui;

        private Dictionary<double, IAbilityUnit> units = new Dictionary<double, IAbilityUnit>();

        #endregion

        #region Constructors and Destructors

        internal AbilityManager()
        {
            // AbilityBootstrapper.ComposeParts(this);

            this.abilitySkillPartObserver = new DataObserver<IAbilitySkillPart>(this.AbilitySkillPartAdded);
        }

        #endregion

        #region Public Events

        public event SkillEventHandler SkillAdded;

        public event SkillEventHandler SkillRemoved;

        public event UnitEventHandler UnitAdded;

        public event UnitEventHandler UnitRemoved;

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the allies.
        /// </summary>
        public IReadOnlyDictionary<double, IAbilityUnit> Allies
        {
            get
            {
                return this.allies;
            }

            set
            {
                this.allies = value.ToDictionary(x => x.Key, x => x.Value);
            }
        }

        /// <summary>
        ///     Gets or sets the units.
        /// </summary>
        public IReadOnlyDictionary<double, IAbilityUnit> ControllableUnits
        {
            get
            {
                return this.controllableUnits;
            }

            set
            {
                this.controllableUnits = value.ToDictionary(x => x.Key, x => x.Value);
            }
        }

        /// <summary>
        ///     Gets or sets the enemies.
        /// </summary>
        public IReadOnlyDictionary<double, IAbilityUnit> Enemies
        {
            get
            {
                return this.enemies;
            }

            set
            {
                this.enemies = value.ToDictionary(x => x.Key, x => x.Value);
            }
        }

        /// <summary>Gets a value indicating whether generate menu.</summary>
        public bool GenerateMenu { get; } = false;

        /// <summary>
        ///     Gets or sets the local team.
        /// </summary>
        public IAbilityTeam LocalTeam { get; set; }

        public DataProvider<IAbilityTeam> TeamAdd { get; } = new DataProvider<IAbilityTeam>();

        /// <summary>
        ///     Gets or sets the teams.
        /// </summary>
        public ICollection<IAbilityTeam> Teams { get; set; } = new List<IAbilityTeam>();

        /// <summary>
        ///     Gets or sets the units.
        /// </summary>
        public IReadOnlyDictionary<double, IAbilityUnit> Units
        {
            get
            {
                return this.units;
            }

            set
            {
                this.units = value.ToDictionary(x => x.Key, x => x.Value);
            }
        }

        public IAbilityUnit LocalHero { get; set; }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the ability factory.
        /// </summary>
        [Import(typeof(IAbilityFactory))]
        protected Lazy<IAbilityFactory> AbilityFactory { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The add skill.
        /// </summary>
        /// <param name="skill">
        ///     The skill.
        /// </param>
        public void AddTemporarySkill(Ability skill)
        {
            var owner = skill.Owner as Unit;
            if (owner == null)
            {
                return;
            }

            var abilitySkill = this.AbilityFactory.Value.CreateNewControllableSkill(skill, this.Units.First().Value);
            if (abilitySkill.SkillControl.SkillCastingFunction == null)
            {
                return;
            }

            this.temporarySkills.Add(abilitySkill.Name, abilitySkill);
            abilitySkill.IsTemporary = true;
            this.OnSkillAdded(new SkillEventArgs { AbilitySkill = abilitySkill });
        }

        /// <summary>
        ///     The add unit.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        public void AddUnit(Unit unit)
        {
            var contains = false;
            if (!unit.IsValid || (contains = this.units.ContainsKey(unit.Handle)))
            {
                if (contains)
                {
                    Console.WriteLine("unit :" + unit.Name + " already added");
                }

                return;
            }

            if (unit.IsControllable && unit.Team != GlobalVariables.EnemyTeam)
            {
                this.AddControllableUnit(unit);
                return;
            }

            // Console.WriteLine(unit.Name);
            // foreach (var modifier in unit.Modifiers)
            // {
            // Console.WriteLine(modifier.Name);
            // }
            var team = this.Teams.FirstOrDefault(x => x.Name == unit.Team);
            if (team == null)
            {
                team = new AbilityTeam(unit.Team) { OtherTeams = new List<IAbilityTeam>(this.Teams) };
                team.OtherTeams.Add(this.LocalTeam);
                foreach (var abilityTeam in this.Teams)
                {
                    abilityTeam.OtherTeams.Add(team);
                }

                this.Teams.Add(team);
                this.LocalTeam.OtherTeams.Add(team);
                this.TeamAdd.Next(team);
            }

            var abilityUnit = this.AbilityFactory.Value.CreateNewUnit(unit, team);

            this.AssignSkills(unit, abilityUnit);

            if (abilityUnit.IsEnemy)
            {
                this.enemies.Add(unit.Handle, abilityUnit);
            }
            else
            {
                this.allies.Add(unit.Handle, abilityUnit);
            }

            this.units.Add(unit.Handle, abilityUnit);
            this.OnUnitAdded(new UnitEventArgs { AbilityUnit = abilityUnit });
            this.abilityUnitProvider.Next(abilityUnit);
        }

        /// <summary>The menu.</summary>
        /// <returns>The <see cref="GetMenu" />.</returns>
        public Menu GetMenu()
        {
            return null;
        }

        /// <summary>
        ///     The on add entity.
        /// </summary>
        /// <param name="args">
        ///     The args.
        /// </param>
        public void OnAddEntity(EntityEventArgs args)
        {
            if (!args.Entity.IsValid)
            {
                return;
            }

            var unit = args.Entity as Unit;
            var isHero = unit is Hero;

            // if (args.Entity is Creep
            // && (args.Entity.ClassId == ClassID.CDOTA_BaseNPC_Creep_Lane
            // || args.Entity.ClassId == ClassID.CDOTA_BaseNPC_Creep_Siege))
            // {
            // foreach (var abilityUnit in this.controllableUnits)
            // {
            // var id = abilityUnit.Value.Pathfinding.AddObstacle(unit.Position, unit.RingRadius);
            // abilityUnit.Value.UnitControl.UnitObstacles.Add(id, unit);
            // }
            // }
            if (unit != null && unit.IsValid && unit.Team != GlobalVariables.EnemyTeam && unit.IsControllable
                && !(unit is Courier) && (!isHero || !unit.IsIllusion))
            {
                this.AddUnit(unit);
                return;
            }

            if (unit != null && unit.IsValid && unit.Team == GlobalVariables.EnemyTeam && isHero)
            {
                this.AddUnit(unit);

                // foreach (var keyValuePair in this.controllableUnits)
                // {
                // keyValuePair.Value.DamageDealtDictionary[unit.Handle] = 0;
                // foreach (var valuePair in keyValuePair.Value.SkillBook.AllSkills)
                // {
                // valuePair.Value.CastData.DamageDealtDictionary[unit.Handle] = 0;
                // }
                // }
                return;
            }

            var skill = args.Entity as Ability;
            if (skill == null || !skill.IsValid || skill.Owner == null || !skill.Owner.IsValid)
            {
                return;
            }

            var owner = skill.Owner as Unit;
            if (owner == null)
            {
                return;
            }

            if (owner.Team == GlobalVariables.EnemyTeam)
            {
                IAbilityUnit enemy;
                if (this.enemies.TryGetValue(owner.Handle, out enemy))
                {
                    if (!(skill is Item) && skill.Name.Contains("special_bonus"))
                    {
                        if (enemy.SkillBook.Talents.ContainsKey(skill.Handle))
                        {
                            return;
                        }

                        var abilityTalent = this.AbilityFactory.Value.CreateNewTalent(skill, enemy);
                        enemy.SkillBook.AddTalent(abilityTalent);
                        return;
                    }

                    if (!enemy.SkillBook.AllSkills.ContainsKey(skill.Handle)
                        && (skill is Item || owner.ClassId == ClassId.CDOTA_Unit_Hero_Rubick
                            || owner.ClassId == ClassId.CDOTA_Unit_Hero_DoomBringer) && enemy.SkillBook.IsValid(skill))
                    {

                        var abilitySkill = this.AbilityFactory.Value.CreateNewSkill(skill, enemy);
                        if (abilitySkill == null)
                        {
                            return;
                        }
                        enemy.SkillBook.AddSkill(abilitySkill);
                        this.OnSkillAdded(new SkillEventArgs { AbilitySkill = abilitySkill });
                    }
                }

                return;
            }

            IAbilityUnit ally;
            if (this.allies.TryGetValue(owner.Handle, out ally) || this.controllableUnits.TryGetValue(owner.Handle, out ally))
            {
                if (!(skill is Item) && skill.Name.Contains("special_bonus"))
                {
                    if (ally.SkillBook.Talents.ContainsKey(skill.Handle))
                    {
                        return;
                    }

                    var abilityTalent = this.AbilityFactory.Value.CreateNewTalent(skill, ally);
                    ally.SkillBook.AddTalent(abilityTalent);
                    return;
                }

                if (!ally.SkillBook.AllSkills.ContainsKey(skill.Handle)
                    && (skill is Item || owner.ClassId == ClassId.CDOTA_Unit_Hero_Rubick
                        || owner.ClassId == ClassId.CDOTA_Unit_Hero_DoomBringer) && ally.SkillBook.IsValid(skill))
                {

                    var abilitySkill = this.AbilityFactory.Value.CreateNewControllableSkill(skill, ally);
                    if (abilitySkill == null)
                    {
                        return;
                    }
                    ally.SkillBook.AddSkill(abilitySkill);
                    this.OnSkillAdded(new SkillEventArgs { AbilitySkill = abilitySkill });
                }
            }
            //DelayAction.Add(
            //    100,
            //    () =>
            //        {
                        
            //        });
        }

        /// <summary>
        ///     The on close.
        /// </summary>
        public void OnClose()
        {
            //this.ui.Dispose();
            //this.ui = null;

            ObjectManager.OnAddEntity -= this.OnAddEntity;
            ObjectManager.OnRemoveEntity -= this.OnRemoveEntity;

            // Game.OnUpdate -= this.Game_OnUpdate;
            // Drawing.OnDraw -= this.Drawing_OnDraw;
            this.temporarySkills.Clear();
            this.controllableUnits.Clear();
            this.allies.Clear();
            this.enemies.Clear();
            this.units.Clear();
            this.skills.Clear();
        }

        /// <summary>
        ///     The on load.
        /// </summary>
        public void OnLoad()
        {
            this.LocalTeam = new AbilityTeam(GlobalVariables.Team);
            var enemyTeam = new AbilityTeam(GlobalVariables.EnemyTeam)
                                {
                                   OtherTeams = new List<IAbilityTeam> { this.LocalTeam } 
                                };
            this.LocalTeam.OtherTeams.Add(enemyTeam);
            this.Teams = new List<IAbilityTeam> { this.LocalTeam, enemyTeam };

            // foreach (var unit in ObjectManager.GetEntities<Unit>())
            // {
            // if (!(unit is Hero) || unit.Team == GlobalVariables.EnemyTeam && !(unit is Hero)
            // || unit.ClassId == ClassID.CDOTA_BaseNPC_Creep_Lane
            // || unit.ClassId == ClassID.CDOTA_BaseNPC_Creep_Siege || unit.IsIllusion)
            // {
            // continue;
            // }

            // this.AddUnit(unit);
            // }

            // var delay = Game.GameTime < 0 ? 3000 : 500;
            // DelayAction.Add(
            // delay,
            // () =>
            // {

            // });
            foreach (var hero in Heroes.All)
            {
                this.AddUnit(hero);
            }

            foreach (var hero in Heroes.All)
            {
                foreach (var heroModifier in hero.Modifiers)
                {
                    this.Unit_OnModifierAdded(hero, new ModifierChangedEventArgs(heroModifier));
                }
            }

            foreach (var entity in ObjectManager.GetEntities<Unit>())
            {
                if (entity.IsValid && !(entity is Hero) && !(entity is Courier) && entity.Team == this.LocalTeam.Name && entity.IsControllable)
                {
                    //if (entity.Name == "npc_dota_lone_druid_bear1")
                    //{
                    //    Console.WriteLine(entity.GetType());
                    //}
                    //Console.WriteLine(entity.Name);
                    this.AddUnit(entity);
                }
            }

            var size = new Vector2((float)(HUDInfo.ScreenSizeX() / 2.3), HUDInfo.ScreenSizeY() / 2);
            //this.ui =
            //    new AbilityManagerUserInterface(
            //        new Vector2(HUDInfo.ScreenSizeX() - size.X - 10, (float)(HUDInfo.ScreenSizeY() / 2 - size.Y / 1.5)),
            //        size,
            //        this);

            this.TeamAdd.Next(this.LocalTeam);
            this.TeamAdd.Next(enemyTeam);
            ObjectManager.OnAddEntity += this.OnAddEntity;
            ObjectManager.OnRemoveEntity += this.OnRemoveEntity;

            Unit.OnModifierAdded += this.Unit_OnModifierAdded;
            Unit.OnModifierRemoved += this.Unit_OnModifierRemoved;

            // Game.OnUpdate += this.Game_OnUpdate;
            // Drawing.OnDraw += this.Drawing_OnDraw;
        }

        private Dictionary<double, IAbilityModifier> modifiers = new Dictionary<double, IAbilityModifier>();

        private void Unit_OnModifierRemoved(Unit sender, ModifierChangedEventArgs args)
        {
            return;
            var hero = sender as Hero;
            if (hero == null)
            {
                return;
            }

            IAbilityModifier abilityModifier;
            if (!this.modifiers.TryGetValue(args.Modifier.Handle + sender.Handle, out abilityModifier))
            {
                return;
            }

            abilityModifier.Dispose();
            this.modifiers.Remove(args.Modifier.Handle);
        }

        private void Unit_OnModifierAdded(Unit sender, ModifierChangedEventArgs args)
        {
            return;
            var hero = sender as Hero;
            if (hero == null)
            {
                return;
            }

            Console.WriteLine(
                "name: " + args.Modifier.Name + " sender: " + sender.Name + " caster: " + args.Modifier.Caster?.Name
                + " ability: " + args.Modifier.Ability?.Name + " parent: " + args.Modifier.Parent?.Name + " owner: "
                + args.Modifier.Owner?.Name + " ");

            var affectedUnit = this.Units[hero.Handle];
            var abilityModifier = this.AbilityFactory.Value.CreateNewModifier(args.Modifier, affectedUnit);

            if (!affectedUnit.DataReceiver.SelfModifierGenerators.Any(x => x.Value.TryGenerateModifier(abilityModifier))
                && !this.modifierGenerators.Any(
                    modifierGenerator => modifierGenerator.Value.TryGenerateModifier(abilityModifier)))
            {
                return;
            }

            this.modifiers.Add(abilityModifier.ModifierHandle + sender.Handle, abilityModifier);


        }

        // private void Game_OnUpdate(EventArgs args)
        // {
        // if (Game.IsPaused)
        // {
        // return;
        // }

        // foreach (var abilityUnit in this.units)
        // {
        // if (abilityUnit.Value.UnitControl != null)
        // {
        // this.obstacleUpdater.Update(abilityUnit.Value);
        // }

        // abilityUnit.Value.Updater.Update();
        // }
        // }

        /// <summary>
        ///     The on remove entity.
        /// </summary>
        /// <param name="args">
        ///     The args.
        /// </param>
        public void OnRemoveEntity(EntityEventArgs args)
        {
            var unit = args.Entity as Unit;
            if (unit != null)
            {
                this.RemoveUnit(unit);
                return;
            }

            var skill = args.Entity.ClassId == ClassId.CDOTA_Item || args.Entity is Ability || args.Entity is Item
                            ? args.Entity as Ability
                            : null;

            var owner = skill?.Owner as Unit;

            // Console.WriteLine((skill == null) + " " + (owner == null) + " " + (args.Entity.ClassId == ClassID.CDOTA_Item) + " " + args.Entity.ClassId);
            if (owner == null)
            {
                if (skill == null)
                {
                    return;
                }

                foreach (var abilityUnit in this.Units)
                {
                    var controllable = abilityUnit.Value;
                    if (controllable == null)
                    {
                        var uncontrollable = abilityUnit.Value;
                        if (uncontrollable == null)
                        {
                            continue;
                        }

                        if (!uncontrollable.SkillBook.AllSkills.ContainsKey(skill.Handle))
                        {
                            continue;
                        }

                        var abilitySkill = uncontrollable.SkillBook.AllSkills[skill.Handle];
                        this.OnSkillRemoved(new SkillEventArgs { AbilitySkill = abilitySkill });
                        uncontrollable.SkillBook.RemoveSkill(abilitySkill);
                        return;
                    }

                    if (!controllable.SkillBook.AllSkills.ContainsKey(skill.Handle))
                    {
                        continue;
                    }

                    var controllableskill = controllable.SkillBook.AllSkills[skill.Handle];
                    this.OnSkillRemoved(new SkillEventArgs { AbilitySkill = controllableskill });
                    controllable.SkillBook.RemoveSkill(controllableskill);
                    return;
                }

                return;
            }

            foreach (var abilityUnit in this.Units)
            {
                var controllable = abilityUnit.Value;
                if (controllable == null)
                {
                    var uncontrollable = abilityUnit.Value;
                    if (uncontrollable == null)
                    {
                        return;
                    }

                    if (!abilityUnit.Key.Equals(owner.Handle))
                    {
                        continue;
                    }

                    if (!uncontrollable.SkillBook.AllSkills.ContainsKey(skill.Handle))
                    {
                        return;
                    }

                    var abilitySkill = uncontrollable.SkillBook.AllSkills[skill.Handle];
                    this.OnSkillRemoved(new SkillEventArgs { AbilitySkill = abilitySkill });
                    uncontrollable.SkillBook.RemoveSkill(abilitySkill);
                    return;
                }

                if (!abilityUnit.Key.Equals(owner.Handle))
                {
                    continue;
                }

                if (!controllable.SkillBook.AllSkills.ContainsKey(skill.Handle))
                {
                    return;
                }

                var controllableskill = controllable.SkillBook.AllSkills[skill.Handle];
                this.OnSkillRemoved(new SkillEventArgs { AbilitySkill = controllableskill });
                controllable.SkillBook.RemoveSkill(controllableskill);
                return;
            }
        }
        
        private Dictionary<double, IModifierGenerator> modifierGenerators = new Dictionary<double, IModifierGenerator>();

        private DataObserver<IAbilitySkillPart> abilitySkillPartObserver;

        public virtual void OnSkillAdded(SkillEventArgs args)
        {
            if (!args.AbilitySkill.IsTemporary)
            {
                if (this.skills.ContainsKey(args.AbilitySkill.SkillHandle))
                {
                    this.skills[args.AbilitySkill.SkillHandle] = args.AbilitySkill;
                }
                else
                {
                    this.skills.Add(args.AbilitySkill.SkillHandle, args.AbilitySkill);
                }
            }

            if (args.AbilitySkill.ModifierGenerator != null
                && args.AbilitySkill.ModifierGenerator.Workers.Any(
                    x => x.AffectsAllies || x.AffectsEnemies || x.AffectsEveryone))
            {
                this.modifierGenerators.Add(args.AbilitySkill.SkillHandle, args.AbilitySkill.ModifierGenerator);
            }

            args.AbilitySkill.PartAdded.Subscribe(this.abilitySkillPartObserver);

            DelayAction.Add(50, () => this.SkillAdded?.Invoke(args));
        }

        private void AbilitySkillPartAdded(IAbilitySkillPart part)
        {
            var modifierGenerator = part as IModifierGenerator;
            if (modifierGenerator != null && modifierGenerator.Workers.Any(
                    x => x.AffectsAllies || x.AffectsEnemies || x.AffectsEveryone))
            {
                this.modifierGenerators.Add(part.Skill.SkillHandle, modifierGenerator);
            }
        }

        /// <summary>
        ///     The remove skill.
        /// </summary>
        /// <param name="skill">
        ///     The skill.
        /// </param>
        public void RemoveTemporarySkill(string skillName)
        {
            this.OnSkillRemoved(new SkillEventArgs { AbilitySkill = this.temporarySkills[skillName] });
            this.temporarySkills.Remove(skillName);
        }

        /// <summary>
        ///     The remove unit.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        public void RemoveUnit(Unit unit)
        {
            var team = this.Teams.FirstOrDefault(x => x.Name == unit.Team);
            if (this.controllableUnits.ContainsKey(unit.Handle))
            {
                var abilityUnit = this.controllableUnits[unit.Handle];
                team?.UnitManager.RemoveUnit(abilityUnit);
                if (team?.UnitManager.Units.Count(x => x.Value != null && x.Value != abilityUnit) <= 0)
                {
                    team.Dispose();
                    this.Teams.Remove(team);
                }

                foreach (var skill in abilityUnit.SkillBook.AllSkills)
                {
                    this.OnSkillRemoved(new SkillEventArgs { AbilitySkill = skill.Value });
                }

                abilityUnit.SkillBook.Dispose();
                this.OnUnitRemoved(new UnitEventArgs { AbilityUnit = abilityUnit });
                this.controllableUnits.Remove(unit.Handle);
                this.units.Remove(unit.Handle);
                return;
            }

            if (this.enemies.ContainsKey(unit.Handle))
            {
                var abilityUnit = this.enemies[unit.Handle];
                team?.UnitManager.RemoveUnit(abilityUnit);
                if (team?.UnitManager.Units.Count(x => x.Value != null && x.Value != abilityUnit) <= 0)
                {
                    team.Dispose();
                    this.Teams.Remove(team);
                }

                foreach (var skill in abilityUnit.SkillBook.AllSkills)
                {
                    this.OnSkillRemoved(new SkillEventArgs { AbilitySkill = skill.Value });
                }

                abilityUnit.SkillBook.Dispose();
                this.OnUnitRemoved(new UnitEventArgs { AbilityUnit = abilityUnit });
                this.enemies.Remove(unit.Handle);
                this.units.Remove(unit.Handle);
                return;
            }

            if (this.allies.ContainsKey(unit.Handle))
            {
                var abilityUnit = this.allies[unit.Handle];
                team?.UnitManager.RemoveUnit(abilityUnit);
                if (team?.UnitManager.Units.Count(x => x.Value != null && x.Value != abilityUnit) <= 0)
                {
                    team.Dispose();
                    this.Teams.Remove(team);
                }

                foreach (var skill in abilityUnit.SkillBook.Skills)
                {
                    this.OnSkillRemoved(new SkillEventArgs { AbilitySkill = skill.Value });
                }

                abilityUnit.SkillBook.Dispose();
                this.OnUnitRemoved(new UnitEventArgs { AbilityUnit = abilityUnit });
                this.allies.Remove(unit.Handle);
                this.units.Remove(unit.Handle);
                return;
            }
        }

        private DataProvider<IAbilityUnit> abilityUnitProvider = new DataProvider<IAbilityUnit>();

        /// <summary>Notifies the provider that an observer is to receive notifications.</summary>
        /// <returns>
        ///     A reference to an interface that allows observers to stop receiving notifications before the provider has
        ///     finished sending them.
        /// </returns>
        /// <param name="observer">The object that is to receive notifications.</param>
        public IDisposable Subscribe(IObserver<IAbilityUnit> observer)
        {
            return this.abilityUnitProvider.Subscribe(observer);
        }

        #endregion

        #region Methods

        protected virtual void OnSkillRemoved(SkillEventArgs args)
        {
            if (!args.AbilitySkill.IsTemporary)
            {
                this.skills.Remove(args.AbilitySkill.SkillHandle);
            }

            if (args.AbilitySkill.ModifierGenerator != null
                && args.AbilitySkill.ModifierGenerator.Workers.Any(
                    x => x.AffectsAllies || x.AffectsEnemies || x.AffectsEveryone))
            {
                this.modifierGenerators.Remove(args.AbilitySkill.SkillHandle);
            }

            this.SkillRemoved?.Invoke(args);
        }

        protected virtual void OnUnitAdded(UnitEventArgs args)
        {
            // foreach (var abilitySkill in this.skills)
            // {
            // abilitySkill.Value.CastData.CanUseOnDictionary.Add(args.AbilityUnit.UnitHandle, false);
            // abilitySkill.Value.CastData.DamageDealtDictionary[args.AbilityUnit.UnitHandle] = 0;
            // }
            this.UnitAdded?.Invoke(args);
        }

        protected virtual void OnUnitRemoved(UnitEventArgs args)
        {
            args.AbilityUnit.Dispose();
            this.UnitRemoved?.Invoke(args);
        }

        private void AddControllableUnit(Unit unit)
        {
            var abilityUnit = this.AbilityFactory.Value.CreateNewUnit(unit, this.LocalTeam);
            Console.WriteLine("adding unit " + abilityUnit.Name);
            this.AssignSkills(unit, abilityUnit);
            if (abilityUnit.IsCreep)
            {
                this.controllableUnits.Add(unit.Handle, abilityUnit);
            }
            else
            {
                this.allies.Add(unit.Handle, abilityUnit);
            }

            this.units.Add(unit.Handle, abilityUnit);
            this.OnUnitAdded(new UnitEventArgs { AbilityUnit = abilityUnit });
            this.abilityUnitProvider.Next(abilityUnit);

            if (abilityUnit.IsLocalHero)
            {
                this.LocalHero = abilityUnit;
            }
        }

        private void AssignSkills(Unit unit, IAbilityUnit abilityUnit)
        {
            try
            {
                foreach (var skill in unit.Spellbook.Spells)
                {
                    if (!skill.IsValid)
                    {
                        continue;
                    }

                    if (skill.Name.Contains("special_bonus"))
                    {
                        if (abilityUnit.SkillBook.Talents.ContainsKey(skill.Handle))
                        {
                            continue;
                        }

                        var abilityTalent = this.AbilityFactory.Value.CreateNewTalent(skill, abilityUnit);
                        abilityUnit.SkillBook.AddTalent(abilityTalent);
                        continue;
                    }

                    if (!abilityUnit.SkillBook.IsValid(skill))
                    {
                        continue;
                    }

                    if (skill.AbilityType != AbilityType.Attribute
                        && !abilityUnit.SkillBook.AllSkills.ContainsKey(skill.Handle))
                    {

                        var abilitySkill = this.AbilityFactory.Value.CreateNewSkill(skill, abilityUnit);
                        if (abilitySkill == null)
                        {
                            continue;
                        }

                        // if (abilitySkill.SkillControl.SkillCastingFunction == null)
                        // {
                        // continue;
                        // }
                        abilityUnit.SkillBook.AddSkill(abilitySkill);

                        this.OnSkillAdded(new SkillEventArgs { AbilitySkill = abilitySkill });
                    }
                }
            }
            catch (EntityNotFoundException)
            {
                Console.WriteLine("invalid spells for unit: " + unit.Name);
            }

            foreach (var physicalItem in ObjectManager.GetEntities<PhysicalItem>())
            {
                var owner = physicalItem.Item?.OldOwner ?? physicalItem.Item?.Owner;
                if (owner != null && owner.Handle.Equals(unit.Handle) && !physicalItem.Item.IsRecipe
                    && abilityUnit.SkillBook.IsValid(physicalItem.Item))
                {
                    var abilitySkill = this.AbilityFactory.Value.CreateNewSkill(physicalItem.Item, abilityUnit);
                    abilityUnit.SkillBook.AddSkill(abilitySkill);
                    this.OnSkillAdded(new SkillEventArgs { AbilitySkill = abilitySkill });
                }
            }

            if (unit.Inventory == null)
            {
                return;
            }

            if (unit.Inventory.Items != null)
            {
                var items = unit.Inventory.Items;
                foreach (var item in items)
                {
                    if (item.IsRecipe || !abilityUnit.SkillBook.IsValid(item))
                    {
                        continue;
                    }

                    // if (item.CommonProperties() != null)
                    // {
                    var abilitySkill = this.AbilityFactory.Value.CreateNewSkill(item, abilityUnit);

                    // if (abilitySkill.SkillControl.SkillCastingFunction == null)
                    // {
                    // continue;
                    // }
                    abilityUnit.SkillBook.AddSkill(abilitySkill);

                    this.OnSkillAdded(new SkillEventArgs { AbilitySkill = abilitySkill });

                    // }
                }
            }

            if (unit.Inventory.Backpack != null)
            {
                var items = unit.Inventory.Backpack;
                foreach (var item in items)
                {
                    if (item.IsRecipe || !abilityUnit.SkillBook.IsValid(item))
                    {
                        continue;
                    }

                    // if (item.CommonProperties() != null)
                    // {
                    var abilitySkill = this.AbilityFactory.Value.CreateNewSkill(item, abilityUnit);

                    // if (abilitySkill.SkillControl.SkillCastingFunction == null)
                    // {
                    // continue;
                    // }
                    abilityUnit.SkillBook.AddSkill(abilitySkill);

                    this.OnSkillAdded(new SkillEventArgs { AbilitySkill = abilitySkill });

                    // }
                }
            }

            if (unit.Inventory.Stash != null)
            {
                var items = unit.Inventory.Stash;
                foreach (var item in items)
                {
                    if (item.IsRecipe || !abilityUnit.SkillBook.IsValid(item))
                    {
                        continue;
                    }

                    // if (item.CommonProperties() != null)
                    // {
                    var abilitySkill = this.AbilityFactory.Value.CreateNewSkill(item, abilityUnit);

                    // if (abilitySkill.SkillControl.SkillCastingFunction == null)
                    // {
                    // continue;
                    // }
                    abilityUnit.SkillBook.AddSkill(abilitySkill);

                    this.OnSkillAdded(new SkillEventArgs { AbilitySkill = abilitySkill });

                    // }
                }
            }
        }

        private void Drawing_OnDraw(EventArgs args)
        {
            if (Game.IsPaused)
            {
                return;
            }

            foreach (var keyValuePair in this.Units)
            {
                keyValuePair.Value.OnDraw();
            }
        }

        #endregion
    }
}