﻿using PixelAdventure.ObjectsScripts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;

namespace PixelAdventure
{
    public class Game1 : Game
    {
        private GameState state;
        #region
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Player player;

        public static int windowHeight;
        public static int windowWidth;

        private Texture2D floor;
        private Texture2D playerTexture;
        private Texture2D platform;
        private Texture2D platform1;
        private Texture2D trapTexture;
        private Texture2D cubeTexture;
        private Texture2D skyBackground;

        private Point floorSize;
        private Point platformSize;
        private Point trapSize;
        private Point movingPlatformSize;
        private Point cubeSize;

        private float gravity;
        private float fallGravity;

        private Platform[] platforms;

        private Platform bottomPlatform;
        private Platform bottomPlatform1;
        private Platform floorPlatform;
        private Platform finalPlatform;
        private Platform cube;

        private Trap trap;

        public Texture2D playerWalkRight;
        public Texture2D playerWalkLeft;
        public Texture2D playerIdle;
        public Texture2D playerIdleLeft;
        public Texture2D playerJumpRight;
        public Texture2D playerJumpLeft;

        public Texture2D enemyWalkRight;
        public Texture2D enemyWalkLeft;
        public Texture2D enemyDeadRight;
        public Texture2D enemyDeadLeft;

        public Texture2D coinTexture;

        SpriteFont highlight;
        SpriteFont text;

        MovingPlatform movingPlatform;
        Texture2D movingPlatformTexture;

        Enemy enemy;
        Texture2D enemyTexture;
        List<Enemy> enemies;
        #endregion

        private Coin coin;
        private List<Coin> coinList;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        
        protected override void Initialize()
        {

            //_graphics.IsFullScreen = true;
            //_graphics.PreferredBackBufferWidth = 1920;
            //_graphics.PreferredBackBufferHeight = 1080;
            //_graphics.ApplyChanges();

            player = new Player();

            windowHeight = Window.ClientBounds.Height; //480
            windowWidth = Window.ClientBounds.Width; //800

            floorSize = new Point(windowWidth, 100);
            platformSize = new Point(100, 30);
            movingPlatformSize = new Point(30, 5);
            trapSize = new Point(10, 10);

            gravity = 3.75f;
            fallGravity = gravity;

            floorPlatform = new Platform(floorSize, new Point(0, windowHeight - floorSize.Y));

            bottomPlatform = new Platform(platformSize, new Point(windowWidth / 2, windowHeight - floorSize.Y - player.Size.Y));

            bottomPlatform1 = new Platform(platformSize, new Point(windowWidth / 2 + 100, windowHeight - floorSize.Y - player.Size.Y - 30));

            finalPlatform = new Platform(platformSize, new Point(windowWidth / 2 + 300, windowHeight - floorSize.Y - player.Size.Y - 30));

            movingPlatform = new MovingPlatform(movingPlatformSize, new Point(windowWidth / 2 + 100 + platformSize.X, windowHeight - floorSize.Y - bottomPlatform.Size.Y - bottomPlatform1.Size.Y - 10),
                windowWidth / 2 + 100 + platformSize.X, windowWidth / 2 + 100 + platformSize.X + 70);

            enemy = new Enemy(player.Size, new Point(100, windowHeight - 130), 50, 150);

            enemies = new List<Enemy> { enemy };

            platforms = new Platform[] { bottomPlatform1, bottomPlatform, floorPlatform, finalPlatform,movingPlatform };

            trap = new Trap(new Point(100, 10), new Point(windowWidth/2 + 200, windowHeight - floorSize.Y - player.Size.Y + 1 + 20));

            coin = new Coin(new Point(20, 20), new Point(200, windowHeight - floorSize.Y - 20));
            coinList = new List<Coin>() 
            { 
                coin, 
                new (new Point(20,20), new Point(250, windowHeight - floorSize.Y - 20)),
                new (new Point(20,20), new Point(720, windowHeight - floorSize.Y - 20 - platformSize.Y * 2)),
                new (new Point(20,20), new Point(770, windowHeight - floorSize.Y - 20 - platformSize.Y * 2)),
            };

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            floor = Content.Load<Texture2D>("blackFloor");
            playerTexture = Content.Load<Texture2D>("Player");
            platform = Content.Load<Texture2D>("blackFloor");
            platform1 = Content.Load<Texture2D>("blackFloor");
            cubeTexture = Content.Load<Texture2D>("tilemap");

            movingPlatformTexture = Content.Load<Texture2D>("blackFloor");

            enemyTexture = Content.Load<Texture2D>("Owlet_Monster");

            trapTexture = Content.Load<Texture2D>("trap");

            playerWalkRight = Content.Load<Texture2D>("Dude_Monster_Walk_6");
            playerWalkLeft = Content.Load<Texture2D>("Dude_Monster_Walk_Left");
            playerIdle = Content.Load<Texture2D>("Dude_Monster_Idle_4");
            playerIdleLeft = Content.Load<Texture2D>("Dude_Monster_Idle_Left");
            playerJumpRight = Content.Load<Texture2D>("Dude_Monster_Jump_8");
            playerJumpLeft = Content.Load<Texture2D>("Dude_Monster_Jump_Left");

            enemyWalkRight = Content.Load<Texture2D>("Owlet_Monster_Walk_6");
            enemyWalkLeft = Content.Load<Texture2D>("Owlet_Monster_Walk_Left");

            coinTexture = Content.Load<Texture2D>("Coin");

            highlight = Content.Load<SpriteFont>("myText1");
            text = Content.Load<SpriteFont>("File");

            skyBackground = Content.Load<Texture2D>("sky");

            player.InicializeSprites(playerWalkRight, playerWalkLeft, playerIdle, playerIdleLeft, playerJumpRight, playerJumpLeft);

            foreach (var enemy in enemies)
                enemy.InicializeSprites(enemyWalkRight, enemyWalkLeft);
        }

