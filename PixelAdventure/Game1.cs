using PixelAdventure.ObjectsScripts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework.Media;
using PixelAdventure.Scenes;
using PixelAdventure.PlayerScripts;
using System.ComponentModel.Design;
using System.Threading;

namespace PixelAdventure
{
    public class Game1 : Game
    {
        private GameState state;
        private GameState currentLevel;
        #region
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static int windowHeight;
        public static int windowWidth;

        private Texture2D trapTexture;
        private Texture2D cubeTexture;
        private Texture2D skyBackground;
        private Texture2D backgroundUI;

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

        private SpriteFont highlight;
        private SpriteFont text;

        public Texture2D movingPlatformTexture;

        MapCreator mapCreator;
        #endregion

        private PlayerController playerController;

        private Level1 level1;

        private Level2 level2;

        private Level3 level3;

        private Texture2D finishTexture;

        private Song song;

        Menu menu;

        GameOver gameOver;

        Pause pause;

        Win win;

        LevelSelector levelSelector;

        private string[] fileNames;

        private Texture2D textures;

        private Texture2D select;

        private Texture2D level1Texture;
        private Texture2D level2Texture;
        private Texture2D level3Texture;

        private int initializeCounter = 0;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            //fileNames = new string[]
            //{
            //    "tilemap",
            //    "blackFloor", "sky","trap", "Coin",
            //    "Dude_Monster_Walk_6", "Dude_Monster_Walk_Left", "Dude_Monster_Idle_4",
            //    "Dude_Monster_Idle_Left", "Dude_Monster_Jump_8", "Dude_Monster_Jump_Left",
            //    "Owlet_Monster_Walk_6", "Owlet_Monster_Walk_Left", "finish", 
            //};

            //textures = new Texture2D[]
            //{

            //};

            currentLevel = GameState.Level3;
        }

        protected override void Initialize()
        {
            initializeCounter++;
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.ApplyChanges();

            windowHeight = Window.ClientBounds.Height;
            windowWidth = Window.ClientBounds.Width;

            playerController = new PlayerController(_spriteBatch);

            mapCreator = new MapCreator();
            

            level1 = new Level1(windowWidth, windowHeight, _spriteBatch);

            level2 = new Level2(windowWidth, windowHeight, _spriteBatch);

            level3 = new Level3(windowWidth, windowHeight, _spriteBatch);

            base.Initialize();

            if (initializeCounter > 1)
                menu = new Menu(highlight, text, backgroundUI, select, 0, 500);
            else
                menu = new Menu(highlight, text, backgroundUI, select, 0, 0);

            gameOver = new GameOver(highlight, text, backgroundUI, select);

            levelSelector = new LevelSelector(highlight, text, backgroundUI, select, 0, 500);

            pause = new Pause(highlight, text, backgroundUI, select);

            win = new Win(highlight, text, backgroundUI);
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
            backgroundUI = Content.Load<Texture2D>("backGroundUI");

            playerController.Viewer.InicializeSprites(playerWalkRight, playerWalkLeft, playerIdle, playerIdleLeft, playerJumpRight, playerJumpLeft);

            finishTexture = Content.Load<Texture2D>("finish");

            select = Content.Load<Texture2D>("select");

            level1Texture = Content.Load<Texture2D>("level1");
            level2Texture = Content.Load<Texture2D>("level2");

            song = Content.Load<Song>("81cebf7e45fdef7");

            //MediaPlayer.Play(song);
            //MediaPlayer.IsRepeating = true;

            foreach (var enemy in level2.Enemies)
                enemy.InicializeSprites(enemyWalkRight, enemyWalkLeft);
        }

