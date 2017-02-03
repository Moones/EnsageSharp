// <copyright file="AbilityUnit.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using Ability.Core.AbilityFactory.AbilitySkill;
    using Ability.Core.AbilityFactory.AbilityTeam;
    using Ability.Core.AbilityFactory.AbilityUnit.Data;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Composer;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Health;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.IconDrawer;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Interaction;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Level;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Mana;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Modifiers;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Overlay;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Position;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.PositionTracker;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.ScreenInfo;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.SkillBook;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.UnitControl;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.UnitDataReceiver;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Visibility;

    using Ensage;
    using Ensage.Common.Objects;

    /// <summary>
    ///     The ability unit.
    /// </summary>
    public abstract class AbilityUnit : IAbilityUnit
    {
        #region Fields

        private readonly Dictionary<Type, IAbilityUnitPart> parts = new Dictionary<Type, IAbilityUnitPart>();

        /// <summary>The data receiver.</summary>
        private IUnitDataReceiver dataReceiver;

        /// <summary>The health.</summary>
        private IHealth health;

        /// <summary>The drawer.</summary>
        private IUnitIconDrawer iconDrawer;

        private IUnitInteraction interaction;

        /// <summary>The last intersection data.</summary>
        private ObstacleIntersection lastIntersectionData;

        private IUnitLevel level;

        /// <summary>The mana.</summary>
        private IMana mana;

        /// <summary>The modifiers.</summary>
        private IModifiers modifiers;

        /// <summary>The overlay.</summary>
        private IUnitOverlay overlay;

        private IOverlayEntryProvider overlayEntryProvider;

        /// <summary>The position.</summary>
        private IPosition position;

        /// <summary>The particle tracker.</summary>
        private IPositionTracker positionTracker;

        private IScreenInfo screenInfo;

        /// <summary>The skill book.</summary>
        private ISkillBook<IAbilitySkill> skillBook;

        /// <summary>The unit control.</summary>
        private IUnitControl unitControl;

        /// <summary>The visibility.</summary>
        private IVisibility visibility;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AbilityUnit" /> class.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        protected AbilityUnit(Unit unit)
        {
            this.UnitHandle = unit.Handle;
            this.SourceUnit = unit;
            this.UnitHandleString = unit.Handle.ToString(CultureInfo.CurrentCulture);
            this.Name = unit.StoredName();

            // foreach (var hero in Heroes.GetByTeam(GlobalVariables.EnemyTeam))
            // {
            // this.DamageDealtDictionary.Add(hero.Handle, 0);
            // }
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the data receiver.
        /// </summary>
        public IUnitDataReceiver DataReceiver { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether debug draw.
        /// </summary>
        public bool DebugDraw { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether draw.
        /// </summary>
        public bool Draw { get; set; }

        /// <summary>
        ///     Gets or sets the health.
        /// </summary>
        public IHealth Health { get; set; }

        /// <summary>
        ///     Gets or sets the drawer.
        /// </summary>
        public IUnitIconDrawer IconDrawer { get; set; }

        /// <summary>
        ///     Gets or sets the interaction.
        /// </summary>
        public IUnitInteraction Interaction { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether is enemy.
        /// </summary>
        public bool IsEnemy { get; set; }

        /// <summary>Gets a value indicating whether is local hero.</summary>
        public bool IsLocalHero { get; set; }

        /// <summary>
        ///     Gets or sets the level.
        /// </summary>
        public IUnitLevel Level { get; set; }

        /// <summary>
        ///     Gets or sets the mana.
        /// </summary>
        public IMana Mana { get; set; }

        /// <summary>
        ///     Gets or sets the modifiers.
        /// </summary>
        public IModifiers Modifiers { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the overlay.
        /// </summary>
        public IUnitOverlay Overlay { get; set; }

        /// <summary>
        ///     Gets or sets the overlay entry provider.
        /// </summary>
        public IOverlayEntryProvider OverlayEntryProvider { get; set; }

        /// <summary>Gets the parts.</summary>
        public IReadOnlyDictionary<Type, IAbilityUnitPart> Parts => this.parts;

        /// <summary>
        ///     Gets or sets the position.
        /// </summary>
        public IPosition Position { get; set; }

        /// <summary>
        ///     Gets or sets the fog of war exploit.
        /// </summary>
        public IPositionTracker PositionTracker { get; set; }

        /// <summary>
        ///     Gets or sets the screen position.
        /// </summary>
        public IScreenInfo ScreenInfo { get; set; }

        /// <summary>
        ///     Gets or sets the skill book.
        /// </summary>
        public ISkillBook<IAbilitySkill> SkillBook { get; set; }

        /// <summary>
        ///     Gets or sets the source unit.
        /// </summary>
        public Unit SourceUnit { get; set; }

        /// <summary>
        ///     Gets or sets the team.
        /// </summary>
        public IAbilityTeam Team { get; set; }

        /// <summary>Gets or sets the unit composer.</summary>
        public IAbilityUnitComposer UnitComposer { get; set; }

        /// <summary>
        ///     Gets or sets the unit control.
        /// </summary>
        public IUnitControl UnitControl { get; set; }

        /// <summary>
        ///     Gets or sets the unit handle.
        /// </summary>
        public uint UnitHandle { get; set; }

        /// <summary>
        ///     Gets or sets the unit handle string.
        /// </summary>
        public string UnitHandleString { get; set; }

        /// <summary>
        ///     Gets or sets the visibility.
        /// </summary>
        public IVisibility Visibility { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>The add part.</summary>
        /// <typeparam name="T">The type of part</typeparam>
        /// <param name="partFactory">The part Factory.</param>
        public void AddPart<T>(Func<IAbilityUnit, T> partFactory) where T : IAbilityUnitPart
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

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            foreach (var keyValuePair in this.Parts)
            {
                keyValuePair.Value.Dispose();
            }
        }

        /// <summary>The get part.</summary>
        /// <typeparam name="T">The type of part</typeparam>
        /// <returns>The <see cref="T" />.</returns>
        public T GetPart<T>() where T : IAbilityUnitPart
        {
            IAbilityUnitPart part;
            if (!this.Parts.TryGetValue(typeof(T), out part))
            {
                return (T)part;
            }

            return (T)part;
        }

        /// <summary>
        ///     The on draw.
        /// </summary>
        public virtual void OnDraw()
        {
            // if (Game.IsPaused)
            // {
            // return;
            // }
            // this.Overlay.OnDraw();
        }

        /// <summary>The remove part.</summary>
        /// <typeparam name="T">The type of part</typeparam>
        public void RemovePart<T>() where T : IAbilityUnitPart
        {
            this.parts.Remove(typeof(T));
        }

        #endregion

        #region Methods

        private void Drawing_OnDraw(EventArgs args)
        {
            // if (!this.SourceUnit.IsValid)
            // {
            // return;
            // }

            // var heroPos = this.SourceUnit.Position;
            // var heroScreenpos =
            // Drawing.WorldToScreen(
            // new Vector3(
            // this.SourceUnit.Position.X,
            // this.SourceUnit.Position.Y,
            // this.SourceUnit.Position.Z + this.SourceUnit.HealthBarOffset)) + new Vector2(-60, -190);

            // // Drawing the navmesh grid
            // const int CellCount = 25;
            // for (var i = 0; i < CellCount; ++i)
            // {
            // for (var j = 0; j < CellCount; ++j)
            // {
            // Vector2 p;
            // p.X = this.Pathfinding.CellSize * (i - CellCount / 2) + heroPos.X;
            // p.Y = this.Pathfinding.CellSize * (j - CellCount / 2) + heroPos.Y;

            // Color c;
            // if (i == CellCount / 2 && j == CellCount / 2) c = Color.Blue - new Color(0, 0, 0, 70);
            // else
            // {
            // var isFlying = this.SourceUnit.MoveCapability == MoveCapability.Fly
            // || this.SourceUnit.IsUnitState(UnitState.Flying);
            // var flag = this.Pathfinding.GetCellFlags(p);
            // if (!isFlying && flag.HasFlag(NavMeshCellFlags.Walkable))
            // {
            // c = flag.HasFlag(NavMeshCellFlags.Tree)
            // ? Color.Purple - new Color(0, 0, 0, 70)
            // : Color.Green - new Color(0, 0, 0, 70);
            // if (flag.HasFlag(NavMeshCellFlags.GridFlagObstacle)) c = Color.BlanchedAlmond - new Color(0, 0, 0, 70);
            // }
            // else if (isFlying && !flag.HasFlag(NavMeshCellFlags.MovementBlocker)) c = Color.Green - new Color(0, 0, 0, 70);
            // else c = Color.Red - new Color(0, 0, 0, 70);
            // }

            // Drawing.DrawRect(
            // heroScreenpos + new Vector2(i * 5, 30 + (CellCount - j - 1) * 5),
            // new Vector2(5, 5),
            // c,
            // false);
            // }
            // }
        }

        #endregion
    }
}