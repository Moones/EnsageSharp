using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.LoneDruid.AttackRange
{
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.AttackRange;
    using Ability.Core.AbilityFactory.Utilities;

    using Ensage;
    using Ensage.Common.Extensions;

    public class LoneDruidAttackRange : UnitAttackRange
    {
        public LoneDruidAttackRange(IAbilityUnit unit)
            : base(unit)
        {
        }

        public bool TrueForm { get; set; }

        public override void Initialize()
        {
            base.Initialize();

            this.TrueForm =
                this.Unit.SourceUnit.HasModifiers(
                    new[] { "modifier_lone_druid_true_form", "modifier_lone_druid_true_form_transform" },
                    false);

            if (this.TrueForm)
            {
                Console.WriteLine("trueform " + this.TrueForm);
                this.Value -= 423;
            }

            this.Unit.Modifiers.ModifierAdded.Subscribe(
                new DataObserver<Modifier>(
                    modifier =>
                        {
                            if (this.TrueForm && modifier.Name == "modifier_lone_druid_druid_form_transform")
                            {
                                this.Value += 423;
                                this.TrueForm = false;
                                Console.WriteLine("trueform " + this.TrueForm);
                            }
                        }));

            this.Unit.Modifiers.ModifierRemoved.Subscribe(
                new DataObserver<Modifier>(
                    modifier =>
                        {
                            if (!this.TrueForm && modifier.Name == "modifier_lone_druid_true_form_transform")
                            {
                                this.Value -= 423;
                                this.TrueForm = true;
                                Console.WriteLine("trueform " + this.TrueForm);
                            }
                        }));
        }
    }
}
