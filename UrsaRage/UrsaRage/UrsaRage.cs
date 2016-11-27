namespace UrsaRage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Extensions;
    using Ensage.Common.Menu;

    using SharpDX;

    /// <summary>
    ///     The ursa rage.
    /// </summary>
    internal class UrsaRage
    {
        #region Static Fields

        /// <summary>
        ///     The menu.
        /// </summary>
        private static readonly Menu Menu = new Menu("UrsaRage", "ursaRage", true, "npc_dota_hero_ursa", true);

        /// <summary>
        ///     The abyssal blade.
        /// </summary>
        private static Item abyssalBlade;

        /// <summary>
        ///     The blink.
        /// </summary>
        private static Item blink;

        /// <summary>
        ///     The blink range.
        /// </summary>
        private static float blinkRange;

        /// <summary>
        ///     The earth shock.
        /// </summary>
        private static Ability earthshock;

        /// <summary>
        ///     The earth shock cast point.
        /// </summary>
        private static double earthshockCastPoint;

        /// <summary>
        ///     The enrage.
        /// </summary>
        private static Ability enrage;

        /// <summary>
        ///     The hull sum.
        /// </summary>
        private static float hullsum;

        /// <summary>
        ///     The loaded.
        /// </summary>
        private static bool loaded;

        /// <summary>
        ///     The me.
        /// </summary>
        private static Hero me;

        /// <summary>
        ///     The menu value.
        /// </summary>
        private static AbilityToggler menuValue;

        /// <summary>
        ///     The menuvalue set.
        /// </summary>
        private static bool menuvalueSet;

        /// <summary>
        ///     The me position.
        /// </summary>
        private static Vector3 mePosition;

        /// <summary>
        ///     The overpower.
        /// </summary>
        private static Ability overpower;

        /// <summary>
        ///     The overpower cast point.
        /// </summary>
        private static double overpowerCastPoint;

        /// <summary>
        ///     The scythe of vyse.
        /// </summary>
        private static Item scytheOfVyse;

        /// <summary>
        ///     The target.
        /// </summary>
        private static Hero target;

        /// <summary>
        ///     The target distance.
        /// </summary>
        private static float targetDistance;

        /// <summary>
        ///     The turn time.
        /// </summary>
        private static double turnTime;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The init.
        /// </summary>
        public static void Init()
        {
            var dict = new Dictionary<string, bool>
                           {
                               { "ursa_enrage", true }, { "ursa_overpower", true }, { "ursa_earthshock", true }, 
                               { "item_sheepstick", true }, { "item_abyssal_blade", true }, { "item_blink", true }
                           };
            Menu.AddItem(new MenuItem("enabledAbilities", "Abilities:").SetValue(new AbilityToggler(dict)));
            Menu.AddItem(
                new MenuItem("manaSlider", "Minimum mana for combo:").SetValue(new Slider(0, 0, 500))
                    .SetTooltip("Only for abilities that require mana to be casted!"));
            Menu.AddItem(
                new MenuItem("targetSelecting", "Target selection:").SetValue(
                    new StringList(new[] { "FastestKillable", "ClosestToMouse" })));
            Menu.AddItem(
                new MenuItem("lockTarget", "Lock on target when they become not visible:").SetValue(true)
                    .SetTooltip(
                        "It will not chase to fog, but it wont change target if current target just ran into fog, you can reset target by stopping holding the key"));
            Menu.AddItem(new MenuItem("comboKey", "Combo Key").SetValue(new KeyBind(32, KeyBindType.Press)));
            Menu.AddToMainMenu();
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
            menuvalueSet = false;
            Orbwalking.Load();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The cast combo.
        /// </summary>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        private static bool CastCombo()
        {
            var canCancel = Orbwalking.CanCancelAnimation();
            var manaCheck = Menu.Item("manaSlider").GetValue<Slider>().Value < me.Mana;
            if (!Utils.SleepCheck("casting") || !me.CanCast() || !target.IsVisible || !canCancel)
            {
                return false;
            }

            if (manaCheck && abyssalBlade != null && abyssalBlade.IsValid && menuValue.IsEnabled(abyssalBlade.Name)
                && abyssalBlade.CanBeCasted() && targetDistance <= (350 + hullsum) && Utils.SleepCheck("abyssal"))
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

            if (manaCheck && scytheOfVyse != null && scytheOfVyse.IsValid && menuValue.IsEnabled(scytheOfVyse.Name)
                && scytheOfVyse.CanBeCasted() && targetDistance <= (scytheOfVyse.CastRange + hullsum)
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

            if (manaCheck && menuValue.IsEnabled(earthshock.Name) && earthshock.CanBeCasted() && Utils.SleepCheck("Q")
                && ((me.Mana - earthshock.ManaCost) > overpower.ManaCost || !overpower.CanBeCasted()))
            {
                var radius = earthshock.GetAbilityData("shock_radius");
                var pos = target.Position
                          + target.Vector3FromPolarAngle() * ((Game.Ping / 1000 + 0.3f) * target.MovementSpeed);

                if (mePosition.Distance(pos) < targetDistance)
                {
                    pos = target.Position;
                }

                if (mePosition.Distance2D(pos) <= radius
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

            // Console.WriteLine(blink != null);
            if (blink != null && menuValue.IsEnabled(blink.Name) && blink.CanBeCasted() && targetDistance > 400
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

            if (manaCheck && menuValue.IsEnabled(overpower.Name) && overpower.CanBeCasted() && Utils.SleepCheck("W")
                && !(earthshock.CanBeCasted() && Utils.ChainStun(target, 0.3 + Game.Ping / 1000, null, false)))
            {
                if (mePosition.Distance2D(target) <= (Radius + hullsum))
                {
                    overpower.UseAbility();
                    Utils.Sleep(overpowerCastPoint * 1000 + Game.Ping, "W");
                    Utils.Sleep(overpowerCastPoint * 1000, "casting");
                    return true;
                }
            }

            if (!menuValue.IsEnabled(enrage.Name) || !enrage.CanBeCasted() || !Utils.SleepCheck("R"))
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

        /// <summary>
        ///     The game_ on update.
        /// </summary>
        /// <param name="args">
        ///     The args.
        /// </param>
        private static void Game_OnUpdate(EventArgs args)
        {
            if (!loaded)
            {
                me = ObjectManager.LocalHero;
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
                Game.PrintMessage(
                    "<font color='#3377ff'>UrsaRage</font>: loaded! <font face='Tahoma' size='9'>(by MOON<font color='#ff9900'>ES</font>)</font>", 
                    MessageType.ChatMessage);
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

            if (!menuvalueSet)
            {
                menuValue = Menu.Item("enabledAbilities").GetValue<AbilityToggler>();
                menuvalueSet = true;

                // Utils.Sleep(100000, "updateMenuValue");
            }

            if (Game.IsPaused)
            {
                return;
            }

            if (blink == null || !blink.IsValid)
            {
                blink = me.FindItem("item_blink");
            }

            if (abyssalBlade == null || !abyssalBlade.IsValid)
            {
                abyssalBlade = me.FindItem("item_abyssal_blade");
            }

            if (scytheOfVyse == null || !scytheOfVyse.IsValid)
            {
                scytheOfVyse = me.FindItem("item_sheepstick");
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

            if (!Menu.Item("comboKey").GetValue<KeyBind>().Active || Game.IsChatOpen)
            {
                target = null;
                return;
            }

            if (Utils.SleepCheck("blink"))
            {
                mePosition = me.Position;
            }

            if (earthshock.IsInAbilityPhase
                && (target == null || !target.IsAlive
                    || target.Distance2D(me) > earthshock.GetAbilityData("shock_radius")))
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
                blinkRange = blink.AbilitySpecialData.FirstOrDefault(x => x.Name == "blink_range").GetValue(0);
                range = blinkRange + me.HullRadius + 500;
            }

            var canCancel = (Orbwalking.CanCancelAnimation() && Orbwalking.AttackOnCooldown(target))
                            || (!Orbwalking.AttackOnCooldown(target)
                                && (targetDistance > 350 || (target != null && !target.IsVisible))) || target == null;
            if (canCancel)
            {
                if (target != null && !target.IsVisible)
                {
                    if (!Menu.Item("lockTarget").GetValue<bool>())
                    {
                        var closestToMouse = me.ClosestToMouseTarget(128);
                        if (closestToMouse != null)
                        {
                            target = me.ClosestToMouseTarget(range);
                        }
                    }
                }
                else
                {
                    var index = Menu.Item("targetSelecting").GetValue<StringList>().SelectedIndex;
                    target = index == 0 ? me.BestAATarget(blinkRange) : me.ClosestToMouseTarget();
                }
            }

            if (target == null || !target.IsAlive
                || ((!target.IsVisible || target.Distance2D(mousePosition) > target.Distance2D(me) + 1000) && canCancel))
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

        /// <summary>
        ///     The orb walk.
        /// </summary>
        /// <param name="canCancel">
        ///     The can cancel.
        /// </param>
        private static void OrbWalk(bool canCancel)
        {
            var canAttack = !Orbwalking.AttackOnCooldown(target) && !target.IsInvul() && !target.IsAttackImmune()
                            && me.CanAttack();
            if (canAttack && (targetDistance <= 350))
            {
                if (!Utils.SleepCheck("attack"))
                {
                    return;
                }

                me.Attack(target);
                Utils.Sleep(100, "attack");
                return;
            }

            var canMove = (canCancel && Orbwalking.AttackOnCooldown(target))
                          || (!Orbwalking.AttackOnCooldown(target) && targetDistance > 350);
            if (!Utils.SleepCheck("move") || !canMove)
            {
                return;
            }

            var mousePos = Game.MousePosition;
            if (target.Distance2D(me) < 500)
            {
                var pos = target.Position
                          + target.Vector3FromPolarAngle()
                          * (float)
                            Math.Max(
                                (Game.Ping / 1000 + (targetDistance / me.MovementSpeed) + turnTime)
                                * target.MovementSpeed, 
                                500);

                // Console.WriteLine(pos.Distance(me.Position) + " " + target.Distance2D(pos));
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