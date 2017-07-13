namespace Ability.Core.AbilityModule.Combo
{
    using System;
    using System.Collections.Generic;

    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Orbwalker;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderIssuer;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.TargetSelector;
    using Ability.Core.AbilityFactory.Utilities;
    using Ability.Core.MenuManager.Menus.AbilityMenu.Items;
    using Ability.Core.MenuManager.Menus.AbilityMenu.Submenus;

    using Ensage.Common.Extensions;
    using Ensage.Common.Menu;

    public class OneKeyCombo : IDisposable
    {
        public OneKeyCombo(
            List<IOrderIssuer> orderIssuers,
            AbilitySubMenu subMenu,
            uint key,
            float maxTargetDistance,
            Action targetAssign,
            Action targetReset,
            bool toggle = false,
            string description = null)
        {
            this.OrderIssuers = orderIssuers;
            this.SubMenu = subMenu;
            this.MaxTargetDistance = maxTargetDistance;

            var name = "KeyTo" + (toggle ? "Toggle" : "Hold");

            this.Key = new AbilityMenuItem<KeyBind>(
                name,
                new KeyBind(key, toggle ? KeyBindType.Toggle : KeyBindType.Press));

            if (description != null)
            {
                this.SubMenu.AddDescription(description);
            }

            this.Key.NewValueProvider.Subscribe(
                new DataObserver<KeyBind>(
                    bind =>
                        {
                            if (bind.Active)
                            {
                                targetAssign();
                            }
                            else
                            {
                                targetReset();
                            }

                            foreach (var orderIssuer in this.OrderIssuers)
                            {
                                if (bind.Active)
                                {
                                    orderIssuer.Unit.AddOrderIssuer(orderIssuer);
                                }
                                else
                                {
                                    orderIssuer.Unit.RemoveOrderIssuer(orderIssuer);
                                }

                                orderIssuer.Enabled = bind.Active;
                            }
                        }));

            this.Key.AddToMenu(subMenu);
        }

        public float MaxTargetDistance { get; set; } = 2000;

        public IAbilityUnit Target { get; set; }

        public void AddOrderIssuer(IOrderIssuer orderIssuer)
        {
            var newList = new List<IOrderIssuer> { orderIssuer };
            newList.AddRange(this.OrderIssuers);
            this.OrderIssuers = newList;
        }

        public AbilityMenuItem<KeyBind> Key { get; }

        public IReadOnlyCollection<IOrderIssuer> OrderIssuers { get; set; }

        public AbilitySubMenu SubMenu { get; }

        public void Dispose()
        {
            this.Key.Dispose();
        }
    }
}
