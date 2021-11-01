﻿using System;
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
        private readonly BulletManager bulletManager;

        #endregion

        #region Properties
        /// <summary>
        /// a function that handles when the score is updated
        /// </summary>
        /// <param name="score">The score.</param>
        public delegate void UpdateScoreHandler(int score);

        /// <summary>
        /// Occurs when [score updated].
        /// </summary>
        public event UpdateScoreHandler ScoreUpdated;

        /// <summary>
        /// a function that Handles when the game is over
        /// </summary>
        public delegate void GameEndedHandler();

        /// <summary>
        /// Occurs when [game ended].
        /// </summary>
        public event GameEndedHandler GameEnded;

        /// <summary>
        /// a function that handles when the player is hit
        /// </summary>
        /// <param name="score">The lives remaining for the player.</param>
        public delegate void PlayerWasHitHandler(int lives);

        /// <summary>
        /// Occurs when [Player Was Hit].
        /// </summary>
        public event PlayerWasHitHandler PlayerHit;

        /// <summary>
        /// Gets the background canvas.
        /// </summary>
        /// <value>
        /// The background canvas.
        /// </value>
        public Canvas BackgroundCanvas { get; }

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
        public bool DidPlayerLose => this.playerShipManager.WasPlayerDestroyed;

        /// <summary>
        /// Gets the players lives.
        /// </summary>
        /// <value>
        /// The players lives.
        /// </value>
        public int PlayersLives => this.playerShipManager.playerShipsLives;

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
            if (!this.playerShipManager.justFired)
            {
                this.bulletManager.addBullet(this.playerShipManager.FirePlayerShip());
                this.playerShipManager.justFired = true;
            }
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
            if (this.isPlayerShipHit())
            {
                this.playerShipManager.HandlePlayerGettingHit();
                this.onPlayerWasHit(this.playerShipManager.playerShipsLives);
            }
            if (this.isAnEnemyShipHit())
            {
                this.playerShipManager.TogglePlayerShipsGun();
            }
        }

        private bool isAnEnemyShipHit()
        {
            return this.bulletManager.CheckForCollisionsWithEnemyShips(this.enemyShipManager.EnemyShips);
        }

        private bool isPlayerShipHit()
        {
            return this.bulletManager.CheckForCollisionWithPlayerShip(this.playerShipManager.PlayerShip);
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

            this.playerShipManager.justFired = false;

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
            this.ScoreUpdated?.Invoke(score);
        }

        private void onGamedEndedEvent()
        {
            this.GameEnded?.Invoke();
        }

        private void onPlayerWasHit(int lives)
        {
            this.PlayerHit?.Invoke(lives);
        }
        #endregion
    }
}