namespace Ability.Casting
{
    using System.Collections.Generic;

    using Ability.ObjectManager;

    using Ensage;

    internal class ComboOrder
    {
        #region Static Fields

        public static Dictionary<string, uint> AbilityOrderDictionary = new Dictionary<string, uint>
                                                                            {
                                                                                { "tusk_walrus_kick", 10 }, 
                                                                                { "tusk_walrus_punch", 0 }, 
                                                                                { "tusk_snowball", 1 }, 
                                                                                { "item_ethereal_blade", 2 }, 
                                                                                { "item_veil_of_discord", 2 }, 
                                                                                { "necrolyte_reapers_scythe", 0 }, 
                                                                                { "morphling_waveform", 3 }, 
                                                                                { "necrolyte_death_pulse", 1 }, 
                                                                                { "item_shivas_guard", 1 }, 
                                                                                { "tinker_heat_seeking_missile", 3 }, 
                                                                                { "pudge_meat_hook", 2 }, 
                                                                                { "pudge_rot", 3 }, 
                                                                                { "axe_culling_blade", 10 }, 
                                                                                { "item_cyclone", 0 }, 
                                                                                { "leshrac_lightning_storm", 0 }, 
                                                                                { "slardar_slithereen_crush", 2 }, 
                                                                                { "lion_voodoo", 0 }, 
                                                                                { "item_sheepstick", 0 }, 
                                                                                { "shadow_shaman_voodoo", 0 }, 
                                                                                { "zuus_lightning_bolt", 4 }, 
                                                                                { "zuus_thundergods_wrath", 10 }, 
                                                                                { "templar_assassin_psionic_trap", 2 }

                                                                                // { "item_dagon", 3 }, { "item_dagon_2", 3 },

                                                                                // { "item_dagon_3", 3 },

                                                                                // { "item_dagon_4", 3 },

                                                                                // { "item_dagon_5", 3 },
                                                                            };

        public static Dictionary<string, Dictionary<string, uint>> CustomHeroOrderDictionary =
            new Dictionary<string, Dictionary<string, uint>>
                {
                    {
                        "npc_dota_hero_ancient_apparition", 
                        new Dictionary<string, uint>
                            {
                               { "ancient_apparition_cold_feet", 0 }, { "item_cyclone", 1 }, { "item_rod_of_atos", 2 } 
                            }
                    }, 
                    {
                        "npc_dota_hero_chaos_knight", new Dictionary<string, uint> { { "chaos_knight_reality_rift", 3 } }
                    }, 
                    { "npc_dota_hero_naga_siren", new Dictionary<string, uint> { { "naga_siren_ensnare", 3 } } }, 
                    {
                        "npc_dota_hero_templar_assassin", 
                        new Dictionary<string, uint> { { "templar_assassin_refraction", 3 } }
                    }
                };

        public static Dictionary<string, uint> DamageOrderDictionary = new Dictionary<string, uint>
                                                                           {
                                                                               { "necrolyte_reapers_scythe", 10 }, 
                                                                               { "item_ethereal_blade", 1 }, 
                                                                               { "item_veil_of_discord", 0 }, 
                                                                               { "tusk_walrus_punch", 0 }, 
                                                                               { "item_dagon", 2 }, { "item_dagon_2", 2 }, 
                                                                               { "item_dagon_3", 2 }, 
                                                                               { "morphling_adaptive_strike", 2 }, 
                                                                               { "item_dagon_4", 2 }, 
                                                                               { "item_dagon_5", 2 }, 
                                                                               { "item_shivas_guard", 7 }, 
                                                                               { "zuus_lightning_bolt", 3 }, 
                                                                               { "zuus_thundergods_wrath", 10 }
                                                                           };

        public static Dictionary<string, uint> DisableOrderDictionary = new Dictionary<string, uint>
                                                                            {
                                                                                { "tusk_walrus_kick", 10 }, 
                                                                                { "tusk_walrus_punch", 0 }, 
                                                                                { "tusk_snowball", 1 }, 
                                                                                { "item_ethereal_blade", 2 }, 
                                                                                { "item_veil_of_discord", 2 }, 
                                                                                { "necrolyte_reapers_scythe", 0 }, 
                                                                                { "morphling_waveform", 3 }, 
                                                                                { "morphling_adaptive_strike", 2 }, 
                                                                                { "necrolyte_death_pulse", 1 }, 
                                                                                { "tinker_laser", 4 }, { "pudge_rot", 3 }, 
                                                                                { "axe_culling_blade", 10 }, 
                                                                                { "item_cyclone", 0 }, 
                                                                                { "leshrac_lightning_storm", 0 }, 
                                                                                { "slardar_slithereen_crush", 2 }, 
                                                                                { "lion_voodoo", 0 }, 
                                                                                { "item_sheepstick", 0 }, 
                                                                                { "shadow_shaman_voodoo", 0 }, 
                                                                                { "zuus_lightning_bolt", 4 }, 
                                                                                { "zuus_thundergods_wrath", 10 }

                                                                                // { "item_dagon", 3 }, { "item_dagon_2", 3 },

                                                                                // { "item_dagon_3", 3 },

                                                                                // { "item_dagon_4", 3 },

                                                                                // { "item_dagon_5", 3 },
                                                                            };

        #endregion

        #region Public Methods and Operators

        public static long GetAbilityOrder(Ability ability)
        {
            return !AbilityOrderDictionary.ContainsKey(NameManager.Name(ability))
                       ? 4
                       : AbilityOrderDictionary[NameManager.Name(ability)];
        }

        public static long GetComboOrder(Ability ability, bool disable)
        {
            if (CustomHeroOrderDictionary.ContainsKey(NameManager.Name(AbilityMain.Me))
                && CustomHeroOrderDictionary[NameManager.Name(AbilityMain.Me)].ContainsKey(NameManager.Name(ability)))
            {
                return CustomHeroOrderDictionary[NameManager.Name(AbilityMain.Me)][NameManager.Name(ability)];
            }

            if (disable)
            {
                return !DisableOrderDictionary.ContainsKey(NameManager.Name(ability))
                           ? 4
                           : DisableOrderDictionary[NameManager.Name(ability)];
            }

            return !AbilityOrderDictionary.ContainsKey(NameManager.Name(ability))
                       ? 4
                       : AbilityOrderDictionary[NameManager.Name(ability)];
        }

        public static long GetDamageOrder(Ability ability)
        {
            return !DamageOrderDictionary.ContainsKey(NameManager.Name(ability))
                       ? 3
                       : DamageOrderDictionary[NameManager.Name(ability)];
        }

        #endregion
    }
}