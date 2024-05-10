using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelAdventure
{
    internal class Coin
    {
        public Point Size { get; set; }
        public Point SpawnPoint { get; set; }
        public Coin(Point size, Point spawn)
        {
            Size = size;
            SpawnPoint = spawn;
        }

        public bool Collide(Vector2 playerVector, Point playerSize)
        {
            var coinRectangle = new Rectangle(SpawnPoint.X, SpawnPoint.Y, Size.X, Size.Y);
            var playerRectangle = new Rectangle((int)playerVector.X + 12, (int)playerVector.Y, playerSize.X - 15, playerSize.Y);

            if (playerRectangle.Intersects(coinRectangle))
                return true;
            return false;  
        }

        public void DrawCoin(SpriteBatch _spriteBatch, Texture2D texture)
        {
            _spriteBatch.Draw(texture, new Rectangle(SpawnPoint, Size), Color.White);
        }
    }
}
