namespace Ability.ObjectManager.Heroes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ability.OnUpdate;

    using Ensage;
    using Ensage.Common;

    internal class AllyHeroes
    {
        #region Static Fields

        public static Dictionary<string, List<Ability>> AbilityDictionary;

        public static List<Hero> Heroes;

        public static Dictionary<string, List<Item>> ItemDictionary;

        public static Hero[] UsableHeroes;

        #endregion

        #region Public Methods and Operators

        public static void Update(EventArgs args)
        {
            if (!OnUpdateChecks.CanUpdate())
            {
                return;
            }

            if (Utils.SleepCheck("getallyheroes") && Heroes.Count(x => x.IsValid) < 5)
            {
                UpdateHeroes();
                Utils.Sleep(1000, "getallyheroes");
            }

            Heroes = Heroes.Where(x => x.IsValid).ToList();
            UsableHeroes = Heroes.Where(x => x.Health > 0 && x.IsAlive && x.IsVisible).ToArray();
            if (Utils.SleepCheck("allyHeroesCheckValid"))
            {
                Utils.Sleep(2000, "allyHeroesCheckValid");
                foreach (var hero in UsableHeroes)
                {
                    var name = NameManager.Name(hero);
                    var items = hero.Inventory.Items.ToList();

                    if (ItemDictionary.ContainsKey(name))
                    {
                        ItemDictionary[name] =
                            items.Where(
                                x => x.AbilityType != AbilityType.Attribute && x.AbilityType != AbilityType.Hidden)
                                .ToList();
                        continue;
                    }

                    var itemlist =
                        items.Where(x => x.AbilityType != AbilityType.Attribute && x.AbilityType != AbilityType.Hidden)
                            .ToList();
                    ItemDictionary.Add(name, itemlist);
                }
            }
        }

        public static void UpdateHeroes()
        {
            var list = Ensage.Common.Objects.Heroes.GetByTeam(AbilityMain.Me.Team);
            var herolist = new List<Hero>(Heroes);
            foreach (var hero in list.Where(x => x.IsValid && !x.IsIllusion && x.IsVisible))
            {
                var name = NameManager.Name(hero);
                var spells = hero.Spellbook.Spells.ToList();
                if (!herolist.Contains(hero))
                {
                    // if (name == "npc_dota_hero_ogre_magi")
                    // {
                    // Game.PrintMessage(
                    // "[ABILITY#]: SpellOverlay is temporary disabled for OgreMagi due to Ensage.Core issues",
                    // MessageType.ChatMessage);
                    // }
                    Heroes.Add(hero);
                }

                // if (name == "npc_dota_hero_ogre_magi")
                // {
                // continue;
                // }
                var abilitylist =
                    spells.Where(x => x.AbilityType != AbilityType.Attribute && x.AbilityType != AbilityType.Hidden)
                        .ToList();
                if (AbilityDictionary.ContainsKey(name))
                {
                    AbilityDictionary[name] = abilitylist;
                    continue;
                }

                AbilityDictionary.Add(name, abilitylist);
            }
        }

        #endregion
    }
}