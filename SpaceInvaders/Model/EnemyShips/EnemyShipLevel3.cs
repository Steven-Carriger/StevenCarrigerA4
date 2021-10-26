using SpaceInvaders.Model.Enum_Classes;
using SpaceInvaders.View.Sprites;

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
            Sprite = new EnemyShipSpriteLevel3();
            ScoreValue = ScoreValue.AdvanceValue;
            ShipLevel = ShipLevel.LevelThree;
            ShipRow = Row.TopRow;
        }

        #endregion
    }
}