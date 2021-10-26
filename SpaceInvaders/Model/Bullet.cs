using SpaceInvaders.Model.Enum_Classes;
using SpaceInvaders.View.Sprites;

namespace SpaceInvaders.Model
{
    /// <summary>
    ///     Bullet model class
    /// </summary>
    public class Bullet : GameObject
    {
        #region Data members

        private const int SpeedXDirection = 0;
        private const int SpeedYDirection = 20;

        #endregion

        #region Properties

        /// <summary>
        ///     The type of ship that fired the bullet.
        /// </summary>
        /// <value>
        ///     The type of the home ship.
        /// </value>
        public ShipType HomeShipType { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     creates a bullet object
        /// </summary>
        /// <param name="type">the type of ship that fired the bullet</param>
        /// <param name="ship">the ship that fired the bullet</param>
        public Bullet(ShipType type, GameObject ship)
        {
            Sprite = new BulletSprite();
            SetSpeed(SpeedXDirection, SpeedYDirection);
            X = this.xCenteredOnTheShip(ship.X, ship.Width);
            Y = ship.Y;
            this.HomeShipType = type;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     moves the ship according to its direction
        /// </summary>
        public void Move()
        {
            if (this.HomeShipType == ShipType.Player)
            {
                MoveUp();
            }
            else
            {
                MoveDown();
            }
        }

        private double xCenteredOnTheShip(double xValue, double shipsWidth)
        {
            return xValue + shipsWidth / 2;
        }

        #endregion
    }
}