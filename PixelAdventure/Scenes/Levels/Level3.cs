using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixelAdventure.ObjectsScripts.Traps;
using PixelAdventure.ObjectsScripts.Platforms;
using PixelAdventure.ObjectsScripts.OtherObjects;

namespace PixelAdventure.Scenes.Levels
{
    internal class Level3
    {
        readonly float gravity = 4.5f;
        public Platform[] Platforms { get; private set; }
        public MovingPlatform[] MovingPlatforms { get; private set; }
        public List<Coin> Coins { get; private set; }

        public List<Trap> Traps { get; private set; }

        public List<Saw> Saws { get; private set; }

        public Finish FinishObj { get; private set; }

        public List<Turret> Turrets { get; private set; }

        public List<Enemy> Enemies { get; private set; }
        public Saw MovingSaw { get; private set; }
        public Saw MovingSaw1 { get; private set; }

        public Level3(int windowWidth, int windowHeight, SpriteBatch spriteBatch)
        {
            var floorSize = new Point(windowWidth, 180);
            var flyPlatformSize = new Point(60, 30);

            var movingPlatformSize = new Point(30, 5);

            var floorPlatform = new Platform(floorSize, new Point(0, windowHeight - floorSize.Y));

            var platform1 = new Platform(new Point(300, 30), new Point(windowWidth - 300, 300));

            var platform2 = new Platform(new Point(300, 30), new Point(900, 300));
            var platform3 = new Platform(new Point(400, 30), new Point(400, 300));
            var platform4 = new Platform(new Point(200, 30), new Point(100, 550));
            var platform5 = new Platform(new Point(600, 30), new Point(400, 550));
            var platform6 = new Platform(new Point(100, 30), new Point(500, 500));
            var platform7 = new Platform(new Point(100, 30), new Point(1700, 550));
            var platform8 = new Platform(new Point(100, 30), new Point(1850, 550));
            var platform9 = new Platform(new Point(1500, 500), new Point(500, windowHeight - floorSize.Y - 70));

            var platform10 = new Platform(new Point(100, 30), new Point(1200, windowHeight - floorSize.Y - 160));
            var platform11 = new Platform(new Point(100, 30), new Point(1300, windowHeight - floorSize.Y - 130));
            var platform12 = new Platform(new Point(100, 30), new Point(1400, windowHeight - floorSize.Y - 100));

            var platformFly1 = new Platform(flyPlatformSize, new Point(1100, 550));
            var platformFly2 = new Platform(flyPlatformSize, new Point(1250, 550));
            var platformFly3 = new Platform(flyPlatformSize, new Point(1400, 550));

            var movingPlatform = new MovingPlatform(movingPlatformSize, new Point(1220, 290), 1220, 1570, 2f);
            var movingPlatform1 = new MovingPlatform(movingPlatformSize, new Point(1500, 540), 1480, 1650, 2f);

            var turret = new Turret(new Point(30, 30), new Point(1000, 270));
            var turret2 = new Turret(new Point(30, 30), new Point(1650, windowHeight - floorSize.Y - 100));

            Turrets = new List<Turret> { turret, turret2 };

            Platforms = new Platform[]
            {
                platform1, platform2, platform3, platform4, platform5, platform6, platform7, platform8, platform9,
                platform10, platform11, platform12,
                platformFly1, platformFly2, platformFly3, floorPlatform, turret, turret2,
                movingPlatform, movingPlatform1,
            };

            MovingPlatforms = new MovingPlatform[]
            {
                movingPlatform, movingPlatform1,
            };

            var coinSize = new Point(30, 30);

            Coins = new List<Coin>
            {
                new(coinSize, new Point(1500, 150)),
                new(coinSize, new Point(1400, 150)),
                new(coinSize, new Point(1300, 150)),
                new(coinSize, new Point(1000, 240)),
                new(coinSize, new Point(770, 270)),
                new(coinSize, new Point(400, 270)),
                new(coinSize, new Point(835, 520)),
                new(coinSize, new Point(1115, 520)),
                new(coinSize, new Point(1265, 520)),
                new(coinSize, new Point(1415, 520)),
                new(coinSize, new Point(1870, 520)),
            };

            Enemy enemy = new(new Point(30, 30), new Point(500, 270), 500, 665, 3f);
            Enemy enemy1 = new(new Point(30, 30), new Point(470, 470), 500, 570, 2f);
            Enemy enemy2 = new(new Point(30, 30), new Point(470, 800), 950, 1150, 3f);

            Enemies = new List<Enemy>()
            {
                enemy, enemy1, enemy2,
            };

            Traps = new List<Trap>();

            AddTraps(-15, windowHeight - floorPlatform.Size.Y - 15, 33);

            AddTraps(1430, 785, 3);
            AddTraps(1330, 755, 3);
            AddTraps(1230, 725, 3);

            AddTraps(680, 285, 5);

            AddTraps(420, 285, 5);

            AddTraps(420, 535, 15);

            FinishObj = new Finish(new Point(10, 50), new Point(600, windowHeight - floorSize.Y - 70 - 50));

            var sawSize = new Point(50, 50);
            MovingSaw = new Saw(sawSize, new Point(810, 720), 2, 720, 810);
            MovingSaw1 = new Saw(sawSize, new Point(900, 810), 2, 720, 810);
            var saw1 = new Saw(sawSize, new Point(1700, 300), 0, 0, 0);
            var saw2 = new Saw(sawSize, new Point(800, 550), 0, 0, 0);
            var saw3 = new Saw(sawSize, new Point(900, 550), 0, 0, 0);
            var saw4 = new Saw(sawSize, new Point(1700, 300), 0, 0, 0);
            var saw5 = new Saw(sawSize, new Point(1325, 830), 0, 0, 0);
            var saw6 = new Saw(sawSize, new Point(1375, 830), 0, 0, 0);
            var saw7 = new Saw(sawSize, new Point(1275, 830), 0, 0, 0);
            var saw8 = new Saw(sawSize, new Point(1225, 830), 0, 0, 0);
            Saws = new List<Saw> { saw1, saw2, saw3, saw4, MovingSaw, MovingSaw1, saw5, saw6, saw7, saw8 };
        }

