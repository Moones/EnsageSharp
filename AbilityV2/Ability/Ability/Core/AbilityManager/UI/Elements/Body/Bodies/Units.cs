// <copyright file="Units.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityManager.UI.Elements.Body.Bodies
{
    using System.Collections.ObjectModel;
    using System.Linq;

    using Ensage.Common.Extensions;

    using SharpDX;

    public class Units : Body
    {
        #region Fields

        private Collection<TeamEntry> teamEntries = new Collection<TeamEntry>();

        #endregion

        #region Constructors and Destructors

        public Units(Vector2 size, Vector2 position, IAbilityManager abilityManager)
            : base(size, position)
        {
            foreach (var abilityTeam in abilityManager.Teams)
            {
                this.teamEntries.Add(new TeamEntry(abilityTeam, this));
            }

            this.UpdatePosition();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The draw elements.
        /// </summary>
        public override void DrawElements()
        {
            foreach (var teamEntry in this.teamEntries)
            {
                teamEntry.Draw();
            }
        }

        /// <summary>
        ///     The mouse down.
        /// </summary>
        /// <param name="mousePosition">
        ///     The mouse position.
        /// </param>
        public override bool MouseDown(Vector2 mousePosition)
        {
            return this.teamEntries.Any(x => x.MouseDown(mousePosition));
        }

        /// <summary>
        ///     The mouse move.
        /// </summary>
        /// <param name="mousePosition">
        ///     The mouse position.
        /// </param>
        public override void MouseMove(Vector2 mousePosition)
        {
            this.teamEntries.ForEach(x => x.MouseMove(mousePosition));
        }

        /// <summary>
        ///     The mouse up.
        /// </summary>
        /// <param name="mousePosition">
        ///     The mouse position.
        /// </param>
        public override void MouseUp(Vector2 mousePosition)
        {
            this.teamEntries.ForEach(x => x.MouseUp(mousePosition));
        }

        /// <summary>
        ///     The update position.
        /// </summary>
        public override sealed void UpdatePosition()
        {
            var pos = this.Position;
            var size = new Vector2(this.Size.X, 0);
            foreach (var teamEntry in this.teamEntries)
            {
                teamEntry.Position = pos;
                pos += new Vector2(0, teamEntry.Size.Y);
                size += new Vector2(0, teamEntry.Size.Y);
            }

            this.Size = size;
        }

        #endregion
    }
}