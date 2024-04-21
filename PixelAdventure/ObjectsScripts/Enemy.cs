using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelAdventure.ObjectsScripts
{
    internal class Enemy : MovingPlatform
    {
        public bool IsLife { get; private set; }
        public Enemy(Point enemySize, Point spawnPoint, int leftBound, int rightBound) : base(enemySize, spawnPoint, leftBound, rightBound)
        {
            Vector = new Vector2(spawnPoint.X, spawnPoint.Y);
            speed = 0.6f;
            this.leftBound = leftBound;
            this.rightBound = rightBound;
            IsLife = true;
        }

        public override CollideState Collide(Vector2 playerVector, Point playerSize)
        {
            var topRectangle = new Rectangle((int)Vector.X + 6, SpawnPoint.Y + 4, Size.X - 11, 1);
            var playerRectangle = new Rectangle((int)playerVector.X + 12, (int)playerVector.Y, playerSize.X - 12, playerSize.Y);

            if (playerRectangle.Intersects(topRectangle))
            {
                IsLife = false;
                return CollideState.Kill;
            }
            return CollideState.Fall;
        }

        public override CollideState IsFromTheLeft(Vector2 playerVector, Point playerSize)
        {
            var playerRectangle = new Rectangle((int)playerVector.X + 12, (int)playerVector.Y, playerSize.X - 12, playerSize.Y);
            var leftRectangle = new Rectangle((int)Vector.X + 6, SpawnPoint.Y + 6, 1, Size.Y);

            if (playerRectangle.Intersects(leftRectangle))
                return CollideState.Death;
            return CollideState.Fall;
        }

        public override CollideState IsFromTheRight(Vector2 playerVector, Point playerSize)
        {
            var playerRectangle = new Rectangle((int)playerVector.X + 12, (int)playerVector.Y, playerSize.X - 12, playerSize.Y);
            var rightRectangle = new Rectangle((int)Vector.X + Size.X - 6, SpawnPoint.Y + 6, 1, Size.Y);

            if (playerRectangle.Intersects(rightRectangle))
                return CollideState.Death;
            return CollideState.Fall;
        }

        public void DrawCurrentAnimation(SpriteBatch _spriteBatch, Texture2D texture/*, Animation animation*/)
        {
            if (IsLife)
                _spriteBatch.Draw(texture, new Rectangle((int)Vector.X, SpawnPoint.Y, Size.X, Size.Y), Color.White);
        }
    }
}
