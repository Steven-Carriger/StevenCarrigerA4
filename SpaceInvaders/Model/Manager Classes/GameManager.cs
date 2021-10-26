using System;
using Windows.UI.Xaml.Controls;

namespace SpaceInvaders.Model.Manager_Classes
{
    /// <summary> Manages the entire game. </summary>
    public class GameManager
    {
        #region Data members

        private readonly EnemyShipManager enemyShipManager;
        private readonly PlayerShipManager playerShipManager;
        private readonly BulletManager bulletManager;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the player's score.
        /// </summary>
        /// <value>
        ///     The player's score.
        /// </value>
        public int PlayerScore => this.enemyShipManager.ScoreValueOfDestroyedShips;

        /// <summary>
        ///     Gets a value indicating whether [did player win].
        /// </summary>
        /// <value>
        ///     <c>true</c> if the [player did win]; otherwise, <c>false</c>.
        /// </value>
        public bool DidPlayerWin => this.enemyShipManager.AreAllShipsDestroyed;

        /// <summary>
        ///     Gets a value indicating whether the [player was destroyed].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [player did lose]; otherwise, <c>false</c>.
        /// </value>
        public bool DidPlayerLose => this.playerShipManager.WasPlayerHit;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="GameManager" /> class.
        ///     Precondition: backgroundHeight > 0 AND backgroundWidth > 0
        /// </summary>
        /// <param name="background">the canvas the game will be using</param>
        public GameManager(Canvas background)
        {
            if (background == null)
            {
                throw new ArgumentNullException(nameof(background));
            }

            this.bulletManager = new BulletManager(background);
            this.enemyShipManager = new EnemyShipManager(background);
            this.playerShipManager = new PlayerShipManager(background);
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Moves the player ship to the left.
        ///     Precondition: none
        ///     Post condition: The player ship has moved left if it is within boundaries.
        /// </summary>
        public void MovePlayerShipLeft()
        {
            this.playerShipManager.MovePlayerShipLeft();
        }

        /// <summary>
        ///     Moves the player ship to the right.
        ///     Precondition: none
        ///     Post condition: The player ship has moved right if it is within boundaries.
        /// </summary>
        public void MovePlayerShipRight()
        {
            this.playerShipManager.MovePlayerShipRight();
        }

        /// <summary>
        ///     Fires the player ships gun.
        /// </summary>
        public void FirePlayerShipsGun()
        {
            this.bulletManager.addBullet(this.playerShipManager.FirePlayerShip());
        }

        private void makeEnemyShipsTakeAStep()
        {
            this.enemyShipManager.TakeAStep();
        }

        /// <summary>
        ///     This method executes the necessary methods for every game tick.
        /// </summary>
        public void GameTick()
        {
            this.checkForCollisions();
            this.makeEnemyShipsTakeAStep();
            this.fireEnemyShips();
            this.updateBullets();
        }

        private void fireEnemyShips()
        {
            this.bulletManager.addBullet(this.enemyShipManager.fireEnemyShips());
        }

        private void updateBullets()
        {
            this.bulletManager.TakeAStep();
            if (this.bulletManager.RemovePlayersOffScreenBullet())
            {
                this.playerShipManager.TogglePlayerShipsGun();
            }

            this.bulletManager.RemoveEnemiesOffScreenBullet();
        }

        private void checkForCollisions()
        {
            this.bulletManager.CheckForCollisionWithPlayerShip(this.playerShipManager.PlayerShip);

            if (this.isAnEnemyShipHit())
            {
                this.playerShipManager.TogglePlayerShipsGun();
            }
        }

        private bool isAnEnemyShipHit()
        {
            return this.bulletManager.CheckForCollisionsWithEnemyShips(this.enemyShipManager.EnemyShips);
        }

        #endregion
    }
}