﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PixelAdventure.ObjectsScripts.Platforms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelAdventure.ObjectsScripts.Traps
{
    internal class Enemy : MovingPlatform
    {
        public bool IsLife { get; private set; }

        private Animation walk;

        private Animation currentAnimation;

        private static Dictionary<string, Texture2D> animationSprites;
        private static Dictionary<Texture2D, Animation> animations;

        private static Point currentFrameWalk = new (0, 0);
        private static Point spriteSizeWalk = new (6, 0);

        public Enemy(Point enemySize, Point spawnPoint, int leftBound, int rightBound, float speed) : base(enemySize, spawnPoint, leftBound, rightBound, speed)
        {
            Vector = new Vector2(spawnPoint.X, spawnPoint.Y);
            this.speed = speed;
            this.leftBound = leftBound;
            this.rightBound = rightBound;
            IsLife = true;
        }

        public void InicializeSprites(Texture2D walkRightSprite, Texture2D walkLeftSprite)
        {
            walk = new Animation(32, 32, currentFrameWalk, spriteSizeWalk);
            animationSprites = new Dictionary<string, Texture2D>()
            {
                { "walkLeft", walkLeftSprite },
                { "walkRight", walkRightSprite },
            };

            animations = new Dictionary<Texture2D, Animation>()
            {
                { walkLeftSprite, walk },
                { walkRightSprite, walk },
            };

            currentAnimation = new Animation(32, 32, currentFrameWalk, spriteSizeWalk);
        }

        public bool GoLeft = false;

        public override void HorizontalMove(GameTime gameTime)
        {
            Vector.X += speed;
            currentAnimation = walk;
            currentAnimation.StartAnimation(gameTime);
            if (Vector.X <= leftBound)
            {
                speed *= -1;
                GoLeft = false;
                Vector.X = leftBound;
            }
            if (Vector.X >= rightBound)
            {
                speed *= -1;
                GoLeft = true;
                Vector.X = rightBound;
            }
        }

        public override CollideState Collide(Vector2 playerVector, Point playerSize, Player player)
        {
            var topRectangle = new Rectangle((int)Vector.X, SpawnPoint.Y, Size.X, 1);
            var playerRectangle = new Rectangle((int)playerVector.X + 13, (int)playerVector.Y, playerSize.X - 25, playerSize.Y - 1);

            if (playerRectangle.Intersects(topRectangle))
            {
                return CollideState.Kill;
            }
            return CollideState.Fall;
        }

        public override CollideState IsFromTheLeft(Vector2 playerVector, Point playerSize)
        {
            var playerRectangle = new Rectangle((int)playerVector.X + 13, (int)playerVector.Y, playerSize.X - 25, playerSize.Y - 1);
            var leftRectangle = new Rectangle((int)Vector.X, SpawnPoint.Y + 3, 1, Size.Y);

            if (playerRectangle.Intersects(leftRectangle))
                return CollideState.Death;
            return CollideState.Fall;
        }

        public override CollideState IsFromTheRight(Vector2 playerVector, Point playerSize)
        {
            var playerRectangle = new Rectangle((int)playerVector.X + 13, (int)playerVector.Y, playerSize.X - 25, playerSize.Y - 1);
            var rightRectangle = new Rectangle((int)Vector.X + Size.X, SpawnPoint.Y + 3, 1, Size.Y);

            if (playerRectangle.Intersects(rightRectangle))
                return CollideState.Death;
            return CollideState.Fall;
        }

        public void DrawCurrentAnimation(SpriteBatch _spriteBatch, Texture2D texture, Animation animation)
        {
            _spriteBatch.Draw(texture,
                        new Rectangle((int)Vector.X, (int)Vector.Y - 10, Size.X + 10, Size.Y + 10),
                        currentAnimation.CreateRectangle(animation.FrameWidth),
                        Color.White);
        }

        public void DrawEnemyAnimation(SpriteBatch _spriteBatch)
        {
            if (!GoLeft)
                DrawCurrentAnimation(_spriteBatch, animationSprites["walkRight"], animations[animationSprites["walkRight"]]);
            else if (GoLeft)
                DrawCurrentAnimation(_spriteBatch, animationSprites["walkLeft"], animations[animationSprites["walkLeft"]]);
        }
    }
}