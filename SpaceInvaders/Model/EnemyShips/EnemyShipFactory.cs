using SpaceInvaders.Model.Enum_Classes;
using SpaceInvaders.View.Sprites;
using SpaceInvaders.View.Sprites.EnemySprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.Model.EnemyShips
{
    /// <summary>
    /// Creates EnemyShips
    /// </summary>
    static public class EnemyShipFactory
    {
        /// <summary>
        /// Makes the enemy ship.
        /// </summary>
        /// <param name="shipLevel">The ship level.</param>
        /// <returns>The enemy ship</returns>
        public static EnemyShip MakeEnemyShip(ShipLevel shipLevel)
        {
            switch (shipLevel)
            {
                default:
                    return new EnemyShipLevel1();

                case ShipLevel.LevelTwo:
                    return new EnemyShipLevel2();

                case ShipLevel.LevelThree:
                    return new EnemyShipLevel3();

                case ShipLevel.LevelFour:
                    return new EnemyShipLevel4();
            }
        }

        /// <summary>
        /// Makes the sprite based on the frame number.
        /// </summary>
        /// <param name="frameNumber">The frame number.</param>
        /// <param name="shipLevel">The level of the ship to make the sprite for</param>
        /// <returns>the appropriate sprite. if none could be found appropriate, a default sprite will be returned</returns>
        public static BaseSprite MakeSprite(FrameNumber frameNumber, ShipLevel shipLevel)
        {
            switch (shipLevel)
            {
                case ShipLevel.LevelOne:
                    return frameNumber == FrameNumber.FrameOne ? new EnemyShipSpriteLevel1Frame1() : (BaseSprite)new EnemyShipSpriteLevel1Frame2();
                case ShipLevel.LevelTwo:
                    return frameNumber == FrameNumber.FrameOne ? new EnemyShipSpriteLevel2Frame1() : (BaseSprite)new EnemyShipSpriteLevel2Frame2();
                case ShipLevel.LevelThree:
                    return frameNumber == FrameNumber.FrameOne ? new EnemyShipSpriteLevel3Frame1() : (BaseSprite)new EnemyShipSpriteLevel3Frame2();
                case ShipLevel.LevelFour:
                    return frameNumber == FrameNumber.FrameOne ? new EnemyShipSpriteLevel4Frame1() : (BaseSprite)new EnemyShipSpriteLevel4Frame2();
                default:
                    break;
            }
            return new EnemyShipSpriteLevel1Frame1();
        }
    }
}
