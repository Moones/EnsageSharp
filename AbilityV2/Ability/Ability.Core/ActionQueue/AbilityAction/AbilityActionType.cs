// <copyright file="AbilityActionType.cs" company="EnsageSharp">
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
namespace Ability.Core.ActionQueue.AbilityAction
{
    /// <summary>
    ///     The ability action type.
    /// </summary>
    public enum AbilityActionType
    {
        /// <summary>
        ///     The none.
        /// </summary>
        None,

        /// <summary>
        ///     The cast.
        /// </summary>
        Cast,

        /// <summary>
        ///     The attack.
        /// </summary>
        Attack,

        /// <summary>
        ///     The move.
        /// </summary>
        Move,

        /// <summary>
        ///     The evade.
        /// </summary>
        Evade
    }
}