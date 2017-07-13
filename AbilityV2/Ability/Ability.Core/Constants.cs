// <copyright file="Constants.cs" company="EnsageSharp">
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
namespace Ability.Core
{
    using System;
    using System.Globalization;
    using System.IO;

    /// <summary>
    ///     The constants.
    /// </summary>
    public class Constants
    {
        #region Static Fields

        /// <summary>
        ///     The app data.
        /// </summary>
        public static readonly string AppDataFolder =
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "LS" + Environment.UserName.GetHashCode().ToString("X", CultureInfo.CurrentCulture));

        /// <summary>
        ///     The assembly name.
        /// </summary>
        public static readonly string AssemblyName = "Ability#";

        /// <summary>
        ///     The log directory.
        /// </summary>
        public static readonly string LogDirectory = Path.Combine(AppDataFolder, "Logs", "Ability");

        /// <summary>
        ///     The log file name.
        /// </summary>
        public static readonly string LogFileName = DateTime.Now.ToString("d", CultureInfo.CurrentCulture)
                                                        .Replace('/', '-') + ".log";

        #endregion

        #region Constructors and Destructors

        private Constants()
        {
        }

        #endregion
    }
}