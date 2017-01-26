// <copyright file="ResourceImportAttribute.cs" company="EnsageSharp">
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
namespace Ability.Utilities.Resources
{
    using System;

    /// <summary>
    ///     Marks a field/class that needs it's data from a JSON resource. Populated at runtime.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property)]
    internal class ResourceImportAttribute : Attribute
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ResourceImportAttribute" /> class.
        /// </summary>
        public ResourceImportAttribute()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ResourceImportAttribute" /> class.
        /// </summary>
        /// <param name="file">The file.</param>
        public ResourceImportAttribute(string file)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            this.File = file;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the file.
        /// </summary>
        /// <value>
        ///     The file.
        /// </value>
        public string File { get; set; }

        /// <summary>
        ///     Gets or sets the filter.
        /// </summary>
        /// <value>
        ///     The filter.
        /// </value>
        public Type Filter { get; set; }

        #endregion
    }
}