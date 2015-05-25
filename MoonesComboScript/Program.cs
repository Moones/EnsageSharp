#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ensage;
using SharpDX;

#endregion

namespace MoonesComboScript
{
    class Program
    {
        const int WM_KEYUP = 0x0101;
        const int WM_KEYDOWN = 0x0105;

        private static Hero _victim;
        private static double _victimHp;
        private static bool Retreat = false;
        private static Vector3 _mePosition;
        private static Vector3 _xposition;
        private readonly static Timer ComboTimer = new Timer();
        private readonly static Timer AttackTimer = new Timer();
        private readonly static Timer MoveTimer = new Timer();

        static void Main(string[] args)
        {
            ComboTimer.Tick += ComboTimer_Tick;
            AttackTimer.Tick += AttackTimer_Tick;
            MoveTimer.Tick += MoveTimer_Tick;
            Game.OnUpdate += OrbWalker;
            Game.OnUpdate += AutoCombo;
        }

        static void ComboTimer_Tick(object sender, EventArgs e)
        {
            ComboTimer.Enabled = false;
        }

        static void AttackTimer_Tick(object sender, EventArgs e)
        {
            AttackTimer.Enabled = false;
        }

        static void MoveTimer_Tick(object sender, EventArgs e)
        {
            MoveTimer.Enabled = false;
        }

