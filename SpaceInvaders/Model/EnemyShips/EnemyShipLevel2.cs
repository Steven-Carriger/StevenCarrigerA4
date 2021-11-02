using SpaceInvaders.Model.Enum_Classes;
using SpaceInvaders.View.Sprites.EnemySprites.FirstFrameSprites;

namespace SpaceInvaders.Model.EnemyShips
{
    /// <summary>
    ///     The enemy ship level 2 class.
    /// </summary>
    /// <seealso cref="EnemyShipLevel1" />
    public class EnemyShipLevel2 : EnemyShipLevel1
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="EnemyShipLevel2" /> class.
        /// </summary>
        public EnemyShipLevel2()
        {
            this.Sprite = new EnemyShipSpriteLevel2Frame1();
            this.ScoreValue = ScoreValue.AverageValue;
            this.ShipLevel = ShipLevel.LevelTwo;
            this.ShipRow = Row.SecondRow;
        }

        #endregion
    }
}