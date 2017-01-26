// <copyright file="TeamEntry.cs" company="EnsageSharp">
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
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ability.Core.AbilityFactory.AbilityTeam;
    using Ability.Core.AbilityFactory.AbilityTeam.Parts;
    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.Utilities;
    using Ability.Core.AbilityManager.UI.Elements.Button;

    using Ensage.Common;
    using Ensage.Common.Extensions;
    using Ensage.Common.Objects.DrawObjects;

    using SharpDX;

    /// <summary>
    ///     The team entry.
    /// </summary>
    public class TeamEntry : IUserInterfaceElement, IAbilityTeamPart
    {
        #region Fields

        private bool drawElements = true;

        private Button hideButton;

        private Vector2 position;

        private DrawText teamName = new DrawText { Color = new Color(180, 180, 180) };

        private DrawRect teamNameBg = new DrawRect(new Color(0, 0, 0, 200)) { Border = true };

        /// <summary>
        ///     The unit entries.
        /// </summary>
        private Dictionary<double, UnitOverlayEntry> unitEntries = new Dictionary<double, UnitOverlayEntry>();

        #endregion

        #region Constructors and Destructors

        public TeamEntry(IAbilityTeam team, IUserInterfaceElement parent)
        {
            this.Team = team;
            this.Parent = parent;
            this.teamNameBg.Size = new Vector2(parent.Size.X, HUDInfo.GetHpBarSizeY() * 2);
            this.teamName.Text = team.Name.ToString();
            this.teamName.TextSize = new Vector2(this.teamNameBg.Size.Y);
            this.hideButton = new Button(
                new Vector2(this.teamNameBg.Size.Y),
                new Vector2(this.teamNameBg.Size.X - this.teamNameBg.Size.Y, 0),
                "-",
                Color.White,
                this,
                () =>
                    {
                        this.drawElements = !this.drawElements;
                        if (this.drawElements)
                        {
                            this.hideButton.Text = "-";
                        }
                        else
                        {
                            this.hideButton.Text = "+";
                        }

                        this.UpdateSize();
                    });

            // foreach (var abilityUnit in this.Team.UnitManager.Units)
            // {
            // this.unitEntries.Add(abilityUnit.Key, new UnitEntry(abilityUnit.Value, this));
            // }
            this.Team.UnitManager.UnitAdded.Subscribe(
                new DataObserver<IAbilityUnit>(
                    unit =>
                        {
                            this.unitEntries.Add(unit.UnitHandle, new UnitOverlayEntry(unit, this));
                            this.UpdateSize();
                        }));
            this.Team.UnitManager.UnitRemoved.Subscribe(
                new DataObserver<IAbilityUnit>(
                    unit =>
                        {
                            this.unitEntries.Remove(unit.UnitHandle);
                            this.UpdateSize();
                        }));
            this.UpdateSize();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the color.
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        ///     Gets or sets the parent.
        /// </summary>
        public IUserInterfaceElement Parent { get; set; }

        /// <summary>
        ///     Gets or sets the position.
        /// </summary>
        public Vector2 Position
        {
            get
            {
                return this.position;
            }

            set
            {
                this.position = value;
                this.teamNameBg.Position = this.position;
                this.teamName.CenterOnRectangleHorizontally(this.teamNameBg, 5);
                this.hideButton.Position = new Vector2(this.teamNameBg.Size.X - this.teamNameBg.Size.Y, 0);
                this.hideButton.UpdatePosition();
                var pos = this.position + new Vector2(0, this.teamNameBg.Size.Y);
                foreach (var unitEntry in this.unitEntries)
                {
                    unitEntry.Value.Position = pos;
                    pos += new Vector2(0, unitEntry.Value.Size.Y);
                }
            }
        }

        /// <summary>
        ///     Gets or sets the size.
        /// </summary>
        public Vector2 Size { get; set; }

        /// <summary>
        ///     Gets or sets the team.
        /// </summary>
        public IAbilityTeam Team { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether visible.
        /// </summary>
        public bool Visible { get; set; } = true;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The draw.
        /// </summary>
        public void Draw()
        {
            this.teamNameBg.Draw();
            this.teamName.Draw();
            this.hideButton.Draw();

            // Console.WriteLine(this.hideButton.Position);
            if (!this.drawElements)
            {
                return;
            }

            foreach (var unitEntry in this.unitEntries)
            {
                unitEntry.Value.Draw();
            }
        }

        /// <summary>
        ///     The mouse down.
        /// </summary>
        /// <param name="mousePosition">
        ///     The mouse position.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public bool MouseDown(Vector2 mousePosition)
        {
            return this.hideButton.MouseDown(mousePosition)
                   || this.unitEntries.Any(x => x.Value.MouseDown(mousePosition));
        }

        /// <summary>
        ///     The mouse move.
        /// </summary>
        /// <param name="mousePosition">
        ///     The mouse position.
        /// </param>
        public void MouseMove(Vector2 mousePosition)
        {
            this.hideButton.MouseMove(mousePosition);
            this.unitEntries.ForEach(x => x.Value.MouseMove(mousePosition));
        }

        /// <summary>
        ///     The mouse up.
        /// </summary>
        /// <param name="mousePosition">
        ///     The mouse position.
        /// </param>
        public void MouseUp(Vector2 mousePosition)
        {
            this.hideButton.MouseUp(mousePosition);
            this.unitEntries.ForEach(x => x.Value.MouseUp(mousePosition));
        }

        /// <summary>
        ///     The update position.
        /// </summary>
        public void UpdatePosition()
        {
        }

        /// <summary>
        ///     The update size.
        /// </summary>
        public void UpdateSize()
        {
            if (!this.drawElements)
            {
                this.Size = this.teamNameBg.Size;
            }
            else
            {
                this.Size = this.teamNameBg.Size;
                foreach (var unitEntry in this.unitEntries)
                {
                    this.Size += new Vector2(0, unitEntry.Value.Size.Y);
                }
            }

            this.Parent.UpdatePosition();
        }

        #endregion
    }
}