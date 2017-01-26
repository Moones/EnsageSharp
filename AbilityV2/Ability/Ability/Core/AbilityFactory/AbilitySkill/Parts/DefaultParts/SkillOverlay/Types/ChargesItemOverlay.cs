// <copyright file="ChargesItemOverlay.cs" company="EnsageSharp">
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

    using Ability.Core.AbilityFactory.Utilities;
    using Ability.Utilities;

    using Ensage.Common.Objects.DrawObjects;

    using SharpDX;

    public class ChargesItemOverlay : ItemOverlay
    {
        #region Fields

        /// <summary>The primary number.</summary>
        private Number primaryNumber;

        /// <summary>
        ///     The primary text.
        /// </summary>
        private DrawText primaryText = new DrawText { Color = Color.White, Text = "0", Shadow = true };

        /// <summary>The secondary number.</summary>
        private Number secondaryNumber;

        /// <summary>
        ///     The secondary text.
        /// </summary>
        private DrawText secondaryText = new DrawText { Color = Color.White, Text = "0", Shadow = true };

        #endregion

        #region Constructors and Destructors

        public ChargesItemOverlay(IAbilitySkill skill)
            : base(skill)
        {
            this.primaryNumber = new Number(NumberTextureColor.Default, true) { Value = 0 };
            this.secondaryNumber = new Number(NumberTextureColor.Default, true) { Value = 0 };
            skill.Charges.PrimaryProvider.Subscribe(
                new DataObserver<uint>(
                    u =>
                        {
                            this.primaryText.Text = u.ToString();
                            this.primaryNumber.Value = (int)u;
                            this.OnPositionSet();
                        }));
            skill.Charges.SecondaryProvider.Subscribe(
                new DataObserver<uint>(
                    u =>
                        {
                            this.secondaryText.Text = u.ToString();
                            this.secondaryNumber.Value = (int)u;
                            this.OnPositionSet();
                        }));
        }

        #endregion

        #region Public Methods and Operators

        public override void DrawElements()
        {
            base.DrawElements();

            // this.primaryText.Draw();
            this.primaryNumber.Draw();
            if (!this.Skill.Charges.HasSecondary)
            {
                return;
            }

            this.secondaryNumber.Draw();

            // this.secondaryText.Draw();
        }

        public override void OnPositionSet()
        {
            base.OnPositionSet();

            this.secondaryNumber.Position = this.Position
                                            + new Vector2(-2, this.Size.Y - this.secondaryNumber.Size.Y - 2);
            this.primaryNumber.Position = this.secondaryNumber.Position
                                          + new Vector2(this.Size.X - this.primaryNumber.Size.X + 3, 0);

            // this.primaryText.Position = this.Position;
            // this.secondaryText.Position = this.Position + new Vector2(this.Size.X / 2, 0);
        }

        public override void OnSizeSet()
        {
            base.OnSizeSet();

            // this.primaryText.TextSize = this.CooldownText.TextSize * (float)0.8;
            // this.secondaryText.TextSize = this.CooldownText.TextSize * (float)0.8;
            this.primaryNumber.NumberSize = (float)Math.Max(this.Size.X / 2.5, 8);
            this.secondaryNumber.NumberSize = this.primaryNumber.NumberSize;
        }

        #endregion
    }
}