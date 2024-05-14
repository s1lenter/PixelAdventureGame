using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PixelAdventure.ObjectsScripts.OtherObjects;
using PixelAdventure.ObjectsScripts.Platforms;
using PixelAdventure.ObjectsScripts.Traps;
using System.Collections.Generic;

namespace PixelAdventure.Scenes.Levels
{
    internal class Level2
    {
        readonly float gravity = 4.5f;

        public Platform[] Platforms { get; private set; }
        public MovingPlatform[] MovingPlatforms { get; private set; }
        public List<Coin> Coins { get; private set; }

        public List<Trap> Traps { get; private set; }

        public List<Enemy> Enemies { get; private set; }

        public Finish FinishObj { get; private set; }

        public Level2(int windowWidth, int windowHeight, SpriteBatch spriteBatch)
        {
            var floorSize = new Point(windowWidth, 180);

            var flyPlatformSize = new Point(90, 30);
            var flyPlatformSize2 = new Point(150, 30);

            var finalPlatformSize2 = new Point(510, 240);
            var movingPlatformSize = new Point(30, 5);

            var floorPlatform = new Platform(floorSize, new Point(0, windowHeight - floorSize.Y));
            var platform = new Platform(new Point(510, 150), new Point(windowWidth - finalPlatformSize2.X, windowHeight - floorSize.Y - finalPlatformSize2.Y + 90));
            var platform1 = new Platform(new Point(450, 30), new Point(windowWidth - finalPlatformSize2.X + 30, windowHeight - floorSize.Y - finalPlatformSize2.Y + 60));
            var platform2 = new Platform(new Point(390, 30), new Point(windowWidth - finalPlatformSize2.X + 60, windowHeight - floorSize.Y - finalPlatformSize2.Y + 30));
            var platform3 = new Platform(new Point(330, 30), new Point(windowWidth - finalPlatformSize2.X + 90, windowHeight - floorSize.Y - finalPlatformSize2.Y));
            var platform4 = new Platform(new Point(270, 30), new Point(windowWidth - finalPlatformSize2.X + 120, windowHeight - floorSize.Y - finalPlatformSize2.Y - 30));
            var finalPlatform = new Platform(new Point(400, 100), new Point(0, 500));

            var platformFly1 = new Platform(flyPlatformSize, new Point(195, windowHeight - floorSize.Y - 80));
            var platformFly2 = new Platform(flyPlatformSize, new Point(350, windowHeight - floorSize.Y - 140));
            var platformFly3 = new Platform(flyPlatformSize, new Point(500, windowHeight - floorSize.Y - 200));
            var platformFly4 = new Platform(flyPlatformSize2, new Point(895, windowHeight - floorSize.Y - 80));
            var platformFly5 = new Platform(flyPlatformSize2, new Point(1260, windowHeight - floorSize.Y - 80));

            var movingPlatform = new MovingPlatform(movingPlatformSize, new Point(1000, windowHeight - floorSize.Y - 100), 1065, 1220, 1f, "horizontal");
            var movingPlatform1 = new MovingPlatform(movingPlatformSize, new Point(1500, windowHeight - floorSize.Y - 290), 1100, 1500, 2f, "horizontal");
            var movingPlatform2 = new MovingPlatform(movingPlatformSize, new Point(670, windowHeight - floorSize.Y - 330), 670, 1070, 2f, "horizontal");
            var movingPlatform3 = new MovingPlatform(movingPlatformSize, new Point(400, windowHeight - floorSize.Y - 360), 410, 650, 3f, "horizontal");

            Platforms = new Platform[]
            {
                floorPlatform, platformFly1, platformFly2, platformFly3, platformFly4, platformFly5,
                platform, platform1, platform2, platform3, platform4, finalPlatform,
                movingPlatform, movingPlatform1, movingPlatform2, movingPlatform3
            };

            MovingPlatforms = new MovingPlatform[]
            {
                movingPlatform, movingPlatform1, movingPlatform2, movingPlatform3
            };

            Enemy enemy = new(new Point(30, 30), new Point(700, windowHeight - 210), 620, 840, 3f, "betweenTraps");
            Enemy enemy1 = new(new Point(30, 30), new Point(900, windowHeight - floorSize.Y - 80 - enemy.Size.Y), 880, 1025, 2f, "onFlyPlatform1");
            Enemy enemy2 = new(new Point(30, 30), new Point(1350, windowHeight - floorSize.Y - 80 - enemy.Size.Y), 1245, 1375, 2f, "onFlyPlatform1");
            Enemy enemy3 = new(new Point(30, 30), new Point(200, 470), 150, 250, 2f, "final1");
            Enemy enemy4 = new(new Point(30, 30), new Point(300, 470), 250, 350, 2f, "final2");

            Enemies = new List<Enemy>()
            {
                enemy, enemy1, enemy2, enemy3, enemy4,
            };

            var coinSize = new Point(30, 30);
            Coins = new List<Coin>
            {
                new(coinSize, new Point(225, windowHeight - floorSize.Y - 110)),
                new(coinSize, new Point(380, windowHeight - floorSize.Y - 170)),
                new(coinSize, new Point(530, windowHeight - floorSize.Y - 230)),

                new(coinSize, new Point(platform.SpawnPoint.X + 220, windowHeight - floorSize.Y - 300)),
                new(coinSize, new Point(platform.SpawnPoint.X + 260, windowHeight - floorSize.Y - 300)),
                new(coinSize, new Point(windowWidth - 120, windowHeight - floorSize.Y - 270)),
                new(coinSize, new Point(windowWidth - 90, windowHeight - floorSize.Y - 240)),
                new(coinSize, new Point(windowWidth - 60, windowHeight - floorSize.Y - 210)),
                new(coinSize, new Point(windowWidth - 30, windowHeight - floorSize.Y - 180)),
            };

            Traps = new List<Trap>();

            AddTraps(165, windowHeight - floorPlatform.Size.Y - 15, 30);

            AddTraps(855, windowHeight - floorPlatform.Size.Y - 15, 36);

            AddTraps(platform.SpawnPoint.X + 140, windowHeight - floorPlatform.Size.Y - 270 - 15, 4);
            AddTraps(platform.SpawnPoint.X + 280, windowHeight - floorPlatform.Size.Y - 270 - 15, 4);

            FinishObj = new Finish(new Point(10, 50), new Point(20, 450));
        }

        private void AddTraps(int x, int y, int count)
        {
            for (int i = 0; i < count; i++)
                Traps.Add(new Trap(new Point(15, 15), new Point(x + 15 * i, y)));
        }

        public GameState Update(GameTime gameTime, PlayerController playerController)
        {
            playerController.Update(gameTime, Platforms, Coins, gravity);

            foreach (var enemy in Enemies)
                enemy.HorizontalMove(gameTime);

            if (playerController.player.CollideWithEnemies(Enemies, Coins))
                return GameState.GameOver;


            if (Keyboard.GetState().IsKeyDown(Keys.P))
                return GameState.Pause;

            foreach (Trap trap in Traps)
                if (trap.Collide(playerController.player.Vector, playerController.player.Size))
                    return GameState.GameOver;

            foreach (var movingPlatform in MovingPlatforms)
            {
                if (movingPlatform.Type == "horizontal")
                    movingPlatform.HorizontalMove(gameTime);
            }

            if (FinishObj.CollideWithFinish(playerController.player.Vector, playerController.player.Size) && Coins.Count == 0)
                return GameState.Level3;

            return GameState.Level2;
        }
    }
}
