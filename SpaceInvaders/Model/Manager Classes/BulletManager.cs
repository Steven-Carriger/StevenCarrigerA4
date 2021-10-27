﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
using SpaceInvaders.Model.EnemyShips;
using SpaceInvaders.Model.Enum_Classes;
using System.Linq;

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
            var collisionBullet = from bullet in this.Bullets where bullet.collidesWith(playerShip) select bullet;
            var result = false;
            foreach (var bullet in collisionBullet)
            {
                if (this.wasNotFiredFromAPlayersShip(bullet))
                {
                    bullet.IsDestroyed = true;
                    playerShip.IsDestroyed = true;

                    this.BackgroundCanvas.Children.Remove(playerShip.Sprite);
                    this.BackgroundCanvas.Children.Remove(bullet.Sprite);

                    result = true;
                }
            }
            return result;
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
        public bool CheckForCollisionsWithEnemyShips(Collection<EnemyShip> enemyShips)
        {
            foreach (var bullet in this.Bullets)
            {
                if (this.doesBulletHitAnyEnemyShips(bullet, enemyShips))
                {
                    return true;
                }
            }
            return false;
        }

        private bool wasNotFiredFromAnEnemyShip(Bullet bullet)
        {
            return bullet.HomeShipType != ShipType.Enemy;
        }

        private bool doesBulletHitAnyEnemyShips(Bullet bullet, Collection<EnemyShip> enemyShips)
        {
            foreach (var enemyShip in enemyShips)
            {
                if (bullet.collidesWith(enemyShip) && this.wasNotFiredFromAnEnemyShip(bullet))
                {
                    enemyShip.IsDestroyed = true;
                    bullet.IsDestroyed = true;
                    return true;
                }
            }
            return false;
        }
        #endregion
    }
}