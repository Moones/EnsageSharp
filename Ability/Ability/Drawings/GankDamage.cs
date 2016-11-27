namespace Ability.Drawings
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ability.AbilityMenu;
    using Ability.ObjectManager;
    using Ability.ObjectManager.Heroes;
    using Ability.OnUpdate;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.AbilityInfo;
    using Ensage.Common.Extensions;

    using SharpDX;

    internal class GankDamage
    {
        #region Static Fields

        public static Dictionary<string, float> IncomingDamages = new Dictionary<string, float>();

        private static Hero[] allies = { };

        private static Hero[] enemies = { };

        #endregion

        #region Public Methods and Operators

        public static void Drawing_OnDraw(EventArgs args)
        {
            if (!Game.IsInGame || !IncomingDamages.Any() || AbilityMain.Me == null || !AbilityMain.Me.IsValid
                || !MainMenu.GankDamageMenu.Item("enableGankDamage").GetValue<bool>())
            {
                return;
            }

            if (MainMenu.GankDamageMenu.Item("enableGankDamageAllies").GetValue<bool>())
            {
                foreach (var ally in
                    allies.Where(x => x != null && x.IsValid && IncomingDamages.ContainsKey(NameManager.Name(x))))
                {
                    var health = ally.Health;
                    var maxHealth = ally.MaximumHealth;
                    var allyName = NameManager.Name(ally);
                    var hpleft = Math.Max(health - IncomingDamages[allyName], 0);
                    var hpperc = hpleft / maxHealth;
                    var dmgperc = Math.Min(IncomingDamages[allyName], health) / maxHealth;
                    Vector2 hbarpos;
                    HpBar.HpBarPositionDictionary.TryGetValue(allyName, out hbarpos);
                    if (hbarpos.X + 20 > HUDInfo.ScreenSizeX() || hbarpos.X - 20 < 0
                        || hbarpos.Y + 100 > HUDInfo.ScreenSizeY() || hbarpos.Y - 30 < 0)
                    {
                        continue;
                    }

                    var hpvarx = HpBar.SizeX;
                    var hpbary = HpBar.SizeY;
                    var position = hbarpos + new Vector2(hpvarx * hpperc, 0);
                    Drawing.DrawRect(
                        position, 
                        new Vector2(hpvarx * dmgperc, (float)(hpbary * 0.4)), 
                        (hpleft > 0) ? new Color(190, 150, 130, 100) : new Color(225, 70, 70, 200));
                    Drawing.DrawRect(position, new Vector2(hpvarx * dmgperc, (float)(hpbary * 0.4)), Color.Black, true);
                }
            }

            if (!MainMenu.GankDamageMenu.Item("enableGankDamageEnemies").GetValue<bool>())
            {
                return;
            }

            foreach (var enemy in
                enemies.Where(x => x != null && x.IsValid && IncomingDamages.ContainsKey(NameManager.Name(x))))
            {
                var health = enemy.Health;
                var maxHealth = enemy.MaximumHealth;
                var enemyName = NameManager.Name(enemy);
                var hpleft = Math.Max(health - IncomingDamages[enemyName], 0);
                var hpperc = hpleft / maxHealth;
                var dmgperc = Math.Min(IncomingDamages[enemyName], health) / maxHealth;
                Vector2 hbarpos;
                HpBar.HpBarPositionDictionary.TryGetValue(enemyName, out hbarpos);
                if (hbarpos.X + 20 > HUDInfo.ScreenSizeX() || hbarpos.X - 20 < 0
                    || hbarpos.Y + 100 > HUDInfo.ScreenSizeY() || hbarpos.Y - 30 < 0)
                {
                    continue;
                }

                var hpvarx = HpBar.SizeX;
                var hpbary = HpBar.SizeY;
                var position = hbarpos + new Vector2(hpvarx * hpperc, 0);
                Drawing.DrawRect(
                    position, 
                    new Vector2(hpvarx * dmgperc, (float)(hpbary * 0.4)), 
                    (hpleft > 0) ? new Color(150, 225, 150, 80) : new Color(70, 225, 150, 225));
                Drawing.DrawRect(position, new Vector2(hpvarx * dmgperc, (float)(hpbary * 0.4)), Color.Black, true);
            }
        }

        public static void UpdateDamage(Hero[] enemyHeroes, Hero[] allyHeroes)
        {
            if (!Utils.SleepCheck("GankDamage.Update"))
            {
                return;
            }

            if (!OnUpdateChecks.CanUpdate() || !MainMenu.GankDamageMenu.Item("enableGankDamage").GetValue<bool>())
            {
                return;
            }

            enemies = enemyHeroes;
            allies = allyHeroes;
            Utils.Sleep(1000, "GankDamage.Update");
            if (MainMenu.GankDamageMenu.Item("enableGankDamageEnemies").GetValue<bool>())
            {
                foreach (var enemyHero in enemyHeroes)
                {
                    var hero = enemyHero;
                    var heroName = NameManager.Name(hero);
                    if (!IncomingDamages.ContainsKey(heroName))
                    {
                        IncomingDamages.Add(heroName, 0);
                    }

                    var tempDmg = 0f;
                    foreach (var allyHero in
                        allyHeroes.Where(
                            x =>
                            AllyHeroes.AbilityDictionary.ContainsKey(NameManager.Name(x))
                            && AllyHeroes.ItemDictionary.ContainsKey(NameManager.Name(x)) && x.Distance2D(hero) < 1700))
                    {
                        var abilities = AllyHeroes.AbilityDictionary[NameManager.Name(allyHero)];
                        var items = AllyHeroes.ItemDictionary[NameManager.Name(allyHero)].Where(x => x.IsValid).ToList();
                        var list = new List<Ability>(abilities.Count + items.Count);
                        list.AddRange(abilities);
                        list.AddRange(items);
                        tempDmg +=
                            list.Where(x => x.CanBeCasted())
                                .Sum(ability => AbilityDamage.CalculateDamage(ability, allyHero, hero));
                    }

                    IncomingDamages[heroName] = tempDmg;
                }
            }

            if (!MainMenu.GankDamageMenu.Item("enableGankDamageAllies").GetValue<bool>())
            {
                return;
            }

            foreach (var allyHero in allyHeroes)
            {
                var hero = allyHero;
                var heroName = NameManager.Name(hero);
                if (!IncomingDamages.ContainsKey(heroName))
                {
                    IncomingDamages.Add(heroName, 0);
                }

                var tempDmg = 0f;
                foreach (var enemyHero in
                    enemyHeroes.Where(
                        x =>
                        EnemyHeroes.AbilityDictionary.ContainsKey(NameManager.Name(x))
                        && EnemyHeroes.ItemDictionary.ContainsKey(NameManager.Name(x)) && x.Distance2D(hero) < 1700))
                {
                    var abilities = EnemyHeroes.AbilityDictionary[NameManager.Name(enemyHero)];
                    var items = EnemyHeroes.ItemDictionary[NameManager.Name(enemyHero)].Where(x => x.IsValid).ToList();
                    var list = new List<Ability>(abilities.Count + items.Count);
                    list.AddRange(abilities);
                    list.AddRange(items);
                    tempDmg +=
                        list.Where(x => x.CanBeCasted())
                            .Sum(ability => AbilityDamage.CalculateDamage(ability, enemyHero, hero));
                }

                IncomingDamages[heroName] = tempDmg;
            }
        }

        #endregion
    }
}