using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelAdventure.ObjectsScripts.Platforms
{
    internal class Turret : Platform
    {
        public Point Spawn { get; private set; }
        public new Point Size { get; private set; }
        public Bullet bulletLeft { get; private set; }
        public Bullet bulletRight { get; private set; }
        public Turret(Point size, Point spawn) : base(size, spawn)
        {
            Spawn = spawn;
            Size = size;

            bulletLeft = new Bullet(new Point(10, 10), new Point(Spawn.X, Spawn.Y + Size.Y / 2 - 5));
            bulletRight = new Bullet(new Point(10, 10), new Point(Spawn.X + Size.X - 10, Spawn.Y + Size.Y / 2 - 5));
        }

        public void Shoot()
        {
            bulletLeft.Move("left");
            bulletRight.Move("right");
        }
    }
}
