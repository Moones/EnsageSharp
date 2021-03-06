﻿// <copyright file="JsonFactory.cs" company="EnsageSharp">
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
    using System;
    using System.IO;
    using System.Reflection;
    using System.Security.Permissions;

    using Ability.Core.Utilities.Resources;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    ///     Creates JSON data from resources.
    /// </summary>
    internal static class JsonFactory
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes static members of the <see cref="JsonFactory" /> class.
        /// </summary>
        static JsonFactory()
        {
            DefaultSettings = new JsonSerializerSettings
                                  {
                                      Formatting = Formatting.Indented,
                                      ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                                      DefaultValueHandling = DefaultValueHandling.Include,
                                      NullValueHandling = NullValueHandling.Include
                                  };
            DefaultSettings.Converters.Add(new StringEnumConverter());
            DefaultSettings.Converters.Add(new VersionConverter());

            JsonConvert.DefaultSettings = () => DefaultSettings;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Default JsonSerializerSettings
        /// </summary>
        public static JsonSerializerSettings DefaultSettings { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Deserialize Object from File
        /// </summary>
        /// <typeparam name="T">
        ///     The T
        /// </typeparam>
        /// <param name="file">
        ///     The file.
        /// </param>
        /// <param name="settings">
        ///     The settings.
        /// </param>
        /// <returns>
        ///     The <see cref="T" />.
        /// </returns>
        [PermissionSet(SecurityAction.Assert, Unrestricted = true)]
        public static T JsonFile<T>(string file, JsonSerializerSettings settings = null)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            return JsonConvert.DeserializeObject<T>(File.ReadAllText(file), settings);
        }

        /// <summary>
        ///     Deserialize Object from File
        /// </summary>
        /// <param name="file">
        ///     The file.
        /// </param>
        /// <param name="type">
        ///     The type.
        /// </param>
        /// <param name="settings">
        ///     The settings.
        /// </param>
        /// <returns>
        ///     The <see cref="object" />.
        /// </returns>
        [PermissionSet(SecurityAction.Assert, Unrestricted = true)]
        public static object JsonFile(string file, Type type = null, JsonSerializerSettings settings = null)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            return JsonConvert.DeserializeObject(File.ReadAllText(file), type, settings);
        }

        /// <summary>
        ///     Deserialize Object from Resource
        /// </summary>
        /// <typeparam name="T">
        ///     The T
        /// </typeparam>
        /// <param name="file">
        ///     The file.
        /// </param>
        /// <param name="assembly">
        ///     The assembly.
        /// </param>
        /// <param name="settings">
        ///     The settings.
        /// </param>
        /// <returns>
        ///     The <see cref="T" />.
        /// </returns>
        [PermissionSet(SecurityAction.Assert, Unrestricted = true)]
        public static T JsonResource<T>(string file, Assembly assembly = null, JsonSerializerSettings settings = null)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            return JsonConvert.DeserializeObject<T>(ResourceFactory.StringResource(file, assembly), settings);
        }

        /// <summary>
        ///     Deserialize Object from Resource
        /// </summary>
        /// <param name="file">
        ///     The file.
        /// </param>
        /// <param name="type">
        ///     The type.
        /// </param>
        /// <param name="assembly">
        ///     The assembly.
        /// </param>
        /// <param name="settings">
        ///     The settings.
        /// </param>
        /// <returns>
        ///     The <see cref="object" />.
        /// </returns>
        [PermissionSet(SecurityAction.Assert, Unrestricted = true)]
        public static object JsonResource(
            string file,
            Type type = null,
            Assembly assembly = null,
            JsonSerializerSettings settings = null)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            return JsonConvert.DeserializeObject(ResourceFactory.StringResource(file, assembly), type, settings);
        }

        /// <summary>
        ///     Deserialize Object from String
        /// </summary>
        /// <typeparam name="T">
        ///     The T
        /// </typeparam>
        /// <param name="s">
        ///     The s.
        /// </param>
        /// <param name="settings">
        ///     The settings.
        /// </param>
        /// <returns>
        ///     The <see cref="T" />.
        /// </returns>
        [PermissionSet(SecurityAction.Assert, Unrestricted = true)]
        public static T JsonString<T>(string s, JsonSerializerSettings settings = null)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }

            return JsonConvert.DeserializeObject<T>(s, settings);
        }

        /// <summary>
        ///     Deserialize Object from String
        /// </summary>
        /// <param name="s">
        ///     The s.
        /// </param>
        /// <param name="type">
        ///     The type.
        /// </param>
        /// <param name="settings">
        ///     The settings.
        /// </param>
        /// <returns>
        ///     The <see cref="object" />.
        /// </returns>
        [PermissionSet(SecurityAction.Assert, Unrestricted = true)]
        public static object JsonString(string s, Type type = null, JsonSerializerSettings settings = null)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }

            return JsonConvert.DeserializeObject(s, type, settings);
        }

        /// <summary>
        ///     Serialize Object to File
        /// </summary>
        /// <param name="file">
        ///     The file.
        /// </param>
        /// <param name="obj">
        ///     The object.
        /// </param>
        /// <param name="settings">
        ///     The settings.
        /// </param>
        [PermissionSet(SecurityAction.Assert, Unrestricted = true)]
        public static void ToFile(string file, object obj, JsonSerializerSettings settings = null)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            File.WriteAllText(file, JsonConvert.SerializeObject(obj, settings));
        }

        /// <summary>
        ///     Serialize Object to String
        /// </summary>
        /// <param name="obj">
        ///     The object.
        /// </param>
        /// <param name="settings">
        ///     The settings.
        /// </param>
        /// <returns>
        ///     The <see cref="string" />.
        /// </returns>
        [PermissionSet(SecurityAction.Assert, Unrestricted = true)]
        public static string ToString(object obj, JsonSerializerSettings settings = null)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            return JsonConvert.SerializeObject(obj, settings);
        }

        #endregion
    }
}