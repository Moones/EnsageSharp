// <copyright file="ISkillCastingFunction.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.CastingFunctions
{
    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.ActionQueue.AbilityAction;

    using Ensage;

    using SharpDX;

    /// <summary>
    ///     The CastingFunction interface.
    /// </summary>
    public interface ISkillCastingFunction : IAbilitySkillPart
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The ability action.
        /// </summary>
        /// <param name="data">
        ///     The target.
        /// </param>
        /// <returns>
        ///     The <see cref="IAbilityAction" />.
        /// </returns>
        IAbilityAction AbilityAction(IAbilityUnit data);

        /// <summary>
        ///     The cancel.
        /// </summary>
        /// <param name="target">
        ///     The target.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        bool Cancel(IAbilityUnit target);

        /// <summary>
        ///     The can invoke.
        /// </summary>
        /// <param name="target">
        ///     The target.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        bool CanInvoke(IAbilityUnit target);

        /// <summary>
        ///     Confirms successful cast
        /// </summary>
        /// <param name="data">
        ///     The target.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        bool ConfirmSuccess(IAbilityUnit data);

        /// <summary>
        ///     The try invoke.
        /// </summary>
        /// <param name="data">
        ///     The target.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        bool TryInvoke(IAbilityUnit data);

        /// <summary>
        ///     The try invoke.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        bool TryInvoke(Unit unit);

        /// <summary>
        ///     The try invoke.
        /// </summary>
        /// <param name="position">
        ///     The position.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        bool TryInvoke(Vector3 position);

        /// <summary>
        ///     The try invoke.
        /// </summary>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        bool TryInvoke();

        /// <summary>
        ///     The wait time after cast.
        /// </summary>
        /// <param name="data">
        ///     The target.
        /// </param>
        /// <returns>
        ///     The <see cref="float" />.
        /// </returns>
        float WaitTime(IAbilityUnit data);

        #endregion
    }
}