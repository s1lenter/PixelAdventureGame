using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Data;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelAdventure.ObjectsScripts
{
    internal class Platform : IObject
    {
        public Point SpawnPoint { get; set; }
        public Point Size { get; set; }

        public Platform(Point platformSize, Point spawnPoint)
        {
            Size = platformSize;
            SpawnPoint = spawnPoint;
        }

        public virtual CollideState Collide(Rectangle playerRectangle, Player player)
        {
            var topRectangle = new Rectangle(SpawnPoint.X, SpawnPoint.Y, Size.X, Size.Y);

            if (playerRectangle.Intersects(topRectangle))
                return CollideState.Top;
            return CollideState.Fall;
        }

        public virtual CollideState IsFromTheLeft(Rectangle playerRectangle)
        {
            var leftRectangle = new Rectangle(SpawnPoint.X, SpawnPoint.Y + 5, 1, Size.Y);

            if (playerRectangle.Intersects(leftRectangle))
                return CollideState.Left;
            return CollideState.Fall;
        }

        public virtual CollideState IsFromTheRight(Rectangle playerRectangle)
        {
            var rightRectangle = new Rectangle(SpawnPoint.X + Size.X, SpawnPoint.Y + 5, 1, Size.Y);

            if (playerRectangle.Intersects(rightRectangle))
                return CollideState.Right;
            return CollideState.Fall;
        }

        //public virtual CollideState CollideBottom(Vector2 playerVector, Point playerSize)
        //{
        //    var bottomRectangle = new Rectangle(SpawnPoint.X, SpawnPoint.Y + Size.Y / 2, Size.X, Size.Y / 2);
        //    var playerRectangle = new Rectangle((int)playerVector.X, (int)playerVector.Y, playerSize.X, playerSize.Y);

        //    if (playerRectangle.Intersects(bottomRectangle))
        //        return CollideState.Bottom;
        //    return CollideState.Fall;
        //}
    }
}
