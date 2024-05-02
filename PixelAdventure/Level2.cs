using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PixelAdventure.ObjectsScripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelAdventure
{
    internal class Level2
    {
        private int windowHeight;
        private int windowWidth;

        private PlayerController playerController;
        public Platform[] platforms { get; private set; }
        public MovingPlatform[] movingPlatforms { get; private set; }
        //public List<Enemy> enemies { get; private set; }
        public List<Coin> coins { get; private set; }

        public List<Trap> traps { get; private set; }

        public Finish finish { get; private set; }

        public Level2(int windowWidth, int windowHeight, SpriteBatch spriteBatch)
        {
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;
            playerController = new PlayerController(spriteBatch);

            var floorSize = new Point(windowWidth, 180);
            var platformSize = new Point(120, 30);

            var platformSize2 = new Point(120, 120);

            var flyPlatformSize = new Point(90, 30);

            var finalPlatformSize2 = new Point(450, 120);
            var movingPlatformSize = new Point(30, 5);
            var trapSize = new Point(10, 10);

            var floorPlatform = new Platform(floorSize, new Point(0, windowHeight - floorSize.Y));

            platforms = new Platform[]
            {
                floorPlatform,
            };
        }
    }
}
