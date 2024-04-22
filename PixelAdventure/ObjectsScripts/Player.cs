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

namespace PixelAdventure.ObjectsScripts
{
    internal static class Player
    {
        public static Point Size { get; private set; }
        //public static Point Spawn { get; private set; }

        public static Point ColliderSize;
        public static Point ColliderSpawn;

        public static Vector2 Vector;

        private static int speed;

        private static int jumpForce;

        public static bool IsMove = false; 
        public static bool IsJump = false;

        public static Animation walk;
        private static Animation idle;
        private static Animation jump;

        public static Animation currentAnimation;

        private static Dictionary<string, Texture2D> animationSprites;
        private static Dictionary<Texture2D, Animation> animations;

        public static Point currentFrameWalk = new Point(0, 0);
        private static Point spriteSizeWalk = new Point(6, 0);

        public static Point currentFrameIdle = new Point(0, 0);
        private static Point spriteSizeIdle = new Point(4, 0);

        public static Point currentFrameJump = new Point(0, 0);
        private static Point spriteSizeJump = new Point(8, 0);

        static Player()
        {
            Size = new Point(30, 30);
            ColliderSpawn = new Point((int)Vector.X - 14, (int)Vector.Y - 7);
            ColliderSize = new Point(18,25);
            Vector = new Vector2(0, Game1.windowHeight - 100 - Size.Y);
            //Spawn = new Point((int)Vector.X, (int)Vector.Y);
            speed = 3;
            jumpForce = 80;
        }

        public static void InicializeSprites(Texture2D walkRightSprite, Texture2D walkLeftSprite,
            Texture2D idleRightSprite, Texture2D idleLeftSprite, Texture2D jumpRightSprite, Texture2D jumpLeftSprite)
        {
            walk = new Animation(32, 32, currentFrameWalk, spriteSizeWalk);
            idle = new Animation(32, 32, currentFrameIdle, spriteSizeIdle);
            //jump = new Animation(32, 32, currentFrameJump, spriteSizeJump);

            animationSprites = new Dictionary<string, Texture2D>()
            {
                { "walkLeft", walkLeftSprite },
                { "walkRight", walkRightSprite },
                { "idleLeft", idleLeftSprite },
                { "idleRight", idleRightSprite },
                //{ "jumpRight",  jumpRightSprite },
                //{ "jumpLeft", jumpLeftSprite },
            };

            animations = new Dictionary<Texture2D, Animation>()
            {
                { walkLeftSprite, walk },
                { walkRightSprite, walk },
                { idleLeftSprite, idle },
                { idleRightSprite, idle },
                //{ jumpRightSprite, jump },
                //{ jumpLeftSprite, jump },
            };
            currentAnimation = new Animation(32, 32, currentFrameWalk, spriteSizeWalk);
        }

        public static bool GoLeft = false;
        public static void Move(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                GoLeft = false;
                IsMove = true;
                Vector.X += speed;
                currentAnimation = walk;
                currentAnimation.StartAnimation(gameTime);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                GoLeft = true;
                IsMove = true;
                Vector.X -= speed;
                currentAnimation = walk;
                currentAnimation.StartAnimation(gameTime);
            }
            else if (!Keyboard.GetState().IsKeyDown(Keys.D) && !GoLeft)
            {
                GoLeft = false;
                IsMove = false;
                currentAnimation = idle;
                currentAnimation.StartAnimation(gameTime);
            }
            else if (!Keyboard.GetState().IsKeyDown(Keys.A) && GoLeft)
            {
                GoLeft = true;
                IsMove = false;
                currentAnimation = idle;
                currentAnimation.StartAnimation(gameTime);
            }
            if (Vector.X <= -11)
                Vector.X = -11;
            if (Vector.X >= Game1.windowWidth - Size.X)
                Vector.X = Game1.windowWidth - Size.X;
        }

        public static void CollideWithPlatforms(Platform[] platforms, float gravity, GameTime gameTime)
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
                        Jump(gameTime);
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
                        Jump(gameTime);
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
                    Jump(gameTime);
                }
            }
        }

        static int countJump = 0;
        public static void Jump(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W) && countJump == 0)
            {
                //currentAnimation = jump;
                //currentAnimation.StartAnimation(gameTime);
                countJump++;
                Vector.Y -= jumpForce;
                //IsJump = true;
            }
            else if (countJump > 0)
            {
                //IsJump = false;
                jumpForce = 0;
                countJump--;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.W))
            { 
                jumpForce = 80;
                //IsJump = false;
            }
        }

        public static GameState CollideWithEnemies(List<Enemy> enemies)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].IsFromTheLeft(Vector, Size) == CollideState.Death &&
                    enemies[i].Collide(Vector, Size) == CollideState.Kill)
                {
                    StartAgain();
                    //enemies[i].StartAgain();
                    return GameState.GameOver;
                }

                else if (enemies[i].IsFromTheRight(Vector, Size) == CollideState.Death &&
                    enemies[i].Collide(Vector, Size) == CollideState.Kill)
                {
                    StartAgain();
                    //enemies[i].StartAgain();
                    return GameState.GameOver;
                }
                else if (enemies[i].Collide(Vector, Size) == CollideState.Kill)
                {
                    Vector.Y -= 50;
                    enemies.RemoveAt(i);
                }
                else if (enemies[i].IsFromTheLeft(Vector, Size) == CollideState.Death)
                {
                    StartAgain();
                    //enemies[i].StartAgain();
                    return GameState.GameOver;
                }
                else if (enemies[i].IsFromTheRight(Vector, Size) == CollideState.Death)
                {
                    StartAgain();
                    //enemies[i].StartAgain();
                    return GameState.GameOver;
                }
            }
            return GameState.GamePlay;
        }

        public static void StartAgain()
        {
            Vector.X = 0;
            GoLeft = false;
        }

        public static void DrawCurrentAnimation(SpriteBatch _spriteBatch, Texture2D texture, Animation animation)
        {
            _spriteBatch.Draw(texture,
                    new Rectangle((int)Vector.X, (int)Vector.Y - 10, Size.X + 10, Size.Y + 10),
                    currentAnimation.CreateRectangle(animation.FrameWidth),
                    Color.White);
        }

        public static void DrawPlayerAnimation(SpriteBatch _spriteBatch)
        {
            if (!GoLeft && IsMove && !IsJump)
                DrawCurrentAnimation(_spriteBatch, animationSprites["walkRight"], animations[animationSprites["walkRight"]]);
            else if (GoLeft && IsMove && !IsJump)
                DrawCurrentAnimation(_spriteBatch, animationSprites["walkLeft"], animations[animationSprites["walkLeft"]]);
            else if (!IsMove && !GoLeft && !IsJump)
                DrawCurrentAnimation(_spriteBatch, animationSprites["idleRight"], animations[animationSprites["idleRight"]]);
            else if (!IsMove && GoLeft && !IsJump)
                DrawCurrentAnimation(_spriteBatch, animationSprites["idleLeft"], animations[animationSprites["idleLeft"]]);
            //else if (IsJump)
            //    DrawCurrentAnimation(_spriteBatch, animationSprites["jumpRight"], animations[animationSprites["jumpRight"]]);
        }
    }
}
