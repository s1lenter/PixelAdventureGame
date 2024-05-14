using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework.Media;
using System.ComponentModel.Design;
using System.Threading;
using PixelAdventure.ObjectsScripts.Platforms;
using PixelAdventure.ObjectsScripts.Traps;
using PixelAdventure.ObjectsScripts.OtherObjects;
using PixelAdventure.Scenes.Levels;
using PixelAdventure.Scenes.UI;

namespace PixelAdventure
{
    public class Game1 : Game
    {
        private GameState state;
        private GameState currentLevel;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static int windowHeight;
        public static int windowWidth;

        private Texture2D trapTexture;
        private Texture2D cubeTexture;
        private Texture2D skyBackground;
        private Texture2D backgroundUI;

        private Texture2D playerWalkRight;
        private Texture2D playerWalkLeft;
        private Texture2D playerIdle;
        private Texture2D playerIdleLeft;
        private Texture2D playerJumpRight;
        private Texture2D playerJumpLeft;

        private Texture2D enemyWalkRight;
        private Texture2D enemyWalkLeft;

        private Texture2D coinTexture;

        private Texture2D movingPlatformTexture;

        private Texture2D bulletTexture;
        private Texture2D sawTexture;

        private Texture2D finishTexture;

        private Texture2D level1Example;
        private Texture2D level2Example;
        private Texture2D level3Example;

        private Texture2D select;

        private SpriteFont highlight;
        private SpriteFont text;

        private MapCreator mapCreator;

        private PlayerController playerController;

        private Level1 level1;

        private Level2 level2;

        private Level3 level3;

        private Song song;

        Menu menu;

        GameOver gameOver;

        Pause pause;

        Win win;

        LevelSelector levelSelector;

        private int initializeCounter = 0;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            currentLevel = GameState.Level1;
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

            playerController = new PlayerController();

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

            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;
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

            foreach (var enemy in level2.Enemies)
                enemy.InicializeSprites(enemyWalkRight, enemyWalkLeft);

            foreach (var enemy in level3.Enemies)
                enemy.InicializeSprites(enemyWalkRight, enemyWalkLeft);

            finishTexture = Content.Load<Texture2D>("finish");

            select = Content.Load<Texture2D>("select");

            level1Example = Content.Load<Texture2D>("level1");
            level2Example = Content.Load<Texture2D>("level2");
            level3Example = Content.Load<Texture2D>("level3");

            bulletTexture = Content.Load<Texture2D>("bullet");

            sawTexture = Content.Load<Texture2D>("saw");

            song = Content.Load<Song>("81cebf7e45fdef7");
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.OemPlus))
                MediaPlayer.Volume += 0.01f;
            else if (Keyboard.GetState().IsKeyDown(Keys.OemMinus))
                MediaPlayer.Volume -= 0.01f;
            switch (state)
            {
                case GameState.Menu:
                    state = menu.Update(gameTime, currentLevel);
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
                    if (state == GameState.Level3) Initialize();
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
                    if (state == GameState.Menu)
                    {
                        Initialize(); 
                        currentLevel = GameState.Level1;
                    }
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
                    levelSelector.Draw(_spriteBatch, level1Example, level2Example, level3Example);
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
                _spriteBatch.Draw(trapTexture, new Rectangle(trap.Spawn.X + trap.Size.X, trap.Spawn.Y, trap.Size.X, trap.Size.Y), Color.White);

            _spriteBatch.Draw(finishTexture, new Rectangle(new Point(level1.FinishObj.SpawnPoint.X - 1, level1.FinishObj.SpawnPoint.Y + 5), new Point(50, 50)), Color.White);

            playerController.AnimationController(gameTime);
            playerController.AnimationGo(_spriteBatch);
            _spriteBatch.End();
        }

        private void DrawLevel2(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(skyBackground, new Rectangle(0, 0, windowWidth, windowHeight), Color.White);
            _spriteBatch.DrawString(text, "Score: " + playerController.player.counter.ToString(), new Vector2(0, 0), Color.Black);

            playerController.AnimationController(gameTime);
            playerController.AnimationGo(_spriteBatch);

            foreach (Coin coin in level2.Coins)
                coin.DrawCoin(_spriteBatch, coinTexture);

            foreach (var trap in level2.Traps)
                _spriteBatch.Draw(trapTexture, new Rectangle(trap.Spawn.X + trap.Size.X, trap.Spawn.Y, trap.Size.X, trap.Size.Y), Color.White);

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

            foreach (var saw in level3.Saws)
                _spriteBatch.Draw(sawTexture,
                    new Rectangle((int)saw.Vector.X, (int)saw.Vector.Y, 50, 50),
                    new Rectangle(0, 0, sawTexture.Width, sawTexture.Height), Color.White,
                    level3.MovingSaw.angleRotate,
                    new Vector2(sawTexture.Width / 2, sawTexture.Height / 2), SpriteEffects.FlipVertically, 0);

            foreach (Coin coin in level3.Coins)
                coin.DrawCoin(_spriteBatch, coinTexture);

            foreach (var platform in level3.Platforms)
                if (platform.GetType() != typeof(MovingPlatform))
                    mapCreator.DrawGround(_spriteBatch, cubeTexture, platform.Size, platform.SpawnPoint);

            foreach (var trap in level3.Traps)
                _spriteBatch.Draw(trapTexture, new Rectangle(trap.Spawn.X + trap.Size.X, trap.Spawn.Y, trap.Size.X, trap.Size.Y), Color.White);

            foreach (var turret in level3.Turrets)
            {
                _spriteBatch.Draw(bulletTexture, new Rectangle((int)turret.bulletLeft.Vector.X, (int)turret.bulletLeft.Vector.Y,
                    turret.bulletLeft.Size.X, turret.bulletLeft.Size.Y), Color.White);

                _spriteBatch.Draw(bulletTexture, new Rectangle((int)turret.bulletRight.Vector.X, (int)turret.bulletRight.Vector.Y,
                    turret.bulletRight.Size.X, turret.bulletRight.Size.Y), Color.White);
                _spriteBatch.Draw(cubeTexture, new Rectangle(turret.Spawn, turret.Size), new Rectangle(171, 0, 18, 18), Color.White);
            }

            foreach (Enemy enemy in level3.Enemies)
                enemy.DrawEnemyAnimation(_spriteBatch);

            foreach (var movingPlatform in level3.MovingPlatforms)
                _spriteBatch.Draw(movingPlatformTexture, new Rectangle((int)movingPlatform.Vector.X, (int)movingPlatform.Vector.Y, movingPlatform.Size.X, movingPlatform.Size.Y), Color.White);

            _spriteBatch.Draw(finishTexture, new Rectangle(new Point(level3.FinishObj.SpawnPoint.X - 1, level3.FinishObj.SpawnPoint.Y + 5), new Point(50, 50)), Color.White);

            playerController.AnimationController(gameTime);
            playerController.AnimationGo(_spriteBatch);
            _spriteBatch.End();
        }
    }
}