        protected override void Update(GameTime gameTime)
        {
            switch (state)
            {
                case GameState.Menu:
                    state = menu.Update(gameTime, currentLevel);
                    //Initialize();
                    if (state == GameState.Quit) Exit();
                    break;
                case GameState.LevelSelector:
                    state = levelSelector.Update(gameTime);
                    if (state == GameState.Menu) Initialize();
                    break;
                case GameState.Level1:
                    currentLevel = GameState.Level1;
                    state = level1.Update(gameTime, playerController);
                    if (state == GameState.Level2) Initialize();
                    break;
                case GameState.Level2:
                    currentLevel = GameState.Level2;
                    state = level2.Update(gameTime, playerController);
                    break;
                case GameState.Level3:
                    currentLevel = GameState.Level3;
                    state = level3.Update(gameTime, playerController);
                    break;
                case GameState.Pause:
                    state = pause.UpdatePause(gameTime, currentLevel);
                    if (state == GameState.Menu)
                    {
                        Initialize();
                        currentLevel = GameState.Level1;
                    }
                    break;
                case GameState.GameOver:
                    state = gameOver.UpdateGameOver(gameTime, currentLevel);
                    if (state == currentLevel) Initialize();
                    if (state == GameState.Menu)
                    {
                        Initialize();
                        currentLevel = GameState.Level1;
                    }
                    break;
                case GameState.Win:
                    state = win.Update();
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            switch (state)
            {
                case GameState.Menu:
                    menu.Draw(gameTime, _spriteBatch);
                    break;
                case GameState.LevelSelector:
                    levelSelector.Draw(gameTime, _spriteBatch, level1Texture, level2Texture);
                    break;
                case GameState.Level1:
                    DrawLevel1(gameTime);
                    break;
                case GameState.Level2:
                    DrawLevel2(gameTime);
                    break;
                case GameState.Level3:
                    DrawLevel3(gameTime);
                    break;
                case GameState.Pause:
                    pause.DrawPause(gameTime, _spriteBatch);
                    break;
                case GameState.GameOver:
                    gameOver.DrawGameOver(gameTime, _spriteBatch);
                    break;
                case GameState.Win:
                    win.Draw(gameTime, _spriteBatch);
                    break;
            }
            base.Draw(gameTime);
        }

        private void DrawLevel1(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(skyBackground, new Rectangle(0, 0, windowWidth, windowHeight), Color.White);
            _spriteBatch.DrawString(text, "Score: " + playerController.player.counter.ToString(), new Vector2(0, 0), Color.Black);

            foreach (Coin coin in level1.Coins)
                coin.DrawCoin(_spriteBatch, coinTexture);

            foreach (var platform in level1.Platforms)
                if (platform.GetType() != typeof(MovingPlatform))
                    mapCreator.DrawGround(_spriteBatch, cubeTexture, platform.Size, platform.SpawnPoint);

            foreach (var trap in level1.Traps)
                _spriteBatch.Draw(trapTexture, new Rectangle(trap.SpawnPoint.X + trap.Size.X, trap.SpawnPoint.Y, trap.Size.X, trap.Size.Y), Color.White);

            _spriteBatch.Draw(finishTexture, new Rectangle(new Point(level1.FinishObj.SpawnPoint.X - 1, level1.FinishObj.SpawnPoint.Y + 5), new Point(50, 50)), Color.White);

            playerController.AnimationController(gameTime);
            playerController.AnimationGo(_spriteBatch, new Rectangle((int)playerController.player.Vector.X, (int)playerController.player.Vector.Y - 10, playerController.player.Size.X + 10, playerController.player.Size.Y + 10));
            _spriteBatch.End();
        }

        private void DrawLevel2(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(skyBackground, new Rectangle(0, 0, windowWidth, windowHeight), Color.White);
            _spriteBatch.DrawString(text, "Score: " + playerController.player.counter.ToString(), new Vector2(0, 0), Color.Black);

            playerController.AnimationController(gameTime);
            playerController.AnimationGo(_spriteBatch, new Rectangle((int)playerController.player.Vector.X, (int)playerController.player.Vector.Y - 10, playerController.player.Size.X + 10, playerController.player.Size.Y + 10));

            foreach (Coin coin in level2.Coins)
                coin.DrawCoin(_spriteBatch, coinTexture);

            foreach (var trap in level2.Traps)
                _spriteBatch.Draw(trapTexture, new Rectangle(trap.SpawnPoint.X + trap.Size.X, trap.SpawnPoint.Y, trap.Size.X, trap.Size.Y), Color.White);

            foreach (var platform in level2.Platforms)
                if (platform.GetType() != typeof(MovingPlatform))
                    mapCreator.DrawGround(_spriteBatch, cubeTexture, platform.Size, platform.SpawnPoint);

            foreach (var movingPlatform in level2.MovingPlatforms)
                _spriteBatch.Draw(movingPlatformTexture, new Rectangle((int)movingPlatform.Vector.X, (int)movingPlatform.Vector.Y, movingPlatform.Size.X, movingPlatform.Size.Y), Color.White);

            foreach (Enemy enemy in level2.Enemies)
                enemy.DrawEnemyAnimation(_spriteBatch);

            _spriteBatch.Draw(finishTexture, new Rectangle(new Point(level2.FinishObj.SpawnPoint.X - 1, level2.FinishObj.SpawnPoint.Y + 5), new Point(50, 50)), Color.White);


            _spriteBatch.End();
        }

        private void DrawLevel3(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(skyBackground, new Rectangle(0, 0, windowWidth, windowHeight), Color.White);
            _spriteBatch.DrawString(text, "Score: " + playerController.player.counter.ToString(), new Vector2(0, 0), Color.Black);

            foreach (Coin coin in level3.Coins)
                coin.DrawCoin(_spriteBatch, coinTexture);

            foreach (var platform in level3.Platforms)
                if (platform.GetType() != typeof(MovingPlatform))
                    mapCreator.DrawGround(_spriteBatch, cubeTexture, platform.Size, platform.SpawnPoint);

            foreach (var trap in level3.Traps)
                _spriteBatch.Draw(trapTexture, new Rectangle(trap.SpawnPoint.X + trap.Size.X, trap.SpawnPoint.Y, trap.Size.X, trap.Size.Y), Color.White);

            _spriteBatch.Draw(finishTexture, new Rectangle(new Point(level3.FinishObj.SpawnPoint.X - 1, level3.FinishObj.SpawnPoint.Y + 5), new Point(50, 50)), Color.White);

            playerController.AnimationController(gameTime);
            playerController.AnimationGo(_spriteBatch, new Rectangle((int)playerController.player.Vector.X, (int)playerController.player.Vector.Y - 10, playerController.player.Size.X + 10, playerController.player.Size.Y + 10));
            _spriteBatch.End();
        }
    }
}