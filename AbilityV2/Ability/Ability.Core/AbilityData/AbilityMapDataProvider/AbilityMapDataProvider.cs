// <copyright file="AbilityMapDataProvider.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityData.AbilityMapDataProvider
{
    using System;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Ability.Core.AbilityData.AbilityMapDataProvider.AbilityMapData;
    using Ability.Core.AbilityData.AbilityMapDataProvider.AbilityMapData.Runes.AbilityRune.Types;

    using Ensage;
    using Ensage.Common.Menu;
    using Ensage.Common.Objects.UtilityObjects;
    using Ensage.Items;

    [Export(typeof(IAbilityMapDataProvider))]
    internal class AbilityMapDataProvider : IAbilityMapDataProvider
    {
        #region Fields

        private readonly Sleeper bountyUpdateSleeper = new Sleeper();

        private Lazy<IAbilityMapData> abilityMapDataLazy;

        private bool noNeedForUpdate;

        private float startTime;

        #endregion

        #region Public Properties

        public IAbilityMapData AbilityMapData { get; set; }

        [Import(typeof(IAbilityMapData))]
        public Lazy<IAbilityMapData> AbilityMapDataLazy
        {
            get
            {
                return this.abilityMapDataLazy;
            }

            set
            {
                this.abilityMapDataLazy = value;
                this.AbilityMapData = this.abilityMapDataLazy.Value;
            }
        }

        public bool GenerateMenu { get; }

        #endregion

        #region Public Methods and Operators

        public void EntityAdded(Entity entity)
        {
            var rune = entity as Rune;
            if (rune != null)
            {
                if (rune.RuneType == RuneType.Bounty)
                {
                    this.AbilityMapData.BountyRuneSpawner.NewRune(new BountyRune(rune));
                }
                else
                {
                    this.AbilityMapData.PowerUpRuneSpawner.NewRune(new PowerUpRune(rune));
                }
            }
        }

        public void EntityRemoved(Entity entity)
        {
            var rune = entity as Rune;
            if (rune != null)
            {
                if (rune.RuneType == RuneType.Bounty)
                {
                    foreach (var runePosition in this.AbilityMapData.BountyRuneSpawner.Positions)
                    {
                        if (runePosition.CurrentRune != null && runePosition.CurrentRune.Handle.Equals(rune.Handle))
                        {
                            runePosition.CurrentRune.Dispose();
                            runePosition.CurrentRune = null;
                            break;
                        }
                    }
                }
                else
                {
                    foreach (var runePosition in this.AbilityMapData.PowerUpRuneSpawner.Positions)
                    {
                        if (runePosition.CurrentRune != null && runePosition.CurrentRune.Handle.Equals(rune.Handle))
                        {
                            runePosition.CurrentRune.Dispose();
                            runePosition.CurrentRune = null;
                            break;
                        }
                    }
                }
            }
        }

        public Menu GetMenu()
        {
            throw new NotImplementedException();
        }

        public void OnClose()
        {
        }

        public void OnDraw()
        {
            this.AbilityMapData.BountyRuneSpawner.Draw();
            this.AbilityMapData.PowerUpRuneSpawner.Draw();
        }

        public void OnLoad()
        {
            this.startTime = Game.RawGameTime;
            foreach (var rune in ObjectManager.GetEntities<Rune>())
            {
                if (rune.RuneType == RuneType.Bounty)
                {
                    this.AbilityMapData.BountyRuneSpawner.NewRune(new BountyRune(rune));
                }
                else
                {
                    this.AbilityMapData.PowerUpRuneSpawner.NewRune(new PowerUpRune(rune));
                }
            }
        }

        public void OnUpdate()
        {
            var gameTime = Game.GameTime;

            this.AbilityMapData.BountyRuneSpawner.UpdateTime(gameTime);
            this.AbilityMapData.PowerUpRuneSpawner.UpdateTime(gameTime);

            if (this.noNeedForUpdate || this.bountyUpdateSleeper.Sleeping)
            {
                return;
            }

            if (Game.RawGameTime - this.startTime > 240)
            {
                this.noNeedForUpdate = true;
            }

            this.bountyUpdateSleeper.Sleep(200);
            foreach (var rune in ObjectManager.GetEntitiesParallel<Rune>())
            {
                if (rune.RuneType == RuneType.Bounty)
                {
                    if (
                        !this.AbilityMapData.BountyRuneSpawner.Positions.Any(
                            x =>
                                x.CurrentRune != null && x.CurrentRune.SourceRune.IsValid
                                && x.CurrentRune.Handle.Equals(rune.Handle)))
                    {
                        this.AbilityMapData.BountyRuneSpawner.NewRune(new BountyRune(rune));
                    }
                }
            }
        }

        #endregion
    }
}