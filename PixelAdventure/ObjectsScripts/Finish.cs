using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelAdventure.ObjectsScripts
{
    internal class Finish
    {
        public Point Size { get; private set; }
        public Point SpawnPoint { get; private set; }

        private Rectangle collider;

        public Finish(Point size, Point spawn)
        {
            Size = size;
            SpawnPoint = spawn;
            collider = new Rectangle(SpawnPoint, Size);
        }

        public bool CollideWithFinish(Vector2 playerVector, Point playerSize)
        {
            Rectangle playerRectangle = new(new Point((int)playerVector.X, (int)playerVector.Y), new Point(playerSize.X - 15, playerSize.Y));

            if (playerRectangle.Intersects(collider))
                return true;
            return false;
        }
    }
}
