using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelAdventure
{
    internal class Saw : Bullet
    {
        public float angleRotate;
        private int topBound;
        private int bottomBound;

        public Saw(Point size, Point spawn, float speed, int topBound, int bottomBound) : base(size, spawn)
        {
            Spawn = spawn;
            Size = size;
            Vector = new Vector2(spawn.X, spawn.Y);
            angleRotate = 0;
            this.speed = speed;

            if (this.speed == 0)
            {
                this.topBound = spawn.Y; 
                this.bottomBound = spawn.Y;
            }
            else
            {
                this.topBound = topBound;
                this.bottomBound = bottomBound;
            }
        }

        public override void Move()
        {
            Vector.Y += speed;
            if (Vector.Y <= topBound)
            {
                Vector.Y = topBound;
                speed *= -1;
            }
            if (Vector.Y >= bottomBound)
            {
                Vector.Y = bottomBound;
                speed *= -1;
            }
            Spin();
        }

        public void Spin() => angleRotate += 0.2f;

        public override bool Collide(Vector2 playerVector, Point playerSize)
        {
            Rectangle playerRectangle = new((int)playerVector.X, (int)playerVector.Y, playerSize.X, playerSize.Y);
            Rectangle collider = new((int)Vector.X - 20, (int)Vector.Y - 15, Size.X - 20, Size.Y - 15);

            if (playerRectangle.Intersects(collider))
                return true;
            return false;
        }
    }
}
