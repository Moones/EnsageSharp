using System.Collections.Generic;

namespace Ensage.Common
{
    public static class SpellDatabase
    {
        public static List<SpellData> Spells = new List<SpellData>();

        static SpellDatabase()
        {
            #region dark_seer_ion_shell

            Spells.Add(
                new SpellData
                {
                    SpellName = "dark_seer_ion_shell",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = "radius",
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

            #region magnataur_shockwave

            Spells.Add(
                new SpellData
                {
                    SpellName = "magnataur_shockwave",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = "shock_width",
                    Speed = "shock_speed",
                    Width = null,
                    AllyBlock = false,
                    EnemyBlock = false,
                    MagicImmunityPierce = false,
                    FakeCastRange = false,
                    RealCastRange = null,
                    OnlyForKillSteal = false
                });

            #endregion

            #region ursa_earthshock

            Spells.Add(
                new SpellData
                {
                    SpellName = "ursa_earthshock",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
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

            #region chaos_knight_chaos_strike

            Spells.Add(
                new SpellData
                {
                    SpellName = "chaos_knight_chaos_strike",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region skywrath_mage_mystic_flare

            Spells.Add(
                new SpellData
                {
                    SpellName = "skywrath_mage_mystic_flare",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0.2,
                    Radius = 0,
                    StringRadius = null,
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

            #region death_prophet_carrion_swarm

            Spells.Add(
                new SpellData
                {
                    SpellName = "death_prophet_carrion_swarm",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region zuus_lightning_bolt

            Spells.Add(
                new SpellData
                {
                    SpellName = "zuus_lightning_bolt",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = "spread_aoe",
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

            #region axe_berserkers_call

            Spells.Add(
                new SpellData
                {
                    SpellName = "axe_berserkers_call",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = "radius",
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

            #region beastmaster_wild_axes

            Spells.Add(
                new SpellData
                {
                    SpellName = "beastmaster_wild_axes",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region puck_waning_rift

            Spells.Add(
                new SpellData
                {
                    SpellName = "puck_waning_rift",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = "radius",
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

            #region lone_druid_rabid

            Spells.Add(
                new SpellData
                {
                    SpellName = "lone_druid_rabid",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region rattletrap_battery_assault

            Spells.Add(
                new SpellData
                {
                    SpellName = "rattletrap_battery_assault",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0.7,
                    Radius = 0,
                    StringRadius = "radius",
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

            #region sven_storm_bolt

            Spells.Add(
                new SpellData
                {
                    SpellName = "sven_storm_bolt",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
                    Speed = "bolt_speed",
                    Width = null,
                    AllyBlock = false,
                    EnemyBlock = false,
                    MagicImmunityPierce = false,
                    FakeCastRange = false,
                    RealCastRange = null,
                    OnlyForKillSteal = false
                });

            #endregion

            #region chen_test_of_faith

            Spells.Add(
                new SpellData
                {
                    SpellName = "chen_test_of_faith",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region phantom_assassin_stifling_dagger

            Spells.Add(
                new SpellData
                {
                    SpellName = "phantom_assassin_stifling_dagger",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
                    Speed = "dagger_speed",
                    Width = null,
                    AllyBlock = false,
                    EnemyBlock = false,
                    MagicImmunityPierce = false,
                    FakeCastRange = false,
                    RealCastRange = null,
                    OnlyForKillSteal = false
                });

            #endregion

            #region bounty_hunter_track

            Spells.Add(
                new SpellData
                {
                    SpellName = "bounty_hunter_track",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region axe_battle_hunger

            Spells.Add(
                new SpellData
                {
                    SpellName = "axe_battle_hunger",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region storm_spirit_electric_vortex

            Spells.Add(
                new SpellData
                {
                    SpellName = "storm_spirit_electric_vortex",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region ember_spirit_sleight_of_fist

            Spells.Add(
                new SpellData
                {
                    SpellName = "ember_spirit_sleight_of_fist",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = "radius",
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

            #region batrider_flamebreak

            Spells.Add(
                new SpellData
                {
                    SpellName = "batrider_flamebreak",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0.3,
                    Radius = 0,
                    StringRadius = null,
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

            #region mirana_arrow

            Spells.Add(
                new SpellData
                {
                    SpellName = "mirana_arrow",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
                    Speed = "arrow_speed",
                    Width = "arrow_width",
                    AllyBlock = true,
                    EnemyBlock = true,
                    MagicImmunityPierce = false,
                    FakeCastRange = false,
                    RealCastRange = null,
                    OnlyForKillSteal = false
                });

            #endregion

            #region abaddon_aphotic_shield

            Spells.Add(
                new SpellData
                {
                    SpellName = "abaddon_aphotic_shield",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region windrunner_shackleshot

            Spells.Add(
                new SpellData
                {
                    SpellName = "windrunner_shackleshot",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
                    Speed = "arrow_speed",
                    Width = null,
                    AllyBlock = false,
                    EnemyBlock = false,
                    MagicImmunityPierce = false,
                    FakeCastRange = false,
                    RealCastRange = null,
                    OnlyForKillSteal = false
                });

            #endregion

            #region ancient_apparition_cold_feet

            Spells.Add(
                new SpellData
                {
                    SpellName = "ancient_apparition_cold_feet",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 2.01,
                    Radius = 0,
                    StringRadius = null,
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

            #region rattletrap_hookshot

            Spells.Add(
                new SpellData
                {
                    SpellName = "rattletrap_hookshot",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
                    Speed = null,
                    Width = "latch_radius",
                    AllyBlock = true,
                    EnemyBlock = true,
                    MagicImmunityPierce = true,
                    FakeCastRange = false,
                    RealCastRange = null,
                    OnlyForKillSteal = false
                });

            #endregion

            #region shadow_shaman_mass_serpent_ward

            Spells.Add(
                new SpellData
                {
                    SpellName = "shadow_shaman_mass_serpent_ward",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region ogre_magi_unrefined_fireblast

            Spells.Add(
                new SpellData
                {
                    SpellName = "ogre_magi_unrefined_fireblast",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region oracle_fortunes_end

            Spells.Add(
                new SpellData
                {
                    SpellName = "oracle_fortunes_end",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
                    Speed = "bolt_speed",
                    Width = null,
                    AllyBlock = false,
                    EnemyBlock = false,
                    MagicImmunityPierce = false,
                    FakeCastRange = false,
                    RealCastRange = null,
                    OnlyForKillSteal = false
                });

            #endregion

            #region shredder_timber_chain

            Spells.Add(
                new SpellData
                {
                    SpellName = "shredder_timber_chain",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = "chain_radius",
                    Speed = "speed",
                    Width = null,
                    AllyBlock = false,
                    EnemyBlock = false,
                    MagicImmunityPierce = false,
                    FakeCastRange = false,
                    RealCastRange = null,
                    OnlyForKillSteal = false
                });

            #endregion

            #region doom_bringer_lvl_death

            Spells.Add(
                new SpellData
                {
                    SpellName = "doom_bringer_lvl_death",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region crystal_maiden_freezing_field

            Spells.Add(
                new SpellData
                {
                    SpellName = "crystal_maiden_freezing_field",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = -0.1,
                    Radius = 0,
                    StringRadius = null,
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

            #region lich_frost_armor

            Spells.Add(
                new SpellData
                {
                    SpellName = "lich_frost_armor",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region enigma_malefice

            Spells.Add(
                new SpellData
                {
                    SpellName = "enigma_malefice",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region elder_titan_earth_splitter

            Spells.Add(
                new SpellData
                {
                    SpellName = "elder_titan_earth_splitter",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region shredder_return_chakram_2

            Spells.Add(
                new SpellData
                {
                    SpellName = "shredder_return_chakram_2",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region zuus_thundergods_wrath

            Spells.Add(
                new SpellData
                {
                    SpellName = "zuus_thundergods_wrath",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = -0.1,
                    Radius = 0,
                    StringRadius = null,
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

            #region shredder_chakram_2

            Spells.Add(
                new SpellData
                {
                    SpellName = "shredder_chakram_2",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0.5,
                    Radius = 0,
                    StringRadius = "radius",
                    Speed = "speed",
                    Width = null,
                    AllyBlock = false,
                    EnemyBlock = false,
                    MagicImmunityPierce = false,
                    FakeCastRange = false,
                    RealCastRange = null,
                    OnlyForKillSteal = false
                });

            #endregion

            #region phantom_assassin_phantom_strike

            Spells.Add(
                new SpellData
                {
                    SpellName = "phantom_assassin_phantom_strike",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region pugna_decrepify

            Spells.Add(
                new SpellData
                {
                    SpellName = "pugna_decrepify",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region omniknight_purification

            Spells.Add(
                new SpellData
                {
                    SpellName = "omniknight_purification",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = "radius",
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

            #region keeper_of_the_light_blinding_light

            Spells.Add(
                new SpellData
                {
                    SpellName = "keeper_of_the_light_blinding_light",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region queenofpain_shadow_strike

            Spells.Add(
                new SpellData
                {
                    SpellName = "queenofpain_shadow_strike",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
                    Speed = "projectile_speed",
                    Width = null,
                    AllyBlock = false,
                    EnemyBlock = false,
                    MagicImmunityPierce = false,
                    FakeCastRange = false,
                    RealCastRange = null,
                    OnlyForKillSteal = false
                });

            #endregion

            #region shredder_chakram

            Spells.Add(
                new SpellData
                {
                    SpellName = "shredder_chakram",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0.5,
                    Radius = 0,
                    StringRadius = "radius",
                    Speed = "speed",
                    Width = null,
                    AllyBlock = false,
                    EnemyBlock = false,
                    MagicImmunityPierce = false,
                    FakeCastRange = false,
                    RealCastRange = null,
                    OnlyForKillSteal = false
                });

            #endregion

            #region undying_soul_rip

            Spells.Add(
                new SpellData
                {
                    SpellName = "undying_soul_rip",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region invoker_sun_strike

            Spells.Add(
                new SpellData
                {
                    SpellName = "invoker_sun_strike",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 1.7,
                    Radius = 0,
                    StringRadius = null,
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

            #region winter_wyvern_splinter_blast

            Spells.Add(
                new SpellData
                {
                    SpellName = "winter_wyvern_splinter_blast",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region dark_seer_surge

            Spells.Add(
                new SpellData
                {
                    SpellName = "dark_seer_surge",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region mirana_starfall

            Spells.Add(
                new SpellData
                {
                    SpellName = "mirana_starfall",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 400,
                    StringRadius = null,
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

            #region luna_lucent_beam

            Spells.Add(
                new SpellData
                {
                    SpellName = "luna_lucent_beam",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region nyx_assassin_mana_burn

            Spells.Add(
                new SpellData
                {
                    SpellName = "nyx_assassin_mana_burn",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region dark_seer_vacuum

            Spells.Add(
                new SpellData
                {
                    SpellName = "dark_seer_vacuum",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region rattletrap_power_cogs

            Spells.Add(
                new SpellData
                {
                    SpellName = "rattletrap_power_cogs",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0.1,
                    Radius = 125,
                    StringRadius = null,
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

            #region dark_seer_wall_of_replica

            Spells.Add(
                new SpellData
                {
                    SpellName = "dark_seer_wall_of_replica",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region ogre_magi_fireblast

            Spells.Add(
                new SpellData
                {
                    SpellName = "ogre_magi_fireblast",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region riki_blink_strike

            Spells.Add(
                new SpellData
                {
                    SpellName = "riki_blink_strike",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region invoker_cold_snap

            Spells.Add(
                new SpellData
                {
                    SpellName = "invoker_cold_snap",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region wisp_spirits

            Spells.Add(
                new SpellData
                {
                    SpellName = "wisp_spirits",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 1300,
                    StringRadius = null,
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

            #region warlock_fatal_bonds

            Spells.Add(
                new SpellData
                {
                    SpellName = "warlock_fatal_bonds",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region oracle_purifying_flames

            Spells.Add(
                new SpellData
                {
                    SpellName = "oracle_purifying_flames",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
                    Speed = null,
                    Width = null,
                    AllyBlock = false,
                    EnemyBlock = false,
                    MagicImmunityPierce = false,
                    FakeCastRange = false,
                    RealCastRange = null,
                    OnlyForKillSteal = true
                });

            #endregion

            #region burrow_width

            Spells.Add(
                new SpellData
                {
                    SpellName = "burrow_width",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = "burrow_width",
                    Speed = "burrow_speed",
                    Width = null,
                    AllyBlock = false,
                    EnemyBlock = false,
                    MagicImmunityPierce = false,
                    FakeCastRange = false,
                    RealCastRange = null,
                    OnlyForKillSteal = false
                });

            #endregion

            #region pudge_meat_hook

            Spells.Add(
                new SpellData
                {
                    SpellName = "pudge_meat_hook",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
                    Speed = "hook_speed",
                    Width = "hook_width",
                    AllyBlock = true,
                    EnemyBlock = true,
                    MagicImmunityPierce = true,
                    FakeCastRange = false,
                    RealCastRange = null,
                    OnlyForKillSteal = false
                });

            #endregion

            #region keeper_of_the_light_mana_leak

            Spells.Add(
                new SpellData
                {
                    SpellName = "keeper_of_the_light_mana_leak",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region keeper_of_the_light_spirit_form_illuminate

            Spells.Add(
                new SpellData
                {
                    SpellName = "keeper_of_the_light_spirit_form_illuminate",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region templar_assassin_meld

            Spells.Add(
                new SpellData
                {
                    SpellName = "templar_assassin_meld",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region disruptor_glimpse

            Spells.Add(
                new SpellData
                {
                    SpellName = "disruptor_glimpse",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region disruptor_thunder_strike

            Spells.Add(
                new SpellData
                {
                    SpellName = "disruptor_thunder_strike",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region disruptor_kinetic_field

            Spells.Add(
                new SpellData
                {
                    SpellName = "disruptor_kinetic_field",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 1.2,
                    Radius = 0,
                    StringRadius = null,
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

            #region ember_spirit_searing_chains

            Spells.Add(
                new SpellData
                {
                    SpellName = "ember_spirit_searing_chains",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = "radius",
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

            #region silencer_curse_of_the_silent

            Spells.Add(
                new SpellData
                {
                    SpellName = "silencer_curse_of_the_silent",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region venomancer_plague_ward

            Spells.Add(
                new SpellData
                {
                    SpellName = "venomancer_plague_ward",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region phoenix_icarus_dive_stop

            Spells.Add(
                new SpellData
                {
                    SpellName = "phoenix_icarus_dive_stop",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region phoenix_icarus_dive

            Spells.Add(
                new SpellData
                {
                    SpellName = "phoenix_icarus_dive",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region puck_ethereal_jaunt

            Spells.Add(
                new SpellData
                {
                    SpellName = "puck_ethereal_jaunt",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region enchantress_enchant

            Spells.Add(
                new SpellData
                {
                    SpellName = "enchantress_enchant",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region treant_leech_seed

            Spells.Add(
                new SpellData
                {
                    SpellName = "treant_leech_seed",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region phoenix_launch_fire_spirit

            Spells.Add(
                new SpellData
                {
                    SpellName = "phoenix_launch_fire_spirit",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
                    Speed = "spirit_speed",
                    Width = null,
                    AllyBlock = false,
                    EnemyBlock = false,
                    MagicImmunityPierce = false,
                    FakeCastRange = false,
                    RealCastRange = null,
                    OnlyForKillSteal = false
                });

            #endregion

            #region nyx_assassin_impale

            Spells.Add(
                new SpellData
                {
                    SpellName = "nyx_assassin_impale",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region tidehunter_gush

            Spells.Add(
                new SpellData
                {
                    SpellName = "tidehunter_gush",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
                    Speed = "projectile_speed",
                    Width = null,
                    AllyBlock = false,
                    EnemyBlock = false,
                    MagicImmunityPierce = false,
                    FakeCastRange = false,
                    RealCastRange = null,
                    OnlyForKillSteal = false
                });

            #endregion

            #region shadow_demon_demonic_purge

            Spells.Add(
                new SpellData
                {
                    SpellName = "shadow_demon_demonic_purge",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region puck_illusory_orb

            Spells.Add(
                new SpellData
                {
                    SpellName = "puck_illusory_orb",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
                    Speed = "orb_speed",
                    Width = null,
                    AllyBlock = false,
                    EnemyBlock = false,
                    MagicImmunityPierce = false,
                    FakeCastRange = false,
                    RealCastRange = null,
                    OnlyForKillSteal = false
                });

            #endregion

            #region legion_commander_duel

            Spells.Add(
                new SpellData
                {
                    SpellName = "legion_commander_duel",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region jakiro_dual_breath

            Spells.Add(
                new SpellData
                {
                    SpellName = "jakiro_dual_breath",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region lina_light_strike_array

            Spells.Add(
                new SpellData
                {
                    SpellName = "lina_light_strike_array",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0.5,
                    Radius = 0,
                    StringRadius = null,
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

            #region clinkz_strafe

            Spells.Add(
                new SpellData
                {
                    SpellName = "clinkz_strafe",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 630,
                    StringRadius = null,
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

            #region shadow_demon_disruption

            Spells.Add(
                new SpellData
                {
                    SpellName = "shadow_demon_disruption",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region slardar_slithereen_crush

            Spells.Add(
                new SpellData
                {
                    SpellName = "slardar_slithereen_crush",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = "crush_radius",
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

            #region terrorblade_conjure_image

            Spells.Add(
                new SpellData
                {
                    SpellName = "terrorblade_conjure_image",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region terrorblade_metamorphosis

            Spells.Add(
                new SpellData
                {
                    SpellName = "terrorblade_metamorphosis",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 700,
                    StringRadius = null,
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

            #region slark_dark_pact

            Spells.Add(
                new SpellData
                {
                    SpellName = "slark_dark_pact",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = "radius",
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

            #region terrorblade_reflection

            Spells.Add(
                new SpellData
                {
                    SpellName = "terrorblade_reflection",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region shadow_demon_soul_catcher

            Spells.Add(
                new SpellData
                {
                    SpellName = "shadow_demon_soul_catcher",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region terrorblade_sunder

            Spells.Add(
                new SpellData
                {
                    SpellName = "terrorblade_sunder",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region bloodseeker_rupture

            Spells.Add(
                new SpellData
                {
                    SpellName = "bloodseeker_rupture",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
                    Speed = null,
                    Width = null,
                    AllyBlock = false,
                    EnemyBlock = false,
                    MagicImmunityPierce = true,
                    FakeCastRange = false,
                    RealCastRange = null,
                    OnlyForKillSteal = false
                });

            #endregion

            #region undying_tombstone

            Spells.Add(
                new SpellData
                {
                    SpellName = "undying_tombstone",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = "radius",
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

            #region undying_decay

            Spells.Add(
                new SpellData
                {
                    SpellName = "undying_decay",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region gyrocopter_flak_cannon

            Spells.Add(
                new SpellData
                {
                    SpellName = "gyrocopter_flak_cannon",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = "radius",
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

            #region spectre_spectral_dagger

            Spells.Add(
                new SpellData
                {
                    SpellName = "spectre_spectral_dagger",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region naga_siren_rip_tide

            Spells.Add(
                new SpellData
                {
                    SpellName = "naga_siren_rip_tide",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = "radius",
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

            #region visage_soul_assumption

            Spells.Add(
                new SpellData
                {
                    SpellName = "visage_soul_assumption",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region earthshaker_echo_slam

            Spells.Add(
                new SpellData
                {
                    SpellName = "earthshaker_echo_slam",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 625,
                    StringRadius = null,
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

            #region invoker_ice_wall

            Spells.Add(
                new SpellData
                {
                    SpellName = "invoker_ice_wall",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 1,
                    Radius = 590,
                    StringRadius = null,
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

            #region tinker_laser

            Spells.Add(
                new SpellData
                {
                    SpellName = "tinker_laser",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region rattletrap_rocket_flare

            Spells.Add(
                new SpellData
                {
                    SpellName = "rattletrap_rocket_flare",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region invoker_forge_spirit

            Spells.Add(
                new SpellData
                {
                    SpellName = "invoker_forge_spirit",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 700,
                    StringRadius = null,
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

            #region weaver_the_swarm

            Spells.Add(
                new SpellData
                {
                    SpellName = "weaver_the_swarm",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = "radius",
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

            #region ember_spirit_fire_remnant

            Spells.Add(
                new SpellData
                {
                    SpellName = "ember_spirit_fire_remnant",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region earthshaker_fissure

            Spells.Add(
                new SpellData
                {
                    SpellName = "earthshaker_fissure",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = -0.1,
                    Radius = 0,
                    StringRadius = null,
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

            #region magnataur_skewer

            Spells.Add(
                new SpellData
                {
                    SpellName = "magnataur_skewer",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = "skewer_radius",
                    Speed = "skewer_speed",
                    Width = null,
                    AllyBlock = false,
                    EnemyBlock = false,
                    MagicImmunityPierce = false,
                    FakeCastRange = false,
                    RealCastRange = null,
                    OnlyForKillSteal = false
                });

            #endregion

            #region beastmaster_primal_roar

            Spells.Add(
                new SpellData
                {
                    SpellName = "beastmaster_primal_roar",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region tusk_walrus_kick

            Spells.Add(
                new SpellData
                {
                    SpellName = "tusk_walrus_kick",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region brewmaster_thunder_clap

            Spells.Add(
                new SpellData
                {
                    SpellName = "brewmaster_thunder_clap",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = "radius",
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

            #region bane_nightmare

            Spells.Add(
                new SpellData
                {
                    SpellName = "bane_nightmare",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 1,
                    Radius = 0,
                    StringRadius = null,
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

            #region keeper_of_the_light_illuminate

            Spells.Add(
                new SpellData
                {
                    SpellName = "keeper_of_the_light_illuminate",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region nevermore_shadowraze3

            Spells.Add(
                new SpellData
                {
                    SpellName = "nevermore_shadowraze3",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = "shadowraze_range",
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

            #region windrunner_powershot

            Spells.Add(
                new SpellData
                {
                    SpellName = "windrunner_powershot",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
                    Speed = "arrow_speed",
                    Width = null,
                    AllyBlock = false,
                    EnemyBlock = false,
                    MagicImmunityPierce = false,
                    FakeCastRange = false,
                    RealCastRange = null,
                    OnlyForKillSteal = false
                });

            #endregion

            #region tusk_ice_shards

            Spells.Add(
                new SpellData
                {
                    SpellName = "tusk_ice_shards",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
                    Speed = "shard_speed",
                    Width = null,
                    AllyBlock = false,
                    EnemyBlock = false,
                    MagicImmunityPierce = false,
                    FakeCastRange = false,
                    RealCastRange = null,
                    OnlyForKillSteal = false
                });

            #endregion

            #region brewmaster_drunken_haze

            Spells.Add(
                new SpellData
                {
                    SpellName = "brewmaster_drunken_haze",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region leshrac_lightning_storm

            Spells.Add(
                new SpellData
                {
                    SpellName = "leshrac_lightning_storm",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = "radius",
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

            #region necrolyte_death_pulse

            Spells.Add(
                new SpellData
                {
                    SpellName = "necrolyte_death_pulse",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = "area_of_effect",
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

            #region invoker_alacrity

            Spells.Add(
                new SpellData
                {
                    SpellName = "invoker_alacrity",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region shadow_shaman_voodoo

            Spells.Add(
                new SpellData
                {
                    SpellName = "shadow_shaman_voodoo",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region pugna_nether_blast

            Spells.Add(
                new SpellData
                {
                    SpellName = "pugna_nether_blast",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region morphling_adaptive_strike

            Spells.Add(
                new SpellData
                {
                    SpellName = "morphling_adaptive_strike",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region lone_druid_true_form

            Spells.Add(
                new SpellData
                {
                    SpellName = "lone_druid_true_form",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 700,
                    StringRadius = null,
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

            #region nevermore_shadowraze2

            Spells.Add(
                new SpellData
                {
                    SpellName = "nevermore_shadowraze2",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = "shadowraze_range",
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

            #region storm_spirit_ball_lightning

            Spells.Add(
                new SpellData
                {
                    SpellName = "storm_spirit_ball_lightning",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
                    Speed = "ball_lightning_move_speed",
                    Width = null,
                    AllyBlock = false,
                    EnemyBlock = false,
                    MagicImmunityPierce = false,
                    FakeCastRange = false,
                    RealCastRange = null,
                    OnlyForKillSteal = false
                });

            #endregion

            #region tusk_snowball

            Spells.Add(
                new SpellData
                {
                    SpellName = "tusk_snowball",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
                    Speed = "snowball_speed",
                    Width = null,
                    AllyBlock = false,
                    EnemyBlock = false,
                    MagicImmunityPierce = false,
                    FakeCastRange = false,
                    RealCastRange = null,
                    OnlyForKillSteal = false
                });

            #endregion

            #region phoenix_sun_ray_toggle_move

            Spells.Add(
                new SpellData
                {
                    SpellName = "phoenix_sun_ray_toggle_move",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region doom_bringer_scorched_earth

            Spells.Add(
                new SpellData
                {
                    SpellName = "doom_bringer_scorched_earth",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = "radius",
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

            #region phoenix_sun_ray

            Spells.Add(
                new SpellData
                {
                    SpellName = "phoenix_sun_ray",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
                    Speed = null,
                    Width = null,
                    AllyBlock = false,
                    EnemyBlock = false,
                    MagicImmunityPierce = false,
                    FakeCastRange = false,
                    RealCastRange = null,
                    OnlyForKillSteal = true
                });

            #endregion

            #region phoenix_fire_spirits

            Spells.Add(
                new SpellData
                {
                    SpellName = "phoenix_fire_spirits",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
                    Speed = "spirit_speed",
                    Width = null,
                    AllyBlock = false,
                    EnemyBlock = false,
                    MagicImmunityPierce = false,
                    FakeCastRange = false,
                    RealCastRange = null,
                    OnlyForKillSteal = false
                });

            #endregion

            #region silencer_last_word

            Spells.Add(
                new SpellData
                {
                    SpellName = "silencer_last_word",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 4,
                    Radius = 0,
                    StringRadius = null,
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

            #region phantom_lancer_spirit_lance

            Spells.Add(
                new SpellData
                {
                    SpellName = "phantom_lancer_spirit_lance",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
                    Speed = "lance_speed",
                    Width = null,
                    AllyBlock = false,
                    EnemyBlock = false,
                    MagicImmunityPierce = false,
                    FakeCastRange = false,
                    RealCastRange = null,
                    OnlyForKillSteal = false
                });

            #endregion

            #region ursa_enrage

            Spells.Add(
                new SpellData
                {
                    SpellName = "ursa_enrage",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 350,
                    StringRadius = null,
                    Speed = null,
                    Width = null,
                    AllyBlock = false,
                    EnemyBlock = false,
                    MagicImmunityPierce = true,
                    FakeCastRange = false,
                    RealCastRange = null,
                    OnlyForKillSteal = false
                });

            #endregion

            #region chen_penitence

            Spells.Add(
                new SpellData
                {
                    SpellName = "chen_penitence",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region invoker_deafening_blast

            Spells.Add(
                new SpellData
                {
                    SpellName = "invoker_deafening_blast",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = "travel_distance",
                    Speed = "travel_speed",
                    Width = null,
                    AllyBlock = false,
                    EnemyBlock = false,
                    MagicImmunityPierce = false,
                    FakeCastRange = false,
                    RealCastRange = null,
                    OnlyForKillSteal = false
                });

            #endregion

            #region warlock_shadow_word

            Spells.Add(
                new SpellData
                {
                    SpellName = "warlock_shadow_word",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region centaur_double_edge

            Spells.Add(
                new SpellData
                {
                    SpellName = "centaur_double_edge",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region earth_spirit_stone_caller

            Spells.Add(
                new SpellData
                {
                    SpellName = "earth_spirit_stone_caller",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 400,
                    StringRadius = null,
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

            #region bane_fiends_grip

            Spells.Add(
                new SpellData
                {
                    SpellName = "bane_fiends_grip",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region ogre_magi_bloodlust

            Spells.Add(
                new SpellData
                {
                    SpellName = "ogre_magi_bloodlust",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region earthshaker_enchant_totem

            Spells.Add(
                new SpellData
                {
                    SpellName = "earthshaker_enchant_totem",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 350,
                    StringRadius = null,
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

            #region earth_spirit_rolling_boulder

            Spells.Add(
                new SpellData
                {
                    SpellName = "earth_spirit_rolling_boulder",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0.6,
                    Radius = 0,
                    StringRadius = null,
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

            #region visage_grave_chill

            Spells.Add(
                new SpellData
                {
                    SpellName = "visage_grave_chill",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region tusk_frozen_sigil

            Spells.Add(
                new SpellData
                {
                    SpellName = "tusk_frozen_sigil",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region night_stalker_void

            Spells.Add(
                new SpellData
                {
                    SpellName = "night_stalker_void",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region riki_smoke_screen

            Spells.Add(
                new SpellData
                {
                    SpellName = "riki_smoke_screen",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = -0.1,
                    Radius = 0,
                    StringRadius = null,
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

            #region vengefulspirit_magic_missile

            Spells.Add(
                new SpellData
                {
                    SpellName = "vengefulspirit_magic_missile",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
                    Speed = "magic_missile_speed",
                    Width = null,
                    AllyBlock = false,
                    EnemyBlock = false,
                    MagicImmunityPierce = false,
                    FakeCastRange = false,
                    RealCastRange = null,
                    OnlyForKillSteal = false
                });

            #endregion

            #region leshrac_diabolic_edict

            Spells.Add(
                new SpellData
                {
                    SpellName = "leshrac_diabolic_edict",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region earth_spirit_boulder_smash

            Spells.Add(
                new SpellData
                {
                    SpellName = "earth_spirit_boulder_smash",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region slardar_sprint

            Spells.Add(
                new SpellData
                {
                    SpellName = "slardar_sprint",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region razor_plasma_field

            Spells.Add(
                new SpellData
                {
                    SpellName = "razor_plasma_field",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = "radius",
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

            #region ember_spirit_flame_guard

            Spells.Add(
                new SpellData
                {
                    SpellName = "ember_spirit_flame_guard",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = "radius",
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

            #region morphling_waveform

            Spells.Add(
                new SpellData
                {
                    SpellName = "morphling_waveform",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
                    Speed = "projectile_speed",
                    Width = null,
                    AllyBlock = false,
                    EnemyBlock = false,
                    MagicImmunityPierce = false,
                    FakeCastRange = false,
                    RealCastRange = null,
                    OnlyForKillSteal = false
                });

            #endregion

            #region axe_culling_blade

            Spells.Add(
                new SpellData
                {
                    SpellName = "axe_culling_blade",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
                    Speed = null,
                    Width = null,
                    AllyBlock = false,
                    EnemyBlock = false,
                    MagicImmunityPierce = true,
                    FakeCastRange = false,
                    RealCastRange = null,
                    OnlyForKillSteal = true
                });

            #endregion

            #region weaver_shukuchi

            Spells.Add(
                new SpellData
                {
                    SpellName = "weaver_shukuchi",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region death_prophet_silence

            Spells.Add(
                new SpellData
                {
                    SpellName = "death_prophet_silence",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = -0.1,
                    Radius = 0,
                    StringRadius = null,
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

            #region earth_spirit_geomagnetic_grip

            Spells.Add(
                new SpellData
                {
                    SpellName = "earth_spirit_geomagnetic_grip",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region enigma_midnight_pulse

            Spells.Add(
                new SpellData
                {
                    SpellName = "enigma_midnight_pulse",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region sniper_assassinate

            Spells.Add(
                new SpellData
                {
                    SpellName = "sniper_assassinate",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
                    Speed = null,
                    Width = null,
                    AllyBlock = false,
                    EnemyBlock = false,
                    MagicImmunityPierce = false,
                    FakeCastRange = false,
                    RealCastRange = null,
                    OnlyForKillSteal = true
                });

            #endregion

            #region elder_titan_echo_stomp

            Spells.Add(
                new SpellData
                {
                    SpellName = "elder_titan_echo_stomp",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = "radius",
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

            #region nevermore_shadowraze1

            Spells.Add(
                new SpellData
                {
                    SpellName = "nevermore_shadowraze1",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = "shadowraze_range",
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

            #region nevermore_requiem

            Spells.Add(
                new SpellData
                {
                    SpellName = "nevermore_requiem",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region tiny_toss

            Spells.Add(
                new SpellData
                {
                    SpellName = "tiny_toss",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region slark_pounce

            Spells.Add(
                new SpellData
                {
                    SpellName = "slark_pounce",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = "pounce_radius",
                    Speed = "pounce_speed",
                    Width = null,
                    AllyBlock = false,
                    EnemyBlock = false,
                    MagicImmunityPierce = false,
                    FakeCastRange = false,
                    RealCastRange = null,
                    OnlyForKillSteal = false
                });

            #endregion

            #region ogre_magi_ignite

            Spells.Add(
                new SpellData
                {
                    SpellName = "ogre_magi_ignite",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
                    Speed = "projectile_speed",
                    Width = null,
                    AllyBlock = false,
                    EnemyBlock = false,
                    MagicImmunityPierce = false,
                    FakeCastRange = false,
                    RealCastRange = null,
                    OnlyForKillSteal = false
                });

            #endregion

            #region furion_sprout

            Spells.Add(
                new SpellData
                {
                    SpellName = "furion_sprout",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = -0.1,
                    Radius = 0,
                    StringRadius = null,
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

            #region kunkka_ghostship

            Spells.Add(
                new SpellData
                {
                    SpellName = "kunkka_ghostship",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
                    Speed = "ghostship_speed",
                    Width = null,
                    AllyBlock = false,
                    EnemyBlock = false,
                    MagicImmunityPierce = false,
                    FakeCastRange = false,
                    RealCastRange = null,
                    OnlyForKillSteal = false
                });

            #endregion

            #region troll_warlord_whirling_axes_ranged

            Spells.Add(
                new SpellData
                {
                    SpellName = "troll_warlord_whirling_axes_ranged",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
                    Speed = "axe_speed",
                    Width = null,
                    AllyBlock = false,
                    EnemyBlock = false,
                    MagicImmunityPierce = false,
                    FakeCastRange = false,
                    RealCastRange = null,
                    OnlyForKillSteal = false
                });

            #endregion

            #region ember_spirit_activate_fire_remnant

            Spells.Add(
                new SpellData
                {
                    SpellName = "ember_spirit_activate_fire_remnant",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region storm_spirit_static_remnant

            Spells.Add(
                new SpellData
                {
                    SpellName = "storm_spirit_static_remnant",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region skeleton_king_hellfire_blast

            Spells.Add(
                new SpellData
                {
                    SpellName = "skeleton_king_hellfire_blast",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
                    Speed = "blast_speed",
                    Width = null,
                    AllyBlock = false,
                    EnemyBlock = false,
                    MagicImmunityPierce = false,
                    FakeCastRange = false,
                    RealCastRange = null,
                    OnlyForKillSteal = false
                });

            #endregion

            #region centaur_hoof_stomp

            Spells.Add(
                new SpellData
                {
                    SpellName = "centaur_hoof_stomp",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = "radius",
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

            #region tusk_walrus_punch

            Spells.Add(
                new SpellData
                {
                    SpellName = "tusk_walrus_punch",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region leshrac_split_earth

            Spells.Add(
                new SpellData
                {
                    SpellName = "leshrac_split_earth",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0.35,
                    Radius = 0,
                    StringRadius = null,
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

            #region spirit_breaker_charge_of_darkness

            Spells.Add(
                new SpellData
                {
                    SpellName = "spirit_breaker_charge_of_darkness",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
                    Speed = "movement_speed",
                    Width = null,
                    AllyBlock = false,
                    EnemyBlock = false,
                    MagicImmunityPierce = false,
                    FakeCastRange = false,
                    RealCastRange = null,
                    OnlyForKillSteal = false
                });

            #endregion

            #region vengefulspirit_wave_of_terror

            Spells.Add(
                new SpellData
                {
                    SpellName = "vengefulspirit_wave_of_terror",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region queenofpain_scream_of_pain

            Spells.Add(
                new SpellData
                {
                    SpellName = "queenofpain_scream_of_pain",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = "area_of_effect",
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

            #region earth_spirit_petrify

            Spells.Add(
                new SpellData
                {
                    SpellName = "earth_spirit_petrify",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region gyrocopter_rocket_barrage

            Spells.Add(
                new SpellData
                {
                    SpellName = "gyrocopter_rocket_barrage",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = "radius",
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

            #region invoker_emp

            Spells.Add(
                new SpellData
                {
                    SpellName = "invoker_emp",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 2.9,
                    Radius = 0,
                    StringRadius = null,
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

            #region bane_enfeeble

            Spells.Add(
                new SpellData
                {
                    SpellName = "bane_enfeeble",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region bristleback_viscous_nasal_goo

            Spells.Add(
                new SpellData
                {
                    SpellName = "bristleback_viscous_nasal_goo",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = "radius",
                    Speed = null,
                    Width = null,
                    AllyBlock = false,
                    EnemyBlock = false,
                    MagicImmunityPierce = true,
                    FakeCastRange = false,
                    RealCastRange = null,
                    OnlyForKillSteal = false
                });

            #endregion

            #region shadow_demon_shadow_poison

            Spells.Add(
                new SpellData
                {
                    SpellName = "shadow_demon_shadow_poison",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region shredder_return_chakram

            Spells.Add(
                new SpellData
                {
                    SpellName = "shredder_return_chakram",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region shadow_shaman_ether_shock

            Spells.Add(
                new SpellData
                {
                    SpellName = "shadow_shaman_ether_shock",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region ancient_apparition_ice_vortex

            Spells.Add(
                new SpellData
                {
                    SpellName = "ancient_apparition_ice_vortex",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region bloodseeker_blood_bath

            Spells.Add(
                new SpellData
                {
                    SpellName = "bloodseeker_blood_bath",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 2.6,
                    Radius = 0,
                    StringRadius = null,
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

            #region gyrocopter_homing_missile

            Spells.Add(
                new SpellData
                {
                    SpellName = "gyrocopter_homing_missile",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 3,
                    Radius = 0,
                    StringRadius = null,
                    Speed = "speed",
                    Width = null,
                    AllyBlock = false,
                    EnemyBlock = false,
                    MagicImmunityPierce = false,
                    FakeCastRange = false,
                    RealCastRange = null,
                    OnlyForKillSteal = false
                });

            #endregion

            #region jakiro_ice_path

            Spells.Add(
                new SpellData
                {
                    SpellName = "jakiro_ice_path",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0.5,
                    Radius = 0,
                    StringRadius = null,
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

            #region drow_ranger_wave_of_silence

            Spells.Add(
                new SpellData
                {
                    SpellName = "drow_ranger_wave_of_silence",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
                    Speed = "wave_speed",
                    Width = null,
                    AllyBlock = false,
                    EnemyBlock = false,
                    MagicImmunityPierce = false,
                    FakeCastRange = false,
                    RealCastRange = null,
                    OnlyForKillSteal = false
                });

            #endregion

            #region invoker_tornado

            Spells.Add(
                new SpellData
                {
                    SpellName = "invoker_tornado",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = "travel_distance",
                    Speed = "travel_speed",
                    Width = null,
                    AllyBlock = false,
                    EnemyBlock = false,
                    MagicImmunityPierce = false,
                    FakeCastRange = false,
                    RealCastRange = null,
                    OnlyForKillSteal = false
                });

            #endregion

            #region dragon_knight_breathe_fire

            Spells.Add(
                new SpellData
                {
                    SpellName = "dragon_knight_breathe_fire",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region naga_siren_ensnare

            Spells.Add(
                new SpellData
                {
                    SpellName = "naga_siren_ensnare",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
                    Speed = "net_speed",
                    Width = null,
                    AllyBlock = false,
                    EnemyBlock = false,
                    MagicImmunityPierce = true,
                    FakeCastRange = false,
                    RealCastRange = null,
                    OnlyForKillSteal = false
                });

            #endregion

            #region kunkka_x_marks_the_spot

            Spells.Add(
                new SpellData
                {
                    SpellName = "kunkka_x_marks_the_spot",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0.1,
                    Radius = 0,
                    StringRadius = null,
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

            #region spirit_breaker_nether_strike

            Spells.Add(
                new SpellData
                {
                    SpellName = "spirit_breaker_nether_strike",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region razor_static_link

            Spells.Add(
                new SpellData
                {
                    SpellName = "razor_static_link",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region necrolyte_reapers_scythe

            Spells.Add(
                new SpellData
                {
                    SpellName = "necrolyte_reapers_scythe",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region broodmother_spawn_spiderlings

            Spells.Add(
                new SpellData
                {
                    SpellName = "broodmother_spawn_spiderlings",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region bristleback_quill_spray

            Spells.Add(
                new SpellData
                {
                    SpellName = "bristleback_quill_spray",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region legion_commander_overwhelming_odds

            Spells.Add(
                new SpellData
                {
                    SpellName = "legion_commander_overwhelming_odds",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region ancient_apparition_ice_blast_release

            Spells.Add(
                new SpellData
                {
                    SpellName = "ancient_apparition_ice_blast_release",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region tinker_heat_seeking_missile

            Spells.Add(
                new SpellData
                {
                    SpellName = "tinker_heat_seeking_missile",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = "radius",
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

            #region shredder_whirling_death

            Spells.Add(
                new SpellData
                {
                    SpellName = "shredder_whirling_death",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = "whirling_radius",
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

            #region elder_titan_ancestral_spirit

            Spells.Add(
                new SpellData
                {
                    SpellName = "elder_titan_ancestral_spirit",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region chaos_knight_chaos_bolt

            Spells.Add(
                new SpellData
                {
                    SpellName = "chaos_knight_chaos_bolt",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region legion_commander_press_the_attack

            Spells.Add(
                new SpellData
                {
                    SpellName = "legion_commander_press_the_attack",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region kunkka_torrent

            Spells.Add(
                new SpellData
                {
                    SpellName = "kunkka_torrent",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 1.7,
                    Radius = 0,
                    StringRadius = null,
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

            #region bounty_hunter_shuriken_toss

            Spells.Add(
                new SpellData
                {
                    SpellName = "bounty_hunter_shuriken_toss",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
                    Speed = null,
                    Width = null,
                    AllyBlock = false,
                    EnemyBlock = false,
                    MagicImmunityPierce = false,
                    FakeCastRange = false,
                    RealCastRange = null,
                    OnlyForKillSteal = true
                });

            #endregion

            #region omniknight_repel

            Spells.Add(
                new SpellData
                {
                    SpellName = "omniknight_repel",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region batrider_sticky_napalm

            Spells.Add(
                new SpellData
                {
                    SpellName = "batrider_sticky_napalm",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0.2,
                    Radius = 0,
                    StringRadius = null,
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

            #region pudge_dismember

            Spells.Add(
                new SpellData
                {
                    SpellName = "pudge_dismember",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
                    Speed = null,
                    Width = null,
                    AllyBlock = false,
                    EnemyBlock = false,
                    MagicImmunityPierce = true,
                    FakeCastRange = false,
                    RealCastRange = null,
                    OnlyForKillSteal = false
                });

            #endregion

            #region batrider_flaming_lasso

            Spells.Add(
                new SpellData
                {
                    SpellName = "batrider_flaming_lasso",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
                    Speed = null,
                    Width = null,
                    AllyBlock = false,
                    EnemyBlock = false,
                    MagicImmunityPierce = true,
                    FakeCastRange = false,
                    RealCastRange = null,
                    OnlyForKillSteal = false
                });

            #endregion

            #region slardar_amplify_damage

            Spells.Add(
                new SpellData
                {
                    SpellName = "slardar_amplify_damage",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region lion_voodoo

            Spells.Add(
                new SpellData
                {
                    SpellName = "lion_voodoo",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region lich_frost_nova

            Spells.Add(
                new SpellData
                {
                    SpellName = "lich_frost_nova",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region beastmaster_call_of_the_wild_boar

            Spells.Add(
                new SpellData
                {
                    SpellName = "beastmaster_call_of_the_wild_boar",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region tiny_avalanche

            Spells.Add(
                new SpellData
                {
                    SpellName = "tiny_avalanche",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0.5,
                    Radius = 0,
                    StringRadius = null,
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

            #region doom_bringer_doom

            Spells.Add(
                new SpellData
                {
                    SpellName = "doom_bringer_doom",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region tinker_rearm

            Spells.Add(
                new SpellData
                {
                    SpellName = "tinker_rearm",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region abaddon_death_coil

            Spells.Add(
                new SpellData
                {
                    SpellName = "abaddon_death_coil",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region templar_assassin_refraction

            Spells.Add(
                new SpellData
                {
                    SpellName = "templar_assassin_refraction",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region troll_warlord_whirling_axes_melee

            Spells.Add(
                new SpellData
                {
                    SpellName = "troll_warlord_whirling_axes_melee",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 400,
                    StringRadius = null,
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

            #region leshrac_pulse_nova

            Spells.Add(
                new SpellData
                {
                    SpellName = "leshrac_pulse_nova",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = "radius",
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

            #region lion_finger_of_death

            Spells.Add(
                new SpellData
                {
                    SpellName = "lion_finger_of_death",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region life_stealer_open_wounds

            Spells.Add(
                new SpellData
                {
                    SpellName = "life_stealer_open_wounds",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region bane_brain_sap

            Spells.Add(
                new SpellData
                {
                    SpellName = "bane_brain_sap",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region zuus_arc_lightning

            Spells.Add(
                new SpellData
                {
                    SpellName = "zuus_arc_lightning",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region tidehunter_anchor_smash

            Spells.Add(
                new SpellData
                {
                    SpellName = "tidehunter_anchor_smash",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = "radius",
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

            #region night_stalker_crippling_fear

            Spells.Add(
                new SpellData
                {
                    SpellName = "night_stalker_crippling_fear",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region skywrath_mage_arcane_bolt

            Spells.Add(
                new SpellData
                {
                    SpellName = "skywrath_mage_arcane_bolt",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region invoker_chaos_meteor

            Spells.Add(
                new SpellData
                {
                    SpellName = "invoker_chaos_meteor",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 1.3,
                    Radius = 0,
                    StringRadius = null,
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

            #region medusa_mystic_snake

            Spells.Add(
                new SpellData
                {
                    SpellName = "medusa_mystic_snake",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region sniper_shrapnel

            Spells.Add(
                new SpellData
                {
                    SpellName = "sniper_shrapnel",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 1.4,
                    Radius = 0,
                    StringRadius = "radius",
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

            #region lina_laguna_blade

            Spells.Add(
                new SpellData
                {
                    SpellName = "lina_laguna_blade",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region ursa_overpower

            Spells.Add(
                new SpellData
                {
                    SpellName = "ursa_overpower",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
                    Speed = null,
                    Width = null,
                    AllyBlock = false,
                    EnemyBlock = false,
                    MagicImmunityPierce = true,
                    FakeCastRange = false,
                    RealCastRange = null,
                    OnlyForKillSteal = false
                });

            #endregion

            #region queenofpain_sonic_wave

            Spells.Add(
                new SpellData
                {
                    SpellName = "queenofpain_sonic_wave",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
                    Speed = "speed",
                    Width = null,
                    AllyBlock = false,
                    EnemyBlock = false,
                    MagicImmunityPierce = false,
                    FakeCastRange = false,
                    RealCastRange = null,
                    OnlyForKillSteal = false
                });

            #endregion

            #region lion_impale

            Spells.Add(
                new SpellData
                {
                    SpellName = "lion_impale",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 450,
                    StringRadius = null,
                    Speed = null,
                    Width = "width",
                    AllyBlock = false,
                    EnemyBlock = false,
                    MagicImmunityPierce = false,
                    FakeCastRange = false,
                    RealCastRange = null,
                    OnlyForKillSteal = false
                });

            #endregion

            #region lina_dragon_slave

            Spells.Add(
                new SpellData
                {
                    SpellName = "lina_dragon_slave",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
                    Speed = "dragon_slave_speed",
                    Width = null,
                    AllyBlock = false,
                    EnemyBlock = false,
                    MagicImmunityPierce = false,
                    FakeCastRange = false,
                    RealCastRange = null,
                    OnlyForKillSteal = false
                });

            #endregion

            #region venomancer_venomous_gale

            Spells.Add(
                new SpellData
                {
                    SpellName = "venomancer_venomous_gale",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region skywrath_mage_ancient_seal

            Spells.Add(
                new SpellData
                {
                    SpellName = "skywrath_mage_ancient_seal",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region antimage_mana_void

            Spells.Add(
                new SpellData
                {
                    SpellName = "antimage_mana_void",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
                    Speed = null,
                    Width = null,
                    AllyBlock = false,
                    EnemyBlock = false,
                    MagicImmunityPierce = false,
                    FakeCastRange = false,
                    RealCastRange = null,
                    OnlyForKillSteal = true
                });

            #endregion

            #region shadow_shaman_shackles

            Spells.Add(
                new SpellData
                {
                    SpellName = "shadow_shaman_shackles",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region huskar_life_break

            Spells.Add(
                new SpellData
                {
                    SpellName = "huskar_life_break",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
                    Speed = "charge_speed",
                    Width = null,
                    AllyBlock = false,
                    EnemyBlock = false,
                    MagicImmunityPierce = false,
                    FakeCastRange = false,
                    RealCastRange = null,
                    OnlyForKillSteal = false
                });

            #endregion

            #region dragon_knight_dragon_tail

            Spells.Add(
                new SpellData
                {
                    SpellName = "dragon_knight_dragon_tail",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region crystal_maiden_frostbite

            Spells.Add(
                new SpellData
                {
                    SpellName = "crystal_maiden_frostbite",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
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

            #region skywrath_mage_concussive_shot

            Spells.Add(
                new SpellData
                {
                    SpellName = "skywrath_mage_concussive_shot",
                    IsStun = true,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = "launch_radius",
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

            #region bloodseeker_bloodrage

            Spells.Add(
                new SpellData
                {
                    SpellName = "bloodseeker_bloodrage",
                    IsStun = false,
                    IsSlow = false,
                    IsNuke = false,
                    IsSkillShot = false,
                    IsHeal = false,
                    IsShield = false,
                    AdditionalDelay = 0,
                    Radius = 0,
                    StringRadius = null,
                    Speed = null,
                    Width = null,
                    AllyBlock = false,
                    EnemyBlock = false,
                    MagicImmunityPierce = true,
                    FakeCastRange = false,
                    RealCastRange = null,
                    OnlyForKillSteal = false
                });

            #endregion


        }
    }

}
