using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityData.AbilityMapDataProvider.AbilityMapData.Runes.AbilityRune
{
    using Ability.Core.AbilityFactory.Utilities;

    using Ensage;

    public abstract class AbilityRune : IAbilityRune
    {
        protected AbilityRune(Rune sourceRune)
        {
            this.SourceRune = sourceRune;
            this.Name = this.SourceRune.Name;
            this.Handle = this.SourceRune.Handle;
            this.TypeName = Game.Localize(this.SourceRune.RuneType.ToString());
            this.PickUpRange = 200;
        }

        public Rune SourceRune { get; }

        public string Name { get; }

        public double Handle { get; }

        public float PickUpRange { get; }

        public Notifier RuneDisposed { get; } = new Notifier();

        public string TypeName { get; }

        public void Dispose()
        {
            this.RuneDisposed.Notify();
            this.RuneDisposed.Dispose();
        }
    }
}
