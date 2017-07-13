// <copyright file="DefaultPriorityDatabase.cs" company="EnsageSharp">
//    Copyright (c) 2016 Moones.
// </copyright>
namespace Ability.Core.AbilityFactory.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Security.Permissions;
    using System.Text;

    using Ability.Core.AbilityFactory.AbilitySkill.Data;
    using Ability.Core.Properties;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    ///     The default priority database.
    /// </summary>
    [Export(typeof(IAbilityDatabase))]
    internal class AbilityDatabase : IAbilityDatabase
    {
        #region Fields

        /// <summary>
        ///     The cached priority data.
        /// </summary>
        private readonly Dictionary<string, PriorityData> cachedPriorityData =
            new Dictionary<string, PriorityData>();

        /// <summary>
        ///     The default priority data.
        /// </summary>
        private readonly PriorityData defaultPriorityData;

        /// <summary>
        ///     The priority data.
        /// </summary>
        private readonly Dictionary<string, PriorityData> priorityData =
            new Dictionary<string, PriorityData>();

        /// <summary>
        /// The skill data dictionary.
        /// </summary>
        private readonly Dictionary<string, SkillJson> skillDataDictionary = new Dictionary<string, SkillJson>();

        #endregion

        #region Constructors and Destructors

        [PermissionSet(SecurityAction.Assert, Unrestricted = true)]
        internal AbilityDatabase()
        {
            //var asd = JObject.Parse(Encoding.Default.GetString(Resources.PriorityData).Substring(3));
            //foreach (var data in asd)
            //{
            //    var priority = JsonConvert.DeserializeObject<PriorityData>(data.Value.ToString());
            //    this.priorityData.Add(data.Key, priority);
            //    if (data.Key == "default")
            //    {
            //        this.defaultPriorityData = priority;
            //    }
            //}


            //Console.WriteLine(Encoding.ASCII.GetString(Resources.SkillData));
            var skillDataJson = JObject.Parse(Encoding.Default.GetString(Resources.SkillData).Substring(3));
            foreach (var data in skillDataJson)
            {
                var skillData = JsonConvert.DeserializeObject<SkillJson>(data.Value.ToString());
                this.skillDataDictionary.Add(data.Key, skillData);
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The get data.
        /// </summary>
        /// <param name="heroName">
        ///     The hero name.
        /// </param>
        /// <returns>
        ///     The <see cref="Dictionary" />.
        /// </returns>
        public PriorityData GetData(string heroName)
        {
            PriorityData data;
            if (this.cachedPriorityData.TryGetValue(heroName, out data) || !this.priorityData.ContainsKey(heroName))
            {
                return data;
            }

            data = this.priorityData[heroName];
            this.cachedPriorityData.Add(heroName, data);
            return data;
        }

        /// <summary>
        ///     The get priority.
        /// </summary>
        /// <param name="skillName">
        ///     The skill name.
        /// </param>
        /// <returns>
        ///     The <see cref="uint" />.
        /// </returns>
        public uint GetCastPriority(string skillName)
        {
            return this.defaultPriorityData.CastPriority.ContainsKey(skillName)
                       ? this.defaultPriorityData.CastPriority[skillName]
                       : 4;
        }

        /// <summary>
        /// The get damage dealt priority.
        /// </summary>
        /// <param name="skillName">
        /// The skill name.
        /// </param>
        /// <returns>
        /// The <see cref="uint"/>.
        /// </returns>
        public uint GetDamageDealtPriority(string skillName)
        {
            return this.defaultPriorityData.DamageDealtPriority.ContainsKey(skillName)
                       ? this.defaultPriorityData.DamageDealtPriority[skillName]
                       : 4;
        }

        /// <summary>
        ///     The get priority.
        /// </summary>
        /// <param name="skillName">
        ///     The skill name.
        /// </param>
        /// <param name="heroName">
        ///     The hero name.
        /// </param>
        /// <returns>
        ///     The <see cref="uint" />.
        /// </returns>
        public uint GetCastPriority(string skillName, string heroName)
        {
            var data = this.GetData(heroName);
            return data != null && data.CastPriority.ContainsKey(skillName)
                       ? data.CastPriority[skillName]
                       : this.GetCastPriority(skillName);
        }

        /// <summary>
        /// The get skill data.
        /// </summary>
        /// <param name="skillName">
        /// The skill name.
        /// </param>
        /// <returns>
        /// The <see cref="SkillJson"/>.
        /// </returns>
        public SkillJson GetSkillData(string skillName)
        {
            return this.skillDataDictionary.ContainsKey(skillName) ? this.skillDataDictionary[skillName] : null;
        }

        #endregion
    }
}