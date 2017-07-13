using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Types.Meepo
{
    using Ability.Core.AbilityFactory.AbilitySkill;

    using Ensage;
    using Ensage.Abilities;
    using Ensage.Heroes;

    public class AbilityUnitMeepo : AbilityUnit
    {
        private readonly Dictionary<double, AbilityUnitMeepo> otherMeepos = new Dictionary<double, AbilityUnitMeepo>();

        internal AbilityUnitMeepo(Meepo meepo)
            : base(meepo)
        {
            this.SourceMeepo = meepo;
            this.DividedWeStand =
                this.SourceMeepo.Spellbook.Spells.FirstOrDefault(x => x is DividedWeStand) as DividedWeStand;
            this.MeepoId = this.DividedWeStand.UnitIndex + 1;
        }

        public Meepo SourceMeepo { get; }

        public int MeepoId { get; }

        public DividedWeStand DividedWeStand { get; set; }

        public override void Initialize()
        {

        }

        public IReadOnlyDictionary<double, AbilityUnitMeepo> OtherMeepos => this.otherMeepos;
    }
}
