namespace Ability.ObjectManager.Heroes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ability.Casting;
    using Ability.OnUpdate;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.AbilityInfo;
    using Ensage.Common.Extensions;

    internal class EnemyHeroes
    {
        #region Static Fields

        public static List<Ability> Abilities;

        public static Dictionary<string, List<Ability>> AbilityDictionary;

        public static List<Hero> Heroes;

        public static Dictionary<string, List<Item>> ItemDictionary;

        public static List<Item> Items;

        public static Hero[] UsableHeroes;

        #endregion

        #region Public Methods and Operators

        public static void Update(EventArgs args)
        {
            if (!OnUpdateChecks.CanUpdate())
            {
                return;
            }

            if (Utils.SleepCheck("getheroes") && Heroes.Count < 5)
            {
                UpdateHeroes();
                Utils.Sleep(1000, "getheroes");
            }

            Heroes = Heroes.Where(x => x.IsValid).ToList();
            UsableHeroes = Heroes.Where(x => x.Health > 0 && x.IsAlive && x.IsVisible).ToArray();
            if (Utils.SleepCheck("enemyHeroesCheckValid")
                || UsableHeroes.Any(x => !ItemDictionary.ContainsKey(NameManager.Name(x))))
            {
                Utils.Sleep(2000, "enemyHeroesCheckValid");
                var itemList = new List<Item>(Items);
                foreach (var hero in UsableHeroes)
                {
                    var name = NameManager.Name(hero);
                    var items = hero.Inventory.Items.ToList();
                    foreach (var ability in
                        items.Where(x => !itemList.Contains(x) && AbilityDatabase.Find(NameManager.Name(x)) != null)
                            .OrderBy(ComboOrder.GetAbilityOrder))
                    {
                        Items.Add(ability);
                    }

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
            var list = Ensage.Common.Objects.Heroes.GetByTeam(AbilityMain.Me.GetEnemyTeam());
            var herolist = new List<Hero>(Heroes);
            var abilityList = new List<Ability>(Abilities.Where(x => x.IsValid));
            foreach (var hero in list.Where(x => x.IsValid && x.IsVisible))
            {
                var name = NameManager.Name(hero);
                var spells = hero.Spellbook.Spells.ToList();
                if (!herolist.Contains(hero))
                {
                    // if (name == "npc_dota_hero_ogre_magi")
                    // {
                    // Game.PrintMessage(
                    // "<font face='Calibri'>[ABILITY#]: SpellOverlay is temporary disabled for OgreMagi due to Ensage.Core issues</font>",
                    // MessageType.ChatMessage);
                    // }
                    Heroes.Add(hero);
                }

                // if (name == "npc_dota_hero_ogre_magi")
                // {
                // continue;
                // }
                foreach (var ability in
                    spells.Where(x => !abilityList.Contains(x) && AbilityDatabase.Find(NameManager.Name(x)) != null)
                        .OrderBy(ComboOrder.GetAbilityOrder))
                {
                    Abilities.Add(ability);
                }

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