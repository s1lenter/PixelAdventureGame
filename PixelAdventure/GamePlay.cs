using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace PixelAdventure.ObjectsScripts
{
    internal class GamePlay
    {
        private int windowWidth;
        private int windowHeight;

        private Player player; 
        public Platform[] platforms { get; private set; }
        public MovingPlatform[] movingPlatforms { get; private set; }
        private Enemy[] enemies;
        private Coin[] coins;

        public GamePlay(int windowWidth, int windowHeight)
        {
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;
            player = new Player();
            platforms = new Platform[]
            {
                new (new Point(windowWidth, 100), new Point(0, windowHeight - 100)),
                new (new Point(100, 30), new Point(windowWidth / 2, windowHeight - 130)),
                new (new Point(100, 30), new Point(windowWidth / 2 + 100, windowHeight - 160)),
                new (new Point(100, 30), new Point(windowWidth / 2 + 300, windowHeight - 160)),
                new MovingPlatform(new Point(30, 5), new Point(windowWidth / 2 + 200, windowHeight - 170),
                    windowWidth / 2 + 200, windowWidth / 2 + 270),
            };

            movingPlatforms = new MovingPlatform[]
            {
                new (new Point(30, 5), new Point(windowWidth / 2 + 200, windowHeight - 170),
                    windowWidth / 2 + 200, windowWidth / 2 + 270),
            };
        }
    }
}
