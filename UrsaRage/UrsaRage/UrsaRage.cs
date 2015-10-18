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

    using unitDB = Ensage.Common.UnitDatabase;

    internal class UrsaRage
    {
        #region Static Fields

        private static Item abyssalBlade;

        private static Item scytheOfVyse;

        private static Item blink;

        private static float blinkRange;

        private static Ability earthshock;

        private static double earthshockCastPoint;

        private static bool enableQ = true;

        private static Ability enrage;

        private static float hullsum;

        private static bool loaded;

        private static Hero me;

        private static Vector3 mePosition;

        private static Ability overpower;

        private static double overpowerCastPoint;

        private static Hero target;

        private static float targetDistance;

        private static Font text;

        private static double turnTime;

        #endregion

        #region Public Methods and Operators

        public static void Init()
        {
            Game.OnUpdate += Game_OnUpdate;
            loaded = false;
            me = null;
            target = null;
            earthshock = null;
            overpower = null;
            enrage = null;
            abyssalBlade = null;
            scytheOfVyse = null;
            blink = null;
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
            AppDomain.CurrentDomain.DomainUnload += CurrentDomainDomainUnload;
            Game.OnWndProc += Game_OnWndProc;
            Orbwalking.Load();
        }

        #endregion

        #region Methods

        private static bool CastCombo()
        {
            var canCancel = (Orbwalking.CanCancelAnimation()
                             && (Orbwalking.AttackOnCooldown(target) || !me.IsAttacking()))
                            || (!Orbwalking.AttackOnCooldown(target) && targetDistance > 250);
            if (!Utils.SleepCheck("casting") || !me.CanCast() || !target.IsVisible || !canCancel)
            {
                return false;
            }
            if (abyssalBlade != null && abyssalBlade.CanBeCasted() && targetDistance <= (350 + hullsum)
                && Utils.SleepCheck("abyssal"))
            {
                var canUse = Utils.ChainStun(target, turnTime + 0.1 + Game.Ping / 1000, null, false);
                if (canUse)
                {
                    abyssalBlade.UseAbility(target);
                    Utils.Sleep(turnTime * 1000 + 100 + Game.Ping, "abyssal");
                    Utils.Sleep(turnTime * 1000 + 50, "move");
                    Utils.Sleep(
                        turnTime * 1000 + 100
                        + (Math.Max(targetDistance - hullsum - abyssalBlade.CastRange, 0) / me.MovementSpeed) * 1000,
                        "casting");
                    Utils.Sleep(turnTime * 1000 + 200, "CHAINSTUN_SLEEP");
                    return true;
                }
            }
            if (scytheOfVyse != null && scytheOfVyse.CanBeCasted() && targetDistance <= (scytheOfVyse.CastRange + hullsum)
                && Utils.SleepCheck("hex"))
            {
                var canUse = Utils.ChainStun(target, turnTime + 0.1 + Game.Ping / 1000, null, false);
                if (canUse)
                {
                    scytheOfVyse.UseAbility(target);
                    Utils.Sleep(turnTime * 1000 + 100 + Game.Ping, "hex");
                    Utils.Sleep(turnTime * 1000 + 50, "move");
                    Utils.Sleep(
                        turnTime * 1000 + 100
                        + (Math.Max(targetDistance - hullsum - scytheOfVyse.CastRange, 0) / me.MovementSpeed) * 1000,
                        "casting");
                    Utils.Sleep(turnTime * 1000 + 200, "CHAINSTUN_SLEEP");
                    return true;
                }
            }
            if (earthshock.CanBeCasted() && Utils.SleepCheck("Q") && enableQ
                && ((me.Mana - earthshock.ManaCost) > overpower.ManaCost || !overpower.CanBeCasted()))
            {
                var radius = earthshock.AbilityData.FirstOrDefault(x => x.Name == "shock_radius").GetValue(0);
                var pos = target.Position
                          + target.Vector3FromPolarAngle() * ((Game.Ping / 1000 + 0.3f) * target.MovementSpeed);

                if (mePosition.Distance(pos) < targetDistance)
                {
                    pos = target.Position;
                }
                if (mePosition.Distance2D(pos) <= (radius)
                    && (abyssalBlade == null || !abyssalBlade.CanBeCasted() || mePosition.Distance2D(pos) > 200)
                    && (scytheOfVyse == null || !scytheOfVyse.CanBeCasted() || mePosition.Distance2D(pos) > 200))
                {
                    var canUse = Utils.ChainStun(target, 0.3 + Game.Ping / 1000, null, false);
                    if (canUse)
                    {
                        earthshock.UseAbility();
                        Utils.Sleep(earthshockCastPoint * 1000 + Game.Ping, "Q");
                        Utils.Sleep(earthshockCastPoint * 1000, "casting");
                        return true;
                    }
                }
                else if (Utils.SleepCheck("moveCloser"))
                {
                    me.Move(pos);
                    Utils.Sleep(200, "moveCloser");
                    return true;
                }
            }
            if (blink != null && blink.CanBeCasted() && targetDistance > 400
                && targetDistance < (blinkRange + hullsum * 2 + me.AttackRange) && Utils.SleepCheck("blink"))
            {
                var position = target.Position;
                if (target.NetworkActivity != NetworkActivity.Idle)
                {
                    position = target.Position + target.Vector3FromPolarAngle() * (hullsum + me.AttackRange);
                    if (mePosition.Distance(position) < targetDistance)
                    {
                        position = target.Position;
                    }
                }
                var dist = position.Distance2D(mePosition);
                if (dist > blinkRange)
                {
                    position = (position - mePosition) * (blinkRange - 1) / position.Distance2D(me) + mePosition;
                }
                blink.UseAbility(position);
                mePosition = position;
                Utils.Sleep(turnTime * 1000 + 100 + Game.Ping, "blink");
                Utils.Sleep(turnTime * 1000 + 50, "move");
                Utils.Sleep(turnTime * 1000, "casting");
                return true;
            }
            const int Radius = 300;
            var canAttack = !target.IsInvul() && !target.IsAttackImmune() && me.CanAttack();
            if (!canAttack)
            {
                return false;
            }
            if (overpower.CanBeCasted() && Utils.SleepCheck("W")
                && !(earthshock.CanBeCasted() && enableQ && Utils.ChainStun(target, 0.3 + Game.Ping / 1000, null, false)))
            {
                if (mePosition.Distance2D(target) <= (Radius + hullsum))
                {
                    overpower.UseAbility();
                    Utils.Sleep(overpowerCastPoint * 1000 + Game.Ping, "W");
                    Utils.Sleep(overpowerCastPoint * 1000, "casting");
                    return true;
                }
            }
            if (!enrage.CanBeCasted() || !Utils.SleepCheck("R"))
            {
                return false;
            }
            if (!(mePosition.Distance2D(target) <= (Radius + hullsum)))
            {
                return false;
            }
            enrage.UseAbility();
            Utils.Sleep(100 + Game.Ping, "R");
            Utils.Sleep(100, "casting");
            return true;
        }

        private static void CurrentDomainDomainUnload(object sender, EventArgs e)
        {
            text.Dispose();
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
                Color.IndianRed);
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
                if (!Game.IsInGame || me == null || me.ClassID != ClassID.CDOTA_Unit_Hero_Ursa)
                {
                    return;
                }
                earthshock = me.Spellbook.Spell1;
                overpower = me.Spellbook.SpellW;
                enrage = me.FindSpell("ursa_enrage");
                blink = me.FindItem("item_blink");
                abyssalBlade = me.FindItem("item_abyssal_blade");
                scytheOfVyse = me.FindItem("item_sheepstick");
                loaded = true;
            }

            if (!Game.IsInGame || me == null)
            {
                overpowerCastPoint = 0;
                earthshockCastPoint = 0;
                loaded = false;
                me = null;
                target = null;
                earthshock = null;
                overpower = null;
                enrage = null;
                abyssalBlade = null;
                scytheOfVyse = null;
                blink = null;
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

            if (abyssalBlade == null)
            {
                abyssalBlade = me.FindItem("item_sheepstick");
            }

            if (earthshock == null)
            {
                earthshock = me.Spellbook.Spell1;
            }
            else if (earthshockCastPoint == 0)
            {
                earthshockCastPoint = 0.3;
            }

            if (overpower == null)
            {
                overpower = me.Spellbook.SpellW;
            }
            else if (overpowerCastPoint == 0)
            {
                overpowerCastPoint = 0.3;
            }

            if (enrage == null)
            {
                enrage = me.FindSpell("ursa_enrage");
            }

            if (!Game.IsKeyDown(Key.Space) || Game.IsChatOpen)
            {
                target = null;
                return;
            }
            if (Utils.SleepCheck("blink"))
            {
                mePosition = me.Position;
            }
            if (earthshock.IsInAbilityPhase && (target == null || !target.IsAlive || target.Distance2D(me) > earthshock.AbilityData.FirstOrDefault(x => x.Name == "shock_radius").GetValue(0)))
            {
                me.Stop();
                if (target != null)
                {
                    me.Attack(target);
                }
            }
            if (overpower.IsInAbilityPhase && (target == null || !target.IsAlive))
            {
                me.Stop();
                if (target != null)
                {
                    me.Attack(target);
                }
            }
            var range = 1000f;
            var mousePosition = Game.MousePosition;
            if (blink != null)
            {
                blinkRange = blink.AbilityData.FirstOrDefault(x => x.Name == "blink_range").GetValue(0);
                range = blinkRange + me.HullRadius + 500;
            }
            var canCancel = (Orbwalking.CanCancelAnimation() && Orbwalking.AttackOnCooldown(target))
                            || (!Orbwalking.AttackOnCooldown(target)
                                && (targetDistance > 350 || (target != null && !target.IsVisible))) || target == null;
            if (canCancel)
            {
                if (target != null && !target.IsVisible)
                {
                    var closestToMouse = me.ClosestToMouseTarget(128);
                    if (closestToMouse != null)
                    {
                        target = me.ClosestToMouseTarget(range);
                    }
                }
                else
                {
                    target = me.ClosestToMouseTarget(range);
                }       
            }
            if (target == null || !target.IsAlive || ((!target.IsVisible
                   || target.Distance2D(mousePosition) > target.Distance2D(me) + 1000) && canCancel))
            {
                if (!Utils.SleepCheck("move"))
                {
                    return;
                }
                me.Move(mousePosition);
                Utils.Sleep(100, "move");
                return;
            }
            targetDistance = mePosition.Distance2D(target);
            hullsum = (me.HullRadius + target.HullRadius) * 2;
            turnTime = me.GetTurnTime(target);
            var casting = CastCombo();
            if (casting)
            {
                return;
            }
            if (!Utils.SleepCheck("casting"))
            {
                return;
            }
            OrbWalk(Orbwalking.CanCancelAnimation());
        }

        private static void Game_OnWndProc(WndEventArgs args)
        {
            if (args.Msg != (ulong)Utils.WindowsMessages.WM_KEYUP || args.WParam != 'G' || Game.IsChatOpen)
            {
                return;
            }
            enableQ = !enableQ;
        }

        private static void OrbWalk(bool canCancel)
        {
            //var modifier = target.Modifiers.FirstOrDefault(x => x.Name == "modifier_ursa_fury_swipes_damage_increase");
            // && UnitDatabase.GetAttackSpeed(me) < 300 && !me.Modifiers.Any(x => x.Name == "modifier_ursa_overpower")
            //var overpowering = me.Modifiers.Any(x => x.Name == "modifier_ursa_overpower");
            var canAttack = !Orbwalking.AttackOnCooldown(target) && !target.IsInvul() && !target.IsAttackImmune()
                            && me.CanAttack();
            if (canAttack && (targetDistance <= (350)))
            {
                if (!Utils.SleepCheck("attack"))
                {
                    return;
                }
                me.Attack(target);
                Utils.Sleep(100, "attack");
                return;
            }

            var canMove = (canCancel && Orbwalking.AttackOnCooldown(target)) || (!Orbwalking.AttackOnCooldown(target) && targetDistance > 350);
            if (!Utils.SleepCheck("move") || !canMove)
            {
                return;
            }
            var mousePos = Game.MousePosition;
            if (target.Distance2D(me) < 500)
            {
                var pos = target.Position
                          + target.Vector3FromPolarAngle()
                          * (float)Math.Max((Game.Ping / 1000 + (targetDistance / me.MovementSpeed) + turnTime) * target.MovementSpeed, 500);

                //Console.WriteLine(pos.Distance(me.Position) + " " + target.Distance2D(pos));
                if (pos.Distance(me.Position) > target.Distance2D(pos) - 80)
                {

                    me.Move(pos);
                }
                else
                {
                    me.Follow(target);
                }
            }
            else
            {
                me.Move(mousePos);
            }
            Utils.Sleep(100, "move");
        }

        #endregion
    }
}