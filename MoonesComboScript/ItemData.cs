#region

using Ensage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace MoonesComboScript
{
    public class ItemData
    {
        public string Name;
        public bool Stun;
        public bool Slow;
        public bool Special;
        public float Range;
        public bool Killsteal;
        public bool ThroughBKB;
        public bool Retreat;

        public ItemData() { }

        public ItemData(string name,
            bool stun,
            bool slow,
            bool special,
            float range,
            bool killsteal,
            bool throughBKB,
            bool retreat)
        {
            Name = name;
            Stun = stun;
            Slow = slow;
            Special = special;
            Range = range;
            Killsteal = killsteal;
            ThroughBKB = throughBKB;
            Retreat = retreat;
        }

    }
}
