namespace BreakerSharp.Utilities
{
    using System.Collections.Generic;
    using System.Linq;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.AbilityInfo;
    using Ensage.Common.Extensions;
    using Ensage.Common.Objects;
    using Ensage.Items;

    /// <summary>
    ///     The item combo.
    /// </summary>
    public class ItemCombo
    {
        #region Fields

        /// <summary>
        ///     The items order dictionary.
        /// </summary>
        private readonly Dictionary<string, int> itemsOrderDictionary = new Dictionary<string, int>
                                                                            {
                                                                                { "item_solar_crest", 2 }, 
                                                                                { "item_medallion_of_courage", 2 }, 
                                                                                { "item_urn_of_shadows", 2 }, 
                                                                                { "item_mask_of_madness", 2 }, 
                                                                                { "item_armlet", 2 }, { "item_orchid", 2 }, 
                                                                                { "item_shivas_guard", 1 }, 
                                                                                { "item_sheepstick", 0 }
                                                                            };

        /// <summary>
        ///     The invis.
        /// </summary>
        private Item invis;

        /// <summary>
        ///     The items.
        /// </summary>
        private List<Item> items;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ItemCombo" /> class.
        /// </summary>
        public ItemCombo()
        {
            this.UpdateItems();
            this.OrderItems();
            ObjectManager.OnAddEntity += this.ObjectManager_OnAddEntity;
            ObjectManager.OnRemoveEntity += this.ObjectManager_OnRemoveEntity;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets a value indicating whether pause.
        /// </summary>
        public bool Pause { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The execute combo.
        /// </summary>
        /// <param name="target">
        ///     The target.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public bool ExecuteCombo(Unit target)
        {
            if (!Variables.Hero.CanUseItems())
            {
                return false;
            }

            foreach (var item in this.items)
            {
                var name = item.StoredName();
                if (!item.IsValid || !Variables.MenuManager.AbilityEnabled(name) || !item.CanBeCasted())
                {
                    continue;
                }

                var data = AbilityDatabase.Find(name);
                if (data == null)
                {
                    continue;
                }

                var n = "BreakerSharp." + name;
                if (!Utils.SleepCheck(n))
                {
                    continue;
                }

                if (data.IsDisable || data.IsSilence || data.IsSlow || name == "item_heavens_halberd")
                {
                    if (!item.CanHit(target))
                    {
                        continue;
                    }

                    if (!item.CastStun(target))
                    {
                        continue;
                    }

                    Utils.Sleep((item.GetCastDelay(Variables.Hero, target) * 1000) + Game.Ping, n);
                    return true;
                }

                if (data.WeakensEnemy)
                {
                    if (!item.CanHit(target))
                    {
                        continue;
                    }

                    item.UseAbility(target);
                    Utils.Sleep((item.GetCastDelay(Variables.Hero, target) * 1000) + Game.Ping, n);
                    return true;
                }

                if (name == "item_urn_of_shadows")
                {
                    if (!item.CanHit(target))
                    {
                        continue;
                    }

                    if (!target.HasModifier("modifier_item_urn_damage"))
                    {
                        item.UseAbility(target);
                        Utils.Sleep(500, n);
                        return true;
                    }

                    continue;
                }

                if (!data.IsBuff || Variables.Hero.Distance2D(target) > 800)
                {
                    continue;
                }

                if (name == "item_armlet")
                {
                    var canEnableArmlet =
                        !Variables.Hero.HasModifiers(
                            new[] { "modifier_item_armlet_unholy_strength", "modifier_ice_blast" }, 
                            false) && !item.IsToggled;
                    if (!canEnableArmlet)
                    {
                        continue;
                    }

                    item.ToggleAbility();
                    Utils.Sleep(1000, n);
                    return true;
                }

                item.UseAbility();
                Utils.Sleep(100, n);
                return true;
            }

            return false;
        }

        /// <summary>
        ///     The update items.
        /// </summary>
        public void UpdateItems()
        {
            if (Variables.Hero == null || !Variables.Hero.IsValid)
            {
                return;
            }

            var myHeroItems = Variables.Hero.Inventory.Items.ToList();
            this.items =
                myHeroItems.Where(
                    x => x.Name != "item_invis_sword" && x.Name != "item_silver_edge" && x.Name != "item_glimmer_cape")
                    .ToList();
            var powerTreads = this.items.FirstOrDefault(x => x.StoredName() == "item_power_treads");
            if (powerTreads != null)
            {
                Variables.PowerTreadsSwitcher = new PowerTreadsSwitcher(powerTreads as PowerTreads);
            }

            var armlet = this.items.FirstOrDefault(x => x.Name == "item_armlet");
            if (armlet != null)
            {
                Variables.ArmletToggler = new ArmletToggler(armlet);
            }

            var invisItem =
                myHeroItems.FirstOrDefault(
                    x => x.Name == "item_invis_sword" || x.Name == "item_silver_edge" || x.Name == "item_glimmer_cape");
            if (invisItem != null)
            {
                this.invis = invisItem;
            }

            this.OrderItems();
        }

        /// <summary>
        ///     The use invisibility.
        /// </summary>
        /// <param name="overRide">
        ///     The over Ride.
        /// </param>
        public void UseInvis(bool overRide)
        {
            if (this.invis == null || !this.invis.IsValid || !this.invis.CanBeCasted()
                || !Variables.MenuManager.AbilityEnabled(this.invis.StoredName())
                || !Utils.SleepCheck("BreakerSharp.Invis") || (!Variables.ChargeOfDarkness.IsCharging && !overRide))
            {
                return;
            }

            if (this.invis.StoredName() == "item_glimmer_cape")
            {
                this.invis.UseAbility(Variables.Hero);
            }
            else
            {
                this.invis.UseAbility();
            }

            Utils.Sleep(500, "BreakerSharp.Invis");
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The get order.
        /// </summary>
        /// <param name="name">
        ///     The name.
        /// </param>
        /// <returns>
        ///     The <see cref="int" />.
        /// </returns>
        private int GetOrder(string name)
        {
            return !this.itemsOrderDictionary.ContainsKey(name) ? 4 : this.itemsOrderDictionary[name];
        }

        /// <summary>
        ///     The object manager_ on add entity.
        /// </summary>
        /// <param name="args">
        ///     The args.
        /// </param>
        private void ObjectManager_OnAddEntity(EntityEventArgs args)
        {
            if (this.Pause || Variables.Hero == null || !Variables.Hero.IsValid)
            {
                return;
            }

            var e = args.Entity as Item;
            DelayAction.Add(
                100, 
                () =>
                    {
                        try
                        {
                            if (e == null || e.Owner == null || !e.Owner.IsValid || !e.Owner.Equals(Variables.Hero))
                            {
                                return;
                            }

                            switch (e.StoredName())
                            {
                                case "item_invis_sword":
                                case "item_silver_edge":
                                case "item_glimmer_cape":
                                    this.invis = e;
                                    return;
                                case "item_power_treads":
                                    Variables.PowerTreadsSwitcher = new PowerTreadsSwitcher(e as PowerTreads);
                                    break;
                                case "item_armlet":
                                    Variables.ArmletToggler = new ArmletToggler(e);
                                    break;
                            }

                            this.items.Add(e);
                            this.OrderItems();
                        }
                        catch (EntityNotFoundException)
                        {
                            // no work
                        }
                    });
        }

        /// <summary>
        ///     The object manager_ on remove entity.
        /// </summary>
        /// <param name="args">
        ///     The args.
        /// </param>
        private void ObjectManager_OnRemoveEntity(EntityEventArgs args)
        {
            if (this.Pause || Variables.Hero == null || !Variables.Hero.IsValid)
            {
                return;
            }

            var e = args.Entity as Item;
            if (e == null || !e.Owner.Equals(Variables.Hero) || !this.items.Contains(e))
            {
                return;
            }

            this.items.Remove(e);
            if (e.StoredName() == "item_armlet" && Variables.ArmletToggler != null)
            {
                Variables.ArmletToggler = null;
            }

            if (e.StoredName() == "item_power_treads")
            {
                Variables.PowerTreadsSwitcher = null;
            }

            if (e.Equals(this.invis))
            {
                this.invis = null;
            }
        }

        /// <summary>
        ///     The order items.
        /// </summary>
        private void OrderItems()
        {
            if (this.items == null || !this.items.Any())
            {
                return;
            }

            this.items = this.items.OrderBy(x => this.GetOrder(x.StoredName())).ToList();
        }

        #endregion
    }
}