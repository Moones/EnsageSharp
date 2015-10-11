namespace UrsaRage
{
    using System;
    using System.Linq;
    using System.Windows.Input;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Extensions;

    using SharpDX;
    using SharpDX.Direct3D9;

    internal class UrsaRage
    {
        #region Constants

        private const int WM_KEYUP = 0x0101;

        #endregion

        #region Static Fields

        private static readonly DotaTexture MarkTexture = Drawing.GetTexture(@"vgui\hud\minimap_glowenemy.vmat_c");

        private static readonly DotaTexture PortraitTexture = Drawing.GetTexture(
            @"vgui\hud\drawportraittoscreen.vmat_c");

        private static Item abyssalBlade;

        private static Item blink;

        private static Ability Earthshock;

        private static bool enableQ = true;

        private static float lastStack;

        private static bool loaded;

        private static Hero me;

        private static Vector3 mePosition;

        private static Hero target;

        private static Font text;

        #endregion

        #region Public Methods and Operators

        public static void Init()
        {
            Game.OnUpdate += Game_OnUpdate;
            loaded = false;
            text = new Font(
                Drawing.Direct3DDevice9,
                new FontDescription
                    {
                        FaceName = "Tahoma", Height = 13, OutputPrecision = FontPrecision.Default,
                        Quality = FontQuality.Default
                    });

            Drawing.OnPreReset += Drawing_OnPreReset;
            Drawing.OnPostReset += Drawing_OnPostReset;
            Drawing.OnEndScene += Drawing_OnEndScene;
            Drawing.OnDraw += Drawing_OnDraw;
            AppDomain.CurrentDomain.DomainUnload += CurrentDomainDomainUnload;
            Game.OnWndProc += Game_OnWndProc;
        }

        #endregion

        #region Methods

        private static void CurrentDomainDomainUnload(object sender, EventArgs e)
        {
            text.Dispose();
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            if (target == null)
            {
                return;
            }
            var pos = target.Position + new Vector3(0, 0, me.HealthBarOffset);
            Vector2 screenPos;
            if (!Drawing.WorldToScreen(pos, out screenPos))
            {
                return;
            }
            Drawing.DrawRect(new Vector2(screenPos.X - 65, screenPos.Y - 85), new Vector2(32, 32), PortraitTexture);
            Drawing.DrawRect(new Vector2(screenPos.X - 40, screenPos.Y - 100), new Vector2(64, 64), MarkTexture);
        }

        private static void Drawing_OnEndScene(EventArgs args)
        {
            if (Drawing.Direct3DDevice9 == null || Drawing.Direct3DDevice9.IsDisposed || !Game.IsInGame)
            {
                return;
            }

            var player = ObjectMgr.LocalPlayer;
            if (player == null || player.Team == Team.Observer)
            {
                return;
            }

            text.DrawText(
                null,
                enableQ ? "UrsaRage: Q - ENABLED! | [G] for toggle" : "UrsaRage: Q - DISABLED! | [G] for toggle",
                5,
                96,
                Color.Aquamarine);
        }

        private static void Drawing_OnPostReset(EventArgs args)
        {
            text.OnResetDevice();
        }

        private static void Drawing_OnPreReset(EventArgs args)
        {
            text.OnLostDevice();
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            if (!loaded)
            {
                me = ObjectMgr.LocalHero;
                Earthshock = me.Spellbook.Spell1;
                lastStack = 0;
                if (!Game.IsInGame || me == null)
                {
                    return;
                }
                loaded = true;
            }

            if (!Game.IsInGame || me == null)
            {
                loaded = false;
                return;
            }

            if (Game.IsPaused)
            {
                return;
            }

            if (blink == null)
            {
                blink = me.FindItem("item_blink");
            }

            if (abyssalBlade == null)
            {
                abyssalBlade = me.FindItem("item_abyssal_blade");
            }

            if (!Game.IsKeyDown(Key.Space) || Game.IsChatOpen)
            {
                target = null;
                lastStack = 0;
                return;
            }
            if (Utils.SleepCheck("blink"))
            {
                mePosition = me.Position;
            }
            var range = 500f;
            var blinkRange = 0f;
            var mousePosition = Game.MousePosition;
            if (blink != null)
            {
                blinkRange = blink.AbilityData.FirstOrDefault(x => x.Name == "blink_range").GetValue(0);
                range = blinkRange + me.HullRadius;
            }
            target = me.ClosestToMouseTarget(range);
            if (target == null || target.Distance2D(mousePosition) > target.Distance2D(me) + 700)
            {
                if (!Utils.SleepCheck("move") || me.IsAttacking())
                {
                    return;
                }
                me.Move(mousePosition);
                Utils.Sleep(100, "move");
                return;
            }
            var modifier = target.Modifiers.FirstOrDefault(x => x.Name == "modifier_ursa_fury_swipes_damage_increase");
            var stackCount = lastStack;
            if (modifier != null)
            {
                lastStack = modifier.StackCount;
            }
            var targetDistance = mePosition.Distance2D(target);
            var hullsum = (me.HullRadius + target.HullRadius) * 2;
            var turnTime =
                (Math.Max(
                    Math.Abs(me.FindAngleR() - Utils.DegreeToRadian(me.FindAngleBetween(target.Position))) - 0.69,
                    0) / (0.5 * (1 / 0.03)));
            var notAttacking = ((targetDistance > (me.AttackRange + hullsum) && !me.IsAttacking())
                                || ((stackCount < lastStack) && target.NetworkActivity != NetworkActivity.Idle1
                                    && target.NetworkActivity != NetworkActivity.Idle2));
            if (notAttacking)
            {
                if (blink != null && blink.CanBeCasted() && targetDistance > 400
                    && targetDistance < (blinkRange + hullsum) && Utils.SleepCheck("blink"))
                {
                    var position = target.Position + target.Vector3FromPolarAngle() * (hullsum + me.AttackRange);
                    var dist = position.Distance2D(mePosition);
                    if (dist > blinkRange)
                    {
                        position = (position - mePosition) * (blinkRange - 1) / position.Distance2D(me) + mePosition;
                    }
                    blink.UseAbility(position);
                    mePosition = position;
                    Utils.Sleep(Game.Ping + turnTime * 1000 + 100, "blink");
                    Utils.Sleep(Game.Ping + turnTime * 1000, "move");
                    return;
                }
                if (abyssalBlade != null && abyssalBlade.CanBeCasted() && targetDistance <= (300 + hullsum)
                    && Utils.SleepCheck("abyssal"))
                {
                    var canUse = Utils.ChainStun(target, Game.Ping / 1000 + turnTime, null, false);
                    if (canUse)
                    {
                        abyssalBlade.UseAbility(target);
                        Utils.Sleep(Game.Ping / 1000 + turnTime + 100, "abyssal");
                        Utils.Sleep(Game.Ping + turnTime * 1000, "move");
                        Utils.Sleep(Game.Ping / 1000 + turnTime + 500, "Q");
                        return;
                    }
                }
                if (Earthshock.CanBeCasted() && Utils.SleepCheck("Q") && enableQ)
                {
                    var radius = Earthshock.AbilityData.FirstOrDefault(x => x.Name == "shock_radius").GetValue(0);
                    if (me.Distance2D(target) <= (radius + hullsum))
                    {
                        var canUse = Utils.ChainStun(target, Game.Ping / 1000 + 300, null, false);
                        if (canUse && !target.IsStunned())
                        {
                            Earthshock.UseAbility();
                            Utils.Sleep(Game.Ping / 1000 + 300, "Q");
                            Utils.Sleep(Game.Ping + turnTime * 1000, "move");
                            return;
                        }
                    }
                }
                if (Utils.SleepCheck("move"))
                {
                    var position = target.Position + target.Vector3FromPolarAngle() * (hullsum + me.AttackRange);
                    me.Move(position);
                    Utils.Sleep(100, "move");
                    return;
                }
            }
            if (Utils.SleepCheck("attack") && targetDistance < (me.AttackRange + hullsum))
            {
                me.Attack(target);
                Utils.Sleep(150, "attack");
            }
        }

        private static void Game_OnWndProc(WndEventArgs args)
        {
            if (args.Msg != WM_KEYUP || args.WParam != 'G' || Game.IsChatOpen)
            {
                return;
            }
            enableQ = !enableQ;
        }

        #endregion
    }
}