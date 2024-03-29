﻿using SpaceInvaders.Model.Enum_Classes;
using SpaceInvaders.View.Sprites.EnemySprites.FirstFrameSprites;

namespace SpaceInvaders.Model.EnemyShips
{
    /// <summary>
    ///     The enemyShipLevel3 class
    /// </summary>
    /// <seealso cref="SpaceInvaders.Model.EnemyShips.EnemyShipLevel2" />
    public class EnemyShipLevel3 : EnemyShipLevel2
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="EnemyShipLevel3" /> class.
        /// </summary>
        public EnemyShipLevel3()
        {
            this.Sprite = new EnemyShipSpriteLevel3Frame1();
            this.ScoreValue = ScoreValue.AdvanceValue;
            this.ShipLevel = ShipLevel.LevelThree;
            this.ShipRow = Row.ThirdRow;
            this.CanFire = true;
        }

        #endregion
    }
}