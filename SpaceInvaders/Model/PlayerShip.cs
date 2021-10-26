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

        #endregion

        #region Properties

        private ShipType ShipType { get; }

        /// <summary>
        ///     Gets or sets a value indicating whether the player ship has fired.
        /// </summary>
        /// <value>
        ///     <c>true</c> if the player ship has fired; otherwise, <c>false</c>.
        /// </value>
        public bool HasFired { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PlayerShip" /> class.
        /// </summary>
        public PlayerShip()
        {
            Sprite = new PlayerShipSprite();
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
            if (!this.HasFired)
            {
                this.HasFired = true;
                return new Bullet(this.ShipType, this);
            }

            return null;
        }

        #endregion
    }
}