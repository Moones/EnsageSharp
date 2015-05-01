using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Ensage;
using SharpDX;
using SharpDX.Direct3D9;

namespace AxeBlinkUlti
{
    class Program
    {
        const int WM_KEYUP = 0x0101;
        const int WM_KEYDOWN = 0x0105;

        private static Hero _target;
        private static bool activated;
        static void Main(string[] args)
        {
            Game.OnGameUpdate += AutoChop;
        }

        static void AutoChop(EventArgs args)
        {
            if (Game.IsInGame || Game.IsPaused || activated)
                return;

            var me = EntityList.Hero;
            if (me == null)
                return;

            if (me.ClassId != ClassId.CDOTA_Unit_Hero_Axe)
            {
                Game.OnGameUpdate -= AutoChop;
                return;
            }

            Game.OnGameUpdate -= AutoChop;
            Game.OnGameWndProc += Game_OnGameWndProc;

            var Chop = me.Spellbook.SpellR;
            var blink = me.Inventory.Items.FirstOrDefault(x => x.Name == "item_blink");

            _target = GetChoppableEnemy(Chop.Damage);

            if (_target != null)
            {
                if (Vector3.DistanceSquared(me.Position, _target.Position) > 400)
                    { me.CastAbility(blink, _target.Position);
                      return; }
                else
                    { me.CastAbility(Chop, _target);
                    return;}
            }

        }

        static void Game_OnGameWndProc(WndProcEventArgs args)
        {
            if (args.MsgId != WM_KEYUP || args.WParam != 'F' || Game.IsChatOpen)
                return;

            if (activated == true)
            {
                activated = false;
                return;
            }

            if (activated == false)
            {
                activated = true;
                return;
            }

        }

        static Hero GetChoppableEnemy(int ChopDmg)
        {
            var enemies = EntityList.GetEntities<Hero>().Where(x => x.IsVisible && x.IsAlive && !x.IsIllusion && x.Team != EntityList.Player.Team && x.Health <= ChopDmg).ToList();
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
            return (float) Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }
    }
}
