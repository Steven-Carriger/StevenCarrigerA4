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

        private Collection<Bullet> Bullets { get; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="BulletManager" /> class.
        /// </summary>
        public BulletManager()
        {
            this.Bullets = new Collection<Bullet>();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Takes a step.
        /// </summary>
        public void takeAStep()
        {
            this.moveBullets();
        }

        /// <summary>
        ///     checks if the players bullet went off screen.
        /// </summary>
        /// <param name="background">The background.</param>
        /// <returns>true if the players bullet went off the screen and was removed, false otherwise</returns>
        public bool RemovePlayersOffScreenBullet(Canvas background)
        {
            foreach (var bullet in this.Bullets)
            {
                if (bullet.Y <= background.MinHeight)
                {
                    bullet.IsDestroyed = true;
                    background.Children.Remove(bullet.Sprite);
                    this.Bullets.Remove(bullet);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        ///     removes the enemies bullet if it goes off screen.
        /// </summary>
        /// <param name="background">The background.</param>
        /// <returns>true if an enemies bullet went off the screen and was removed. false otherwise</returns>
        public bool RemoveEnemiesOffScreenBullet(Canvas background)
        {
            foreach (var bullet in this.Bullets)
            {
                if (bullet.Y >= background.Height)
                {
                    bullet.IsDestroyed = true;
                    background.Children.Remove(bullet.Sprite);
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
        /// <param name="background">The background to place it on.</param>
        public void addBullet(Bullet bullet, Canvas background)
        {
            if (bullet != null)
            {
                this.Bullets.Add(bullet);
                background.Children.Add(bullet.Sprite);
            }
        }

        /// <summary>
        ///     Checks for collision with player ship.
        /// </summary>
        /// <param name="playerShip">The player ship.</param>
        /// <param name="background">The background.</param>
        /// <returns>true if the player ship is destroyed, false otherwise.</returns>
        public bool CheckForCollisionWithPlayerShip(GameObject playerShip, Canvas background)
        {
            foreach (var bullet in this.Bullets)
            {
                if (this.wasNotFiredFromAPlayersShip(bullet))
                {
                    if (bullet.collidesWith(playerShip))
                    {
                        bullet.IsDestroyed = true;
                        playerShip.IsDestroyed = true;

                        background.Children.Remove(playerShip.Sprite);
                        background.Children.Remove(bullet.Sprite);
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
        /// <param name="background">The background.</param>
        /// <returns>true if an enemy ship was destroyed, false otherwise.</returns>
        public bool CheckForCollisionsWithEnemyShips(Collection<EnemyShipLevel1> enemyShips, Canvas background)
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

                            background.Children.Remove(ship.Sprite);
                            background.Children.Remove(bullet.Sprite);

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