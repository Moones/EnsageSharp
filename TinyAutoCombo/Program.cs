using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Ensage;
using SharpDX;
using SharpDX.Direct3D9;

namespace TinyAutoCombo
{
    class Program
    {
        const int WM_KEYUP = 0x0101;
        const int WM_KEYDOWN = 0x0105;

        private static Hero _target;
        private static bool activated;
        static void Main(string[] args)
        {
            Game.OnGameUpdate += AutoCombo;
        }

        static void AutoCombo(EventArgs args)
        {
            if (Game.IsInGame || Game.IsPaused || activated)
                return;

            var me = EntityList.Hero;
            if (me == null)
                return;

            if (me.ClassId != ClassId.CDOTA_Unit_Hero_Tiny)
            {
                Game.OnGameUpdate -= AutoCombo;
                return;
            }

            Game.OnGameUpdate -= AutoCombo;
            Game.OnGameWndProc += Game_OnGameWndProc;

            var Avalanche = me.Spellbook.SpellQ;
            var Toss = me.Spellbook.SpellW;
            var blink = me.Inventory.Items.FirstOrDefault(x => x.Name == "item_blink");

            if (_target != null && _target.IsAlive && _target.IsVisible)
            {
                if (Vector3.DistanceSquared(me.Position, _target.Position) > 400)
                {
                    me.CastAbility(blink, _target.Position);
                    return;
                }
                else
                {
                    me.CastAbility(Avalanche, _target.Position);
                    me.CastAbility(Toss, _target);
                    return;
                }
            }

        }

        static void Game_OnGameWndProc(WndProcEventArgs args)
        {
            if (args.MsgId != WM_KEYUP || args.WParam != 'F' || Game.IsChatOpen)
                return;

            if (activated == true)
            {
                _target = GetClosestEnemyHeroToMouse();
                activated = false;
                return;
            }

            if (activated == false)
            {
                _target = null;
                activated = true;
                return;
            }

        }

        static Hero GetClosestEnemyHeroToMouse()
        {
            var mousePosition = Game.MousePosition;
            var enemies = EntityList.GetEntities<Hero>().Where(x => x.IsVisible && x.IsAlive && !x.IsIllusion && x.Team != EntityList.Player.Team && !x.UnitState.HasFlag(UnitState.MagicImmune)).ToList();

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

        static Hero GetKillableEnemy(int Dmg)
        {
            var enemies = EntityList.GetEntities<Hero>().Where(x => x.IsVisible && x.IsAlive && !x.IsIllusion && x.Team != EntityList.Player.Team && x.Health <= Dmg).ToList();
            var me = EntityList.GetLocalPlayer().Hero;

            Hero result = null;
            var int minimumDistance = 1500;

            foreach (var hero in enemies)
            {
                var distance = Vector3.DistanceSquared(me.Position, hero.Position);
                if (result == null || distance < minimumDistance)
                {
                    minimumDistance = distance;
                    result = hero;
                }
            }
            return result;
        }

        static float GetDistance2D(Vector3 p1, Vector3 p2)
        {
            return (float)Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }
    }
}
