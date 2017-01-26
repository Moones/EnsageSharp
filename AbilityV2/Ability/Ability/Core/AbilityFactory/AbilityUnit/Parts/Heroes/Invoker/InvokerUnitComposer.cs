// <copyright file="InvokerUnitComposer.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.Invoker
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    using Ability.Core.AbilityFactory.AbilitySkill;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Composer;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Modifiers;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Overlay;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.PositionTracker;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.PositionTracker.Types;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.ScreenInfo;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.SkillBook;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.Invoker.Modifiers;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.Invoker.Overlay;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.Invoker.PositionTracker;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.Invoker.SkillBook;
    using Ability.Core.AbilityFactory.Metadata;

    using Ensage;

    /// <summary>
    ///     The invoker unit composer.
    /// </summary>
    [Export(typeof(IAbilityUnitComposer))]
    [AbilityUnitMetadata(ClassID.CDOTA_Unit_Hero_Invoker)]
    public class InvokerUnitComposer : AbilityUnitComposer
    {
        /// <summary>Initializes a new instance of the <see cref="InvokerUnitComposer"/> class.</summary>
        public InvokerUnitComposer()
        {
            this.AssignPart<IUnitOverlay>(unit => new InvokerUnitOverlay(unit));
            this.AssignPart<ISkillBook<IAbilitySkill>>(unit => new InvokerSkillBook(unit));
            this.AssignPart<IModifiers>(unit => new InvokerModifiers(unit));
            this.AssignPart<IPositionTracker>(unit => new InvokerPositionTracker(unit));
        }

        /// <summary>The custom composition.</summary>
        /// <param name="unit">The unit.</param>
        //public override void CustomComposition(IAbilityUnit unit)
        //{
        //    unit.Overlay = new InvokerUnitOverlay(unit);
        //    unit.SkillBook = new InvokerSkillBook(unit);
        //    unit.Modifiers = new InvokerModifiers(unit);
        //}
    }
}