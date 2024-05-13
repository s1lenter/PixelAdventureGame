using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelAdventure
{
    internal class Bullet : Trap
    {
        public Vector2 Vector;
        protected float speed;

        public Bullet(Point size, Point spawn) : base(size, spawn)
        {
            Spawn = spawn;
            Size = size;
            Vector = new Vector2(spawn.X, spawn.Y);

            speed = 1;
        }

        public virtual void Move()
        {
            Vector.X -= speed;
            if (Vector.X < Spawn.X - 100)
                Vector.X = Spawn.X;
        }

        public override bool Collide(Vector2 playerVector, Point playerSize)
        {
            Rectangle playerRectangle = new ((int)playerVector.X, (int)playerVector.Y, playerSize.X, playerSize.Y);
            Rectangle collider = new ((int)Vector.X, (int)Vector.Y, Size.X - 10, Size.Y);

            if (playerRectangle.Intersects(collider))
                return true;
            return false;
        }
    }
}
