namespace Ability.Casting
{
    using System.Collections.Generic;

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
                                                                                //{ "item_dagon", 3 }, { "item_dagon_2", 3 },
                                                                                //{ "item_dagon_3", 3 },
                                                                                //{ "item_dagon_4", 3 },
                                                                                // { "item_dagon_5", 3 },
                                                                                { "necrolyte_reapers_scythe", 0 },
                                                                                { "morphling_waveform", 3 },
                                                                                { "necrolyte_death_pulse", 1 },
                                                                                { "item_shivas_guard", 1 },
                                                                                { "tinker_heat_seeking_missile", 3 }, { "pudge_rot", 3 },
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
                                                                            };

        public static Dictionary<string, uint> DamageOrderDictionary = new Dictionary<string, uint>
                                                                           {
                                                                               { "necrolyte_reapers_scythe", 10 },
                                                                               { "item_ethereal_blade", 1 },
                                                                               { "item_veil_of_discord", 0 },
                                                                               { "tusk_walrus_punch", 0 },
                                                                               { "item_dagon", 2 }, { "item_dagon_2", 2 },
                                                                               { "item_dagon_3", 2 },
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
                                                                                //{ "item_dagon", 3 }, { "item_dagon_2", 3 },
                                                                                //{ "item_dagon_3", 3 },
                                                                                //{ "item_dagon_4", 3 },
                                                                                // { "item_dagon_5", 3 },
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
                                                                            };

        #endregion

        #region Public Methods and Operators

        public static long GetAbilityOrder(Ability ability)
        {
            return !AbilityOrderDictionary.ContainsKey(ability.Name) ? 4 : AbilityOrderDictionary[ability.Name];
        }

        public static long GetComboOrder(Ability ability, bool disable)
        {
            if (disable)
            {
                return !DisableOrderDictionary.ContainsKey(ability.Name) ? 4 : DisableOrderDictionary[ability.Name];
            }
            return !AbilityOrderDictionary.ContainsKey(ability.Name) ? 4 : AbilityOrderDictionary[ability.Name];
        }

        public static long GetDamageOrder(Ability ability)
        {
            return !DamageOrderDictionary.ContainsKey(ability.Name) ? 3 : DamageOrderDictionary[ability.Name];
        }

        #endregion
    }
}