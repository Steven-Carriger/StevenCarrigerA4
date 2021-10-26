using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SpaceInvaders.Model.Manager_Classes
{
    /// <summary> Manages the entire game. </summary>
    public class GameManager
    {
        #region Data members

        private readonly EnemyShipManager enemyShipManager;
        private readonly PlayerShipManager playerShipManager;

        public Canvas BackgroundCanvas { get; }

        private readonly BulletManager bulletManager;

        public delegate void UpdateScoreHandler(int score);

        public event UpdateScoreHandler ScoreUpdated;

        public delegate void GameEndedHandler();

        public event GameEndedHandler GameEnded;
        #endregion

        #region Properties

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

        private DispatcherTimer timer;
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
            this.BackgroundCanvas = background;

            this.createTimer();

            this.enemyShipManager.ScoreUpdated += new EnemyShipManager.ScoreUpdatedHandler(this.onUpdateScoreEvent);
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

        private void createTimer()
        {
            this.timer = new DispatcherTimer();
            this.timer.Tick += this.timerTick;
            this.timer.Interval = new TimeSpan(0, 0, 0, 0, 55);
            this.timer.Start();
        }

        private void timerTick(object sender, object e)
        {
            this.checkForCollisions();
            this.makeEnemyShipsTakeAStep();
            this.fireEnemyShips();
            this.updateBullets();

            if (this.isGameOver())
            {
                this.timer.Stop();
                this.onGamedEndedEvent();
            }
        }

        private bool isGameOver()
        {
            return this.DidPlayerWin || this.DidPlayerLose;
        }

        private void onUpdateScoreEvent(int score)
        {
            if (this.ScoreUpdated != null)
            {
                this.ScoreUpdated(score);
            }
        }

        private void onGamedEndedEvent()
        {
            if (this.GameEnded != null)
            {
                this.GameEnded();
            }
        }
        #endregion
    }
}