using SpaceInvaders.Model.Enum_Classes;
using SpaceInvaders.View.Sprites;

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
            Sprite = new EnemyShipSpriteLevel2();
            ScoreValue = ScoreValue.AverageValue;
            ShipLevel = ShipLevel.LevelTwo;
            ShipRow = Row.MiddleRow;
        }

        #endregion
    }
}