#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ensage;

#endregion

namespace MoonesComboScript
{
    class Program
    {
        const int WM_KEYUP = 0x0101;
        const int WM_KEYDOWN = 0x0105;

        private static Hero victim;
        private readonly static Timer Timer = new Timer();

        static void Main(string[] args)
        {
            Timer.Tick += Timer_Tick;
            Game.OnUpdate += OrbWalker;
        }

        static void Timer_Tick(object sender, EventArgs e)
        {
            Timer.Enabled = false;
        }

        static void OrbWalker(EventArgs args)
        {
            if (Timer.Enabled || !Game.IsInGame || Game.IsPaused)
                return;

            var me = EntityList.Hero;
            var attackRange = GetAttackRange(me);
            var victimdistance = GetDistance2D(victim.Position,me.Position);
            if (victim == null || victimdistance > attackRange+100)
                victim = GetClosestEnemyHeroToMouse();
            var canMove = AttackAnimationData.canMove;
            var mousePosition = Game.MousePosition;

            if (canMove == false && victim != null && !victim.UnitState.HasFlag(UnitState.AttackImmune) && victimdistance < attackRange+100)
            {
                me.Attack(victim);
                Timer.Start(200);
            }
            else
            {
                me.Move(mousePosition);
                Timer.Start(200);
            }
        }

        static Hero GetClosestEnemyHeroToMouse()
        {
            var mousePosition = Game.MousePosition;
            var enemies = EntityList.GetEntities<Hero>().Where(x => x.IsVisible && x.IsAlive && !x.IsIllusion && x.Team != EntityList.Player.Team).ToList();

            var minimumDistance = float.MaxValue;
            Hero result = null;
            foreach (var hero in enemies)
            {
                var distance = Vector3.DistanceSquared(mousePosition, hero.Position);
                if (result == null || distance < minimumDistance)
                {
                    minimumDistance = distance;
                    result = hero;
                }
            }
            return result;
        }

        static float GetAttackRange(Unit unit)
        {
            var bonus = 0;
            ClassId classId = unit.ClassId;
            if (classId == ClassId.CDOTA_Unit_Hero_TemplarAssassin)
            {
                Ability psi = unit.Spellbook.SpellW;
            } 
            else if (classId == ClassId.CDOTA_Unit_Hero_Sniper)
            {
                Ability aim = unit.Spellbook.SpellE;
            }
            else if (classId == ClassId.CDOTA_Unit_Hero_Enchantress)
            {
                Ability impetus = unit.Spellbook.SpellR;
                if (impetus.Level > 0 && unit.Inventory.Items.Any(x => (x.Name == "item_ultimate_scepter")))
                    bonus = 190;
            } 
            else if (unit.Modifiers.Any(x => (x.Name == "modifier_lone_druid_true_form")))
                bonus = -423;
            else if (unit.Modifiers.Any(x => (x.Name =="dragon_knight_elder_dragon_form")))
                bonus = 372;
            else if (unit.Modifiers.Any(x => (x.Name == "terrorblade_metamorphosis")))
                bonus = 422;
            return unit.AttackRange + bonus;
        }

        static float FindAngleR(Entity ent)
        {
            return (float)(ent.RotationRad < 0 ? Math.Abs(ent.RotationRad) : 2 * Math.PI - ent.RotationRad);
        }

        static float FindAngleBetween(Vector3 first, Vector3 second)
        {
            var xAngle = (float)(Math.Atan(Math.Abs(second.X - first.X) / Math.Abs(second.Y - first.Y)) * (180.0 / Math.PI));
            if (first.X <= second.X && first.Y >= second.Y)
                return 90 - xAngle;
            if (first.X >= second.X && first.Y >= second.Y)
                return xAngle + 90;
            if (first.X >= second.X && first.Y <= second.Y)
                return 90 - xAngle + 180;
            if (first.X <= second.X && first.Y <= second.Y)
                return xAngle + 90 + 180;
            return 0;
        }

        static float GetDistance2D(Vector3 p1, Vector3 p2)
        {
            return (float)Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }

        static double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }
           
    }
}
