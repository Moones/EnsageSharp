namespace Ability.ObjectManager.Heroes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ability.Casting;
    using Ability.ObjectManager.Players;
    using Ability.OnUpdate;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.AbilityInfo;

    internal class AllyHeroes
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
            var list = Ensage.Common.Objects.Heroes.GetByTeam(AbilityMain.Me.Team);
            var herolist = new List<Hero>(Heroes);
            var abilityList = new List<Ability>(Abilities.Where(x => x.IsValid));
            foreach (var hero in list.Where(x => x.IsValid && x.IsVisible))
            {
                var name = NameManager.Name(hero);
                var spells = hero.Spellbook.Spells.ToList();
                if (!herolist.Contains(hero))
                {
                    Heroes.Add(hero);
                }
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