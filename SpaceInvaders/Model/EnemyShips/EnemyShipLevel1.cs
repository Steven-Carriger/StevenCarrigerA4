using SpaceInvaders.Model.Enum_Classes;
using SpaceInvaders.View.Sprites.EnemySprites.FirstFrameSprites;

namespace SpaceInvaders.Model.EnemyShips
{
    /// <summary>
    ///     The most basic enemy ship class, EnemyShipLevel1.
    /// </summary>
    /// <seealso cref="EnemyShip" />
    public class EnemyShipLevel1 : EnemyShip
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PlayerShip" /> class.
        /// </summary>
        public EnemyShipLevel1()
        {
            this.Sprite = new EnemyShipSpriteLevel1Frame1();
            this.ShipLevel = ShipLevel.LevelOne;
            this.ShipRow = Row.FirstRow;
        }

        #endregion
    }
}