using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Data;
using System.Threading;
using System.Reflection.Metadata.Ecma335;
using System.Net.Mime;
using System.Reflection.Metadata;
using System.Diagnostics.Metrics;
using System.Reflection;
using PixelAdventure.ObjectsScripts.Platforms;
using PixelAdventure.ObjectsScripts.Traps;
using PixelAdventure.ObjectsScripts.OtherObjects;

namespace PixelAdventure
{
    internal class Player
    {
        public Point Size { get; private set; }

        public Vector2 Vector;

        private int speed;

        private int jumpForce;

        public bool IsMove = false;
        public bool IsJump = false;
        public bool IsFall = false;

        public bool GoLeft = false;

        private int countJump = 0;

        public int counter;

        public bool IsStay;
        public bool IsWalk;

        public Player(int startX, int startY)
        {
            Size = new Point(30, 30);
            Vector = new Vector2(startX, startY);
            speed = 3;
            jumpForce = 10;
            counter = 0;
        }

        public void Move()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                GoLeft = false;
                IsMove = true;
                Vector.X += speed;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                GoLeft = true;
                IsMove = true;
                Vector.X -= speed;
            }
            else if (!Keyboard.GetState().IsKeyDown(Keys.D) && !GoLeft)
            {
                GoLeft = false;
                IsMove = false;
            }
            else if (!Keyboard.GetState().IsKeyDown(Keys.A) && GoLeft)
            {
                GoLeft = true;
                IsMove = false;
            }
            if (Vector.X <= -11)
                Vector.X = -11;
            if (Vector.X >= Game1.windowWidth - Size.X)
                Vector.X = Game1.windowWidth - Size.X;
        }

        public void CollideWithPlatforms(Platform[] platforms, float gravity)
        {
            foreach (Platform platform in platforms)
            {
                if (platform.IsFromTheLeft(Vector, Size) == CollideState.Left &&
                    platform.Collide(Vector, Size, this) == CollideState.Top)
                {
                    if (!Keyboard.GetState().IsKeyDown(Keys.D))
                    {
                        Vector.X -= speed;
                        Vector.Y -= gravity;
                        Jump();
                    }
                    else
                        Vector.X -= speed;
                }

                if (platform.IsFromTheRight(Vector, Size) == CollideState.Right &&
                    platform.Collide(Vector, Size, this) == CollideState.Top)
                {

                    if (!Keyboard.GetState().IsKeyDown(Keys.A))
                    {
                        Vector.X += speed;
                        Vector.Y -= gravity;
                        Jump();
                    }
                    else
                        Vector.X += speed;
                }

                else if (platform.Collide(Vector, Size, this) == CollideState.Top)
                {
                    Vector.Y = platform.SpawnPoint.Y - Size.Y;
                    Jump();
                }
            }
        }
        public void Jump()
        {
            var startY = Vector.Y;

            if ((Keyboard.GetState().IsKeyDown(Keys.W) && countJump == 0))
            {
                speed = 4;
                for (int i = 1; i < 14; i++)
                    Vector.Y -= jumpForce;
                countJump++;
            }
            if (Vector.Y == startY)
            {
                speed = 3;
            }

            if (countJump > 0)
            {
                IsJump = true;
                jumpForce = 0;
                countJump--;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W) && Vector.Y == startY)
                IsJump = false;
            if (Keyboard.GetState().IsKeyUp(Keys.W))
            {
                IsJump = false;
                jumpForce = 10;
            }
        }

        public bool CollideWithEnemies(List<Enemy> enemies, List<Coin> coins)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].IsFromTheLeft(Vector, Size) == CollideState.Death &&
                    enemies[i].Collide(Vector, Size, this) == CollideState.Kill)
                    return true;

                else if (enemies[i].IsFromTheRight(Vector, Size) == CollideState.Death &&
                    enemies[i].Collide(Vector, Size, this) == CollideState.Kill)
                    return true;

                else if (enemies[i].Collide(Vector, Size, this) == CollideState.Kill)
                {
                    Vector.Y -= 50;
                    coins.Add(new Coin(new Point(30,30), new Point((int)enemies[i].Vector.X, (int)enemies[i].Vector.Y)));
                    enemies.RemoveAt(i);
                    return false;
                }

                else if (enemies[i].IsFromTheLeft(Vector, Size) == CollideState.Death)
                    return true;

                else if (enemies[i].IsFromTheRight(Vector, Size) == CollideState.Death)
                    return true;
            }
            return false;
        }

        public void CollideWithCoins(List<Coin> coins)
        {
            for (int i = 0; i < coins.Count; i++)
            {
                if (coins[i].Collide(Vector, Size))
                {
                    coins.RemoveAt(i);
                    counter++;
                }
            }
        }
    }
}