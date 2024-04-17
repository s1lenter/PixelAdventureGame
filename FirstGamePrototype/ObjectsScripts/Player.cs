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

namespace FirstGamePrototype.ObjectsScripts
{
    internal static class Player
    {
        private static Game1 game;
        public static Point Size { get; private set; }
        public static Point Spawn { get; private set; }

        public static Point ColliderSize;
        public static Point ColliderSpawn;

        public static Vector2 Vector;

        private static int speed;

        private static int jumpForce;

        public static bool IsMove = false; 

        public static Animation walkRight;
        public static Animation walkLeft;
        private static Animation idleAnim;
        private static Animation idleAnimLeft;

        private static Texture2D walkRightSprite;
        private static Texture2D walkLeftSprite;
        private static Texture2D idleSprite;
        private static Texture2D idleLeftSprite;

        public static Point currentFrameWalk = new Point(0, 0);
        private static Point spriteSizeWalk = new Point(6, 0);

        public static Point currentFrameIdle = new Point(0, 0);
        private static Point spriteSizeIdle = new Point(4, 0);

        static Player()
        {
            Size = new Point(30, 30);
            ColliderSpawn = new Point((int)Vector.X - 14, (int)Vector.Y - 7);
            ColliderSize = new Point(18,25);
            Vector = new Vector2(0, Game1.windowHeight - 100 - Size.Y);
            Spawn = new Point((int)Vector.X, (int)Vector.Y);
            speed = 3;
            jumpForce = 50;
        }

        public static void InicializeSprites(Texture2D right, Texture2D left, Texture2D idle)
        {
            walkLeftSprite = right;
            walkRightSprite = left;
            idleSprite = idle;
            walkRight = new Animation(walkRightSprite, 32, 32, currentFrameWalk, spriteSizeWalk);
            walkLeft = new Animation(walkLeftSprite, 32, 32, currentFrameWalk, spriteSizeWalk);
            idleAnim = new Animation(idleSprite, 32, 32, currentFrameIdle, spriteSizeIdle);
            idleAnimLeft = new Animation(idleLeftSprite, 32, 32, currentFrameIdle, spriteSizeIdle);
        }
        public static Animation currentAnimation = new Animation(walkRightSprite, 32, 32, currentFrameWalk, spriteSizeWalk);
        public static bool GoLeft = false;
        public static void Move(Point currentFrame, GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                GoLeft = false;
                Vector.X += speed;
                IsMove = true;
                currentAnimation = walkRight;
                currentAnimation.StartAnimation(gameTime);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                GoLeft = true;
                Vector.X -= speed;
                IsMove = true;
                currentAnimation = walkLeft;
                currentAnimation.StartAnimation(gameTime);
            }
            else if (!Keyboard.GetState().IsKeyDown(Keys.D) && !GoLeft)
            {
                GoLeft = false;
                IsMove = false;
                currentAnimation = idleAnim;
                currentAnimation.StartAnimation(gameTime);
            }
            else if (!Keyboard.GetState().IsKeyDown(Keys.A) && GoLeft)
            {
                GoLeft = true;
                IsMove = false;
                currentAnimation = idleAnimLeft;
                currentAnimation.StartAnimation(gameTime);
            }
            if (Vector.X <= -11)
                Vector.X = -11;
            if (Vector.X >= Game1.windowWidth - Size.X)
                Vector.X = Game1.windowWidth - Size.X;
        }

        public static bool IsNotJump()
        {
            if (Keyboard.GetState().IsKeyUp(Keys.W))
                return true;
            return false;
        }

        public static void CollideWithPlatforms(Platform[] platforms, float gravity)
        {
            foreach (Platform platform in platforms)
            {
                if (platform.IsFromTheLeft(Vector, Size) == CollideState.Left &&
                    platform.Collide(Vector, Size) == CollideState.Top)
                {
                    if (!Keyboard.GetState().IsKeyDown(Keys.D))
                    {
                        Vector.X -= speed;
                        Vector.Y -= gravity;
                        Jump();
                    }
                    else
                    {
                        Vector.X -= speed;
                        Vector.Y += gravity;
                    }
                }

                if (platform.IsFromTheRight(Vector, Size) == CollideState.Right &&
                    platform.Collide(Vector, Size) == CollideState.Top)
                {
                    if (!Keyboard.GetState().IsKeyDown(Keys.A))
                    {
                        Vector.X += speed;
                        Vector.Y -= gravity;
                        Jump();
                    }
                    else
                    {
                        Vector.X += speed;
                        Vector.Y += gravity;
                    }
                }

                else if (platform.Collide(Vector, Size) == CollideState.Top)
                {
                    Vector.Y = platform.SpawnPoint.Y - Size.Y;
                    Jump();
                }
            }
        }
        private static Keys previousKey = Keys.W;
        //private static bool KeyIsPressed()
        //{
        //    WHIKeyboard.GetState().IsKeyUp(Keys.W))
        //        return false;
        //    return true;
        //}
        static int countJump = 0;
        public static void Jump()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W) && countJump == 0)
            {
                countJump++;
                Vector.Y -= jumpForce;
                previousKey = Keys.W;
            }
            else if (countJump > 0)
            {
                jumpForce = 0;
                countJump--;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.W))
                jumpForce = 50;
        }

        public static void StartAgain()
        {
            Vector.X = 0;
        }

        public static void DrawPlayerAnimation(SpriteBatch _spriteBatch, Dictionary<string, Texture2D> sprites, Dictionary<Texture2D, Animation> animations)
        {
            if (!Player.GoLeft && Player.IsMove)
                _spriteBatch.Draw(sprites["playerWalkRight"],
                    new Rectangle((int)Player.Vector.X, (int)Player.Vector.Y - 10, Player.Size.X + 10, Player.Size.Y + 10),
                    Player.currentAnimation.CreateRectangle(animations[sprites["playerWalkRight"]].frameWidth),
                    Color.White);
            else if (Player.GoLeft && Player.IsMove)
                _spriteBatch.Draw(sprites["playerWalkLeft"],
                    new Rectangle((int)Player.Vector.X, (int)Player.Vector.Y - 10, Player.Size.X + 10, Player.Size.Y + 10),
                    Player.currentAnimation.CreateRectangle(animations[sprites["playerWalkLeft"]].frameWidth),
                    Color.White);
            else if (!Player.IsMove && !Player.GoLeft)
                _spriteBatch.Draw(sprites["playerIdleRight"],
                    new Rectangle((int)Player.Vector.X, (int)Player.Vector.Y - 10, Player.Size.X + 10, Player.Size.Y + 10),
                    Player.currentAnimation.CreateRectangle(animations[sprites["playerIdleRight"]].frameWidth),
                    Color.White);
            else if (!Player.IsMove && Player.GoLeft)
                _spriteBatch.Draw(sprites["playerIdleLeft"],
                    new Rectangle((int)Player.Vector.X, (int)Player.Vector.Y - 10, Player.Size.X + 10, Player.Size.Y + 10),
                    Player.currentAnimation.CreateRectangle(animations[sprites["playerIdleLeft"]].frameWidth),
                    Color.White);
        }
    }
}
