using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PixelAdventure.ObjectsScripts
{
    internal class Level1
    {
        private float gravity = 4.5f;

        private int windowWidth;
        private int windowHeight;

        //private PlayerController playerController;
        public Platform[] platforms { get; private set; }
        public MovingPlatform[] movingPlatforms { get; private set; }
        //public List<Enemy> enemies { get; private set; }
        public List<Coin> coins { get; private set; }

        public List<Trap> traps { get; private set; }

        public Finish finish { get; private set; }

        public Level1(int windowWidth, int windowHeight, SpriteBatch spriteBatch)
        {
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;
            //playerController = new PlayerController(spriteBatch);

            var floorSize = new Point(windowWidth, 180);
            var platformSize = new Point(120, 30);

            var platformSize2 = new Point(120, 120);

            var flyPlatformSize = new Point(90, 30);

            var finalPlatformSize2 = new Point(450, 120);
            var movingPlatformSize = new Point(30, 5);
            var trapSize = new Point(10, 10);

            var floorPlatform = new Platform(floorSize, new Point(0, windowHeight - floorSize.Y));

            var platform1 = new Platform(platformSize, new Point(300, windowHeight - floorSize.Y - platformSize.Y));
            var platform2 = new Platform(platformSize, new Point(510, windowHeight - floorSize.Y - platformSize.Y));
            var platform3 = new Platform(platformSize, new Point(720, windowHeight - floorSize.Y - platformSize.Y));
            var platform4 = new Platform(platformSize2, new Point(windowWidth / 2 - 120, windowHeight - floorSize.Y - platformSize2.Y));
            var finalPlatform = new Platform(finalPlatformSize2, new Point(windowWidth - finalPlatformSize2.X + 30, windowHeight - floorSize.Y - finalPlatformSize2.Y));

            var platformFly1 = new Platform(flyPlatformSize, new Point(windowWidth / 2 + 60, windowHeight - floorSize.Y - platformSize.Y - platformSize2.Y));
            var platformFly2 = new Platform(flyPlatformSize, new Point(windowWidth / 2 + 120 + flyPlatformSize.X, windowHeight - floorSize.Y - platformSize.Y - platformSize2.Y));
            var platformFly3 = new Platform(flyPlatformSize, new Point(windowWidth / 2 + 180 + flyPlatformSize.X * 2, windowHeight - floorSize.Y - platformSize.Y - platformSize2.Y));

            platforms = new Platform[]
            {
                platform1, platform2, platform4, platform3, finalPlatform, platformFly1, platformFly2, platformFly3, floorPlatform, /*movingPlatform*/
            };

            var coinSize = new Point(30,30);

            coins = new List<Coin>
            {
                new(coinSize, new Point(150, windowHeight-floorSize.Y-coinSize.Y)),
                new(coinSize, new Point(340, windowHeight-floorSize.Y-coinSize.Y-platform1.Size.Y)),
                new(coinSize, new Point(550, windowHeight-floorSize.Y-coinSize.Y-platform1.Size.Y)),
                new(coinSize, new Point(1050, windowHeight-floorSize.Y-coinSize.Y-platform4.Size.Y-platform1.Size.Y)),
                new(coinSize, new Point(1200, windowHeight-floorSize.Y-coinSize.Y-platform4.Size.Y-platform1.Size.Y)),
                new(coinSize, new Point(1350, windowHeight-floorSize.Y-coinSize.Y-platform4.Size.Y-platform1.Size.Y)),
            };

            traps = new List<Trap>();

            finish = new Finish(new Point(10, 50), new Point(1700, windowHeight - floorSize.Y - finalPlatformSize2.Y - 50));

            AddTraps(945, windowHeight - floorPlatform.Size.Y - 15, 36);
            AddTraps(405, windowHeight - floorPlatform.Size.Y - 15, 6);
            AddTraps(615, windowHeight - floorPlatform.Size.Y - 15, 6);
        }

        private void AddTraps(int x, int y, int count)
        {
            for (int i = 0; i < count; i++)
                traps.Add(new Trap(new Point(15, 15), new Point(x + 15 * i, y)));
        }

        public GameState UpdateLevel1(GameTime gameTime, PlayerController playerController)
        {
            playerController.Update(gameTime, platforms, coins, gravity);

            foreach (Trap trap in traps)
                if (trap.CollideWithTrap(playerController.player.Vector, playerController.player.Size))
                    return GameState.GameOver;

            if (Keyboard.GetState().IsKeyDown(Keys.P))
                return GameState.Pause;

            if (finish.CollideWithFinish(playerController.player.Vector, playerController.player.Size))
                return GameState.Level2;

            return GameState.Level1;
        }
    }
}