        protected override void Update(GameTime gameTime)
        {
            switch (state)
            {
                //case GameState.LevelCreatorTest:
                //    UpdateLevelCreatorTest(gameTime); 
                //    break;
                case GameState.Menu:
                    player.StartAgain();
                    Initialize();
                    UpdateMenu(gameTime);
                    break;
                case GameState.GamePlay:
                    UpdateGamePlay(gameTime);
                    break;
                case GameState.Pause:
                    UpdatePause(gameTime);
                    break;
                case GameState.GameOver:
                    Initialize();
                    UpdateGameOver(gameTime);
                    break;
            }

            base.Update(gameTime);
        }

        private void UpdatePause(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                state = GameState.Menu;
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                state = GameState.GamePlay;
        }

        //private void UpdateLevelCreatorTest(GameTime gameTime)
        //{

        //}

        #region
        private void UpdateMenu(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                state = GameState.GamePlay;
        }

        private void UpdateGamePlay(GameTime gameTime)
        {
            //if (Player.IsFall)
            //    gravity += 0.2f;
            //else if (!Player.IsFall)
            //    gravity = 3.75f;

            player.Vector.Y += gravity;

            state = player.CollideWithEnemies(enemies);

            if (Keyboard.GetState().IsKeyDown(Keys.P))
                state = GameState.Pause;

            if (trap.CollideWithTrap(player.Vector, player.Size))
            {
                state = GameState.GameOver;
                player.StartAgain();
            }

            movingPlatform.Move(gameTime);

            enemy.Move(gameTime);

            player.Move(gameTime);

            player.CollideWithPlatforms(platforms, gravity, gameTime);

            player.CollideWithCoins(coinList);
        }

        private void UpdateGameOver(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                state = GameState.GamePlay;
        }
        #endregion

