// <copyright file="AbilityUnitComposer.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Composer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Ability.Core.AbilityFactory.AbilitySkill;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Drawer;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Health;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Level;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Mana;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Modifiers;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Overlay;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Position;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.PositionTracker;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.ScreenInfo;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.SkillBook;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.UnitDataReceiver;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Visibility;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.Invoker.SkillBook;
    using Ability.Core.AbilityFactory.Metadata;

    /// <summary>
    ///     The ability unit composer.
    /// </summary>
    public class AbilityUnitComposer : IAbilityUnitComposer
    {
        /// <summary>Initializes a new instance of the <see cref="AbilityUnitComposer"/> class.</summary>
        public AbilityUnitComposer()
        {
            this.AssignPart<IOverlayEntryProvider>(abilityUnit => new OverlayEntryProvider(abilityUnit));
            this.AssignPart<ISkillBook<IAbilitySkill>>(abilityUnit => new SkillBook<IAbilitySkill>(abilityUnit));
            this.AssignPart<IScreenInfo>(abilityUnit => new ScreenInfo(abilityUnit));
            this.AssignPart<IHealth>(abilityUnit => new Health(abilityUnit));
            this.AssignPart<IMana>(abilityUnit => new Mana(abilityUnit));
            this.AssignPart<IPosition>(abilityUnit => new Position(abilityUnit));
            this.AssignPart<IUnitDrawer>(abilityUnit => new UnitDrawer(abilityUnit));
            this.AssignPart<IPositionTracker>(abilityUnit => new PositionTracker(abilityUnit));
            this.AssignPart<IModifiers>(abilityUnit => new Modifiers(abilityUnit));
            this.AssignPart<IUnitLevel>(abilityUnit => new UnitLevel(abilityUnit));
            this.AssignPart<IUnitOverlay>(abilityUnit => new UnitOverlay(abilityUnit));
            this.AssignPart<IVisibility>(abilityUnit => new Visibility(abilityUnit));
            this.AssignPart<IUnitDataReceiver>(abilityUnit => new UnitDataReceiver(abilityUnit));
        }

        #region Public Properties

        /// <summary>Gets the assignments.</summary>
        public IDictionary<Type, Action<IAbilityUnit>> Assignments { get; } =
            new Dictionary<Type, Action<IAbilityUnit>>();

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The compose.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        public void Compose(IAbilityUnit unit)
        {
            // if (unit.SourceUnit.IsControllable && !unit.IsEnemy)
            // {
            // this.AssignPart<IUnitControl>(abilityUnit => new UnitControl(abilityUnit));
            // }

            foreach (var keyValuePair in this.Assignments)
            {
                keyValuePair.Value.Invoke(unit);
            }

            foreach (var keyValuePair in unit.Parts)
            {
                keyValuePair.Value.Initialize();
            }

            // unit.Interaction = new UnitInteraction(unit);
            // unit.Updater = new AbilityUnitUpdater(unit);
        }

        /// <summary>The assign part.</summary>
        /// <param name="factory">The factory.</param>
        /// <typeparam name="T">The type of part</typeparam>
        public void AssignPart<T>(Func<IAbilityUnit, T> factory) where T : IAbilityUnitPart
        {
            var type = typeof(T);
            this.Assignments[type] = unit => unit.AddPart(factory);
        }

        #endregion
    }
}