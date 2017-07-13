namespace Ability.Core.AbilityModule.ModuleBase
{
    using System;
    using System.Collections.Generic;

    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Orbwalker;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderIssuer;
    using Ability.Core.AbilityModule.Combo;
    using Ability.Core.MenuManager.Menus.AbilityMenu.Submenus;

    //[InheritedExport(typeof(IAbilityHeroModule))]
    public abstract class AbilityHeroModuleBase : AbilityModuleBase, IAbilityHeroModule
    {
        private IAbilityUnit localHero;

        protected AbilityHeroModuleBase(
            string name,
            string shortDescription,
            bool generateMenu,
            bool enabledByDefault,
            bool loadOnGameStart,
            string heroName)
            : base(name, shortDescription, generateMenu, enabledByDefault, loadOnGameStart, heroName)
        {
            this.HeroName = heroName;
        }

        public string HeroName { get; }

        private List<OneKeyCombo> Combos { get; } = new List<OneKeyCombo>();

        public override void OnClose()
        {
            foreach (var oneKeyCombo in this.Combos)
            {
                oneKeyCombo.Dispose();
            }
        }

        public override IAbilityUnit LocalHero
        {
            get
            {
                return this.localHero;
            }

            set
            {
                this.localHero = value;
            }
        }

        public OneKeyCombo NewCombo(
            string name,
            List<IUnitOrbwalker> orbwalkers,
            List<IOrderIssuer> orderIssuers,
            uint key,
            Action targetAssign,
            Action targetReset,
            bool toggle = false,
            string description = null)
        {
            var subMenu = new AbilitySubMenu(name);
            subMenu.AddToMenu(this.Menu);

            foreach (var orbwalker in orbwalkers)
            {
                orderIssuers.Add(orbwalker);
            }

            var combo = new OneKeyCombo(
                orderIssuers,
                subMenu,
                key,
                2000,
                targetAssign,
                targetReset,
                toggle,
                description);
            this.Combos.Add(combo);
            return combo;
        }



        public void AddOrbwalker(IUnitOrbwalker orbwalker)
        {
            this.AbilityDataCollector.AddOrbwalker(orbwalker);
        }
    }
}
