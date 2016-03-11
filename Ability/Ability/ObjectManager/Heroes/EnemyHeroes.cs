namespace Ability.ObjectManager.Heroes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ability.OnUpdate;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Extensions;

    internal class EnemyHeroes
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

            if (Utils.SleepCheck("getheroes") && Heroes.Count(x => x.IsValid) < 5)
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
            var list = Ensage.Common.Objects.Heroes.GetByTeam(AbilityMain.Me.GetEnemyTeam());
            var herolist = new List<Hero>(Heroes);
            foreach (var hero in list.Where(x => x.IsValid && !x.IsIllusion && x.IsVisible))
            {
                var name = NameManager.Name(hero);
                var spells = hero.Spellbook.Spells.ToList();
                if (!herolist.Contains(hero))
                {
                    Heroes.Add(hero);
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