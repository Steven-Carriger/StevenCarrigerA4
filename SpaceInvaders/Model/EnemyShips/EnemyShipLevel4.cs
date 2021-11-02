using SpaceInvaders.Model.Enum_Classes;
using SpaceInvaders.View.Sprites.EnemySprites.FirstFrameSprites;

namespace SpaceInvaders.Model.EnemyShips
{
    public class EnemyShipLevel4 : EnemyShipLevel3
    {
        #region Constructors

        public EnemyShipLevel4()
        {
            this.Sprite = new EnemyShipSpriteLevel4Frame1();
            this.ScoreValue = ScoreValue.ChallengingValue;
            this.ShipLevel = ShipLevel.LevelFour;
            this.ShipRow = Row.FourthRow;
        }

        #endregion
    }
}