namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.HeroParts.Abaddon.AphoticShield
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    using Ability.Core.AbilityFactory.AbilityModifier;
    using Ability.Core.AbilityFactory.AbilityModifier.Parts.Default.ModifierEffectApplier;
    using Ability.Core.AbilityFactory.AbilityModifier.Parts.Default.ModifierEffectApplier.Workers.DamageManipulation;
    using Ability.Core.AbilityFactory.AbilitySkill.Metadata;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.ModifierGenerator;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillComposer;
    using Ability.Core.AbilityFactory.AbilityTalent;
    using Ability.Core.AbilityFactory.Utilities;

    using Ensage;
    using Ensage.Common.Extensions;

    [Export(typeof(IAbilitySkillComposer))]
    [AbilitySkillMetadata((uint)AbilityId.abaddon_aphotic_shield)]
    internal class AphoticShieldSkillComposer : DefaultSkillComposer
    {
        internal AphoticShieldSkillComposer()
        {
            this.AssignPart<IModifierGenerator>(
                skill =>
                    {
                        Func<IAbilityModifier, double> getValue =
                            abilityModifier => abilityModifier.SourceSkill.SourceAbility.GetAbilityData("damage_absorb");

                        skill.Owner.SkillBook.TalentAdded.Subscribe(
                            new DataObserver<IAbilityTalent>(
                                talent =>
                                    {
                                        if (talent.SourceAbility.Id
                                            == AbilityId.special_bonus_unique_abaddon)
                                        {
                                            talent.TalentLeveledNotifier.Subscribe(
                                                () =>
                                                    {
                                                        getValue =
                                                            modifier =>
                                                                modifier.SourceSkill.SourceAbility.GetAbilityData(
                                                                    "damage_absorb")
                                                                + talent.SourceAbility.GetAbilityData("value");
                                                    });
                                        }
                                    }));

                        return new ModifierGenerator(skill)
                                   {
                                       Workers =
                                           new List<ModifierGeneratorWorker>
                                               {
                                                   new ModifierGeneratorWorker(
                                                       "modifier_abaddon_aphotic_shield",
                                                       modifier =>
                                                           {
                                                               modifier.AssignModifierEffectApplier(
                                                                   new ModifierEffectApplier(modifier)
                                                                       {
                                                                           Workers =
                                                                               new List<IEffectApplierWorker>
                                                                                   {
                                                                                       new DamageNegationEffectApplierWorker
                                                                                           (modifier, true, getValue)
                                                                                   }
                                                                       });
                                                           },
                                                       false,
                                                       true,
                                                       true)
                                               }
                                   };
                    });

        }
    }
}
