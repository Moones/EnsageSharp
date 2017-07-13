namespace Ability.Core.AbilityFactory.AbilitySkill.Data
{
    using System.Collections.Generic;

    /// <summary>
    /// The priority data.
    /// </summary>
    public class PriorityData
    {
        /// <summary>
        /// Gets the cast priority.
        /// </summary>
        public Dictionary<string, uint> CastPriority { get; set; }

        /// <summary>
        /// Gets the damage dealt priority.
        /// </summary>
        public Dictionary<string, uint> DamageDealtPriority { get; set; }
    }
}
