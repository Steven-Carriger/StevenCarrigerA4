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

            this.bulletManager = new BulletManager();
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
        /// <param name="background">The background.</param>
        public void FirePlayerShipsGun(Canvas background)
        {
            this.bulletManager.addBullet(this.playerShipManager.FirePlayerShip(), background);
        }

        private void makeEnemyShipsTakeAStep()
        {
            this.enemyShipManager.TakeAStep();
        }

        /// <summary>
        ///     This method executes the necessary methods for every game tick.
        /// </summary>
        /// <param name="background">The background.</param>
        public void GameTick(Canvas background)
        {
            this.checkForCollisions(background);
            this.makeEnemyShipsTakeAStep();
            this.fireEnemyShips(background);
            this.updateBullets(background);
        }

        private void fireEnemyShips(Canvas background)
        {
            this.bulletManager.addBullet(this.enemyShipManager.fireEnemyShips(), background);
        }

        private void updateBullets(Canvas background)
        {
            this.bulletManager.takeAStep();
            if (this.bulletManager.RemovePlayersOffScreenBullet(background))
            {
                this.playerShipManager.TogglePlayerShipsGun();
            }

            this.bulletManager.RemoveEnemiesOffScreenBullet(background);
        }

        private void checkForCollisions(Canvas background)
        {
            this.bulletManager.CheckForCollisionWithPlayerShip(this.playerShipManager.PlayerShip, background);

            if (this.isAnEnemyShipHit(background))
            {
                this.playerShipManager.TogglePlayerShipsGun();
            }
        }

        private bool isAnEnemyShipHit(Canvas background)
        {
            return this.bulletManager.CheckForCollisionsWithEnemyShips(this.enemyShipManager.EnemyShips, background);
        }

        #endregion
    }
}