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

    public class AttackAnimationData
    {
        public string UnitName;
        public ClassId UnitClassId;
        public double AttackRate;
        public double AttackPoint;
        public double AttackBackswing;
        public int ProjectileSpeed;
        public double TurnRate;
        private static double moveTime = 0;
        private static double endTime = 0;
        private static bool canMove = false;

        public AttackAnimationData() { }

        public AttackAnimationData(string unitName,
            ClassId unitClassId,
            double attackRate,
            double attackPoint,
            double attackBackswing,
            int projectileSpeed,
            double turnRate)
        {
            UnitName = unitName;
            UnitClassId = unitClassId;
            AttackRate = attackRate;
            AttackPoint = attackPoint;
            AttackBackswing = attackBackswing;
            ProjectileSpeed = projectileSpeed;
            TurnRate = turnRate;
        }

        static void Main(string[] args)
        {
            Entity.OnIntegerPropertyChange += Entity_OnIntegerPropertyChange;
        }

        static void Entity_OnIntegerPropertyChange(Entity sender, EntityIntegerPropertyChangeEventArgs args)
        {
            if (args.Property == "m_nGameState")
            {
                if (!Game.IsInGame)
                    return;
            }

            var me = EntityList.Hero;
            var meData = AttackAnimationDatabase.GetByClassId(me.ClassId);
            var gameTime = Game.GameTime;
            var attackSpeed = Math.Max(me.AttackSpeed, 600);
            var attackPoint = meData.AttackPoint / (1 + (attackSpeed - 100) / 100);
            var attackRate = me.AttackBaseTime / (1 + (attackSpeed - 100) / 100);
            if (sender != null && me.Equals(sender) && args.Property == "m_NetworkActivity")
            {
                if (args.NewValue == 424 || args.NewValue == 419)
                    if (moveTime == 0)
                    {
                        moveTime = gameTime + attackPoint;
                        endTime = gameTime + attackRate; 
                    }
                    else if (moveTime <= gameTime)
                    {
                        canMove = true;
                    }
                    else if (endTime <= gameTime)
                    {
                        canMove = false;
                        moveTime = 0;
                        endTime = 0;
                    }
                }
            }
        }

        //static bool HaveModifier(Entity ent, String name)
        //{
        //    foreach (var modifier in ent.modifiers)
        //    {
        //        if (modifier.name == name)
        //        {
        //            return true
        //        }
        //    }
        //    return false
        //}

    }
}
