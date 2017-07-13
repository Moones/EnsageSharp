namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.ModifierGenerator
{
    using System.Collections.Generic;

    using Ability.Core.AbilityFactory.AbilityModifier;

    public class ModifierGenerator : IModifierGenerator
    {

        public ModifierGenerator(IAbilitySkill skill)
        {
            this.Skill = skill;
        }

        public void Dispose()
        {
            foreach (var modifierGeneratorWorker in this.Workers)
            {
                modifierGeneratorWorker.Dispose();
            }

            this.Workers.Clear();
        }

        public IAbilitySkill Skill { get; set; }

        public virtual void Initialize()
        {
            
        }

        public ICollection<ModifierGeneratorWorker> Workers { get; set; } = new List<ModifierGeneratorWorker>();

        public virtual bool TryGenerateModifier(IAbilityModifier modifier)
        {
            var isEnemy = modifier.AffectedUnit.Team != this.Skill.Owner.Team;
            foreach (var modifierGeneratorWorker in this.Workers)
            {
                if (((!isEnemy && modifierGeneratorWorker.AffectsAllies)
                     || (isEnemy && modifierGeneratorWorker.AffectsEnemies) || modifierGeneratorWorker.AffectsEveryone
                     || ((!modifier.AffectedUnit.UnitHandle.Equals(this.Skill.Owner.UnitHandle)
                          && modifierGeneratorWorker.AffectsSelf) || modifierGeneratorWorker.AffectsSelf))
                    && modifierGeneratorWorker.ModifierName.Equals(modifier.Name))
                {
                    modifier.SourceSkill = this.Skill;
                    modifierGeneratorWorker.GenerateModifier(modifier);
                    return true;
                }
            }

            return false;
        }
    }
}
