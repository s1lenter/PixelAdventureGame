using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PixelAdventure.ObjectsScripts
{
    internal class GamePlay
    {
        private int windowWidth;
        private int windowHeight;

        private PlayerController playerController; 
        public Platform[] platforms { get; private set; }
        public MovingPlatform[] movingPlatforms { get; private set; }
        public List<Enemy> enemies { get; private set; }
        public List<Coin> coins { get; private set; }

        public GamePlay(int windowWidth, int windowHeight, SpriteBatch spriteBatch)
        {
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;
            playerController = new PlayerController(spriteBatch);

            var floorSize = new Point(windowWidth, 180);
            var platformSize = new Point(100, 30);
            var movingPlatformSize = new Point(30, 5);
            var trapSize = new Point(10, 10);

            var floorPlatform = new Platform(floorSize, new Point(0, windowHeight - floorSize.Y));

            var bottomPlatform = new Platform(platformSize, new Point(windowWidth / 2, windowHeight - floorSize.Y - 30));

            var bottomPlatform2 = new Platform(platformSize, new Point(windowWidth / 2 - 200, windowHeight - floorSize.Y - 30));

            var bottomPlatform1 = new Platform(platformSize, new Point(windowWidth / 2 + 100, windowHeight - floorSize.Y - 60));

            var finalPlatform = new Platform(platformSize, new Point(windowWidth / 2 + 300, windowHeight - floorSize.Y - 170));

            var movingPlatform = new MovingPlatform(movingPlatformSize, new Point(windowWidth / 2 + 100 + platformSize.X, windowHeight - floorSize.Y - 70),
                windowWidth / 2 + 200, windowWidth / 2 + 270);

            platforms = new Platform[]
            {
                movingPlatform, bottomPlatform2, bottomPlatform1, bottomPlatform, floorPlatform, finalPlatform,  
            };

            movingPlatforms = new MovingPlatform[]
            {
                movingPlatform
            };

            enemies = new List<Enemy>
            {

            };
            var coin = new Coin(new Point(20, 20), new Point(200, windowHeight - floorSize.Y - 20));
            coins = new List<Coin>
            {
                coin,
                //new (new Point(20, 20), new Point(200, windowHeight - floorSize.Y - 20)),
                new (new Point(20,20), new Point(250, windowHeight - floorSize.Y - 20)),
                new (new Point(20,20), new Point(720, windowHeight - floorSize.Y - 20 - platformSize.Y * 2)),
                new (new Point(20,20), new Point(770, windowHeight - floorSize.Y - 20 - platformSize.Y * 2)),
            };
        }
    }
}
