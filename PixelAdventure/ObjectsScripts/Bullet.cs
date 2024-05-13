using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelAdventure
{
    internal class Bullet
    {
        public Point Spawn { get; private set; }
        public Point Size { get; private set; }

        public Vector2 Vector;

        private float speed;

        public Bullet(Point size, Point spawn)
        {
            Spawn = spawn;
            Size = size;
            Vector = new Vector2(spawn.X, spawn.Y);

            speed = 5;
        }

        public void Move()
        {
            Vector.X -= speed;
            if (Vector.X < Spawn.X - 100)
                Vector.X = Spawn.X;
        }

        public bool Collide(Vector2 playerVector, Point playerSize)
        {
            Rectangle playerRectangle = new Rectangle(new Point((int)playerVector.X, (int)playerVector.Y), new Point(playerSize.X, playerSize.Y));
            Rectangle collider = new Rectangle((int)Vector.X, (int)Vector.Y, Size.X, Size.Y);

            if (playerRectangle.Intersects(collider))
                return true;
            return false;
        }
    }
}
