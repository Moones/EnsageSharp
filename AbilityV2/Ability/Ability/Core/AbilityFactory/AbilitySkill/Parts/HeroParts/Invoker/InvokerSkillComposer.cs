// <copyright file="InvokerSkillComposer.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.HeroParts.Invoker
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillCastData;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillComposer;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillOverlay;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.HeroParts.Invoker.CastData;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.HeroParts.Invoker.Overlay;
    using Ability.Core.AbilityFactory.Metadata;

    using Ensage;
    using AbilityId = Ensage.Common.Enums.AbilityId;
    using Ensage.Common.Extensions;

    /// <summary>
    ///     The invoker skill composer.
    /// </summary>
    [Export(typeof(IAbilitySkillComposer))]
    [AbilitySkillMetadata(ClassID.CDOTA_Unit_Hero_Invoker, (uint)AbilityId.invoker_sun_strike,
        (uint)AbilityId.invoker_alacrity, (uint)AbilityId.invoker_chaos_meteor, (uint)AbilityId.invoker_forge_spirit,
        (uint)AbilityId.invoker_deafening_blast, (uint)AbilityId.invoker_ghost_walk, (uint)AbilityId.invoker_tornado,
        (uint)AbilityId.invoker_emp, (uint)AbilityId.invoker_cold_snap, (uint)AbilityId.invoker_ice_wall)]
    internal class InvokerSkillComposer : DefaultSkillComposer
    {
        #region Fields

        /// <summary>The orb count dictionary.</summary>
        private readonly Dictionary<AbilityId, uint[]> orbCountDictionary = new Dictionary<AbilityId, uint[]>
                                                                                {
                                                                                    {
                                                                                        AbilityId.invoker_sun_strike,
                                                                                        new uint[] { 0, 0, 3 }
                                                                                    },
                                                                                    {
                                                                                        AbilityId.invoker_alacrity,
                                                                                        new uint[] { 0, 2, 1 }
                                                                                    },
                                                                                    {
                                                                                        AbilityId.invoker_chaos_meteor,
                                                                                        new uint[] { 0, 1, 2 }
                                                                                    },
                                                                                    {
                                                                                        AbilityId.invoker_forge_spirit,
                                                                                        new uint[] { 1, 0, 2 }
                                                                                    },
                                                                                    {
                                                                                        AbilityId.invoker_deafening_blast,
                                                                                        new uint[] { 1, 1, 1 }
                                                                                    },
                                                                                    {
                                                                                        AbilityId.invoker_ghost_walk,
                                                                                        new uint[] { 2, 1, 0 }
                                                                                    },
                                                                                    {
                                                                                        AbilityId.invoker_tornado,
                                                                                        new uint[] { 1, 2, 0 }
                                                                                    },
                                                                                    {
                                                                                        AbilityId.invoker_emp,
                                                                                        new uint[] { 0, 3, 0 }
                                                                                    },
                                                                                    {
                                                                                        AbilityId.invoker_cold_snap,
                                                                                        new uint[] { 3, 0, 0 }
                                                                                    },
                                                                                    {
                                                                                        AbilityId.invoker_ice_wall,
                                                                                        new uint[] { 2, 0, 1 }
                                                                                    }
                                                                                };

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="InvokerSkillComposer" /> class.</summary>
        public InvokerSkillComposer()
        {
            this.AssignPart<ISkillCastData>(
                skill =>
                    {
                        var orbs = this.orbCountDictionary[skill.SourceAbility.GetAbilityId()];
                        return new InvokerSkillCastData(skill, orbs[0], orbs[1], orbs[2]);
                    });
            this.AssignPart<ISkillOverlayProvider>(skill => new InvokerSkillOverlayProvider(skill));
        }

        #endregion
    }
}
