// <copyright file="InvokerSkillOverlay.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.HeroParts.Invoker.Overlay
{
    using Ability.Core.AbilityFactory.AbilitySkill.Data;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillOverlay;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.Invoker.SkillBook;
    using Ability.Core.AbilityFactory.Utilities;

    using Ensage.Common;

    using SharpDX;

    /// <summary>
    ///     The invoker skill overlay.
    /// </summary>
    public class InvokerSkillOverlay : SkillOverlay
    {
        #region Constructors and Destructors

        public InvokerSkillOverlay(IAbilitySkill skill)
            : base(skill)
        {
            var skillbook = skill.Owner.SkillBook as InvokerSkillBook;
            DelayAction.Add(
                200,
                () =>
                    {
                        skillbook?.Invoke.AbilityCast.Subscribe(
                            new DataObserver<AbilityCast>(cast => { this.Update(); }));
                    });
            this.Skill.Owner.Visibility.CameOutOfFogNotifier.Subscribe(this.Update);
            this.Update();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The draw.
        /// </summary>
        public override void DrawElements()
        {
            // if (!this.Skill.SourceAbility.IsHidden)
            // {
            // if (this.BorderColor == this.ReadyColor)
            // {
            // this.BorderColor = new Color();
            // }
            // //Drawing.DrawRect(this.Position + new Vector2(1), this.Size - new Vector2(1), new Color(230, 230, 120), true);
            // }
        }

        #endregion

        #region Methods

        private void Update()
        {
            if (!this.Skill.SourceAbility.IsHidden)
            {
                this.BorderColor = new Color(230, 230, 120);
                this.ReadyColor = this.BorderColor;
            }
            else
            {
                this.BorderColor = this.NotReadyColor;
                this.ReadyColor = this.NotReadyColor;
            }
        }

        #endregion
    }
}