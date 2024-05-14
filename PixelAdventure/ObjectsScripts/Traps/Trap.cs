using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelAdventure.ObjectsScripts.Traps
{
    internal class Trap
    {
        public Point Size { get; protected set; }
        public Point Spawn { get; protected set; }

        private Rectangle topCollRect;
        private Rectangle bottomCollRect;

        public Trap(Point size, Point spawnPoint)
        {
            Spawn = spawnPoint;
            Size = size;
            topCollRect = new Rectangle(Spawn.X + 5, Spawn.Y, Size.X - 10, Size.Y);
            bottomCollRect = new Rectangle(Spawn.X, Spawn.Y + 7, Size.X - 10, Size.Y);
        }

        public virtual bool Collide(Vector2 playerVector, Point playerSize)
        {
            Rectangle playerRectangle = new(new Point((int)playerVector.X, (int)playerVector.Y), new Point(playerSize.X - 15, playerSize.Y));

            if (playerRectangle.Intersects(bottomCollRect) || playerRectangle.Intersects(topCollRect))
                return true;
            return false;
        }
    }
}
