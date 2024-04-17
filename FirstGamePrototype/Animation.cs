using FirstGamePrototype.ObjectsScripts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstGamePrototype
{
    internal class Animation
    {
        private string name;
        private IObject gameObject;
        private Texture2D sprite;
        public int frameHeight { get; set; }
        public int frameWidth { get; set; }
        public Point currentFrame;
        private Point spriteSize;
        private int currentTime;
        private int period;

        
        public Animation(Texture2D sprite, int frameHeight, int frameWidth, Point currentFrame, Point spriteSize, IObject gameObject = null)
        {
            this.gameObject = gameObject;
            this.sprite = sprite;
            this.frameHeight = frameHeight;
            this.frameWidth = frameWidth;
            this.currentFrame = currentFrame;
            this.spriteSize = spriteSize;
            currentTime = 0;
            period = 100;
        }

        public void StartAnimation(GameTime gameTime)
        {
            currentTime += gameTime.ElapsedGameTime.Milliseconds;
            if (currentTime > period)
            {
                currentTime -= period;
                ++currentFrame.X;
                if (currentFrame.X >= spriteSize.X)
                {
                    currentFrame.X = 0;
                    ++currentFrame.Y;
                    if (currentFrame.Y >= spriteSize.Y)
                        currentFrame.Y = 0;
                }
            }
        }

        

        public Rectangle CreateRectangle(int frameWidth)
        {
            return new Rectangle(currentFrame.X * frameWidth, currentFrame.Y * frameHeight, frameWidth, frameHeight);
        }
    }
}
