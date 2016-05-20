namespace Techies.Utility
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Extensions;
    using Ensage.Common.Objects;

    /// <summary>
    ///     The damage.
    /// </summary>
    internal class Damage
    {
        #region Fields

        /// <summary>
        ///     The land mine damage dictionary.
        /// </summary>
        private Dictionary<uint, Dictionary<uint, float>> landMineDamageDictionary =
            new Dictionary<uint, Dictionary<uint, float>>();

        /// <summary>
        ///     The remote mine damage dictionary.
        /// </summary>
        private Dictionary<uint, Dictionary<uint, float>> remoteMineDamageDictionary =
            new Dictionary<uint, Dictionary<uint, float>>();

        /// <summary>
        ///     The suicide damage dictionary.
        /// </summary>
        private Dictionary<uint, float> suicideDamageDictionary = new Dictionary<uint, float>();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Damage" /> class.
        /// </summary>
        public Damage()
        {
            this.CurrentLandMineDamage = Variables.LandMinesAbility.GetAbilityData("damage");
            Events.OnUpdate += this.Game_OnUpdate;
            this.CurrentRemoteMineDamage = Variables.Techies.AghanimState()
                                               ? Variables.RemoteMinesAbility.GetAbilityData("damage_scepter")
                                               : Variables.RemoteMinesAbility.GetAbilityData("damage");
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the current land mine damage.
        /// </summary>
        public float CurrentLandMineDamage { get; set; }

        /// <summary>
        ///     Gets or sets the current remote mine damage.
        /// </summary>
        public float CurrentRemoteMineDamage { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The get land mine damage.
        /// </summary>
        /// <param name="level">
        ///     The level.
        /// </param>
        /// <param name="handle">
        ///     The handle.
        /// </param>
        /// <returns>
        ///     The <see cref="float" />.
        /// </returns>
        public float GetLandMineDamage(uint level, uint handle)
        {
            Dictionary<uint, float> dictionary;
            if (!this.landMineDamageDictionary.TryGetValue(level, out dictionary))
            {
                var damage = Variables.LandMinesAbility.GetAbilityData("damage", level);
                dictionary =
                    Heroes.GetByTeam(Variables.EnemyTeam)
                        .Where(x => x.IsValid && !x.IsIllusion)
                        .ToDictionary(
                            hero => hero.Handle, 
                            hero => hero.DamageTaken(damage, DamageType.Physical, Variables.Techies));
            }

            return !dictionary.ContainsKey(handle) ? 0 : dictionary[handle];
        }

        /// <summary>
        ///     The get remote mine damage.
        /// </summary>
        /// <param name="level">
        ///     The level.
        /// </param>
        /// <param name="handle">
        ///     The handle.
        /// </param>
        /// <param name="creep">
        ///     The creep.
        /// </param>
        /// <returns>
        ///     The <see cref="float" />.
        /// </returns>
        public float GetRemoteMineDamage(uint level, uint handle, Unit creep = null)
        {
            Dictionary<uint, float> dictionary;
            if (!this.remoteMineDamageDictionary.TryGetValue(level, out dictionary))
            {
                var damage = Variables.Techies.AghanimState()
                                 ? Variables.RemoteMinesAbility.GetAbilityData("damage_scepter", level)
                                 : Variables.RemoteMinesAbility.GetAbilityData("damage", level);
                dictionary =
                    Heroes.GetByTeam(Variables.EnemyTeam)
                        .Where(x => x.IsValid && !x.IsIllusion)
                        .ToDictionary(
                            hero => hero.Handle, 
                            hero => hero.DamageTaken(damage, DamageType.Magical, Variables.Techies));
            }

            if (!dictionary.ContainsKey(1) && creep != null)
            {
                handle = 1;
                dictionary.Add(
                    1, 
                    creep.DamageTaken(this.CurrentRemoteMineDamage, DamageType.Magical, Variables.Techies));
            }

            return !dictionary.ContainsKey(handle) ? 0 : dictionary[handle];
        }

        /// <summary>
        ///     The get suicide damage.
        /// </summary>
        /// <returns>
        ///     The <see cref="Dictionary" />.
        /// </returns>
        public Dictionary<uint, float> GetSuicideDamage()
        {
            return this.suicideDamageDictionary;
        }

        /// <summary>
        ///     The on close.
        /// </summary>
        public void OnClose()
        {
            Events.OnUpdate -= this.Game_OnUpdate;
        }

        /// <summary>
        ///     The on load.
        /// </summary>
        public void OnLoad()
        {
            Events.OnUpdate += this.Game_OnUpdate;
            this.remoteMineDamageDictionary = new Dictionary<uint, Dictionary<uint, float>>();
            this.suicideDamageDictionary = new Dictionary<uint, float>();
            this.landMineDamageDictionary = new Dictionary<uint, Dictionary<uint, float>>();
            this.CurrentLandMineDamage = Variables.LandMinesAbility.GetAbilityData("damage");
            this.CurrentRemoteMineDamage = Variables.Techies.AghanimState()
                                               ? Variables.RemoteMinesAbility.GetAbilityData("damage_scepter")
                                               : Variables.RemoteMinesAbility.GetAbilityData("damage");
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The game_ on update.
        /// </summary>
        /// <param name="args">
        ///     The args.
        /// </param>
        private void Game_OnUpdate(EventArgs args)
        {
            if (!Utils.SleepCheck("Techies.Damage.Update"))
            {
                return;
            }

            if (!Game.IsInGame || Game.IsPaused || Variables.Techies == null || !Variables.Techies.IsValid)
            {
                return;
            }

            this.CurrentRemoteMineDamage = Variables.Techies.AghanimState()
                                               ? Variables.RemoteMinesAbility.GetAbilityData("damage_scepter")
                                               : Variables.RemoteMinesAbility.GetAbilityData("damage");
            this.CurrentLandMineDamage = Variables.LandMinesAbility.GetAbilityData("damage");
            var heroes = Heroes.GetByTeam(Variables.EnemyTeam).Where(x => x.IsValid && !x.IsIllusion).ToList();
            this.suicideDamageDictionary = heroes.ToDictionary(
                hero => hero.Handle, 
                hero =>
                hero.DamageTaken(
                    Variables.SuicideAbility.GetAbilityData("damage"), 
                    DamageType.Physical, 
                    Variables.Techies));
            var level = Variables.LandMinesAbility.Level;
            if (!this.landMineDamageDictionary.ContainsKey(level))
            {
                var damage = heroes.ToDictionary(
                    hero => hero.Handle, 
                    hero => hero.DamageTaken(this.CurrentLandMineDamage, DamageType.Physical, Variables.Techies));
                this.landMineDamageDictionary.Add(level, damage);
                return;
            }

            this.landMineDamageDictionary[level] = heroes.ToDictionary(
                hero => hero.Handle, 
                hero => hero.DamageTaken(this.CurrentLandMineDamage, DamageType.Physical, Variables.Techies));
            Utils.Sleep(500, "Techies.Damage.Update");
            level = Variables.RemoteMinesAbility.Level;
            if (!this.remoteMineDamageDictionary.ContainsKey(level))
            {
                var damage = heroes.ToDictionary(
                    hero => hero.Handle, 
                    hero => hero.DamageTaken(this.CurrentRemoteMineDamage, DamageType.Magical, Variables.Techies));
                this.remoteMineDamageDictionary.Add(level, damage);
                return;
            }

            this.remoteMineDamageDictionary[level] = heroes.ToDictionary(
                hero => hero.Handle, 
                hero => hero.DamageTaken(this.CurrentRemoteMineDamage, DamageType.Magical, Variables.Techies));
        }

        #endregion
    }
}