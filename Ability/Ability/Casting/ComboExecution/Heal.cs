namespace Ability.Casting.ComboExecution
{
    using Ability.AutoAttack;

    using Ensage;
    using Ensage.Common.Extensions;

    internal class Heal
    {
        #region Public Methods and Operators

        public static bool Cast(Ability ability, Unit target, string name)
        {
            if (name == "item_tango" || name == "item_tango_single")
            {
                // if (!target.Equals(AbilityMain.Me))
                // {
                // return false;
                // }
                // var closestTree =
                // ObjectMgr.GetEntities<Entity>()
                // .Where(x => x.Name == "ent_dota_tree" && x.Distance2D(target) < 250)
                // .MinOrDefault(x => x.Distance2D(target));
                // Console.WriteLine(closestTree is Rune);
                // if (closestTree == null)
                // {
                // return false;
                // }
                return false;

                // ability.UseAbility(closestTree);
            }

            if (ability.IsAbilityBehavior(AbilityBehavior.NoTarget, name))
            {
                Game.ExecuteCommand("dota_player_units_auto_attack_after_spell 1");
                ManageAutoAttack.AutoAttackDisabled = true;
                SoulRing.Cast(ability);
                ability.UseAbility();
                return true;
            }

            if (name == "abaddon_death_coil" && target.Equals(AbilityMain.Me))
            {
                return false;
            }

            SoulRing.Cast(ability);
            Game.ExecuteCommand("dota_player_units_auto_attack_after_spell 1");
            ManageAutoAttack.AutoAttackDisabled = true;
            ability.UseAbility(target);
            return true;
        }

        #endregion
    }
}