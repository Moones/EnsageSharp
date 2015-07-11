using System;
using System.Linq;
using SharpDX;

namespace Ensage.Common.Extensions
{
    public static class EntityExtensions
    {
        public static float GetDistance2D(this Entity unit, Vector3 vector)
        {
            return unit.Position.GetDistance2D(vector);
        }

        public static Vector3 UnitVectorFromXYAngle(this Entity unit)
        {
            var alpha = unit.RotationRad;
            return new Vector3((float)Math.Cos(alpha), (float)Math.Sin(alpha), 0);
        }
        public static Vector3 UnitVectorFromXYAngle(this Entity unit, double delta)
        {
            var alpha = unit.RotationRad + delta;
            return new Vector3((float)Math.Cos(alpha), (float)Math.Sin(alpha), 0);
        }

        public static float GetAttackRange(this Unit unit)
        {
            var bonus = 0.0;
            var classId = unit.ClassId;
            switch (classId)
            {
                case ClassId.CDOTA_Unit_Hero_TemplarAssassin:
                    var psi = unit.Spellbook.SpellW;
                    if (psi != null && psi.Level > 0)
                    {
                        var firstOrDefault = psi.AbilityData.FirstOrDefault(x => x.Name == "bonus_attack_range");
                        if (firstOrDefault != null)
                            bonus = firstOrDefault.Value;
                    }
                    break;
                case ClassId.CDOTA_Unit_Hero_Sniper:
                    var aim = unit.Spellbook.SpellE;
                    if (aim != null && aim.Level > 0)
                    {
                        var firstOrDefault = aim.AbilityData.FirstOrDefault(x => x.Name == "bonus_attack_range");
                        if (firstOrDefault != null)
                            bonus = firstOrDefault.Value;
                    }
                    break;
                case ClassId.CDOTA_Unit_Hero_Enchantress:
                    var impetus = unit.Spellbook.SpellR;
                    if (impetus.Level > 0 && unit.Inventory.Items.Any(x => (x.Name == "item_ultimate_scepter")))
                        bonus = 190;
                    break;
                default:
                    if (unit.Modifiers.Any(x => (x.Name == "modifier_lone_druid_true_form")))
                        bonus = -423;
                    else if (unit.Modifiers.Any(x => (x.Name == "dragon_knight_elder_dragon_form")))
                        bonus = 372;
                    else if (unit.Modifiers.Any(x => (x.Name == "terrorblade_metamorphosis")))
                        bonus = 422;
                    break;
            }
            return (float)(unit.AttackRange + bonus);
        }

        public static bool CanBeCasted(this Ability ability)
        {
            return ability != null && ability.AbilityState == AbilityState.Ready;
        }

        public static float FindAngleR(this Entity ent)
        {
            return (float)(ent.RotationRad < 0 ? Math.Abs(ent.RotationRad) : 2 * Math.PI - ent.RotationRad);
        }

        public static float FindAngleBetween(this Entity unit, Vector3 second)
        {
            return unit.Position.FindAngleBetween(second);
        }

        public static Item GetDagon()
        {
            return
                EntityList.GetLocalPlayer()
                    .Hero.Inventory.Items.ToList()
                    .FirstOrDefault(x => x.Name.Substring(0, 10) == "item_dagon");
        }

        public static double GetTurnTime(this Entity unit, Vector3 position)
        {
            var data = HeroDatabase.GetByClassId(unit.ClassId) ??
                                       HeroDatabase.GetByName(unit.Name);
            if (data == null)
                return
                    (Math.Max(
                        Math.Abs(FindAngleR(unit) - Utils.DegreeToRadian(unit.FindAngleBetween(position))) - 0.69, 0) /
                     (0.5 * (1 / 0.03)));
            var turnRate = data.TurnRate;
            return
                (Math.Max(
                    Math.Abs(FindAngleR(unit) - Utils.DegreeToRadian(unit.FindAngleBetween(position))) - 0.69, 0) /
                 (turnRate * (1 / 0.03)));
        }

        public static bool CanGoInvis(this Unit unit)
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

        public static Ability FindSpell(Unit unit, String name)
        {
            return unit.Spellbook.Spells.FirstOrDefault(x => x.Name == name);
        }

        public static bool IsUnitState(Unit unit, UnitState state)
        {
            return unit.UnitState.HasFlag(state);
        }

        public static bool CanMove(this Unit unit)
        {
            return !IsRooted(unit) && !IsStunned(unit) &&
                   unit.Modifiers.Any(x => x.Name != "modifier_slark_pounce_leash") && unit.IsAlive;
        }

        public static bool IsRooted(this Unit unit)
        {
            return IsUnitState(unit, UnitState.Rooted);
        }

        public static bool IsStunned(this Unit unit)
        {
            return IsUnitState(unit, UnitState.Stunned);
        }

        public static bool IsDisarmed(this Unit unit)
        {
            return IsUnitState(unit, UnitState.Disarmed);
        }

        public static bool IsAttackImmune(this Unit unit)
        {
            return IsUnitState(unit, UnitState.AttackImmune);
        }

        public static bool IsSilenced(this Unit unit)
        {
            return IsUnitState(unit, UnitState.Silenced);
        }

        public static bool IsHexed(this Unit unit)
        {
            return IsUnitState(unit, UnitState.Hexed);
        }

        public static bool IsInvisible(this Unit unit)
        {
            return IsUnitState(unit, UnitState.Invisible);
        }

        public static bool IsInvul(this Unit unit)
        {
            return IsUnitState(unit, UnitState.Invulnerable);
        }

        public static bool IsMagicImmune(this Unit unit)
        {
            return IsUnitState(unit, UnitState.MagicImmune);
        }

        public static bool IsIllusion(Hero unit)
        {
            return unit.IsIllusion;
        }

        public static bool IsIllusion(Meepo unit)
        {
            return unit.IsIllusion && unit.IsMeepoIllusion;
        }

        public static bool CanCast(this Unit unit)
        {
            return !IsSilenced(unit) && !IsStunned(unit) && unit.IsAlive;
        }

        public static bool CanAttack(this Unit unit)
        {
            return unit.AttackCapabilities != AttackCapabilities.None && !IsDisarmed(unit) && !IsStunned(unit) &&
                   unit.IsAlive;
        }

        public static Team GetEnemyTeam(this Unit unit)
        {
            var team = unit.Team;
            switch (team)
            {
                case Team.Dire:
                    return Team.Radiant;
                case Team.Radiant:
                    return Team.Dire;
            }
            return team;
        }
    }
}
