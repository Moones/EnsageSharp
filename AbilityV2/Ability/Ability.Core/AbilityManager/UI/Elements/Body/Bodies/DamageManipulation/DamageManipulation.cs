namespace Ability.Core.AbilityManager.UI.Elements.Body.Bodies.DamageManipulation
{
    using System.Collections.Generic;

    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.Utilities;

    using SharpDX;

    internal class DamageManipulation : Body
    {
        private IAbilityUnit localHero;

        private DamageManipulationEntry entry;

        private ICollection<DamageManipulationEntry> entries = new List<DamageManipulationEntry>();

        internal DamageManipulation(Vector2 size, Vector2 position, IAbilityManager abilityManager)
            : base(size, position)
        {
            //foreach (var unitManagerUnit in abilityManager.LocalTeam.UnitManager.Units)
            //{
            //    this.entries.Add(new DamageManipulationEntry(unitManagerUnit.Value));
            //}

            foreach (var abilityManagerTeam in abilityManager.Teams)
            {
                abilityManagerTeam.UnitManager.UnitAdded.Subscribe(
                    new DataObserver<IAbilityUnit>(
                        unit =>
                            {
                                this.entries.Add(new DamageManipulationEntry(unit) { Size = this.Size });
                                this.UpdatePosition();
                            }));
            }

            //this.entry = new DamageManipulationEntry(this.localHero);
            //this.entry.Size = this.Size;
            //this.UpdatePosition();
        }

        public override void DrawElements()
        {
            foreach (var damageManipulationEntry in this.entries)
            {
                damageManipulationEntry.Draw();
            }
        }

        public override sealed void UpdatePosition()
        {
            var basePos = this.Position;
            foreach (var damageManipulationEntry in this.entries)
            {
                damageManipulationEntry.Position = basePos;
                basePos += new Vector2(damageManipulationEntry.Size.X, 0);
                if (basePos.X - this.Position.X + damageManipulationEntry.Size.X > this.Size.X)
                {
                    basePos = new Vector2(this.Position.X, basePos.Y + (float)(damageManipulationEntry.Size.Y * 1.1));
                }
            }
            //if (this.entry == null)
            //{
            //    return;
            //}

            //this.entry.Position = this.Position;
        }
    }
}
