using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Fighter.LoneDruid
{
    using System.ComponentModel.Composition;

    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Bodyblocker;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Orbwalker;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderIssuer;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Units.SpiritBear.SkillBook;
    using Ability.Core.AbilityFactory.Utilities;
    using Ability.Core.AbilityModule.Combo;
    using Ability.Core.AbilityModule.Metadata;
    using Ability.Core.AbilityModule.ModuleBase;
    using Ability.Fighter.LoneDruid.BodyblockCombo;
    using Ability.Fighter.LoneDruid.ChaseCombo;
    using Ability.Fighter.LoneDruid.RetreatCombo;

    using Ensage;
    using Ensage.Common.Menu;

    [Export(typeof(IAbilityHeroModule))]
    [AbilityHeroModuleMetadata((uint)HeroId.npc_dota_hero_lone_druid)]
    internal class LoneDruidFighterModule : AbilityHeroModuleBase, IAbilityUnitModule
    {
        public LoneDruidFighterModule()
            : base("LoneDruidFighter", Program.Description("LoneDruid"), true, true, true, "npc_dota_hero_lone_druid")
        {
        }

        public IAbilityUnit Bear { get; set; }

        public UnitBodyblocker BearBodyblocker { get; set; }

        public BearOrbwalker BearOrbwalker { get; set; }

        public BearRetreatOrbwalker BearRetreatOrbwalker { get; set; }

        public LoneDruidOrbwalker LoneDruidOrbwalker { get; set; }

        public OneKeyCombo BodyBlockCombo { get; set; }

        public OneKeyCombo ChaseCombo { get; set; }

        public OneKeyCombo RetreatCombo { get; set; }

        public override void OnLoad()
        {
            this.LoneDruidOrbwalker = new LoneDruidOrbwalker(this.LocalHero);
            this.AddOrbwalker(this.LoneDruidOrbwalker);

            var orbwalkers = new List<IUnitOrbwalker> { this.LoneDruidOrbwalker };
            var orderIssuers = new List<IOrderIssuer>();

            var targetReset = new Action(
                () =>
                    {
                        this.LocalHero.TargetSelector.ResetTarget();
                    });

            var targetAssign = new Action(() =>
                {
                    this.LocalHero.TargetSelector.GetTarget();
                });

            this.LocalHero.TargetSelector.TargetChanged.Subscribe(
                () =>
                    {
                        if (this.Bear == null)
                        {
                            return;
                        }

                        this.Bear.TargetSelector.Target = this.LocalHero.TargetSelector.Target;
                    });

            this.BodyBlockCombo = this.NewCombo(
                "BodyBlockCombo",
                orbwalkers,
                orderIssuers,
                'B',
                targetAssign,
                targetReset,
                false,
                "bear will bodyblock, lone druid will attack");

            this.ChaseCombo = this.NewCombo(
                "ChaseCombo",
                orbwalkers,
                orderIssuers,
                'G',
                targetAssign,
                targetReset,
                false,
                "bear will chase enemy, lone will attack and move to mouse and keep range if not in true form");

            this.BearRetreatOrbwalker = new BearRetreatOrbwalker { LocalHero = this.LocalHero };

            this.RetreatCombo = this.NewCombo(
                "RetreatCombo",
                new List<IUnitOrbwalker> { new LoneDruidRetreatOrbwalker(this.LocalHero) },
                new List<IOrderIssuer>(),
                'H',
                () => { this.Bear?.TargetSelector.GetTarget(); },
                () => { this.Bear?.TargetSelector.ResetTarget(); },
                false,
                "lone will run to mouse, bear will attack/bodyblock target or run if low hp");

            this.BearRetreatOrbwalker.LowHp.AddToMenu(this.RetreatCombo.SubMenu);
        }

        public void UnitAdded(IAbilityUnit unit)
        {
            if (!unit.IsEnemy && this.IsBear(unit))
            {
                if (this.Bear != null)
                {
                    unit.TargetSelector.Target = this.Bear.TargetSelector.Target;
                }

                this.Bear = unit;

                if (this.BearBodyblocker == null)
                {
                    this.BearBodyblocker = new BearBodyblocker(this.Bear);
                    this.BearOrbwalker = new BearOrbwalker { LocalHero = this.LocalHero, Unit = this.Bear };
                    this.BearRetreatOrbwalker.Unit = this.Bear;

                    this.AddOrbwalker(this.BearOrbwalker);
                    this.AddOrbwalker(this.BearRetreatOrbwalker);

                    this.BodyBlockCombo.AddOrderIssuer(this.BearBodyblocker);
                    this.ChaseCombo.AddOrderIssuer(this.BearOrbwalker);
                    this.RetreatCombo.AddOrderIssuer(this.BearRetreatOrbwalker);
                }
                else
                {
                    this.BearBodyblocker.Unit = this.Bear;
                    this.BearOrbwalker.Unit = this.Bear;
                    this.BearRetreatOrbwalker.Unit = this.Bear;

                    if (this.BearBodyblocker.Enabled)
                    {
                        this.Bear.AddOrderIssuer(this.BearBodyblocker);
                    }

                    if (this.BearOrbwalker.Enabled)
                    {
                        this.Bear.AddOrderIssuer(this.BearOrbwalker);
                    }

                    if (this.BearRetreatOrbwalker.Enabled)
                    {
                        this.Bear.AddOrderIssuer(this.BearRetreatOrbwalker);
                    }
                }
            }
        }

        public void UnitRemoved(IAbilityUnit unit)
        {
        }

        public bool IsBear(IAbilityUnit unit)
        {
            return unit.SkillBook is SpiritBearSkillBook;
        }
    }
}
