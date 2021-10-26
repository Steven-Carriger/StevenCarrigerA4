using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
using SpaceInvaders.Model.EnemyShips;
using SpaceInvaders.Model.Enum_Classes;

namespace SpaceInvaders.Model.Manager_Classes
{
    /// <summary>Controls all of the bullets in the game.</summary>
    public class BulletManager
    {
        #region Properties

        private ICollection<Bullet> Bullets { get; }

        private Canvas BackgroundCanvas { get; }
        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="BulletManager" /> class.
        /// </summary>
        public BulletManager(Canvas background)
        {
            this.Bullets = new Collection<Bullet>();
            this.BackgroundCanvas = background;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Takes a step.
        /// </summary>
        public void TakeAStep()
        {
            this.moveBullets();
        }

        /// <summary>
        ///     checks if the players bullet went off screen.
        /// </summary>
        /// <returns>true if the players bullet went off the screen and was removed, false otherwise</returns>
        public bool RemovePlayersOffScreenBullet()
        {
            foreach (var bullet in this.Bullets)
            {
                if (bullet.Y <= this.BackgroundCanvas.MinHeight)
                {
                    bullet.IsDestroyed = true;
                    this.BackgroundCanvas.Children.Remove(bullet.Sprite);
                    this.Bullets.Remove(bullet);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        ///     removes the enemies bullet if it goes off screen.
        /// </summary>
        /// <returns>true if an enemies bullet went off the screen and was removed. false otherwise</returns>
        public bool RemoveEnemiesOffScreenBullet()
        {
            foreach (var bullet in this.Bullets)
            {
                if (bullet.Y >= this.BackgroundCanvas.Height)
                {
                    bullet.IsDestroyed = true;
                    this.BackgroundCanvas.Children.Remove(bullet.Sprite);
                    this.Bullets.Remove(bullet);
                    return true;
                }
            }

            return false;
        }

        private void moveBullets()
        {
            foreach (var bullet in this.Bullets)
            {
                bullet.Move();
            }
        }

        /// <summary>
        ///     Adds the bullet to the designated canvas.
        /// </summary>
        /// <param name="bullet">The bullet to add.</param>
        public void addBullet(Bullet bullet)
        {
            if (bullet != null)
            {
                this.Bullets.Add(bullet);
                this.BackgroundCanvas.Children.Add(bullet.Sprite);
            }
        }

        /// <summary>
        ///     Checks for collision with player ship.
        /// </summary>
        /// <param name="playerShip">The player ship.</param>
        /// <returns>true if the player ship is destroyed, false otherwise.</returns>
        public bool CheckForCollisionWithPlayerShip(GameObject playerShip)
        {
            foreach (var bullet in this.Bullets)
            {
                if (this.wasNotFiredFromAPlayersShip(bullet))
                {
                    if (bullet.collidesWith(playerShip))
                    {
                        bullet.IsDestroyed = true;
                        playerShip.IsDestroyed = true;

                        this.BackgroundCanvas.Children.Remove(playerShip.Sprite);
                        this.BackgroundCanvas.Children.Remove(bullet.Sprite);
                        return true;
                    }
                }
            }

            return false;
        }

        private bool wasNotFiredFromAPlayersShip(Bullet bullet)
        {
            return ShipType.Player != bullet.HomeShipType;
        }

        /// <summary>
        ///     Checks for collisions with enemy ships.
        /// </summary>
        /// <param name="enemyShips">The enemy ships.</param>
        /// <returns>true if an enemy ship was destroyed, false otherwise.</returns>
        public bool CheckForCollisionsWithEnemyShips(ICollection<EnemyShip> enemyShips)
        {
            foreach (var ship in enemyShips)
            {
                foreach (var bullet in this.Bullets)
                {
                    if (this.wasNotFiredFromAnEnemyShip(bullet))
                    {
                        if (bullet.collidesWith(ship))
                        {
                            bullet.IsDestroyed = true;
                            ship.IsDestroyed = true;

                            this.BackgroundCanvas.Children.Remove(ship.Sprite);
                            this.BackgroundCanvas.Children.Remove(bullet.Sprite);

                            this.Bullets.Remove(bullet);
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private bool wasNotFiredFromAnEnemyShip(Bullet bullet)
        {
            return bullet.HomeShipType != ShipType.Enemy;
        }

        #endregion
    }
}