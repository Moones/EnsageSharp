namespace BreakerSharp.Utilities
{
    using System.Collections.Generic;
    using System.Linq;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.AbilityInfo;
    using Ensage.Common.Extensions;
    using Ensage.Common.Objects;

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
                                                                                { "item_orchid", 2 }, 
                                                                                { "item_shivas_guard", 1 }, 
                                                                                { "item_sheepstick", 0 }
                                                                            };

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
                if (!Variables.MenuManager.AbilityEnabled(item.StoredName()))
                {
                    continue;
                }

                if (!item.CanBeCasted())
                {
                    continue;
                }

                var data = AbilityDatabase.Find(item.StoredName());
                if (data == null)
                {
                    continue;
                }

                var n = "BreakerSharp." + item.StoredName();
                if (!Utils.SleepCheck(n))
                {
                    continue;
                }

                if (data.IsDisable || data.IsSilence || data.IsSlow || item.StoredName() == "item_heavens_halberd")
                {
                    if (!item.CastStun(target))
                    {
                        continue;
                    }

                    Utils.Sleep((item.GetCastDelay(Variables.Hero, target) * 1000) + Game.Ping, n);
                    return true;
                }

                if (data.WeakensEnemy)
                {
                    item.UseAbility(target);
                    Utils.Sleep((item.GetCastDelay(Variables.Hero, target) * 1000) + Game.Ping, n);
                    return true;
                }

                if (item.StoredName() == "item_urn_of_shadows")
                {
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

                item.UseAbility();
                Utils.Sleep(100, n);
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

            this.items = Variables.Hero.Inventory.Items.ToList();
            this.OrderItems();
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
            var e = args.Entity as Item;
            if (e != null && e.Owner.Equals(Variables.Hero))
            {
                this.items.Remove(e);
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