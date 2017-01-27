// <copyright file="PositionTracker.cs" company="EnsageSharp">
//    Copyright (c) 2017 Moones.
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see http://www.gnu.org/licenses/
// </copyright>
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.PositionTracker
{
    using Ability.Core.AbilityData;
    using Ability.Core.MenuManager.GetValue;
    using Ability.Utilities;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Menu;
    using Ensage.Common.Objects.UtilityObjects;

    using SharpDX;

    /// <summary>
    ///     The fog of war exploit.
    /// </summary>
    public class PositionTracker : IPositionTracker
    {
        #region Fields

        /// <summary>
        ///     The sleeper.
        /// </summary>
        private Sleeper sleeper = new Sleeper();

        #endregion

        #region Constructors and Destructors

        public PositionTracker(IAbilityUnit unit)
        {
            this.Unit = unit;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the draw on map.
        /// </summary>
        public GetValue<bool, bool> DrawOnMap { get; set; }

        /// <summary>
        ///     Gets or sets the draw on minimap.
        /// </summary>
        public GetValue<bool, bool> DrawOnMinimap { get; set; }

        /// <summary>
        ///     Gets or sets the menu.
        /// </summary>
        public Menu Menu { get; set; }

        /// <summary>
        ///     Gets or sets the unit.
        /// </summary>
        public IAbilityUnit Unit { get; set; }

        #endregion

        #region Public Methods and Operators

        public void Dispose()
        {
        }

        /// <summary>The dota base.</summary>
        /// <param name="dotaBase">The dota base.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public virtual bool DotaBase(Unit dotaBase)
        {
            return false;
        }

        /// <summary>The entity.</summary>
        /// <param name="entity">The entity.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public virtual bool Entity(Entity entity)
        {
            if (this.sleeper.Sleeping || this.Unit.Visibility.Visible
                || !entity.Owner.Handle.Equals(this.Unit.UnitHandle))
            {
                return false;
            }

            this.PositionUpdated(entity.NetworkPosition);
            return true;
        }

        public void Initialize()
        {
        }

        /// <summary>
        ///     The particle is from hero.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="args">
        ///     The args.
        /// </param>
        /// <param name="info">
        ///     The info.
        /// </param>
        public virtual void ParticleIsFromHero(
            Entity sender,
            ParticleEffectAddedEventArgs args,
            ParticleEffectMoreInfo info)
        {
            // Console.WriteLine(this.Unit.Name + " ParticleIsFromHero : " + info.StringContainingHeroName + " was detected " + sender.Name);
            var pos = args.ParticleEffect.GetControlPoint(0);
            this.PositionUpdated(pos);
        }

        /// <summary>The position updated.</summary>
        /// <param name="position">The position.</param>
        public void PositionUpdated(Vector3 position)
        {
            if (this.sleeper.Sleeping)
            {
                return;
            }

            var pos = position;
            this.Unit.Position.Update(pos);
            DrawingDraw draw = eventArgs =>
                {
                    if (this.DrawOnMap.Value)
                    {
                        this.Unit.Drawer.DrawIconOnMap(pos);
                    }
                };
            if (this.DrawOnMinimap.Value)
            {
                this.Unit.Drawer.EndSceneIcon.Position = pos.WorldToMinimap()
                                                         - new Vector2(
                                                             (float)(this.Unit.Drawer.EndSceneIcon.Width / 2.6));
                this.Unit.Drawer.EndSceneIcon.Add();
            }

            Drawing.OnDraw += draw;
            DelayAction.Add(
                2000,
                () =>
                    {
                        Drawing.OnDraw -= draw;
                        if (this.DrawOnMinimap.Value)
                        {
                            this.Unit.Drawer.EndSceneIcon.Remove();
                        }
                    });

            this.sleeper.Sleep(2000);
        }

        /// <summary>
        ///     The sender is hero.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="args">
        ///     The args.
        /// </param>
        /// <param name="info">
        ///     The info.
        /// </param>
        public virtual void SenderIsHero(Entity sender, ParticleEffectAddedEventArgs args, ParticleEffectMoreInfo info)
        {
            // Console.WriteLine(this.Unit.Name + " SenderIsHero : " + info.StringContainingHeroName + " was detected " + sender.Name);
            var pos = args.ParticleEffect.GetControlPoint(0);
            this.PositionUpdated(pos);
        }

        /// <summary>The thinker.</summary>
        /// <param name="thinker">The thinker.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public virtual bool Thinker(Entity thinker)
        {
            return false;
        }

        #endregion
    }
}