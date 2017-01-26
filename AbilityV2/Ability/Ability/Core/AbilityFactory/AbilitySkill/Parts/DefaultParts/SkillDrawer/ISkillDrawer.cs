// <copyright file="ISkillDrawer.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillDrawer
{
    using SharpDX;

    /// <summary>
    ///     The SkillDrawer interface.
    /// </summary>
    public interface ISkillDrawer : IAbilitySkillPart
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The on draw.
        /// </summary>
        /// <param name="position">
        ///     The position.
        /// </param>
        /// <param name="size">
        ///     The size.
        /// </param>
        void DrawIcon(Vector2 position, Vector2 size);

        /// <summary>
        ///     The draw icon overlay.
        /// </summary>
        /// <param name="position">
        ///     The position.
        /// </param>
        /// <param name="size">
        ///     The size.
        /// </param>
        void DrawIconOverlay(Vector2 position, Vector2 size);

        #endregion
    }
}