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

        public static AttackAnimationData GetByName(string unitName)
        {
            return Units.FirstOrDefault(unitData => unitData.UnitName.ToLower() == unitName);
        }

        public static AttackAnimationData GetByClassId(Ensage.ClassId classId)
        {
            return Units.FirstOrDefault(unitData => unitData.UnitClassId.Equals(classId));
        }

    }
}
