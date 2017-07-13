using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Fighter.HeroSpecific.Troll
{
    using System.ComponentModel.Composition;
    using System.Runtime.CompilerServices;

    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Orbwalker;
    using Ability.Core.AbilityFactory.Utilities;
    using Ability.Core.AbilityModule.Metadata;
    using Ability.Core.AbilityModule.ModuleBase;
    using Ability.Core.MenuManager.Menus.AbilityMenu.Items;

    using Ensage;
    using Ensage.Common.Menu;

    [Export(typeof(IAbilityHeroModule))]
    [AbilityHeroModuleMetadata((uint)HeroId.npc_dota_hero_troll_warlord)]
    public class TrollFighterModule : AbilityHeroModuleBase
    {
        public TrollFighterModule()
            : base("TrollFighter", "Fights with your TrollWarlord", true, true, true, "npc_dota_hero_troll_warlord")
        {
        }

        public override void OnLoad()
        {
            //this.NewCombo("SoloFight Combo", 'G', new TrollOrbwalker(this.LocalHero));
        }
    }
}
