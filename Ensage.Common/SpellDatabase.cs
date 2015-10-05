using System.Collections.Generic;

namespace Ensage.Common
{
    public static class SpellDatabase
    {
        public static List<SpellData> Spells = new List<SpellData>();

        static SpellDatabase()
        {
            #region ursa_earthshock

            Spells.Add(
                new SpellData
                {
                    SpellName = "ursa_earthshock",
                    DefaultSlot = 1,
                    IsStun = false,
                    IsSlow = true,
                    IsNuke = true,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = "shock_radius",
                    Speed = null,
                    Width = null,
                    AllyBlock = false,
                    EnemyBlock = false,
                    MagicImmunityPierce = false,
                    FakeCastRange = false,
                    RealCastRange = null,
                    OnlyForKillSteal = false
                });

            #endregion
        }
    }

}
