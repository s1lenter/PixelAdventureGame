using Microsoft.Xna.Framework;
using PixelAdventure.ObjectsScripts;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelAdventure
{
    internal class Finish : IObject
    {
        public Point Size { get; set; }
        public Point SpawnPoint { get; set; }

        private Rectangle collider;

        private int counter;

        public Finish(Point size, Point spawn) 
        {
            Size = size;
            SpawnPoint = spawn;
            collider = new Rectangle(SpawnPoint, Size);
        }

        public bool CollideWithFinish(Vector2 playerVector, Point playerSize)
        {
            Rectangle playerRectangle = new Rectangle(new Point((int)playerVector.X, (int)playerVector.Y), new Point(playerSize.X - 15, playerSize.Y));

            if (playerRectangle.Intersects(collider))
            {
                return true; 
            }

            return false;
        }

        //public void Delete()
        //{
        //    SpawnPoint = new Point(-100, -100);
        //}
    }
}
