using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.Meepo
{
    using Ensage.Common.Menu;

    internal class MeepoUnitMenu : HeroMenuBase
    {
        private Menu meepoNumberMenu;

        public override string HeroName { get; } = "npc_dota_hero_meepo";

        public override string MenuDisplayName { get; } = "Meepo";

        public override void ConnectUnit(IAbilityUnit unit)
        {
            //var overlay = unit.Overlay as MeepoOverlay;
            //if (this.meepoNumberMenu == null)
            //{
            //    this.meepoNumberMenu = ((IUnitOverlayElement)overlay.MeepoNumber).GenerateMenu(this);
            //}
            
            //overlay.MeepoNumber.ConnectToMenu(this, this.meepoNumberMenu);
        }
    }
}
