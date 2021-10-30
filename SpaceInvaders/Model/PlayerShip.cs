using SpaceInvaders.Model.Enum_Classes;
using SpaceInvaders.View.Sprites;

namespace SpaceInvaders.Model
{
    /// <summary>
    ///     Manages the player ship and its behavior.
    /// </summary>
    /// <seealso cref="SpaceInvaders.Model.GameObject" />
    public class PlayerShip : GameObject
    {
        #region Data members

        private const int SpeedXDirection = 10;
        private const int SpeedYDirection = 0;

        private const int MaxNumberOfPlayerShots = 3;
        #endregion

        #region Properties

        private ShipType ShipType { get; }

        /// <summary>
        /// Gets or sets the number of shots fired from this ship
        /// </summary>
        /// <value>
        /// The number of shots fired. 
        /// </value>
        public int numberShotsFired { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the player ship has fired.
        /// </summary>
        /// <value>
        ///     <c>true</c> if the player ship can fire; otherwise, <c>false</c>.
        /// </value>
        public bool CanFire => numberShotsFired < MaxNumberOfPlayerShots;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PlayerShip" /> class.
        /// </summary>
        public PlayerShip()
        {
            this.Sprite = new PlayerShipSprite();
            this.numberShotsFired = 0;
            SetSpeed(SpeedXDirection, SpeedYDirection);
            this.ShipType = ShipType.Player;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Fires this instance.
        /// </summary>
        /// <returns>the bullet that was fired, null if the bullet could not be fired.</returns>
        public Bullet Fire()
        {
            if (this.CanFire)
            {
                this.numberShotsFired++;
                return new Bullet(this.ShipType, this);
            }

            return null;
        }

        #endregion
    }
}