// <copyright file="CastPointSpellOverlay.cs" company="EnsageSharp">
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

    using Ability.Core.AbilityFactory.AbilitySkill.Data;
    using Ability.Core.AbilityFactory.Utilities;
    using Ability.Utilities;

    using Ensage.Common.Objects.DrawObjects;

    using SharpDX;

    /// <summary>
    ///     The cast point spell overlay.
    /// </summary>
    public class CastPointSpellOverlay : SpellOverlay
    {
        #region Fields

        /// <summary>
        ///     The ability phase background.
        /// </summary>
        private readonly DrawRect abilityPhaseBackground = new DrawRect(new Color(150, 50, 25, 160));

        /// <summary>
        ///     The ability phase count.
        /// </summary>
        private readonly DrawText abilityPhaseCount = new DrawText
                                                          {
                                                              Color = new Color(255, 150, 160), Text = string.Empty,
                                                              Shadow = true
                                                          };

        private Number abilityPhaseNumber;

        private DataObserver<AbilityPhase> abilityPhaseObserver;

        #endregion

        #region Constructors and Destructors

        public CastPointSpellOverlay(IAbilitySkill skill)
            : base(skill)
        {
            this.abilityPhaseNumber = new Number(NumberTextureColor.Red, true);

            // this.abilityPhaseObserver = new DataObserver<AbilityPhase>(
            // phase =>
            // {
            // if (phase.Running)
            // {
            // this
            // }
            // });
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The draw.
        /// </summary>
        public override void Draw()
        {
            base.Draw();
            if (this.Skill.AbilityPhase.Running)
            {
                // this.abilityPhaseCount.Text =
                // (Math.Ceiling(this.Skill.AbilityPhase.TimeRemaining * 10) / 10).ToString(CultureInfo.CurrentCulture);
                // this.abilityPhaseCount.CenterOnRectangle(this.abilityPhaseBackground);
                // Console.WriteLine((int)(Math.Ceiling(this.Skill.AbilityPhase.TimeRemaining * 10) / 10));
                this.abilityPhaseNumber.Value = Math.Ceiling(this.Skill.AbilityPhase.TimeRemaining * 10) / 10;
                this.abilityPhaseNumber.CenterOnRectangle(this.abilityPhaseBackground);
                this.abilityPhaseBackground.Draw();
                this.abilityPhaseNumber.Draw();
            }
        }

        /// <summary>
        ///     The on position set.
        /// </summary>
        public override void OnPositionSet()
        {
            this.abilityPhaseBackground.Position = this.Position;
            this.abilityPhaseCount.CenterOnRectangle(this.abilityPhaseBackground);
            base.OnPositionSet();
        }

        /// <summary>
        ///     The on size set.
        /// </summary>
        public override void OnSizeSet()
        {
            this.abilityPhaseBackground.Size = this.Size;
            this.abilityPhaseCount.TextSize = this.CooldownText.TextSize * (float)0.85;
            this.abilityPhaseNumber.NumberSize = (float)Math.Max(this.Size.Y / 1.8, 9);
            base.OnSizeSet();
        }

        #endregion
    }
}