        protected override void Draw(GameTime gameTime)
        {

            switch (state)
            {
                //case GameState.LevelCreatorTest:
                //    DrawLevelCreatorTest(gameTime);
                //    break;
                case GameState.Menu:
                    DrawMenu(gameTime);
                    break;
                case GameState.GamePlay:
                    DrawGamePlay(gameTime);
                    break;
                case GameState.Pause:
                    DrawPause(gameTime);
                    break;
                case GameState.GameOver:
                    DrawGameOver(gameTime);
                    break;
            }
            base.Draw(gameTime);
        }

        private void DrawPause(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(skyBackground, new Rectangle(0, 0, windowWidth, windowHeight), Color.White);
            _spriteBatch.DrawString(highlight, "Pause", new Vector2(100, 50), Color.Black);
            _spriteBatch.DrawString(text, "Press space ESC to quit in menu", new Vector2(100, windowHeight - 100), Color.Black);
            _spriteBatch.DrawString(text, "Press space SPACE to continue", new Vector2(100, windowHeight - 70), Color.Black);
            _spriteBatch.End();
        }

        private void DrawLevelCreatorTest(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(skyBackground, new Rectangle(0, 0, windowWidth, windowHeight), Color.White);
            _spriteBatch.End();
        }

        #region
        private void DrawMenu(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(skyBackground, new Rectangle(0, 0, windowWidth, windowHeight), Color.White);
            _spriteBatch.DrawString(highlight, "Pixel Adventure", new Vector2(100, 50), Color.Black);
            _spriteBatch.DrawString(text, "Press SPACE to start", new Vector2(100, windowHeight - 50), Color.Black);
            _spriteBatch.End();
        }

        private void DrawGamePlay(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            
            _spriteBatch.Draw(skyBackground, new Rectangle(0, 0, windowWidth, windowHeight), Color.White);
            _spriteBatch.DrawString(text, "Score: " + player.counter.ToString(), new Vector2(0, 0), Color.Black);
            _spriteBatch.Draw(floor, new Rectangle(floorPlatform.SpawnPoint, floorPlatform.Size), Color.White);

            player.DrawPlayerAnimation(_spriteBatch);

            _spriteBatch.Draw(platform, new Rectangle(bottomPlatform.SpawnPoint, bottomPlatform.Size), Color.White);

            _spriteBatch.Draw(platform, new Rectangle(finalPlatform.SpawnPoint, finalPlatform.Size), Color.White);

            _spriteBatch.Draw(platform1, new Rectangle(bottomPlatform1.SpawnPoint, bottomPlatform1.Size), new Rectangle(0, 0, 18, 18), Color.White);

            for (int i = 0; i < 10; i++)
                _spriteBatch.Draw(trapTexture, new Rectangle(trap.SpawnPoint.X + i * trapSize.X, trap.SpawnPoint.Y, trapSize.X, trapSize.Y), Color.White);

            foreach(Enemy enemy in enemies)
                enemy.DrawEnemyAnimation(_spriteBatch);

            foreach (Coin coin in coinList)
                coin.DrawCoin(_spriteBatch, coinTexture);

            for (int i = 0; i < 3; i++)
                _spriteBatch.Draw(cubeTexture, new Rectangle(bottomPlatform.SpawnPoint.X + 33 * i, bottomPlatform.SpawnPoint.Y, 30, 30), new Rectangle(0, 0, 18, 18), Color.White);

            _spriteBatch.Draw(movingPlatformTexture, new Rectangle((int)movingPlatform.Vector.X, movingPlatform.SpawnPoint.Y, movingPlatform.Size.X, movingPlatform.Size.Y), Color.White);

            _spriteBatch.End();
        }

        private void DrawGameOver(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(skyBackground, new Rectangle(0, 0, windowWidth, windowHeight), Color.White);
            _spriteBatch.DrawString(highlight, "Game over!", new Vector2(100, 50), Color.Black);
            _spriteBatch.DrawString(text, "Press space to start", new Vector2(100, windowHeight - 50), Color.Black);
            _spriteBatch.End();
        }
    }
    #endregion
}