namespace Ability.Casting
{
    using System;
    using System.Collections.Generic;

    using Ability.AbilityMenu;
    using Ability.ObjectManager;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Extensions;
    using Ensage.Common.Menu;
    using Ensage.Common.Objects;

    /// <summary>
    ///     The invoker.
    /// </summary>
    public static class Invoker
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes static members of the <see cref="Invoker" /> class.
        /// </summary>
        static Invoker()
        {
            OrbCombinationDictionary = new Dictionary<string, Tuple<Ability, Ability, Ability>>
                                           {
                                               { "invoker_sun_strike", new Tuple<Ability, Ability, Ability>(E, E, E) }, 
                                               { "invoker_chaos_meteor", new Tuple<Ability, Ability, Ability>(E, E, W) }, 
                                               { "invoker_alacrity", new Tuple<Ability, Ability, Ability>(W, W, E) }, 
                                               { "invoker_emp", new Tuple<Ability, Ability, Ability>(W, W, W) }, 
                                               { "invoker_tornado", new Tuple<Ability, Ability, Ability>(W, W, Q) }, 
                                               { "invoker_ghost_walk", new Tuple<Ability, Ability, Ability>(Q, Q, W) }, 
                                               { "invoker_cold_snap", new Tuple<Ability, Ability, Ability>(Q, Q, Q) }, 
                                               { "invoker_forge_spirit", new Tuple<Ability, Ability, Ability>(E, E, Q) }, 
                                               { "invoker_ice_wall", new Tuple<Ability, Ability, Ability>(Q, Q, E) }, 
                                               {
                                                   "invoker_deafening_blast", new Tuple<Ability, Ability, Ability>(Q, W, E)
                                               }
                                           };
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the e.
        /// </summary>
        public static Ability E
        {
            get
            {
                return MyAbilities.InvokerE;
            }
        }

        /// <summary>
        ///     Gets or sets the orb combination dictionary.
        /// </summary>
        public static Dictionary<string, Tuple<Ability, Ability, Ability>> OrbCombinationDictionary { get; set; }

        /// <summary>
        ///     Gets the q.
        /// </summary>
        public static Ability Q
        {
            get
            {
                return MyAbilities.InvokerQ;
            }
        }

        /// <summary>
        ///     Gets the w.
        /// </summary>
        public static Ability W
        {
            get
            {
                return MyAbilities.InvokerW;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The can invoke.
        /// </summary>
        /// <param name="ability">
        ///     The ability.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool CanInvoke(this Ability ability)
        {
            if (!ability.StoredName().StartsWith("invoker"))
            {
                return false;
            }

            if (!OrbCombinationDictionary[ability.StoredName()].Item1.CanBeCasted())
            {
                return false;
            }

            if (!OrbCombinationDictionary[ability.StoredName()].Item2.CanBeCasted())
            {
                return false;
            }

            if (!OrbCombinationDictionary[ability.StoredName()].Item3.CanBeCasted())
            {
                return false;
            }

            if (!MainMenu.ComboKeysMenu.Item("abilityKey1").GetValue<KeyBind>().Active)
            {
                return MainMenu.InvokerMenu.Item("Ability#.InvokerInvoke").GetValue<bool>()
                       && AbilityMain.Me.ClassID == ClassID.CDOTA_Unit_Hero_Invoker
                       && MyAbilities.InvokerInvoke.CanBeCasted()
                       && (ability.Cooldown <= 0.5
                           && ability.ManaCost + MyAbilities.InvokerInvoke.ManaCost < AbilityMain.Me.Mana);
            }

            if (MyAbilities.Spell5.Cooldown <= 0.5 && MyAbilities.Spell5.ManaCost < AbilityMain.Me.Mana)
            {
                return false;
            }

            return MainMenu.InvokerMenu.Item("Ability#.InvokerInvoke").GetValue<bool>()
                   && AbilityMain.Me.ClassID == ClassID.CDOTA_Unit_Hero_Invoker
                   && MyAbilities.InvokerInvoke.CanBeCasted()
                   && (ability.Cooldown <= 0.5
                       && ability.ManaCost + MyAbilities.InvokerInvoke.ManaCost < AbilityMain.Me.Mana);
        }

        /// <summary>
        ///     The invoke and cast.
        /// </summary>
        /// <param name="ability">
        ///     The ability.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool Invoke(this Ability ability)
        {
            if (!Utils.SleepCheck("Ability#.InvokerInvoke") || !ability.StoredName().StartsWith("invoker"))
            {
                return false;
            }

            OrbCombinationDictionary[ability.StoredName()].Item1.UseAbility();
            OrbCombinationDictionary[ability.StoredName()].Item2.UseAbility();
            OrbCombinationDictionary[ability.StoredName()].Item3.UseAbility();
            DelayAction.Add(Game.Ping, () => { MyAbilities.InvokerInvoke.UseAbility(); });
            return true;
        }

        #endregion
    }
}