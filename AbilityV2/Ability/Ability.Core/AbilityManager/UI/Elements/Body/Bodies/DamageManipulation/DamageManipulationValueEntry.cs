namespace Ability.Core.AbilityManager.UI.Elements.Body.Bodies.DamageManipulation
{
    using System;

    using Ensage.Common.Objects.DrawObjects;

    using SharpDX;

    public class DamageManipulationValueEntry : DrawObject
    {
        private DrawText nameText;

        private DrawText valueText;

        private Vector2 position;

        private Vector2 size;

        public DamageManipulationValueEntry(string name, Func<string> getValue)
        {
            this.Name = name;
            this.GetValue = getValue;

            this.nameText = new DrawText { Color = Color.White, Text = name };
            this.valueText = new DrawText { Color = Color.White, Text = getValue() };
        }

        public string Name { get; set; }

        public Func<string> GetValue { get; set; }

        public override Vector2 Position
        {
            get
            {
                return this.position;
            }

            set
            {
                this.position = value;
                this.nameText.Position = this.position;
                this.valueText.Position = this.position + new Vector2((float)(this.nameText.Size.X * 1.1), 0);
            }
        }

        public override Vector2 Size
        {
            get
            {
                return this.size;
            }

            set
            {
                this.nameText.TextSize = value;
                this.valueText.TextSize = value;
                this.size = new Vector2((float)(this.nameText.Size.X * 1.3 + this.valueText.Size.X), this.nameText.Size.Y);
            }
        }

        public override void Draw()
        {
            this.nameText.Draw();
            this.valueText.Text = this.GetValue();
            this.valueText.Draw();
        }
    }
}
