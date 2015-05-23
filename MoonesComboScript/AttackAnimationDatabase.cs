#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ensage;

#endregion

namespace MoonesComboScript
{

    public static class AttackAnimationDatabase
    {
        public static List<AttackAnimationData> Units = new List<AttackAnimationData>();

        static AttackAnimationDatabase()
        {
            #region AntiMage

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_antimage",
                    UnitClassId = ClassId.CDOTA_Unit_Hero_AntiMage,
                    AttackRate = 1.45,
                    AttackPoint = 0.3,
                    AttackBackswing = 0.6,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.5,
                });

            #endregion

            #region Axe

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_axe",
                    UnitClassId = ClassId.CDOTA_Unit_Hero_Axe,
                    AttackRate = 1.7,
                    AttackPoint = 0.5,
                    AttackBackswing = 0.5,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.6,
                });

            #endregion

            #region Bane

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_bane",
                    UnitClassId = ClassId.CDOTA_Unit_Hero_Bane,
                    AttackRate = 1.7,
                    AttackPoint = 0.3,
                    AttackBackswing = 0.7,
                    ProjectileSpeed = 900,
                    TurnRate = 0.6,
                });

            #endregion

            #region Bloodseeker

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_bloodseeker",
                    UnitClassId = ClassId.CDOTA_Unit_Hero_Bloodseeker,
                    AttackRate = 1.7,
                    AttackPoint = 0.43,
                    AttackBackswing = 0.74,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.5,
                });

            #endregion

            #region CrystalMaiden

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_crystal_maiden",
                    UnitClassId = ClassId.CDOTA_Unit_Hero_CrystalMaiden,
                    AttackRate = 1.7,
                    AttackPoint = 0.55,
                    AttackBackswing = 0,
                    ProjectileSpeed = 900,
                    TurnRate = 0.5,
                });

            #endregion

            #region DrowRanger

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_drow_ranger",
                    UnitClassId = ClassId.CDOTA_Unit_Hero_DrowRanger,
                    AttackRate = 1.7,
                    AttackPoint = 0.7,
                    AttackBackswing = 0.3,
                    ProjectileSpeed = 1250,
                    TurnRate = 0.5,
                });

            #endregion

            #region DrowRanger

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_drow_ranger",
                    UnitClassId = ClassId.CDOTA_Unit_Hero_DrowRanger,
                    AttackRate = 1.7,
                    AttackPoint = 0.7,
                    AttackBackswing = 0.3,
                    ProjectileSpeed = 1250,
                    TurnRate = 0.6,
                });

            #endregion

            #region Earthshaker

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_earthshaker",
                    UnitClassId = ClassId.CDOTA_Unit_Hero_Earthshaker,
                    AttackRate = 1.7,
                    AttackPoint = 0.467,
                    AttackBackswing = 0.863,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.6,
                });

            #endregion

            #region Juggernaut

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_juggernaut",
                    UnitClassId = ClassId.CDOTA_Unit_Hero_Juggernaut,
                    AttackRate = 1.6,
                    AttackPoint = 0.33,
                    AttackBackswing = 0.84,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.6,
                });

            #endregion

        }

        public static AttackAnimationData GetByName(String unitName)
        {
            return Units.FirstOrDefault(unitData => unitData.UnitName.ToLower() == unitName);
        }

        public static AttackAnimationData GetByClassId(ClassId classId)
        {
            return Units.FirstOrDefault(unitData => unitData.UnitClassId.Equals(classId));
        }

        public static float GetAttackSpeed(Unit unit)
        {
            var attackSpeed = Math.Max(unit.AttackSpeed, 600);
            if (unit.Modifiers.Any(x => (x.Name == "modifier_ursa_overpower")))
                attackSpeed = 600;
            return (float) attackSpeed;
        }

        public static double GetAttackPoint(Unit unit)
        {
            ClassId classId = unit.ClassId;
            String name = unit.Name;
            AttackAnimationData data = GetByClassId(classId);
            if (data == null)
                data = GetByName(name);
            var attackSpeed = GetAttackSpeed(unit);
            return (data.AttackPoint / (1 + (attackSpeed - 100) / 100)) - 0.13950280112044817927170868347339;
        }

        public static double GetAttackBackswing(Unit unit)
        {
            //ClassId classId = unit.ClassId;
            //String name = unit.Name;
            //AttackAnimationData data = GetByClassId(classId);
            //if (data == null)
            //    data = GetByName(name);
            //var attackSpeed = GetAttackSpeed(unit);
            //return (data.AttackBackswing / (1 + (attackSpeed - 100) / 100)) ;
            var attackRate = GetAttackRate(unit);
            var attackPoint = GetAttackPoint(unit);
            return attackRate - attackPoint;
        }

        public static double GetAttackRate(Unit unit)
        {
            ClassId classId = unit.ClassId;
            var attackSpeed = GetAttackSpeed(unit);
            var attackBaseTime = unit.AttackBaseTime;
            Ability spell = null;
            if (unit.Modifiers.Any(x => (x.Name == "modifier_alchemist_chemical_rage" || x.Name == "modifier_terrorblade_metamorphosis" || x.Name == "modifier_lone_druid_true_form" || x.Name == "modifier_troll_warlord_berserkers_rage")))
            {
                if (classId == ClassId.CDOTA_Unit_Hero_Alchemist)
                {
                    spell = unit.Spellbook.Spells.FirstOrDefault(x => x.Name == "alchemist_chemical_rage");
                }
                else if (classId == ClassId.CDOTA_Unit_Hero_Terrorblade)
                {
                    spell = unit.Spellbook.Spells.FirstOrDefault(x => x.Name == "terrorblade_metamorphosis");
                }
                else if (classId == ClassId.CDOTA_Unit_Hero_LoneDruid)
                {
                    spell = unit.Spellbook.Spells.FirstOrDefault(x => x.Name == "lone_druid_true_form");
                }
                else if (classId == ClassId.CDOTA_Unit_Hero_TrollWarlord)
                {
                    spell = unit.Spellbook.Spells.FirstOrDefault(x => x.Name == "troll_warlord_berserkers_rage");
                }
                attackBaseTime = spell.AbilityData.FirstOrDefault(x => x.Name == "base_attack_time").Value;
            }                
            return (attackBaseTime / (1 + (attackSpeed - 100) / 100)) - 0.03;
        }
    }
}
