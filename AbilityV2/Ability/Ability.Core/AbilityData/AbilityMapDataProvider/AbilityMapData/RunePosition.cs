namespace Ability.Core.AbilityData.AbilityMapDataProvider.AbilityMapData
{
    using Ability.Core.AbilityData.AbilityMapDataProvider.AbilityMapData.Runes.AbilityRune;
    using Ability.Core.AbilityFactory.Utilities;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Objects.DrawObjects;

    using SharpDX;

    public class RunePosition<T> where T : IAbilityRune
    {
        private T currentRune;

        private DrawText text;

        private float nextSpawnTime;

        public RunePosition(Vector3 position)
        {
            this.Position = position;
            this.text = new DrawText { Shadow = true, Color = Color.White, Size = new Vector2(20 * HUDInfo.Monitor) };
        }

        public Vector3 Position { get; }

        public DataProvider<T> NewRuneProvider { get; } = new DataProvider<T>();

        public T CurrentRune
        {
            get
            {
                return this.currentRune;
            }

            internal set
            {
                this.currentRune = value;
                this.NewRuneProvider.Next(this.currentRune);
            }
        }

        public float NextSpawnTime
        {
            get
            {
                return this.nextSpawnTime;
            }

            internal set
            {
                this.nextSpawnTime = value;
            }
        }

        internal void Draw()
        {
            this.text.Position = Drawing.WorldToScreen(this.Position);
            if (this.text.Position.Equals(Vector2.Zero))
            {
                return;
            }

            this.text.Text = "current: " + this.CurrentRune?.TypeName + " next in " + (this.nextSpawnTime - Game.GameTime);
            this.text.Draw();
        }
    }
}
