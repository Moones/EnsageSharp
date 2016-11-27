// <copyright file="Program.cs" company="EnsageSharp">
//    Copyright (c) 2016 Moones.
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
namespace SharpScreen
{
    using System;
    using System.Collections.Generic;
    using System.Security.Permissions;
    using System.Text;

    using Ensage;
    using Ensage.Common;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    ///     The program.
    /// </summary>
    internal class Program
    {
        #region Fields

        /// <summary>
        ///     The commands dictionary.
        /// </summary>
        private readonly Dictionary<string, float> commandsDictionary =
            JsonConvert.DeserializeObject<Dictionary<string, float>>(
                JObject.Parse(Encoding.Default.GetString(Resource1.Commands).Substring(3)).ToString());

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Program" /> class.
        /// </summary>
        public Program()
        {
            Events.OnLoad += this.Events_OnLoad;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The main.
        /// </summary>
        [PermissionSet(SecurityAction.Assert, Unrestricted = true)]
        public static void Main()
        {
            new Program().Events_OnLoad(null, null);
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The events_ on load.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="e">
        ///     The e.
        /// </param>
        private void Events_OnLoad(object sender, EventArgs e)
        {
            foreach (var data in this.commandsDictionary)
            {
                var var = Game.GetConsoleVar(data.Key);
                var.RemoveFlags(ConVarFlags.Cheat);
                var.SetValue(data.Value);
            }
        }

        #endregion
    }
}