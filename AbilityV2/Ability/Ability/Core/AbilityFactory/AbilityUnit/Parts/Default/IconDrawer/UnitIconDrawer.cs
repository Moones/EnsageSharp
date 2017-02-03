// <copyright file="UnitIconDrawer.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.IconDrawer
{
    using System;
    using System.Drawing;

    using Ability.Properties;
    using Ability.Utilities;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Objects;

    using SharpDX;

    /// <summary>
    ///     The unit drawer.
    /// </summary>
    public class UnitIconDrawer : IUnitIconDrawer
    {
        #region Fields

        private Vector2 minimapIconSize;

        #endregion

        #region Constructors and Destructors

        public UnitIconDrawer(IAbilityUnit unit)
        {
            this.Unit = unit;
            var name = this.Unit.SourceUnit.Name.Substring("npc_dota_hero_".Length);
            this.Icon = unit.SourceUnit is Hero ? Textures.GetHeroRoundTexture(this.Unit.Name) : null;
            this.MinimapIcon = unit.SourceUnit is Hero ? Textures.GetTexture("ensage_ui/miniheroes/" + name) : null;
            this.WorldIconSize = new Vector2(HUDInfo.GetHpBarSizeY() * 6);

            var icon = (Bitmap)Resources.ResourceManager.GetObject(name);
            if (icon == null)
            {
                Console.WriteLine("Ability#: could not find " + name + " minimap icon");
            }
            else
            {
                this.EndSceneIcon = new Render.Sprite(icon, new Vector2(100, 700));
            }

            this.MinimapIconSize = new Vector2((float)(HUDInfo.GetHpBarSizeY() * 1.5));

            this.EndSceneIcon.SetSaturation(1.5f);

            // sprite.Add();
        }

        #endregion

        #region Public Properties

        public Render.Sprite EndSceneIcon { get; }

        /// <summary>
        ///     Gets or sets the icon.
        /// </summary>
        public DotaTexture Icon { get; set; }

        public DotaTexture MinimapIcon { get; set; }

        /// <summary>
        ///     Gets or sets the minimap icon size.
        /// </summary>
        public Vector2 MinimapIconSize
        {
            get
            {
                return this.minimapIconSize;
            }

            set
            {
                this.minimapIconSize = value;

                var percent = this.minimapIconSize.X / this.EndSceneIcon.Size.X;

                // Console.WriteLine(percent);
                this.EndSceneIcon.Scale = new Vector2(percent, percent);
            }
        }

        /// <summary>
        ///     Gets or sets the unit.
        /// </summary>
        public IAbilityUnit Unit { get; set; }

        /// <summary>
        ///     Gets or sets the world icon size.
        /// </summary>
        public Vector2 WorldIconSize { get; set; }

        #endregion

        #region Public Methods and Operators

        public virtual void Dispose()
        {
        }

        /// <summary>
        ///     The draw icon on map.
        /// </summary>
        /// <param name="position">
        ///     The position.
        /// </param>
        public void DrawIconOnMap(Vector3 position)
        {
            Vector2 pos;
            if (!Drawing.WorldToScreen(position, out pos) || pos.Y == 0)
            {
                return;
            }

            Drawing.DrawRect(pos, this.WorldIconSize, this.Icon);
        }

        /// <summary>
        ///     The draw icon on minimap.
        /// </summary>
        /// <param name="worldPosition">
        ///     The world position.
        /// </param>
        public void DrawIconOnMinimap(Vector3 worldPosition)
        {
            var minpos = worldPosition.WorldToMinimap();
            if (minpos.Y == 0)
            {
                return;
            }

            Drawing.DrawRect(minpos, this.MinimapIconSize, this.Icon);
        }

        public virtual void Initialize()
        {
        }

        #endregion
    }
}