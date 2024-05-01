using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelAdventure.ObjectsScripts
{
    internal class MovingPlatform : Platform
    {
        public Vector2 Vector;
        protected float speed;

        protected int leftBound;
        protected int rightBound;
        public MovingPlatform(Point platformSize, Point spawnPoint, int leftBound, int rightBound) : base(platformSize, spawnPoint)
        {
            Vector = new Vector2(spawnPoint.X, spawnPoint.Y);
            speed = 0.6f;
            this.leftBound = leftBound;
            this.rightBound = rightBound;
        }

        public virtual void Move(GameTime gameTime)
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

        public override CollideState Collide(Rectangle playerRectangle, Player player)
        {
            var topRectangle = new Rectangle((int)Vector.X, SpawnPoint.Y + 1, Size.X, Size.Y / 2);

            if (playerRectangle.Intersects(topRectangle))
            {
                player.Vector.X += speed;
                return CollideState.Top;
            }

            return CollideState.Fall;
        }

        public override CollideState IsFromTheLeft(Rectangle playerRectangle)
        {
            var leftRectangle = new Rectangle((int)Vector.X, SpawnPoint.Y + 4, 1, Size.Y);

            if (playerRectangle.Intersects(leftRectangle))
                return CollideState.Left;
            return CollideState.Fall;
        }

        public override CollideState IsFromTheRight(Rectangle playerRectangle)
        {
            var rightRectangle = new Rectangle((int)Vector.X + Size.X - 1, SpawnPoint.Y + 4, 1, Size.Y);

            if (playerRectangle.Intersects(rightRectangle))
                return CollideState.Right;
            return CollideState.Fall;
        }
    }
}
