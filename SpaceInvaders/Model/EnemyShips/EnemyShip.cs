using SpaceInvaders.Model.Enum_Classes;

namespace SpaceInvaders.Model.EnemyShips
{
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
        /// Gets or sets a value indicating whether this ship can fire.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this ship can fire; otherwise, <c>false</c>.
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
            this.ShipRow = Row.BottomRow;
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
            if (this.CanFire)
            {
                return new Bullet(this.ShipType, this);
            }

            return null;
        }

        #endregion
    }
}
