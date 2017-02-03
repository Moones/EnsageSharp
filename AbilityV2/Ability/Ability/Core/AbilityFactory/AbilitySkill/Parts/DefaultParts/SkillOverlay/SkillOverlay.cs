// <copyright file="SkillOverlay.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillOverlay
{
    using System;

    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.Cooldown;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Mana;
    using Ability.Core.AbilityFactory.Utilities;
    using Ability.Utilities;

    using Ensage;
    using Ensage.Common.Objects.DrawObjects;

    using SharpDX;

    /// <summary>
    ///     The skill overlay.
    /// </summary>
    public class SkillOverlay : ISkillOverlay
    {
        #region Fields

        /// <summary>
        ///     The black overlay.
        /// </summary>
        private readonly DrawRect blackOverlay;

        /// <summary>
        ///     The blue overlay.
        /// </summary>
        private readonly DrawRect blueOverlay;

        /// <summary>The cool down observer.</summary>
        private readonly DataObserver<ICooldown> coolDownObserver;

        /// <summary>
        ///     The icon.
        /// </summary>
        private readonly DrawRect icon;

        /// <summary>The mana observer.</summary>
        private readonly DataObserver<IMana> manaObserver;

        /// <summary>
        ///     The mana text.
        /// </summary>
        private readonly DrawText manaText;

        /// <summary>The off cooldown observer.</summary>
        private readonly DataObserver<ICooldown> offCooldownObserver;

        private Number cooldownNumber;

        private Vector2 levelRectsSize;

        private Number manaNumber;

        /// <summary>
        ///     The position.
        /// </summary>
        private Vector2 position;

        private bool removed;

        /// <summary>
        ///     The size.
        /// </summary>
        private Vector2 size;

        #endregion

        #region Constructors and Destructors

        public SkillOverlay(IAbilitySkill skill)
        {
            this.Skill = skill;
            this.icon = new DrawRect(skill.Texture);
            this.blueOverlay = new DrawRect(new Color(20, 20, 100, 220));
            this.blackOverlay = new DrawRect(new Color(40, 40, 40, 180));
            this.CooldownText = new DrawText
                                    {
                                        Text = string.Empty, Color = Color.FloralWhite, FontFlags = FontFlags.None,
                                        Shadow = true
                                    };
            this.manaText = new DrawText
                                {
                                    Text = string.Empty, Color = new Color(180, 200, 255), FontFlags = FontFlags.None,
                                    Shadow = true
                                };

            this.BorderColor = this.NotLearnedColor;
            this.cooldownNumber = new Number(
                NumberTextureColor.Default,
                true,
                this.Skill.Owner.ScreenInfo.HealthBarSize.Y / 2);
            this.manaNumber = new Number(NumberTextureColor.Blue, true);
            this.Skill.DisposeNotifier.Subscribe(this.Dispose);

            if (this.Skill.Cooldown == null)
            {
                return;
            }

            this.manaObserver = new DataObserver<IMana>(this.OnNext);
            this.manaObserver.Subscribe(this.Skill.Owner.Mana);
            this.OnNext(this.Skill.Owner.Mana);
            this.coolDownObserver = new DataObserver<ICooldown>(this.OnNext);
            this.coolDownObserver.Subscribe(this.Skill.Cooldown);

            this.offCooldownObserver = new DataObserver<ICooldown>(
                cooldown =>
                    {
                        if (!this.Skill.CastData.EnoughMana)
                        {
                            this.BorderColor = this.NotEnoughManaColor;
                            return;
                        }

                        this.BorderColor = this.ReadyColor;
                    });
            this.offCooldownObserver.Subscribe(this.Skill.Cooldown.OffCooldownProvider);
        }

        #endregion

        #region Public Properties

        public Color BorderColor { get; set; }

        /// <summary>
        ///     The cooldown text.
        /// </summary>
        public DrawText CooldownText { get; }

        public Color NotEnoughManaColor { get; set; } = new Color(0, 20, 150);

        public Color NotLearnedColor { get; set; } = new Color(20, 20, 20);

        public Color NotReadyColor { get; set; } = new Color(45, 45, 45);

        /// <summary>
        ///     Gets or sets the position.
        /// </summary>
        public virtual Vector2 Position
        {
            get
            {
                return this.icon.Position;
            }

            set
            {
                this.position = value;
                var position = this.position + Vector2.One;
                this.icon.Position = position;
                this.blueOverlay.Position = position;
                this.blackOverlay.Position = position;
                this.cooldownNumber.CenterOnRectangle(this.blackOverlay);
                this.manaText.CenterOnRectangle(this.blackOverlay);
                this.manaNumber.CenterOnRectangle(this.blackOverlay);
                this.CooldownText.CenterOnRectangle(this.blackOverlay);
                this.OnPositionSet();
            }
        }

        public virtual Color ReadyColor { get; set; } = new Color(90, 90, 90);

        /// <summary>
        ///     Gets or sets the size.
        /// </summary>
        public virtual Vector2 Size
        {
            get
            {
                return this.size;
            }

            set
            {
                this.size = value;
                var size = this.size - Vector2.One;
                if (this.Skill.IsItem)
                {
                    this.icon.Size = size + new Vector2((float)(size.X * 0.45), 0);
                }
                else
                {
                    this.icon.Size = size;
                }

                this.blackOverlay.Size = size;
                this.blueOverlay.Size = size;
                this.cooldownNumber.NumberSize = (float)Math.Max(this.Size.Y / 1.7, 10);
                this.manaNumber.NumberSize = (float)Math.Max(this.Size.Y / 2, 8);
                this.manaText.TextSize = new Vector2((float)((size.Y + size.X) / 2.3));
                this.cooldownNumber.CenterOnRectangle(this.blackOverlay);
                this.CooldownText.TextSize = new Vector2((float)((size.Y + size.X) / 2));
                this.manaText.CenterOnRectangle(this.blackOverlay);
                this.CooldownText.CenterOnRectangle(this.blackOverlay);
                this.OnSizeSet();
            }
        }

        /// <summary>
        ///     Gets or sets the skill.
        /// </summary>
        public IAbilitySkill Skill { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public virtual void Dispose()
        {
            this.removed = true;
            this.manaObserver?.Dispose();
            this.coolDownObserver?.Dispose();
            this.offCooldownObserver?.Dispose();
        }

        /// <summary>
        ///     The draw.
        /// </summary>
        public virtual void Draw()
        {
            if (this.removed)
            {
                return;
            }

            this.icon.Draw();

            // .DrawRect(this.Position, this.Size, Color.White, true);
            Drawing.DrawRect(this.Position - Vector2.One, this.Size + new Vector2(1), Color.Black, true);
            Drawing.DrawRect(this.Position, this.Size - new Vector2(1), this.BorderColor, true);
            if (this.Skill.Level.Current <= 0)
            {
                return;
            }

            this.DrawElements();

            var enoughMana = this.Skill.CastData.EnoughMana;

            if (!enoughMana)
            {
                this.blueOverlay.Draw();
            }

            if (this.Skill.CastData.IsOnCooldown)
            {
                if (enoughMana)
                {
                    this.blackOverlay.Draw();
                }

                // this.CooldownText.Draw();
                this.cooldownNumber.Draw();
            }
            else if (!enoughMana)
            {
                // this.manaText.Draw();
                this.manaNumber.Draw();
            }
        }

        public virtual void DrawElements()
        {
        }

        /// <summary>The initialize.</summary>
        public virtual void Initialize()
        {
        }

        /// <summary>Notifies the observer that the provider has finished sending push-based notifications.</summary>
        public void OnCompleted()
        {
        }

        /// <summary>Notifies the observer that the provider has experienced an error condition.</summary>
        /// <param name="error">An object that provides additional information about the error.</param>
        public void OnError(Exception error)
        {
        }

        /// <summary>Provides the observer with new data.</summary>
        /// <param name="value">The current notification information.</param>
        public void OnNext(ICooldown value)
        {
            if (this.Skill.Level.Current == 0)
            {
                this.BorderColor = this.NotLearnedColor;
                return;
            }

            if (value.Current > 0)
            {
                // this.CooldownText.Text = (Math.Min(value.Current, 99)).ToString(CultureInfo.CurrentCulture);
                // this.CooldownText.CenterOnRectangle(this.blackOverlay);
                this.cooldownNumber.Value = (int)Math.Min(value.Current, 99);
                this.cooldownNumber.CenterOnRectangle(this.blackOverlay);

                ///this.CooldownText.CenterOnRectangle(this.blackOverlay);
                if (this.Skill.CastData.EnoughMana)
                {
                    this.BorderColor = this.NotReadyColor;
                }
                else
                {
                    this.BorderColor = this.NotEnoughManaColor;
                }
            }
            else
            {
                if (this.Skill.CastData.EnoughMana)
                {
                    this.BorderColor = this.ReadyColor;
                }
                else
                {
                    this.BorderColor = this.NotEnoughManaColor;
                }
            }
        }

        /// <summary>Provides the observer with new data.</summary>
        /// <param name="value">The current notification information.</param>
        public void OnNext(IMana value)
        {
            if (!this.Skill.SourceAbility.IsValid)
            {
                return;
            }

            if (this.Skill.Level.Current == 0)
            {
                this.BorderColor = this.NotLearnedColor;
                return;
            }

            if (value.Current < this.Skill.SourceAbility.ManaCost)
            {
                this.manaNumber.Value = Math.Min(Math.Floor(this.Skill.SourceAbility.ManaCost - value.Current), 99);
                this.manaNumber.CenterOnRectangle(this.blackOverlay);

                // this.manaText.Text =
                // Math.Min(Math.Floor(this.Skill.SourceAbility.ManaCost - value.Current), 99)
                // .ToString(CultureInfo.CurrentCulture);
                // this.manaText.CenterOnRectangle(this.blackOverlay);
                this.BorderColor = this.NotEnoughManaColor;
            }
            else
            {
                if (!this.Skill.CastData.IsOnCooldown)
                {
                    this.BorderColor = this.ReadyColor;
                }
            }
        }

        public virtual void OnPositionSet()
        {
        }

        public virtual void OnSizeSet()
        {
        }

        #endregion
    }
}