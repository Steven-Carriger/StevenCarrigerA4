using SpaceInvaders.View.Sprites;
using SpaceInvaders.Model.Enum_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceInvaders.View.Sprites.EnemySprites;

namespace SpaceInvaders.Model.EnemyShips
{
    public class EnemyShipLevel4 : EnemyShipLevel3
    {
        public EnemyShipLevel4()
        {
            this.Sprite = new EnemyShipSpriteLevel4Frame1();
            this.ScoreValue = ScoreValue.ChallengingValue;
            this.ShipLevel = ShipLevel.LevelFour;
            this.ShipRow = Row.FourthRow;
        }
    }
}
