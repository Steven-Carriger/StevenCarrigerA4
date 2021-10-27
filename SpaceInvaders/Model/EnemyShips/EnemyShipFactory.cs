using SpaceInvaders.Model.Enum_Classes;
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
    }
}
