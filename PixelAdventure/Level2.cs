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

        public List<Enemy> enemies { get; private set; }

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
            var flyPlatformSize2 = new Point(150, 30);

            var finalPlatformSize2 = new Point(510, 240);
            var movingPlatformSize = new Point(30, 5);
            var trapSize = new Point(10, 10);

            var floorPlatform = new Platform(floorSize, new Point(0, windowHeight - floorSize.Y));
            var platform = new Platform(new Point(510, 240), new Point(windowWidth - finalPlatformSize2.X, windowHeight - floorSize.Y - finalPlatformSize2.Y));
            var finalPlatform = new Platform(new Point(400, 100), new Point(0, 500));

            var platformFly1 = new Platform(flyPlatformSize, new Point(195, windowHeight - floorSize.Y - 80));
            var platformFly2 = new Platform(flyPlatformSize, new Point(350, windowHeight - floorSize.Y - 140));
            var platformFly3 = new Platform(flyPlatformSize, new Point(500, windowHeight - floorSize.Y - 200));
            var platformFly4 = new Platform(flyPlatformSize2, new Point(895, windowHeight - floorSize.Y - 80));
            var platformFly5 = new Platform(flyPlatformSize2, new Point(1260, windowHeight - floorSize.Y - 80));

            var movingPlatform = new MovingPlatform(movingPlatformSize, new Point(1000, windowHeight - floorSize.Y - 100), 1065, 1220, 1f, "horizontal");
            var movingPlatform1 = new MovingPlatform(movingPlatformSize, new Point(1340, windowHeight - floorSize.Y - 290), 1030, 1340, 2f, "horizontal");
            var movingPlatform2 = new MovingPlatform(movingPlatformSize, new Point(670, windowHeight - floorSize.Y - 330), 670, 980, 2f, "horizontal");
            var movingPlatform3 = new MovingPlatform(movingPlatformSize, new Point(400, windowHeight - floorSize.Y - 360), 410, 650, 4f, "horizontal");

            platforms = new Platform[]
            {
                floorPlatform, platformFly1, platformFly2, platformFly3, platformFly4, platformFly5, platform, finalPlatform,
                movingPlatform, movingPlatform1, movingPlatform2, movingPlatform3
            };

            movingPlatforms = new MovingPlatform[]
            {
                movingPlatform, movingPlatform1, movingPlatform2, movingPlatform3
            };

            Enemy enemy = new(new Point(30, 30), new Point(700, windowHeight - 210), 620, 840, 3f, "betweenTraps");
            Enemy enemy1 = new(new Point(30, 30), new Point(900, windowHeight - floorSize.Y - 80 - enemy.Size.Y), 880, 1025, 2f, "onFlyPlatform1");
            Enemy enemy2 = new(new Point(30, 30), new Point(1350, windowHeight - floorSize.Y - 80 - enemy.Size.Y), 1245, 1375, 2f, "onFlyPlatform1");

            enemies = new List<Enemy>()
            {
                enemy, enemy1, enemy2
            };

            var coinSize = new Point(30, 30);
            coins = new List<Coin>
            {
                new(coinSize, new Point(0, windowHeight-floorSize.Y-coinSize.Y)),
            };

            traps = new List<Trap>();

            AddTraps(165, windowHeight - floorPlatform.Size.Y - 15,30);

            AddTraps(855, windowHeight - floorPlatform.Size.Y - 15, 36);

            AddTraps(platform.SpawnPoint.X + 75, windowHeight - floorPlatform.Size.Y - finalPlatformSize2.Y - 15, 4);
            AddTraps(platform.SpawnPoint.X + 75, windowHeight - floorPlatform.Size.Y - finalPlatformSize2.Y - 15, 4);
            AddTraps(platform.SpawnPoint.X + 195, windowHeight - floorPlatform.Size.Y - finalPlatformSize2.Y - 15, 4);
        }

        private void AddTraps(int x, int y, int count)
        {
            for (int i = 0; i < count; i++)
                traps.Add(new Trap(new Point(15, 15), new Point(x + 15 * i, y)));
        }
    }
}
