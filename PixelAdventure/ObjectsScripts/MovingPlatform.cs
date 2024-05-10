using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using PixelAdventure.PlayerScripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelAdventure
{
    internal class MovingPlatform : Platform
    {
        public new Vector2 Vector;
        protected float speed;

        protected int leftBound;
        protected int rightBound;

        public string Type { get; set; }
        public MovingPlatform(Point platformSize, Point spawnPoint, int leftBound, int rightBound, float speed, string type) : base(platformSize, spawnPoint)
        {
            Vector = new Vector2(spawnPoint.X, spawnPoint.Y);
            this.speed = speed;
            this.leftBound = leftBound;
            this.rightBound = rightBound;
            Type = type;
        }

        public virtual void HorizontalMove(GameTime gameTime)
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

        public virtual void VerticalMove(GameTime gameTime)
        {
            Vector.Y += speed;

            if (Vector.Y <= leftBound)
            {
                speed *= -1;
                Vector.Y = leftBound;
            }
            if (Vector.Y >= rightBound)
            {
                speed *= -1;
                Vector.Y = rightBound;
            }
        }

        public override CollideState Collide(Vector2 playerVector, Point playerSize, Player player)
        {
            var topRectangle = new Rectangle((int)Vector.X + 3, (int)Vector.Y - 10, Size.X - 3, Size.Y / 2);
            var playerRectangle = new Rectangle((int)playerVector.X + 12, (int)playerVector.Y, playerSize.X - 12, playerSize.Y);

            if (playerRectangle.Intersects(topRectangle))
            {
                if (Type == "horizontal")
                    player.Vector.X += speed;
                //if (Type == "vertical")
                //    player.Vector.Y += 1;
                return CollideState.Top;
            }

            return CollideState.Fall;
        }

        public override CollideState IsFromTheLeft(Vector2 playerVector, Point playerSize)
        {
            var playerRectangle = new Rectangle((int)playerVector.X + 12, (int)playerVector.Y, playerSize.X - 12, playerSize.Y);
            var leftRectangle = new Rectangle((int)Vector.X + 1, (int)Vector.Y + 4, 1, 1);

            if (playerRectangle.Intersects(leftRectangle))
                return CollideState.Left;
            return CollideState.Fall;
        }

        public override CollideState IsFromTheRight(Vector2 playerVector, Point playerSize)
        {
            var playerRectangle = new Rectangle((int)playerVector.X + 12, (int)playerVector.Y, playerSize.X - 12, playerSize.Y);
            var rightRectangle = new Rectangle((int)Vector.X + Size.X - 1, (int)Vector.Y + 4, 1, 1);

            if (playerRectangle.Intersects(rightRectangle))
                return CollideState.Right;
            return CollideState.Fall;
        }
    }
}