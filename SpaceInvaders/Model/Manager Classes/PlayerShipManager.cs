using Windows.UI.Xaml.Controls;

namespace SpaceInvaders.Model.Manager_Classes
{
    /// <summary> manages the player's ship </summary>
    public class PlayerShipManager
    {
        #region Data members

        private const double PlayerShipBottomOffset = 30;
        private const int LeftBackgroundBoundary = 0;
        private const int PositionDivider = 2;

        #endregion

        #region Properties

        private double BackgroundWidth { get; }

        /// <summary>
        ///     Gets the background canvas.
        /// </summary>
        /// <value>
        ///     The background canvas.
        /// </value>
        public Canvas BackgroundCanvas { get; }

        /// <summary>
        ///     Gets or sets a value indicating whether the ship [just fired].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [just fired]; otherwise, <c>false</c>.
        /// </value>
        public bool JustFired { get; set; }

        /// <summary>
        ///     Gets a value indicating whether the player ship was destroyed.
        /// </summary>
        /// <value>
        ///     <c>true</c> if [player was destroyed]; otherwise, <c>false</c>.
        /// </value>
        public bool WasPlayerDestroyed => this.PlayerShip.IsDestroyed;

        /// <summary>
        ///     Gets or sets the player ship.
        /// </summary>
        /// <value>
        ///     The player ship.
        /// </value>
        public PlayerShip PlayerShip { get; set; }

        /// <summary>
        ///     Gets or sets the player ships lives.
        /// </summary>
        /// <value>
        ///     The player ships lives.
        /// </value>
        public int PlayerShipsLives => this.PlayerShip.NumberOfLivesRemaining;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PlayerShipManager" /> class.
        ///     precondition: none
        ///     post condition: player ship will be created added
        /// </summary>
        /// <param name="background">The background of the game.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public PlayerShipManager(Canvas background)
        {
            this.BackgroundCanvas = background;
            this.BackgroundWidth = this.BackgroundCanvas.Width;

            this.JustFired = false;

            this.createPlayerShip();
            this.placePlayerNearTheBottomCenter();
        }

        #endregion

        #region Methods

        private void createPlayerShip()
        {
            this.PlayerShip = new PlayerShip();
            this.BackgroundCanvas.Children.Add(this.PlayerShip.Sprite);
        }

        /// <summary>
        ///     Moves the player ship to the right.
        ///     precondition: none
        ///     post condition: player ship has moved right if it would still be within boundaries
        /// </summary>
        public void MovePlayerShipRight()
        {
            if (this.isPlayerShipNotNearRightBoundary())
            {
                this.PlayerShip.MoveRight();
            }
        }

        /// <summary>
        ///     Fires the player ship.
        /// </summary>
        /// <returns>the result of the playerShip firing the bullet.</returns>
        public Bullet FirePlayerShip()
        {
            return this.PlayerShip.Fire();
        }

        /// <summary>
        ///     Moves the player ship to the left.
        ///     precondition: none
        ///     post condition: player ship has moved left if it would still be within boundaries, ship doesn't move otherwise.
        /// </summary>
        public void MovePlayerShipLeft()
        {
            if (this.isPlayerShipNotNearLeftBoundary())
            {
                this.PlayerShip.MoveLeft();
            }
        }

        /// <summary>
        ///     Toggles the player ships gun to where the player ship can fire another bullet.
        ///     pre condition: none
        ///     post condition: player ship can fire an additional round again.
        /// </summary>
        public void TogglePlayerShipsGun()
        {
            this.PlayerShip.NumberShotsFired--;
        }

        /// <summary>
        ///     Handles the player getting hit.
        /// </summary>
        public void HandlePlayerGettingHit()
        {
            this.PlayerShip.NumberOfLivesRemaining--;

            if (this.PlayerShipsLives == 0)
            {
                this.PlayerShip.IsDestroyed = true;
            }
        }

        private bool isPlayerShipNotNearRightBoundary()
        {
            return this.BackgroundWidth > this.PlayerShip.X + this.PlayerShip.Width + this.PlayerShip.SpeedX;
        }

        private bool isPlayerShipNotNearLeftBoundary()
        {
            return LeftBackgroundBoundary < this.PlayerShip.X - this.PlayerShip.SpeedX;
        }

        private void placePlayerNearTheBottomCenter()
        {
            this.PlayerShip.X = this.BackgroundCanvas.Width / PositionDivider - this.PlayerShip.Width / PositionDivider;
            this.PlayerShip.Y = this.BackgroundCanvas.Height - this.PlayerShip.Height - PlayerShipBottomOffset;
        }

        #endregion
    }
}