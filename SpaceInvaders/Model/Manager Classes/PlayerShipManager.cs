﻿using Windows.UI.Xaml.Controls;

namespace SpaceInvaders.Model.Manager_Classes
{
    /// <summary> manages the player's ship </summary>
    public class PlayerShipManager
    {
        #region Data members

        private const double PlayerShipBottomOffset = 30;
        private const int LeftBackgroundBoundary = 0;
        private readonly double backgroundWidth;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets a value indicating whether the player ship was hit.
        /// </summary>
        /// <value>
        ///     <c>true</c> if [was player hit]; otherwise, <c>false</c>.
        /// </value>
        public bool WasPlayerHit => this.PlayerShip.IsDestroyed;

        /// <summary>
        ///     Gets or sets the player ship.
        /// </summary>
        /// <value>
        ///     The player ship.
        /// </value>
        public PlayerShip PlayerShip { get; set; }

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
            this.backgroundWidth = background.Width;
            this.createPlayerShip(background);
            this.placePlayerNearTheBottomCenter(background);
        }

        #endregion

        #region Methods

        private void createPlayerShip(Canvas background)
        {
            this.PlayerShip = new PlayerShip();
            background.Children.Add(this.PlayerShip.Sprite);
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
        ///     Toggles the player ships gun.
        ///     pre condition: none
        ///     post condition: if the property was true it will be false, if it were false it would be toggled to true.
        /// </summary>
        public void TogglePlayerShipsGun()
        {
            if (this.PlayerShip.HasFired)
            {
                this.PlayerShip.HasFired = false;
            }
            else
            {
                this.PlayerShip.HasFired = true;
            }
        }

        private bool isPlayerShipNotNearRightBoundary()
        {
            return this.backgroundWidth > this.PlayerShip.X + this.PlayerShip.Width + this.PlayerShip.SpeedX;
        }

        private bool isPlayerShipNotNearLeftBoundary()
        {
            return LeftBackgroundBoundary < this.PlayerShip.X - this.PlayerShip.SpeedX;
        }

        private void placePlayerNearTheBottomCenter(Canvas background)
        {
            this.PlayerShip.X = background.Width / 2 - this.PlayerShip.Width / 2.0;
            this.PlayerShip.Y = background.Height - this.PlayerShip.Height - PlayerShipBottomOffset;
        }

        #endregion
    }
}