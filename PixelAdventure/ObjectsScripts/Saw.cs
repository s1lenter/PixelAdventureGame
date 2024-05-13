using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelAdventure
{
    internal class Saw
    {
        public Point Spawn { get; private set; }
        public Point Size { get; private set; }
        public Vector2 Vector;
        public float angleRotate;

        public Saw(Point spawn, Point size)
        {
            Spawn = spawn;
            Size = size;
            Vector = new Vector2(spawn.X, spawn.Y);
            angleRotate = 0;
        }

        public bool Collide(Vector2 playerVector, Point playerSize)
        {
            Rectangle playerRectangle = new(new Point((int)playerVector.X, (int)playerVector.Y), new Point(playerSize.X, playerSize.Y));
            Rectangle collider = new((int)Vector.X, (int)Vector.Y, Size.X, Size.Y);

            angleRotate+= 0.2f;

            if (playerRectangle.Intersects(collider))
                return true;
            return false;
        }
    }
}
