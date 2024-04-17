using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstGamePrototype.ObjectsScripts
{
    internal class MovingPlatform : Platform
    {
        public Vector2 Vector;
        private float speed;

        private int leftBound;
        private int rightBound;
        public MovingPlatform(Point platformSize, Point spawnPoint, int leftBound, int rightBound) : base(platformSize, spawnPoint)
        {
            Vector = new Vector2(spawnPoint.X, spawnPoint.Y);
            speed = 0.6f;
            this.leftBound = leftBound;
            this.rightBound = rightBound;
        }

        public void Move()
        {
            Vector.X += speed;

            if (Vector.X <= leftBound)
            {
                speed *= -1;
                Vector.X = leftBound;
            }
            if (Vector.X >= rightBound)
            {
                speed *= -1;
                Vector.X = rightBound;
            }
        }

        public override CollideState Collide(Vector2 playerVector, Point playerSize)
        {
            var topRectangle = new Rectangle((int)Vector.X, SpawnPoint.Y, Size.X, Size.Y / 2);
            var playerRectangle = new Rectangle((int)playerVector.X + 12, (int)playerVector.Y, playerSize.X - 12, playerSize.Y);

            if (playerRectangle.Intersects(topRectangle))
            {
                Player.Vector.X += speed / 2;
                return CollideState.Top;
            }

            return CollideState.Fall;
        }

        public override CollideState IsFromTheLeft(Vector2 playerVector, Point playerSize)
        {
            var playerRectangle = new Rectangle((int)playerVector.X + 12, (int)playerVector.Y, playerSize.X - 12, playerSize.Y);
            var leftRectangle = new Rectangle((int)Vector.X, SpawnPoint.Y + 3, 1, Size.Y);

            if (playerRectangle.Intersects(leftRectangle))
                return CollideState.Left;
            return CollideState.Fall;
        }

        public override CollideState IsFromTheRight(Vector2 playerVector, Point playerSize)
        {
            var playerRectangle = new Rectangle((int)playerVector.X + 12, (int)playerVector.Y, playerSize.X - 12, playerSize.Y);
            var rightRectangle = new Rectangle((int)Vector.X + Size.X, SpawnPoint.Y + 3, 1, Size.Y);

            if (playerRectangle.Intersects(rightRectangle))
                return CollideState.Right;
            return CollideState.Fall;
        }
    }
}
