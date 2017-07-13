using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Fighter.LoneDruid.RetreatCombo
{
    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Orbwalker;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Units.SpiritBear.SkillBook;
    using Ability.Core.MenuManager.Menus.AbilityMenu.Items;
    using Ability.Fighter.LoneDruid.ChaseCombo;

    using Ensage;
    using Ensage.Common.Extensions;
    using Ensage.Common.Menu;

    public class BearRetreatOrbwalker : BearOrbwalker
    {
        private IAbilityUnit unit1;

        public BearRetreatOrbwalker()
        {
            this.LowHp = new AbilityMenuItem<Slider>(
                "BearLowHp",
                new Slider(400, 200, 1000),
                "retreat with bear when he has low hp");
        }

        public override IAbilityUnit Unit
        {
            get
            {
                return this.unit1;
            }

            set
            {
                this.unit1 = value;
                this.Bodyblocker.Unit = this.unit1;
                this.SkillBook = this.unit1.SkillBook as SpiritBearSkillBook;
            }
        }

        public AbilityMenuItem<Slider> LowHp { get; }

        public override bool PreciseIssue()
        {
            if (this.Unit.Health.Current < this.LowHp.Value)
            {
                return false;
            }

            return base.PreciseIssue();
        }

        public override bool IssueMeanwhileActions()
        {
            if (this.Unit.Health.Current < this.LowHp.Value)
            {
                if (!this.RunAround(this.LocalHero, Game.MousePosition))
                {
                    this.Unit.SourceUnit.Move(Game.MousePosition);
                }

                return true;
            }

            if (this.TargetValid
                && this.Unit.TargetSelector.LastDistanceToTarget - 700
                > this.LocalHero.TargetSelector.LastDistanceToTarget
                && this.LocalHero.TargetSelector.LastDistanceToTarget
                < this.Unit.Position.PredictedByLatency.Distance2D(this.LocalHero.Position.PredictedByLatency))
            {
                if (this.SkillBook.Return.CanCast())
                {
                    return this.SkillBook.Return.CastFunction.Cast();
                }
            }

            return base.IssueMeanwhileActions();
        }

        public override void Initialize()
        {
        }
    }
}
