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
        public static bool canMove = false;

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
            Game.OnUpdate += TrackTick;
        }

        static void Entity_OnIntegerPropertyChange(Entity sender, EntityIntegerPropertyChangeEventArgs args)
        {
            if (args.Property == "m_nGameState")
            {
                if (!Game.IsInGame)
                    return;
            }

            var me = EntityList.Hero;
            var gameTime = Game.GameTime;
            var attackPoint = AttackAnimationDatabase.GetAttackPoint(me);
            var attackRate = AttackAnimationDatabase.GetAttackRate(me);

            if (sender != null && me.Equals(sender) && args.Property == "m_NetworkActivity")
            {
                if (args.NewValue == 424 || args.NewValue == 419)
                {    
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
                else
                {
                    canMove = false;
                    moveTime = 0;
                    endTime = 0;
                }
            }
        }
        static void TrackTick(EventArgs args)
        {
           var me = EntityList.Hero;
           var gameTime = Game.GameTime;
           if (moveTime != 0 && moveTime <= gameTime)
           {
               canMove = true;
           }
        }
    }
}
