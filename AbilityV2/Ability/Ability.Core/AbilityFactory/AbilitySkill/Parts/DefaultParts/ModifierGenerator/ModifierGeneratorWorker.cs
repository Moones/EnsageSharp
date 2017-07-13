namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.ModifierGenerator
{
    using System;

    using Ability.Core.AbilityFactory.AbilityModifier;

    public class ModifierGeneratorWorker : IDisposable
    {
        public ModifierGeneratorWorker(
            string modifierName, Action<IAbilityModifier> generateModifier, 
            bool affectsEnemies = false,
            bool affectsAllies = false,
            bool affectsSelf = false,
            bool affectsEveryone = false)
        {
            this.ModifierName = modifierName;
            this.GenerateModifier = generateModifier;
            this.AffectsEnemies = affectsEnemies;
            this.AffectsAllies = affectsAllies;
            this.AffectsSelf = affectsSelf;
            this.AffectsEveryone = affectsEveryone;
        }

        public string ModifierName { get; }

        public bool AffectsEnemies { get; }

        public bool AffectsAllies { get; }

        public bool AffectsSelf { get; }

        public bool AffectsEveryone { get; }

        public Action<IAbilityModifier> GenerateModifier { get; }

        public void Dispose()
        {
        }
    }
}
