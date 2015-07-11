#region

using System;
using System.Linq;

#endregion

namespace Ensage.Common
{
    public class HeroData
    {
        public string UnitName;
        public ClassId UnitClassId;
        public double AttackRate;
        public double AttackPoint;
        public double AttackBackswing;
        public int ProjectileSpeed;
        public double TurnRate;
        public double MoveTime;
        public double EndTime;
        public bool CanMove;

        public HeroData() { }

        public HeroData(string unitName,
            ClassId unitClassId,
            double attackRate,
            double attackPoint,
            double attackBackswing,
            int projectileSpeed,
            double turnRate,
            double moveTime,
            double endTime,
            bool canMove)
        {
            UnitName = unitName;
            UnitClassId = unitClassId;
            AttackRate = attackRate;
            AttackPoint = attackPoint;
            AttackBackswing = attackBackswing;
            ProjectileSpeed = projectileSpeed;
            TurnRate = turnRate;
            MoveTime = moveTime;
            EndTime = endTime;
            CanMove = canMove;
        }

        public static bool IsInBackswingtime(Unit unit)
        {
            var attackPoint = HeroDatabase.GetAttackPoint(unit);
            if (attackPoint * 1000 < Game.Ping)
                return false;
            var data = HeroDatabase.GetByClassId(unit.ClassId) ?? HeroDatabase.GetByName(unit.Name);
            return data != null && data.CanMove;
        }

        public static void Entity_OnIntegerPropertyChange(Entity sender, EntityIntegerPropertyChangeEventArgs args)
        {
            if (!Game.IsInGame || Game.IsPaused || args.Property != "m_NetworkActivity")
                return;

            var me = EntityList.Hero;
            if (me == null)
                return;

            var unit = sender as Unit;
            var data = HeroDatabase.GetByClassId(unit.ClassId) ?? HeroDatabase.GetByName(unit.Name);
            if (data == null) 
                return;
            var gameTime = Game.GameTime;
            var attackPoint = HeroDatabase.GetAttackPoint(unit);
            var attackRate = HeroDatabase.GetAttackRate(unit);
            //Console.WriteLine(attackPoint + " " + attackRate);

            if (args.NewValue == 424 && Math.Abs(data.MoveTime) < 0.00001)
            {
                data.MoveTime = gameTime + attackPoint;
                data.EndTime = gameTime + attackRate;
                // Console.WriteLine(gameTime + " " + data.MoveTime + " " + data.EndTime);
            }
            else if (data.MoveTime > 0 && gameTime > data.MoveTime)
            {
                data.CanMove = true;
            }
            else if (data.EndTime > 0 && data.EndTime <= gameTime)
            {
                data.CanMove = false;
                data.MoveTime = 0;
                data.EndTime = 0;
            }
        }

        public static double Count;
        public static double MaxCount;
        public static double StartTime = 0;

        public static void TrackTick(EventArgs args)
        {
            if (!Game.IsInGame || Game.IsPaused)
                return;
            var me = EntityList.Hero;
            if (me == null) return;
            var gameTime = Game.GameTime;
            var tick = Environment.TickCount;
            if (StartTime == 0)
                StartTime = gameTime;
            else if (gameTime - StartTime >= 1)
            {
                StartTime = gameTime;
                MaxCount = Count;
                Count = 0;
            }
            else
                Count += 1;
            // Console.WriteLine(MaxCount);
            var units = EntityList.GetEntities<Entity>();
            foreach (var data in from unit in units let classId = unit.ClassId let name = unit.Name select HeroDatabase.GetByClassId(classId) ?? HeroDatabase.GetByName(name) into data where data != null select data)
            {

                if (data.MoveTime > 0 && gameTime > data.MoveTime)
                {
                    data.CanMove = true;
                }
                if (!(data.EndTime <= gameTime && data.EndTime > 0)) continue;
                data.CanMove = false;
                data.MoveTime = 0;
                data.EndTime = 0;
            }
        }

    }
}
