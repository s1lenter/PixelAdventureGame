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

namespace PixelAdventure.ObjectsScripts
{
    internal class Player
    {
        public Point Size { get; private set; }
        //public static Point Spawn { get; private set; }

        public Point ColliderSize;
        public Point ColliderSpawn;

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

        public Player(int startX, int startY) //CTOR
        {
            Size = new Point(30, 30);
            ColliderSpawn = new Point((int)Vector.X - 14, (int)Vector.Y - 7);
            ColliderSize = new Point(18, 25);
            Vector = new Vector2(startX, startY /*Game1.windowHeight - 100 - Size.Y*/);
            //Spawn = new Point((int)Vector.X, (int)Vector.Y);
            speed = 3;
            jumpForce = 100;
            counter = 0;
        }

        public void Move(GameTime gameTime) //MODEL
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

        public void CollideWithPlatforms(Platform[] platforms, float gravity, GameTime gameTime) //MODEL
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
                        Jump(gameTime, gravity);
                    }
                    else
                    {
                        Vector.X -= speed;
                        //Vector.Y += gravity;
                    }
                }

                if (platform.IsFromTheRight(Vector, Size) == CollideState.Right &&
                    platform.Collide(Vector, Size, this) == CollideState.Top)
                {

                    if (!Keyboard.GetState().IsKeyDown(Keys.A))
                    {
                        Vector.X += speed;
                        Vector.Y -= gravity;
                        Jump(gameTime, gravity);
                    }
                    else
                    {
                        Vector.X += speed;
                        //Vector.Y += gravity;
                    }
                }

                else if (platform.Collide(Vector, Size, this) == CollideState.Top)
                {
                    Vector.Y = (int)platform.Vector.Y - Size.Y;
                    Jump(gameTime, gravity);
                }
            }
        }
        public void Jump(GameTime gameTime, float gravity) //MODEL
        {
            var startY = Vector.Y;
            
            if (Keyboard.GetState().IsKeyDown(Keys.W) && countJump == 0)
            {
                speed = 4;
                //Vector.Y += 10;
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
                jumpForce = 120;
            }
        }

        public bool CollideWithEnemies(List<Enemy> enemies) //MODEL
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

        public void CollideWithCoins(List<Coin> coins) //MODEL
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