        private void AddTraps(int x, int y, int count)
        {
            for (int i = 0; i < count; i++)
                Traps.Add(new Trap(new Point(15, 15), new Point(x + 15 * i, y)));
        }

        private bool isStart = true;

        public GameState Update(GameTime gameTime, PlayerController playerController)
        {
            playerController.Update(Platforms, Coins, gravity);

            if (playerController.player.Vector.Y > 800 && isStart == true)
            {
                playerController.player.Vector.Y = 200;
                playerController.player.Vector.X = 1850;
                playerController.player.GoLeft = true;
                isStart = false;
            }

            foreach (Trap trap in Traps)
                if (trap.Collide(playerController.player.Vector, playerController.player.Size))
                    return GameState.GameOver;

            foreach (var turret in Turrets)
            {
                turret.Shoot();
                if (turret.bulletLeft.Collide(playerController.player.Vector, playerController.player.Size) ||
                    turret.bulletRight.Collide(playerController.player.Vector, playerController.player.Size))
                    return GameState.GameOver;
            }

            foreach (Saw saw in Saws)
            {
                saw.Move();
                if (saw.Collide(playerController.player.Vector, playerController.player.Size))
                    return GameState.GameOver;
            }

            foreach (var enemy in Enemies)
                enemy.HorizontalMove(gameTime);

            if (playerController.player.CollideWithEnemies(Enemies, Coins))
                return GameState.GameOver;

            foreach (var movingPlatform in MovingPlatforms)
                movingPlatform.HorizontalMove(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.P))
                return GameState.Pause;

            if (FinishObj.CollideWithFinish(playerController.player.Vector, playerController.player.Size) && Coins.Count == 0)
                return GameState.Win;

            return GameState.Level3;
        }
    }
}
