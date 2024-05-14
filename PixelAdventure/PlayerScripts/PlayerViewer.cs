using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PixelAdventure.ObjectsScripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PixelAdventure
{
    internal class PlayerViewer
    {
        public Animation walk;
        public Animation idle;
        public Animation jump;

        public Animation currentAnimation;

        private Dictionary<string, Texture2D> animationSprites;
        private Dictionary<Texture2D, Animation> animations;

        public Point currentFrameWalk = new Point(0, 0);
        private Point spriteSizeWalk = new Point(6, 0);

        public Point currentFrameIdle = new Point(0, 0);
        private Point spriteSizeIdle = new Point(4, 0);

        public Point currentFrameJump = new Point(0, 0);
        private Point spriteSizeJump = new Point(8, 0);

        public void InicializeSprites(Texture2D walkRightSprite, Texture2D walkLeftSprite,
            Texture2D idleRightSprite, Texture2D idleLeftSprite, Texture2D jumpRightSprite, Texture2D jumpLeftSprite)
        {
            walk = new Animation(32, 32, currentFrameWalk, spriteSizeWalk);
            idle = new Animation(32, 32, currentFrameIdle, spriteSizeIdle);
            jump = new Animation(32, 32, currentFrameJump, spriteSizeJump);

            animationSprites = new Dictionary<string, Texture2D>()
            {
                { "walkLeft", walkLeftSprite },
                { "walkRight", walkRightSprite },
                { "idleLeft", idleLeftSprite },
                { "idleRight", idleRightSprite },
                { "jumpRight",  jumpRightSprite },
                { "jumpLeft", jumpLeftSprite },
            };

            animations = new Dictionary<Texture2D, Animation>()
            {
                { walkLeftSprite, walk },
                { walkRightSprite, walk },
                { idleLeftSprite, idle },
                { idleRightSprite, idle },
                { jumpRightSprite, jump },
                { jumpLeftSprite, jump },
            };
            currentAnimation = new Animation(32, 32, currentFrameWalk, spriteSizeWalk);
        }

        public void DrawCurrentAnimation(SpriteBatch _spriteBatch, Texture2D texture, Animation animation, Rectangle drawingRectangle)
        {
            _spriteBatch.Draw(texture,
                    drawingRectangle,
                    currentAnimation.CreateRectangle(animation.FrameWidth),
                    Color.White);
        }

        public void DrawWalkRight(SpriteBatch _spriteBatch, Rectangle rect) => 
            DrawCurrentAnimation(_spriteBatch, animationSprites["walkRight"], animations[animationSprites["walkRight"]], rect);

        public void DrawWalkLeft(SpriteBatch _spriteBatch, Rectangle rect) => 
            DrawCurrentAnimation(_spriteBatch, animationSprites["walkLeft"], animations[animationSprites["walkLeft"]], rect);

        public void DrawIdleRight(SpriteBatch _spriteBatch, Rectangle rect) =>
            DrawCurrentAnimation(_spriteBatch, animationSprites["idleRight"], animations[animationSprites["idleRight"]], rect);

        public void DrawIdleLeft(SpriteBatch _spriteBatch, Rectangle rect) =>
            DrawCurrentAnimation(_spriteBatch, animationSprites["idleLeft"], animations[animationSprites["idleLeft"]], rect);

        public void DrawJumpRight(SpriteBatch _spriteBatch, Rectangle rect) =>
            DrawCurrentAnimation(_spriteBatch, animationSprites["jumpRight"], animations[animationSprites["jumpRight"]], rect);

        public void DrawJumpLeft(SpriteBatch _spriteBatch, Rectangle rect) =>
            DrawCurrentAnimation(_spriteBatch, animationSprites["jumpLeft"], animations[animationSprites["jumpLeft"]], rect);
    }
}
