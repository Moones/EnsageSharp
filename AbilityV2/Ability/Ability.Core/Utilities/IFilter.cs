// <copyright file="IFilter.cs" company="EnsageSharp">
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
namespace Ability.Core.Utilities
{
    /// <summary>
    ///     The Filter interface.
    /// </summary>
    internal interface IFilter
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The apply.
        /// </summary>
        /// <param name="data">
        ///     The data.
        /// </param>
        /// <returns>
        ///     The <see cref="object" />.
        /// </returns>
        object Apply(object data);

        #endregion
    }

    /// <summary>
    ///     The Filter interface.
    /// </summary>
    /// <typeparam name="T">
    ///     The T
    /// </typeparam>
    internal interface IFilter<T>
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The apply.
        /// </summary>
        /// <param name="data">
        ///     The data.
        /// </param>
        /// <returns>
        ///     The <see cref="T" />.
        /// </returns>
        T Apply(T data);

        #endregion
    }
}