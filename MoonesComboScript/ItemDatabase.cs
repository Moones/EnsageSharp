#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ensage;

#endregion

namespace MoonesComboScript
{

    public static class ItemDatabase
    {
        public static List<ItemData> Items = new List<ItemData>();

        static ItemDatabase()
        {
            #region SoulRing

            Items.Add(
                new ItemData
                {
                    Name = "item_soul_ring",
                    Stun = false,
                    Slow = false,
                    Special = false,
                    //Range = float.MaxValue,
                    Killsteal = false,
                    ThroughBKB = true,
                    Retreat = true,
                });

            #endregion

            #region VeilOfDiscord

            Items.Add(
                new ItemData
                {
                    Name = "item_veil_of_discord",
                    Stun = false,
                    Slow = false,
                    Special = false,
                    //Range = null,
                    Killsteal = false,
                    ThroughBKB = false,
                    Retreat = true,
                });

            #endregion

            #region Cyclone

            Items.Add(
                new ItemData
                {
                    Name = "item_cyclone",
                    Stun = true,
                    Slow = false,
                    Special = false,
                    Killsteal = false,
                    ThroughBKB = false,
                    Retreat = true,
                });

            #endregion

            #region RodOfAtos

            Items.Add(
                new ItemData
                {
                    Name = "item_rod_of_atos",
                    Stun = false,
                    Slow = true,
                    Special = false,
                    Killsteal = false,
                    ThroughBKB = false,
                    Retreat = true,
                });

            #endregion

        }
    }
}