        static void AutoCombo(EventArgs args)
        {
            if (ComboTimer.Enabled || !Game.IsInGame || Game.IsPaused)
                return;

            var me = EntityList.Hero;
            var id = me.ClassId;
            var meSpells = me.Spellbook;
            var a1 = meSpells.Spell1;
            var a2 = meSpells.Spell2;
            var a3 = meSpells.Spell3;
            var a4 = meSpells.Spell4;
            var a5 = meSpells.Spell5;
            var a6 = meSpells.Spell6;
            var attackRange = GetAttackRange(me);
            var victimdistance = GetDistance2D(_victim.Position, me.Position);
            var canMove = AttackAnimationData.canMove;
            var mousePosition = Game.MousePosition;
            var meDmg = me.DamageAverage+me.DamageBonus;
            var blink = me.Inventory.Items.FirstOrDefault(x => x.Name == "item_blink");
            if (_victim != null && (!me.UnitState.HasFlag(UnitState.Invisible) || (a2.Name == "templar_assassin_meld" && CanBeCasted(a2) && victimdistance < attackRange+50)) && ((_victim.Health > 0 && _victim.Health > meDmg) || victimdistance > attackRange+200) && me.IsAlive && _victim.IsAlive)
            {
                if (blink != null && CanBeCasted(blink) && _victim.IsVisible && _victim.IsAlive && victimdistance > 500 && victimdistance > attackRange + 100 && victimdistance < 1700)
                {
                    var firstOrDefault = blink.AbilityData.FirstOrDefault(x => x.Name == "blink_range");
                    if (firstOrDefault != null)
                    {
                        var blinkRange = firstOrDefault.Value;
                        var blinkPos = _victim.Position;
                        if (Retreat)
                            blinkPos = mousePosition;
                        if (victimdistance > blinkRange || Retreat)
                            blinkPos = (blinkPos - me.Position) * (blinkRange - 1) / GetDistance2D(blinkPos, me.Position) + me.Position;
                        blink.UseAbility(blinkPos);
                        ComboTimer.Start(GetTurnTime(me, blinkPos) * 1000 + 100);
                        AttackTimer.Start(GetTurnTime(me, blinkPos) * 1000);
                        _mePosition = blinkPos;
                        return;
                    }
                }
            }
            var manaboots = me.Inventory.Items.FirstOrDefault(x => x.Name == "item_arcane_boots");
            var octa = me.Inventory.Items.FirstOrDefault(x => x.Name == "item_octarine_core");
            var dagon = GetDagon();
            var ethereal = me.Inventory.Items.FirstOrDefault(x => x.Name == "item_ethereal_blade");
            if (!me.UnitState.HasFlag(UnitState.Stunned) && _victim.IsVisible && ((a2 == null || (a2.Name == "templar_assassin_meld" || me.Modifiers.Any(x => (x.Name == "modifier_templar_assassin_meld")) || !MoveTimer.Enabled))))
            {
                foreach (var itemData in ItemDatabase.Items)
                {
                    var itemname = itemData.Name;
                    var stun = itemData.Stun;
                    var slow = itemData.Slow;
                    var special = itemData.Special;
                    var throughBkb = itemData.ThroughBKB;
                    var killsteal = itemData.Killsteal;
                    var range = itemData.Range;
                    var retreat = itemData.Retreat;
                    var item = me.Inventory.Items.FirstOrDefault(x => x.Name == itemname);
                    if (item == null || !CanBeCasted(item)) continue;
                    var go = true;
                    if (itemname == "item_refresher" ||
                        (itemname == "item_cyclone" && me.ClassId == ClassId.CDOTA_Unit_Hero_Tinker))
                    {
                        if (
                            meSpells.Spells.Any(
                                x =>
                                    x.Name != "tinker_march_of_the_machines" && x.Name != "tinker_rearm" &&
                                    x.Name != "invoker_alacrity" && x.Name != "invoker_forge_spirit" &&
                                    x.Name != "invoker_ice_wall" && x.Name != "invoker_ghost_walk" &&
                                    x.Name != "invoker_cold_snap" && x.Name != "invoker_quas" &&
                                    x.Name != "invoker_exort" && x.Name != "invoker_wex" && x.Name != "invoker_invoke" &&
                                    x.Level > 0 && x.Cooldown == 0 &&
                                    !x.AbilityBehavior.HasFlag(AbilityBehavior.Passive)))
                            go = false;
                        if (
                            me.Inventory.Items.Any(
                                x =>
                                    x.Name != "item_blink" && x.Name != "item_travel_boots" && x.Name != itemname &&
                                    x.Name != "item_travel_boots_2" && x.Name != "item_tpscroll" && x.Cost > 1000 &&
                                    CanBeCasted(x)))
                            go = false;


                    }
                    if (itemname == "item_refresher" &&
                        (item.ManaCost*2 < me.Mana || (manaboots != null && item.ManaCost*2 < (me.Mana + 135))))
                        go = false;
                    if (itemname == "item_cyclone")
                    {
                        foreach (var ab in meSpells.Spells)
                        {
                            var abcd = ab.CooldownTotal;
                            if (octa != null)
                                abcd *= 0.75f;
                            abcd -= 3;
                            if (ab.Cooldown > abcd && ab.Damage > 0)
                                go = false;
                        }
                    }
                    if ((Equals(item, dagon) || Equals(item, ethereal)) &&
                        ((CanBeCasted(a4) && a4.Name == "necrolyte_reapers_scythe") ||
                         (killsteal && !_victim.Modifiers.Any(x => (x.Name == "modifier_item_ethereal_blade_slow")) &&
                          !_victim.Modifiers.Any(x => (x.Name == "modifier_necrolyte_reapers_scythe")))))
                        go = false;
                    if (itemname == "item_dust" && !CanGoInvis(_victim))
                        go = false;
                    if ((itemname == "item_diffusal_blade" || itemname == "item_diffusal_blade_2") &&
                        !CanBePurged(_victim))
                        go = false;
                    if (Equals(item, manaboots) && (me.ManaMaximum - me.Mana) < 135)
                        go = false;
                    if (itemname == "item_cyclone")
                    {
                        if (_xposition != null)
                            go = false;
                        if (a1 != null && id == ClassId.CDOTA_Unit_Hero_AncientApparition && CanBeCasted(a1))
                            go = false;
                        if (id == ClassId.CDOTA_Unit_Hero_Invoker)
                        {
                            foreach (var ab in meSpells.Spells)
                            {
                                var cd = ab.CooldownTotal;
                                if (octa != null)
                                    cd *= 0.75f;
                                if (ab.Name == "invoker_chaos_meteor")
                                    cd -= 5;
                                else
                                    cd -= 3;
                                if (ab.Cooldown > cd)
                                    go = false;
                            }
                            if ((a4.Name == "invoker_tornado" && CanBeCasted(a4)) ||
                                (a5.Name == "invoker_tornado" && CanBeCasted(a5)))
                                go = false;
                        }
                        if (id == ClassId.CDOTA_Unit_Hero_Pudge &&
                            (me.Modifiers.Any(x => (x.Name == "modifier_pudge_rot")) ||
                             (a4 != null && CanBeCasted(a4) && victimdistance < 400) ||
                             (a1 != null && CanBeCasted(a1) && victimdistance + 100 > item.CastRange)))
                            go = false;
                        if (
                            _victim.Modifiers.Any(
                                x =>
                                    x.Name == "modifier_invoker_chaos_meteor_burn" ||
                                    x.Name == "modifier_invoker_cold_snap" ||
                                    x.Name == "modifier_invoker_ice_wall_slow_debuff" ||
                                    x.Name == "modifier_invoker_ice_wall_slow_aura" ||
                                    x.Name == "modifier_pudge_meat_hook" || x.Name == "modifier_ghost_state" ||
                                    x.Name == "modifier_item_ethereal_blade_slow") || !CanMove(_victim) ||
                            IsHexed(_victim) || IsDisarmed(_victim) || IsSilenced(_victim) ||
                            _victim.MovementSpeedTotal < 250 || IsRooted(_victim))
                            go = false;
                    }
                    if (_victim.Modifiers.Any(x => x.Name == "modifier_" + itemname + "_debuff"))
                        go = false;
                    if ((!IsMagicImmune(_victim) || item.DamageType == DamageType.Physical || throughBkb) && go &&
                        (itemname != "item_refresher" ||
                         (item.ManaCost*2 < me.Mana || (manaboots != null && item.ManaCost*2 < (me.Mana + 135)))) &&
                        ((!stun && !slow && !special) || ChainStun(_victim, 0, null, false) ||
                         (itemname == "item_blade_mail" && ChainStun(_victim, 0, "modifier_axe_berserkers_call", false))) &&
                        (!Retreat || retreat || stun || slow) &&
                        ((me.Modifiers.Any(x => x.Name == "modifier_spirit_breaker_charge_of_darkness")) ||
                         itemname == "item_armlet"))
                    {
                        
                    }

                }
            }
        }

