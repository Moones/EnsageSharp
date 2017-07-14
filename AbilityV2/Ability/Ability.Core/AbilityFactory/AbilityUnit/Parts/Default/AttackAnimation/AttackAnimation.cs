using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.AttackAnimation
{
    using Ability.Core.AbilityFactory.AbilitySkill;
    using Ability.Core.AbilityFactory.Utilities;

    using Ensage;
    using Ensage.Common.Extensions;

    public class AttackAnimation : IAttackAnimation
    {
        public AttackAnimation(IAbilityUnit unit)
        {
            this.Unit = unit;
        }

        public void Dispose()
        {
        }

        public IAbilityUnit Unit { get; set; }

        private float baseAttackPoint;

        private bool overpowerModifier;

        public void Initialize()
        {
            this.baseAttackPoint =
                Game.FindKeyValues(
                    this.Unit.Name + "/AttackAnimationPoint",
                    this.Unit.IsHero ? KeyValueSource.Hero : KeyValueSource.Unit).FloatValue;

            if (this.Unit.SkillBook.Spells.Any(x => x.Value.SourceAbility.Id == AbilityId.ursa_overpower))
            {
                this.GotOverpower();
            }
            else
            {
                this.Unit.SkillBook.SkillAdded.Subscribe(
                    new DataObserver<IAbilitySkill>(
                        skill =>
                            {
                                if (skill.SourceAbility.Id == AbilityId.ursa_overpower)
                                {
                                    this.GotOverpower();
                                }
                            }));
            }
        }

        private void GotOverpower()
        {
            this.overpowerModifier = this.Unit.SourceUnit.HasModifier("modifier_ursa_overpower");
            this.Unit.Modifiers.ModifierAdded.Subscribe(
                new DataObserver<Modifier>(
                    modifier =>
                    {
                        if (modifier.Name == "modifier_ursa_overpower")
                        {
                            this.overpowerModifier = true;
                        }
                    }));

            this.Unit.Modifiers.ModifierRemoved.Subscribe(
                new DataObserver<Modifier>(
                    modifier =>
                    {
                        if (modifier.Name == "modifier_ursa_overpower")
                        {
                            this.overpowerModifier = false;
                        }
                    }));
        }



        public float GetAttackSpeed()
        {
            if (this.overpowerModifier)
            {
                return 600;
            }

            var attackSpeed = Math.Max(20, this.Unit.SourceUnit.AttackSpeedValue);
            return Math.Min(attackSpeed, 600);
        }

        public float GetAttackPoint()
        {
            return (float)(this.baseAttackPoint / (1.0 + ((this.GetAttackSpeed() - 100.0) / 100.0)));
        }

        public float GetAttackRate()
        {
            return 1f / this.Unit.SourceUnit.AttacksPerSecond;
        }
    }
}
