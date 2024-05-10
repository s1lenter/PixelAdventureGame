using PixelAdventure.ObjectsScripts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework.Media;

namespace PixelAdventure
{
    public class Game1 : Game
    {
        private GameState state;
        #region
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static int windowHeight;
        public static int windowWidth;

        private Texture2D trapTexture;
        private Texture2D cubeTexture;
        private Texture2D skyBackground;

        private float gravity;

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

        public Texture2D movingPlatformTexture;

        MapCreator mapCreator;
        #endregion

        private PlayerController playerController;

        private Level1 level1;

        private Level2 level2;

        private Texture2D finishTexture;

        private Song song;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        
        protected override void Initialize()
        {
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.ApplyChanges();

            windowHeight = Window.ClientBounds.Height; //480
            windowWidth = Window.ClientBounds.Width; //800

            gravity = 4.5f;

            playerController = new PlayerController(_spriteBatch);

            mapCreator = new MapCreator();

            level1 = new Level1(windowWidth, windowHeight, _spriteBatch);

            level2 = new Level2(windowWidth, windowHeight, _spriteBatch);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            cubeTexture = Content.Load<Texture2D>("tilemap");

            movingPlatformTexture = Content.Load<Texture2D>("blackFloor");

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

            playerController.Viewer.InicializeSprites(playerWalkRight, playerWalkLeft, playerIdle, playerIdleLeft, playerJumpRight, playerJumpLeft);

            finishTexture = Content.Load<Texture2D>("finish");

            song = Content.Load<Song>("81cebf7e45fdef7");

            //MediaPlayer.Play(song);
            //MediaPlayer.IsRepeating = true;

            foreach (var enemy in level2.enemies)
                enemy.InicializeSprites(enemyWalkRight, enemyWalkLeft);
        }

        protected override void Update(GameTime gameTime)
        {
            switch (state)
            {
                case GameState.Menu:
                    UpdateMenu(gameTime);
                    break;
                case GameState.Level1:
                    UpdateLevel1(gameTime);
                    break;
                case GameState.Level2:
                    UpdateLevel2(gameTime);
                    break;
                case GameState.Pause:
                    UpdatePause(gameTime);
                    break;
                case GameState.GameOver:
                    UpdateGameOver(gameTime);
                    break;
                case GameState.Win:
                    UpdateWin(gameTime);
                    break;
            }

            base.Update(gameTime);
        }

        private void UpdateLevel2(GameTime gameTime)
        {
            playerController.Update(gameTime, level2.platforms, level2.coins, gravity);

            foreach (var enemy in level2.enemies)
                enemy.HorizontalMove(gameTime);

            state = playerController.player.CollideWithEnemies(level2.enemies);

            foreach (Trap trap in level2.traps)
                if (trap.CollideWithTrap(playerController.player.Vector, playerController.player.Size))
                    state = GameState.GameOver;

            foreach (var movingPlatform in level2.movingPlatforms)
            {
                if (movingPlatform.Type == "horizontal")
                    movingPlatform.HorizontalMove(gameTime);
            }
        }

        private void UpdateWin(GameTime gameTime)
        {
            
        }

        private void UpdatePause(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                state = GameState.Menu;
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                state = GameState.Level2;
        }

        private void UpdateLevel1(GameTime gameTime)
        {
            
            playerController.Update(gameTime, level1.platforms, level1.coins, gravity);

            foreach (Trap trap in level1.traps)
                if (trap.CollideWithTrap(playerController.player.Vector, playerController.player.Size))
                    state = GameState.GameOver;

            if (Keyboard.GetState().IsKeyDown(Keys.P))
                state = GameState.Pause;

            if (level1.finish.CollideWithFinish(playerController.player.Vector, playerController.player.Size))
            {
                state = GameState.GamePlay;
                Initialize();
            }
        }

        #region
        private void UpdateMenu(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                state = GameState.Level2;
                Initialize();
            }
        }

        private void UpdateGameOver(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                state = GameState.Level2;
                Initialize();
            }
        }
        #endregion

        protected override void Draw(GameTime gameTime)
        {
            switch (state)
            {
                case GameState.Menu:
                    DrawMenu(gameTime);
                    break;
                case GameState.Level1:
                    DrawLevel1(gameTime);
                    break;
                case GameState.Level2:
                    DrawLevel2(gameTime);
                    break;
                case GameState.Pause:
                    DrawPause(gameTime);
                    break;
                case GameState.GameOver:
                    DrawGameOver(gameTime);
                    break;
                case GameState.Win:
                    DrawWin(gameTime);
                    break;
            }
            base.Draw(gameTime);
        }

        private void DrawLevel2(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(skyBackground, new Rectangle(0, 0, windowWidth, windowHeight), Color.White);
            _spriteBatch.DrawString(text, "Score: " + playerController.player.counter.ToString(), new Vector2(0, 0), Color.Black);

            playerController.AnimationController(gameTime);
            playerController.AnimationGo(_spriteBatch, new Rectangle((int)playerController.player.Vector.X, (int)playerController.player.Vector.Y - 10, playerController.player.Size.X + 10, playerController.player.Size.Y + 10));

            foreach (Coin coin in level2.coins)
                coin.DrawCoin(_spriteBatch, coinTexture);

            foreach (var trap in level2.traps)
                _spriteBatch.Draw(trapTexture, new Rectangle(trap.SpawnPoint.X + trap.Size.X, trap.SpawnPoint.Y, trap.Size.X, trap.Size.Y), Color.White);

            foreach (var platform in level2.platforms)
                if (platform.GetType() != typeof(MovingPlatform))
                    mapCreator.DrawTexture(_spriteBatch, cubeTexture, platform.Size, platform.SpawnPoint);

            foreach (var movingPlatform in level2.movingPlatforms)
                _spriteBatch.Draw(movingPlatformTexture, new Rectangle((int)movingPlatform.Vector.X, (int)movingPlatform.Vector.Y, movingPlatform.Size.X, movingPlatform.Size.Y), Color.White);

            foreach (Enemy enemy in level2.enemies)
                enemy.DrawEnemyAnimation(_spriteBatch);

            _spriteBatch.End();
        }

        private void DrawWin(GameTime gameTime)
        {
            throw new NotImplementedException();
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

        private void DrawLevel1(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(skyBackground, new Rectangle(0, 0, windowWidth, windowHeight), Color.White);
            _spriteBatch.DrawString(text, "Score: " + playerController.player.counter.ToString(), new Vector2(0, 0), Color.Black);

            foreach (Coin coin in level1.coins)
                coin.DrawCoin(_spriteBatch, coinTexture);

            foreach (var platform in level1.platforms)
                if (platform.GetType() != typeof(MovingPlatform))
                    mapCreator.DrawTexture(_spriteBatch, cubeTexture, platform.Size, platform.SpawnPoint);

            foreach (var trap in level1.traps)
                _spriteBatch.Draw(trapTexture, new Rectangle(trap.SpawnPoint.X + trap.Size.X, trap.SpawnPoint.Y, trap.Size.X, trap.Size.Y), Color.White);

            _spriteBatch.Draw(finishTexture, new Rectangle(new Point(level1.finish.SpawnPoint.X - 1, level1.finish.SpawnPoint.Y + 5), new Point(50,50)), Color.White);

            playerController.AnimationController(gameTime);
            playerController.AnimationGo(_spriteBatch, new Rectangle((int)playerController.player.Vector.X, (int)playerController.player.Vector.Y - 10, playerController.player.Size.X + 10, playerController.player.Size.Y + 10));
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