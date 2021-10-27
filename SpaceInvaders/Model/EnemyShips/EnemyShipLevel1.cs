using SpaceInvaders.Model.Enum_Classes;
using SpaceInvaders.View.Sprites;

namespace SpaceInvaders.Model.EnemyShips
{
    /// <summary>
    ///     The most basic enemy ship class, EnemyShipLevel1.
    /// </summary>
    /// <seealso cref="SpaceInvaders.Model.EnemyShip" />
    public class EnemyShipLevel1 : EnemyShip
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PlayerShip" /> class.
        /// </summary>
        public EnemyShipLevel1()
        {
            this.Sprite = new EnemyShipSpriteLevel1();
            this.ShipLevel = ShipLevel.LevelOne;
            this.ShipRow = Row.FirstRow;
        }

        #endregion
    }
}