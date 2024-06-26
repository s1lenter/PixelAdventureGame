﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelAdventure.ObjectsScripts
{
    internal class Animation
    {
        public int FrameHeight { get; private set; }
        public int FrameWidth { get; private set; }
        private Point currentFrame;
        private Point spriteSize;
        private int currentTime;
        private int period;


        public Animation(int frameHeight, int frameWidth, Point currentFrame, Point spriteSize)
        {
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
