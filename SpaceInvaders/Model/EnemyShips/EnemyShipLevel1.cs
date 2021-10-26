using SpaceInvaders.Model.Enum_Classes;
using SpaceInvaders.View.Sprites;

namespace SpaceInvaders.Model.EnemyShips
{
    /// <summary>
    ///     The most basic enemy ship class, EnemyShipLevel1.
    /// </summary>
    /// <seealso cref="SpaceInvaders.Model.GameObject" />
    public class EnemyShipLevel1 : GameObject
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

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PlayerShip" /> class.
        /// </summary>
        public EnemyShipLevel1()
        {
            Sprite = new EnemyShipSpriteLevel1();

            SetSpeed(SpeedXDirection, SpeedYDirection);

            IsDestroyed = false;

            this.ShipLevel = ShipLevel.LevelOne;
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
            if (this.ShipLevel == ShipLevel.LevelThree)
            {
                return new Bullet(this.ShipType, this);
            }

            return null;
        }

        #endregion
    }
}