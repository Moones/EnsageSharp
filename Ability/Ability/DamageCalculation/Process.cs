namespace Ability.DamageCalculation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ability.AbilityMenu;
    using Ability.AbilityMenu.Menus.NukesMenu;
    using Ability.Casting;
    using Ability.ObjectManager;
    using Ability.ObjectManager.Heroes;
    using Ability.OnUpdate;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.AbilityInfo;
    using Ensage.Common.Extensions;
    using Ensage.Common.Menu;

    internal class Process
    {
        #region Public Methods and Operators

        public static void OnUpdate(EventArgs args)
        {
            if (!OnUpdateChecks.CanUpdate())
            {
                return;
            }

            if (!Utils.SleepCheck("DamageUpdate") || !Utils.SleepCheck("GlobalCasting"))
            {
                return;
            }

            Utils.Sleep(100, "DamageUpdate");
            foreach (var hero in
                new List<Hero>(EnemyHeroes.Heroes).Where(x => x != null && x.IsValid && x.IsAlive && x.IsVisible))
            {
                var heroName = NameManager.Name(hero);
                var heroHandle = hero.Handle;
                if (Utils.SleepCheck("calculate"))
                {
                    var enumerable =
                        MyAbilities.OffensiveAbilities.Where(
                            ability =>
                            ability.Value.IsValid && ability.Key.Contains("nuke")
                            && Nukes.NukesMenuDictionary[NameManager.Name(ability.Value)].Item(
                                NameManager.Name(ability.Value) + "herotoggler")
                                   .GetValue<HeroToggler>()
                                   .IsEnabled(heroName)
                            && MainMenu.Menu.Item("nukesToggler")
                                   .GetValue<AbilityToggler>()
                                   .IsEnabled(NameManager.Name(ability.Value))
                            && Nukes.NukesMenuDictionary[NameManager.Name(ability.Value)].Item(
                                NameManager.Name(ability.Value) + "combo").GetValue<bool>())
                            .OrderBy(x => ComboOrder.GetDamageOrder(x.Value))
                            .ToList();
                    float[] intakenDamage = { 0 };
                    var minusMagicResistancePerc = 0f;
                    var tempList = new List<Ability>();
                    float[] outtakenDamage = { 0 };
                    var manaLeft = AbilityMain.Me.Mana;
                    foreach (var ability in
                        enumerable.Where(
                            ability =>
                            (ability.Value.CanBeCasted() || ability.Value.IsInAbilityPhase
                             || (AbilityMain.Me.ClassID == ClassID.CDOTA_Unit_Hero_Invoker
                                 && MyAbilities.InvokerInvoke.CanBeCasted() && ability.Value.Cooldown <= 0.5
                                 && ability.Value.ManaCost + MyAbilities.InvokerInvoke.ManaCost < manaLeft)))
                            .Select(data => data.Value))
                    {
                        var name = NameManager.Name(ability);
                        if (manaLeft < ability.ManaCost
                            || manaLeft
                            < Nukes.NukesMenuDictionary[name].Item(name + "minManaCheck").GetValue<Slider>().Value)
                        {
                            continue;
                        }

                        manaLeft -= ability.ManaCost;
                        if (DamageAmps.IsDamageAmp(ability))
                        {
                            minusMagicResistancePerc += DamageAmps.DamageAmpValue(ability);
                        }

                        float tempdmg;
                        if (ability.CanHit(hero, MyHeroInfo.Position))
                        {
                            tempdmg = AbilityDamage.CalculateDamage(
                                ability, 
                                AbilityMain.Me, 
                                hero, 
                                minusHealth: intakenDamage[0] + outtakenDamage[0], 
                                minusMagicResistancePerc: minusMagicResistancePerc);
                            intakenDamage[0] += tempdmg;
                            tempList.Add(ability);
                            if (!Dictionaries.InDamageDictionary.ContainsKey(heroHandle))
                            {
                                Dictionaries.InDamageDictionary.Add(heroHandle, intakenDamage[0]);
                            }
                            else
                            {
                                Dictionaries.InDamageDictionary[heroHandle] = intakenDamage[0];
                            }

                            if (intakenDamage[0] >= hero.Health)
                            {
                                MyAbilities.NukesCombo = tempList;
                                AbilityMain.DealtDamage = 0;
                                break;
                            }
                        }
                        else
                        {
                            tempdmg = AbilityDamage.CalculateDamage(
                                ability, 
                                AbilityMain.Me, 
                                hero, 
                                minusHealth: outtakenDamage[0] + intakenDamage[0], 
                                minusMagicResistancePerc: minusMagicResistancePerc);
                            outtakenDamage[0] += tempdmg;
                        }
                    }

                    if (!Dictionaries.OutDamageDictionary.ContainsKey(heroHandle))
                    {
                        Dictionaries.OutDamageDictionary.Add(heroHandle, outtakenDamage[0]);
                    }
                    else
                    {
                        Dictionaries.OutDamageDictionary[heroHandle] = outtakenDamage[0];
                    }

                    if (!Dictionaries.InDamageDictionary.ContainsKey(heroHandle))
                    {
                        Dictionaries.InDamageDictionary.Add(heroHandle, intakenDamage[0]);
                    }
                    else
                    {
                        Dictionaries.InDamageDictionary[heroHandle] = intakenDamage[0];
                    }
                }

                float dmg;
                if (!Dictionaries.InDamageDictionary.TryGetValue(heroHandle, out dmg))
                {
                    dmg = 0;
                }

                float outdmg;
                if (!Dictionaries.OutDamageDictionary.TryGetValue(heroHandle, out outdmg))
                {
                    outdmg = 0;
                }

                var hp = Math.Max(hero.Health - dmg, 0);
                var lhp = Math.Max(hp - outdmg, 0);
                float hitDmg;
                if (!Dictionaries.HitDamageDictionary.TryGetValue(heroHandle, out hitDmg))
                {
                    hitDmg = hero.DamageTaken(
                        MyDamage.BonusDamage + MyDamage.MinDamage, 
                        DamageType.Physical, 
                        AbilityMain.Me);
                    Dictionaries.HitDamageDictionary.Add(heroHandle, hitDmg);
                }
                else
                {
                    hitDmg = hero.DamageTaken(
                        MyDamage.BonusDamage + MyDamage.MinDamage, 
                        DamageType.Physical, 
                        AbilityMain.Me);
                    Dictionaries.HitDamageDictionary[heroHandle] = hitDmg;
                    Utils.Sleep(250, heroHandle + "updatehitdamage");
                }

                var currentHits = lhp / hitDmg;
                double hits;
                if (!Dictionaries.HitsDictionary.TryGetValue(heroHandle.ToString(), out hits))
                {
                    hits = Math.Ceiling(
                        (currentHits * MyHeroInfo.AttackRate() * hero.HealthRegeneration + lhp) / hitDmg);
                    Dictionaries.HitsDictionary.Add(heroHandle.ToString(), hits);
                }
                else if (Utils.SleepCheck(heroHandle + "updatehits"))
                {
                    hits = Math.Ceiling(
                        (currentHits * MyHeroInfo.AttackRate() * hero.HealthRegeneration + lhp) / hitDmg);
                    Dictionaries.HitsDictionary[heroHandle.ToString()] = hits;
                    Utils.Sleep(250, heroHandle + "updatehits");
                }
            }
        }

        #endregion
    }
}