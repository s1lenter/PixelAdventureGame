using PixelAdventure.ObjectsScripts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelAdventure
{
    internal class Animation
    {
        //private string name;
        //private IObject gameObject;
        public int FrameHeight { get; set; }
        public int FrameWidth { get; set; }
        public Point currentFrame;
        private Point spriteSize;
        private int currentTime;
        private int period;

        
        public Animation(int frameHeight, int frameWidth, Point currentFrame, Point spriteSize, IObject gameObject = null)
        {
            //this.gameObject = gameObject;
            FrameHeight = frameHeight;
            FrameWidth = frameWidth;
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
            return new Rectangle(currentFrame.X * frameWidth, currentFrame.Y * FrameHeight, frameWidth, FrameHeight);
        }
    }
}