        static void OrbWalker(EventArgs args)
        {
            if (AttackTimer.Enabled || !Game.IsInGame || Game.IsPaused)
                return;

            var me = EntityList.Hero;
            var attackRange = GetAttackRange(me);
            var victimdistance = GetDistance2D(_victim.Position,me.Position);
            if (_victim == null || victimdistance > attackRange+100)
                _victim = GetClosestEnemyHeroToMouse();
            var canMove = AttackAnimationData.canMove;
            var mousePosition = Game.MousePosition;

            if (canMove == false && _victim != null && !_victim.UnitState.HasFlag(UnitState.AttackImmune) &&
                victimdistance < attackRange + 100)
            {
                me.Attack(_victim);
                AttackTimer.Start(200);
                return;
            }
            else
            {
                me.Move(mousePosition);
                AttackTimer.Start(200);
                return;
            }
        }

        static Hero GetClosestEnemyHeroToMouse()
        {
            var mousePosition = Game.MousePosition;
            var enemies =
                EntityList.GetEntities<Hero>()
                    .Where(x => x.IsVisible && x.IsAlive && !x.IsIllusion && x.Team != EntityList.Player.Team)
                    .ToList();

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
            if (result != null)
                _victimHp = result.Health;
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

        static bool CanBeCasted(Ability ability)
        {
            return ability != null && ability.AbilityState == AbilityState.Ready;
        }

        static float FindAngleR(Entity ent)
        {
            return (float) (ent.RotationRad < 0 ? Math.Abs(ent.RotationRad) : 2*Math.PI - ent.RotationRad);
        }

        static float FindAngleBetween(Vector3 first, Vector3 second)
        {
            var xAngle = (float) (Math.Atan(Math.Abs(second.X - first.X)/Math.Abs(second.Y - first.Y))*(180.0/Math.PI));
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

        static Item GetDagon()
        {
            return
                EntityList.GetLocalPlayer()
                    .Hero.Inventory.Items.ToList()
                    .FirstOrDefault(x => x.Name.Substring(0, 10) == "item_dagon");
        }

        static double GetTurnTime(Unit unit, Vector3 position)
        {
            ClassId classId = unit.ClassId;
            String name = unit.Name;
            AttackAnimationData data = AttackAnimationDatabase.GetByClassId(classId) ??
                                       AttackAnimationDatabase.GetByName(name);
            if (data != null)
            {
                var turnRate = data.TurnRate;
                return
                    (Math.Max(
                        Math.Abs(FindAngleR(unit) - DegreeToRadian(FindAngleBetween(unit.Position, position))) - 0.69, 0)/
                     (turnRate*(1/0.03)));
            }
            return
                (Math.Max(
                    Math.Abs(FindAngleR(unit) - DegreeToRadian(FindAngleBetween(unit.Position, position))) - 0.69, 0)/
                 (0.5*(1/0.03)));
        }

        static float GetDistance2D(Vector3 p1, Vector3 p2)
        {
            return (float)Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }

        static double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        static bool CanGoInvis(Unit unit)
        {
            var invis =
                unit.Spellbook.Spells.FirstOrDefault(x => x.Name == "bounty_hunter_wind_walk" || x.Name == "clinkz_skeleton_walk") ??
                unit.Inventory.Items.FirstOrDefault(
                    x =>
                        x.Name == "item_invis_sword" || x.Name == "item_silver_edge" ||
                        x.Name == "item_glimmer_cape" || x.Name == "item_moon_shard");
            var riki = FindSpell(unit, "riki_permanent_invisibility");
            return (invis != null && CanBeCasted(invis)) || (riki != null && riki.Level > 0);
        }

        static bool CanBePurged(Unit unit)
        {
            return
                unit.Modifiers.Any(
                    x =>
                        x.Name == "modifier_ghost_state" || x.Name == "modifier_item_ethereal_blade_slow" ||
                        x.Name == "modifier_omninight_guardian_angel");
        }

        static Ability FindSpell(Unit unit, String name)
        {
            return unit.Spellbook.Spells.FirstOrDefault(x => x.Name == name);
        }

        static bool IsUnitState(Unit unit, UnitState state)
        {
            return unit.UnitState.HasFlag(state);
        }

        static bool CanMove(Unit unit)
        {
            return !IsRooted(unit) && !IsStunned(unit) &&
                   unit.Modifiers.Any(x => x.Name != "modifier_slark_pounce_leash") && unit.IsAlive;
        }

        static bool IsRooted(Unit unit)
        {
            return IsUnitState(unit,UnitState.Rooted);
        }
        static bool IsStunned(Unit unit)
        {
            return IsUnitState(unit, UnitState.Stunned);
        }
        static bool IsDisarmed(Unit unit)
        {
            return IsUnitState(unit, UnitState.Disarmed);
        }
        static bool IsAttackImmune(Unit unit)
        {
            return IsUnitState(unit, UnitState.AttackImmune);
        }
        static bool IsSilenced(Unit unit)
        {
            return IsUnitState(unit, UnitState.Silenced);
        }
        static bool IsHexed(Unit unit)
        {
            return IsUnitState(unit, UnitState.Hexed);
        }
        static bool IsInvisible(Unit unit)
        {
            return IsUnitState(unit, UnitState.Invisible);
        }
        static bool IsInvul(Unit unit)
        {
            return IsUnitState(unit, UnitState.Invulnerable);
        }
        static bool IsMagicImmune(Unit unit)
        {
            return IsUnitState(unit, UnitState.MagicImmune);
        }

        static bool IsIllusion(Hero unit)
        {
            return unit.IsIllusion;
        }

        static bool CanCast(Unit unit)
        {
            return !IsSilenced(unit) && !IsStunned(unit) && unit.IsAlive;
        }

        static bool CanAttack(Unit unit)
        {
            return unit.AttackCapabilities != AttackCapabilities.None && !IsDisarmed(unit) && !IsStunned(unit) &&
                   unit.IsAlive;
        }

        static bool ChainStun(Unit unit, double delay, string except, bool onlychain)
        {
            var chain = false;
            var stunned = false;
            String[] modifiersList =
            {
                "modifier_shadow_demon_disruption", "modifier_obsidian_destroyer_astral_imprisonment_prison",
                "modifier_eul_cyclone", "modifier_invoker_tornado", "modifier_bane_nightmare",
                "modifier_shadow_shaman_shackles",
                "modifier_crystal_maiden_frostbite", "modifier_ember_spirit_searing_chains",
                "modifier_axe_berserkers_call",
                "modifier_lone_druid_spirit_bear_entangle_effect", "modifier_meepo_earthbind",
                "modifier_naga_siren_ensnare",
                "modifier_storm_spirit_electric_vortex_pull", "modifier_treant_overgrowth", "modifier_cyclone",
                "modifier_sheepstick_debuff", "modifier_shadow_shaman_voodoo", "modifier_lion_voodoo",
                "modifier_brewmaster_storm_cyclone",
                "modifier_puck_phase_shift"
            };
            var modifiers = unit.Modifiers.OrderByDescending(x => x.RemainingTime);
            foreach (var m in modifiers)
            {
                if ((m.IsStunDebuff || modifiersList.Contains(m.Name) && (except == null || m.Name == except)))
                {
                    stunned = true;
                    var remainingTime = m.RemainingTime;
                    if (m.Name == "modifier_eul_cyclone")
                        remainingTime += 0.07f;
                    chain = remainingTime <= delay;
                }
            }
            return ((((!(stunned || IsStunned(unit)) || chain) && !onlychain) || (onlychain && chain)));
        }
    }
}
