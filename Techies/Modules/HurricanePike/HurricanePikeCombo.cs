namespace Techies.Modules.HurricanePike
{
    using System.Collections.Generic;
    using System.Linq;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Extensions;
    using Ensage.Common.Extensions.SharpDX;
    using Ensage.Common.Menu;
    using Ensage.Common.Objects.UtilityObjects;

    using global::Techies.Classes;
    using global::Techies.Utility;

    using SharpDX;

    /// <summary>
    ///     The hurricane pike combo.
    /// </summary>
    public class HurricanePikeCombo : ITechiesModule
    {
        #region Constants

        /// <summary>
        ///     The push range.
        /// </summary>
        private const float PushRange = 450;

        #endregion

        #region Fields

        /// <summary>
        ///     The combo sleeper.
        /// </summary>
        private readonly Sleeper comboSleeper;

        /// <summary>
        ///     The find sleeper.
        /// </summary>
        private readonly Sleeper findSleeper;

        /// <summary>
        ///     The blink.
        /// </summary>
        private Ability blinkDagger;

        /// <summary>
        ///     The hurricane pike.
        /// </summary>
        private Ability hurricanePike;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="HurricanePikeCombo" /> class.
        /// </summary>
        public HurricanePikeCombo()
        {
            this.findSleeper = new Sleeper();
            this.comboSleeper = new Sleeper();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The can be executed.
        /// </summary>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public bool CanBeExecuted()
        {
            if (!this.findSleeper.Sleeping)
            {
                var pike = this.hurricanePike == null || !this.hurricanePike.IsValid
                               ? Variables.Techies.FindItem("item_hurricane_pike")
                               : this.hurricanePike;
                var blink = this.blinkDagger == null || !this.blinkDagger.IsValid
                                ? Variables.Techies.FindItem("item_blink")
                                : this.blinkDagger;
                this.hurricanePike = pike;
                this.blinkDagger = blink;
                this.findSleeper.Sleep(500);
            }

            return !this.comboSleeper.Sleeping
                   && Variables.Menu.HurricanePikeMenu.Item("Techies.HurricanePikeEnable").GetValue<bool>()
                   && Variables.Menu.HurricanePikeMenu.Item("Techies.HurricanePikeKey").GetValue<KeyBind>().Active
                   && this.hurricanePike != null && this.blinkDagger != null && this.hurricanePike.IsValid
                   && this.blinkDagger.IsValid && this.hurricanePike.CanBeCasted() && this.blinkDagger.CanBeCasted();
        }

        /// <summary>
        ///     The can draw.
        /// </summary>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public bool CanDraw()
        {
            return false;
        }

        /// <summary>
        ///     The draw.
        /// </summary>
        public void Draw()
        {
        }

        /// <summary>
        ///     The execute.
        /// </summary>
        /// <param name="hero">
        ///     The hero.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public bool Execute(Hero hero = null)
        {
            if (hero == null)
            {
                return false;
            }

            var me = Variables.Techies;
            var heroPosition = hero.PredictedPosition(me.GetTurnTime(hero) * 2);
            if (me.Distance2D(heroPosition) > this.blinkDagger.GetCastRange() + 150)
            {
                return false;
            }

            if (Prediction.StraightTime(hero) / 1000
                < Variables.Menu.HurricanePikeMenu.Item("Techies.HurricanePikeStraightTime").GetValue<Slider>().Value)
            {
                return false;
            }

            if (hero.NetworkActivity == NetworkActivity.Move
                && Variables.Menu.HurricanePikeMenu.Item("Techies.HurricanePikeNoMove").GetValue<bool>())
            {
                return false;
            }

            var stack = GetNearestStackToPush(hero, heroPosition);
            if (stack == null)
            {
                return false;
            }

            stack.AutoDetonate = true;
            stack.MinEnemiesKill = 1;
            var blinkPosition = stack.Position.Extend(heroPosition, stack.Position.Distance2D(hero) + 300);
            this.blinkDagger.UseAbility(blinkPosition);
            var sleeper = new Sleeper();
            Events.OnUpdateDelegate update = args =>
                {
                    if (sleeper.Sleeping || !this.hurricanePike.CanBeCasted())
                    {
                        return;
                    }

                    this.hurricanePike.UseAbility(hero);
                    sleeper.Sleep(150);
                };
            DelayAction.Add((float)(me.GetTurnTime(blinkPosition) * 1000), () => { Events.OnUpdate += update; });
            DelayAction.Add(
                (float)((me.GetTurnTime(blinkPosition) * 1000) + 50 + Game.Ping), 
                () => { Events.OnUpdate -= update; });
            this.comboSleeper.Sleep(1000);
            return true;
        }

        /// <summary>
        ///     The is hero loop.
        /// </summary>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public bool IsHeroLoop()
        {
            return true;
        }

        /// <summary>
        ///     The on close.
        /// </summary>
        public void OnClose()
        {
        }

        /// <summary>
        ///     The on load.
        /// </summary>
        public void OnLoad()
        {
        }

        /// <summary>
        ///     The on wnd proc.
        /// </summary>
        /// <param name="args">
        ///     The args.
        /// </param>
        public void OnWndProc(WndEventArgs args)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The get nearest stack to kill.
        /// </summary>
        /// <param name="hero">
        ///     The hero.
        /// </param>
        /// <param name="predict">
        ///     The predict.
        /// </param>
        /// <returns>
        ///     The <see cref="Stack" />.
        /// </returns>
        private static Stack GetNearestStackToPush(Unit hero, Vector3 predict)
        {
            var detonatableMines = new List<RemoteMine>();
            if (Variables.Stacks == null || !Variables.Stacks.Any())
            {
                return null;
            }

            var heroPosition = predict;

            var tempDamage = 0f;
            var nearestStack =
                Variables.Stacks.Where(
                    x =>
                    VectorExtensions.Distance(x.Position, heroPosition) <= PushRange + 400
                    && VectorExtensions.Distance(x.Position, heroPosition) > PushRange - 400)
                    .MinOrDefault(x => VectorExtensions.Distance(x.Position, heroPosition));
            if (nearestStack == null)
            {
                return null;
            }

            foreach (var landMine in
                nearestStack.LandMines.Where(
                    x =>
                    x.Distance(heroPosition) <= x.Radius + PushRange
                    && x.Distance(hero.Position) <= x.Radius + PushRange))
            {
                if (tempDamage >= hero.Health)
                {
                    return nearestStack;
                }

                tempDamage += Variables.Damage.GetLandMineDamage(landMine.Level, hero.ClassID);
            }

            foreach (var remoteMine in
                nearestStack.RemoteMines.Where(
                    x =>
                    x.Distance(heroPosition) <= x.Radius + PushRange
                    && x.Distance(hero.Position) <= x.Radius + PushRange))
            {
                if (tempDamage >= hero.Health)
                {
                    return nearestStack;
                }

                detonatableMines.Add(remoteMine);
                tempDamage += Variables.Damage.GetRemoteMineDamage(remoteMine.Level, hero.ClassID, hero);
            }

            return !(tempDamage >= hero.Health) ? null : nearestStack;
        }

        #endregion
    }
}