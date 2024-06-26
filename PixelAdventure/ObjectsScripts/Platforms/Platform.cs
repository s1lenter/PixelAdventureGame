﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Data;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelAdventure.ObjectsScripts.Platforms
{
    internal class Platform
    {
        public Point SpawnPoint { get; private set; }
        public Point Size { get; private set; }

        public Platform(Point platformSize, Point spawnPoint)
        {
            Size = platformSize;
            SpawnPoint = spawnPoint;
        }

        public virtual CollideState Collide(Vector2 playerVector, Point playerSize, Player player)
        {
            var topRectangle = new Rectangle(SpawnPoint.X, SpawnPoint.Y, Size.X, Size.Y);
            var playerRectangle = new Rectangle((int)playerVector.X + 12, (int)playerVector.Y, playerSize.X - 14, playerSize.Y);

            if (playerRectangle.Intersects(topRectangle))
                return CollideState.Top;
            return CollideState.Fall;
        }

        public virtual CollideState IsFromTheLeft(Vector2 playerVector, Point playerSize)
        {
            var playerRectangle = new Rectangle((int)playerVector.X + 12, (int)playerVector.Y, playerSize.X - 14, playerSize.Y);
            var leftRectangle = new Rectangle(SpawnPoint.X, SpawnPoint.Y + 5, 1, Size.Y);

            if (playerRectangle.Intersects(leftRectangle))
                return CollideState.Left;
            return CollideState.Fall;
        }

        public virtual CollideState IsFromTheRight(Vector2 playerVector, Point playerSize)
        {
            var playerRectangle = new Rectangle((int)playerVector.X + 12, (int)playerVector.Y, playerSize.X - 14, playerSize.Y);
            var rightRectangle = new Rectangle(SpawnPoint.X + Size.X, SpawnPoint.Y + 5, 1, Size.Y);

            if (playerRectangle.Intersects(rightRectangle))
                return CollideState.Right;
            return CollideState.Fall;
        }
    }
}