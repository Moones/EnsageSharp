// <copyright file="ResourceFactory.cs" company="EnsageSharp">
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
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Security.Permissions;
    using System.Text;

    /// <summary>
    ///     Obtrieves resources.
    /// </summary>
    public static class ResourceFactory
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Gets a byte resource.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="assembly">The assembly.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        [PermissionSet(SecurityAction.Assert, Unrestricted = true)]
        public static byte[] ByteResource(string file, Assembly assembly = null)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            if (assembly == null)
            {
                assembly = Assembly.GetExecutingAssembly();
            }

            var resourceFile = assembly.GetManifestResourceNames().FirstOrDefault(f => f.EndsWith(file));
            foreach (var manifestResourceName in assembly.GetManifestResourceNames())
            {
                Console.WriteLine(manifestResourceName);
            }

            if (resourceFile == null)
            {
                throw new Exception($"{nameof(resourceFile)} Embedded Resource not found");
            }

            using (var ms = new MemoryStream())
            {
                assembly.GetManifestResourceStream(resourceFile)?.CopyTo(ms);
                return ms.ToArray();
            }
        }

        /// <summary>
        ///     Gets a string resource.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="assembly">The assembly.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        [PermissionSet(SecurityAction.Assert, Unrestricted = true)]
        public static string StringResource(string file, Assembly assembly = null)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            return Encoding.Default.GetString(ByteResource(file, assembly));
        }

        #endregion
    }
}