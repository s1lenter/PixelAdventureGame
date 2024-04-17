using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstGamePrototype.ObjectsScripts
{
    internal class Trap : IObject
    {
        public Point Size { get; set; }
        public Point SpawnPoint { get; set; }

        private Rectangle topCollRect;
        private Rectangle bottomCollRect;

        public Trap(Point size, Point spawnPoint)
        {
            SpawnPoint = spawnPoint;
            Size = size;
            topCollRect = new Rectangle(SpawnPoint.X + 5, SpawnPoint.Y, Size.X - 10, Size.Y);
            bottomCollRect = new Rectangle(SpawnPoint.X, SpawnPoint.Y + 7, Size.X - 10, Size.Y);
        }

        public bool CollideWithTrap(Vector2 playerVector, Point playerSize)
        {
            Rectangle playerRectangle = new Rectangle(new Point((int)playerVector.X, (int)playerVector.Y), playerSize);

            if (playerRectangle.Intersects(bottomCollRect) || playerRectangle.Intersects(topCollRect))
                return true;
            return false;
        }
    }
}
