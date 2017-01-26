// <copyright file="SpellOverlay.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillOverlay.Types
{
    using System;

    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillLevel;
    using Ability.Core.AbilityFactory.Utilities;
    using Ability.Utilities;

    using Ensage.Common;
    using Ensage.Common.Objects.DrawObjects;

    using SharpDX;

    /// <summary>
    ///     The spell overlay.
    /// </summary>
    public class SpellOverlay : SkillOverlay
    {
        #region Fields

        /// <summary>
        ///     The level text.
        /// </summary>
        /// <summary>
        ///     The level observer.
        /// </summary>
        private readonly DataObserver<ISkillLevel> levelObserver;

        /// <summary>
        ///     The level text background.
        /// </summary>
        private readonly DrawRect levelTextBackground;

        private Number levelNumber;

        /// <summary>
        ///     The position 1.
        /// </summary>
        private Vector2 position1;

        #endregion

        #region Constructors and Destructors

        public SpellOverlay(IAbilitySkill skill)
            : base(skill)
        {
            // this.levelText = new DrawText
            // { Color = Color.White, Shadow = true, Text = this.Skill.Level.Current.ToString() };
            this.levelNumber = new Number(NumberTextureColor.Default, true) { Value = (int)this.Skill.Level.Current };
            this.levelObserver = new DataObserver<ISkillLevel>(
                level =>
                    {
                        // this.levelText.Text = level.Current.ToString();
                        this.levelNumber.Value = (int)level.Current;
                        this.OnSizeSet();
                        this.OnPositionSet();
                    });
            this.levelObserver.Subscribe(this.Skill.Level);

            this.levelTextBackground = new DrawRect(new Color(0, 0, 0, 210));
            DelayAction.Add(100, this.OnPositionSet);
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public override void Dispose()
        {
            this.levelObserver.Dispose();
            base.Dispose();
        }

        /// <summary>
        ///     The draw.
        /// </summary>
        public override void DrawElements()
        {
            base.DrawElements();
            if (this.Skill.Level.Current > 0)
            {
                this.levelTextBackground.Draw();

                // this.levelText.Draw();
                this.levelNumber.Draw();
            }
        }

        /// <summary>
        ///     The on position set.
        /// </summary>
        public override void OnPositionSet()
        {
            this.levelTextBackground.Position = this.Position
                                                + new Vector2(0, this.Size.Y - this.levelTextBackground.Size.Y - 1);

            // this.levelText.CenterOnRectangle(this.levelTextBackground);
            // this.levelText.Position = this.levelText.Position + new Vector2(1, 2);
            this.levelNumber.CenterOnRectangle(this.levelTextBackground);
        }

        /// <summary>
        ///     The on size set.
        /// </summary>
        public override void OnSizeSet()
        {
            // this.levelText.TextSize = this.Skill.Cooldown == null
            // ? new Vector2((float)((this.Size.Y + this.Size.X) / 2.3))
            // : new Vector2((float)((this.Size.Y + this.Size.X) / 3.7));
            this.levelNumber.NumberSize = (float)Math.Max(this.Size.X / 2.5, 8);
            this.levelTextBackground.Size = this.levelNumber.Size; // + new Vector2(2, 0);
        }

        #endregion
    }
}