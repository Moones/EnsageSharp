namespace Ability.Core.AbilityData.AbilityMapDataProvider.AbilityMapData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ability.Core.AbilityData.AbilityMapDataProvider.AbilityMapData.Runes.AbilityRune;
    using Ability.Core.AbilityFactory.Utilities;

    using Ensage.Common.Extensions;
    using Ensage.Common.Objects.DrawObjects;

    public class RuneSpawner<T> where T : IAbilityRune 
    {
        private readonly float firstSpawnTime;

        private float lastSpawnTime;

        private readonly List<RunePosition<T>> positions = new List<RunePosition<T>>();

        private float nextSpawnTime;

        public RuneSpawner(List<RunePosition<T>> positions, float firstSpawnTime)
        {
            this.positions = positions;
            this.firstSpawnTime = firstSpawnTime;


        }

        public IReadOnlyCollection<RunePosition<T>> Positions => this.positions;

        public float NextSpawnTime
        {
            get
            {
                return this.nextSpawnTime;
            }

            set
            {
                this.nextSpawnTime = value;
                foreach (var runePosition in this.Positions)
                {
                    runePosition.NextSpawnTime = this.nextSpawnTime;
                }
            }
        }

        public DataProvider<T> NewRuneProvider { get; } = new DataProvider<T>();

        internal void UpdateTime(float gameTime)
        {
            if (gameTime > this.firstSpawnTime)
            {
                this.lastSpawnTime = this.firstSpawnTime;
                for (var i = this.firstSpawnTime; i <= gameTime; i += 120)
                {
                    this.lastSpawnTime = i;
                }

                this.NextSpawnTime = this.lastSpawnTime + 120;
            }
            else
            {
                this.NextSpawnTime = this.firstSpawnTime;
            }
        }

        internal void NewRune(T rune)
        {
            var closest = this.positions.MinOrDefault(position => position.Position.Distance(rune.SourceRune.Position));
            closest.CurrentRune = rune;
            this.NewRuneProvider.Next(closest.CurrentRune);
            //foreach (var runePosition in this.Positions)
            //{
            //    if (runePosition.Position.Distance(rune.SourceRune.Position) < 100)
            //    {
            //        runePosition.CurrentRune = rune;
            //        break;
            //    }
            //}
        }

        internal void Draw()
        {
            foreach (var runePosition in this.Positions)
            {
                runePosition.Draw();
            }
        }
    }
}
