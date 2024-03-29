﻿using SpaceInvaders.Model.Enum_Classes;
using SpaceInvaders.View.Sprites;

namespace SpaceInvaders.Model.EnemyShips
{
    /// <summary>
    ///     EnemyShip base class
    /// </summary>
    /// <seealso cref="SpaceInvaders.Model.GameObject" />
    public abstract class EnemyShip : GameObject
    {
        #region Data members

        private const int SpeedXDirection = 5;
        private const int SpeedYDirection = 10;

        /// <summary> The row the ship is on</summary>
        public Row ShipRow;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the type of the ship.
        /// </summary>
        /// <value>
        ///     The type of the ship.
        /// </value>
        public ShipType ShipType { get; set; }

        /// <summary>
        ///     Gets or sets the score value of the ship.
        /// </summary>
        /// <value>
        ///     The score value.
        /// </value>
        public ScoreValue ScoreValue { get; set; }

        /// <summary>
        ///     Gets or sets the level of the ship.
        /// </summary>
        /// <value>
        ///     The ship level.
        /// </value>
        public ShipLevel ShipLevel { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this ship can fire.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this ship can fire; otherwise, <c>false</c>.
        /// </value>
        public bool CanFire { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PlayerShip" /> class.
        /// </summary>
        protected EnemyShip()
        {
            this.SetSpeed(SpeedXDirection, SpeedYDirection);
            this.IsDestroyed = false;
            this.ShipRow = Row.FirstRow;
            this.ScoreValue = ScoreValue.Default;
            this.ShipType = ShipType.Enemy;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Fires a bullet from the ship.
        /// </summary>
        /// <returns>bullet to be shot towards the player if able to. null otherwise</returns>
        public Bullet Fire()
        {
            return this.CanFire ? new Bullet(this.ShipType, this) : null;
        }

        /// <summary>
        ///     Changes the appearance of the ship to another provided.
        /// </summary>
        /// <param name="sprite">The sprite to change to.</param>
        public void ChangeAppearance(BaseSprite sprite)
        {
            this.Sprite = sprite;
        }

        #endregion
    }
}