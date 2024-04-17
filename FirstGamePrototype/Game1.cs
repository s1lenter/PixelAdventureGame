using PixelAdventure.ObjectsScripts;
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
        #region
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

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

        private Platform[] platforms;

        private Platform bottomPlatform;
        private Platform bottomPlatform1;
        private Platform floorPlatform;
        private Platform finalPlatform;
        private Platform cube;

        private Trap trap;

        public int frameWidth = 32;
        public int frameHeight = 32;
        public Point currentFrame = new Point(0, 0);
        private Point spriteSize = new Point(6,0);

        public Point currentFrameIdle = new Point(0, 0);
        private Point spriteSizeIdle = new Point(4, 0);

        public Texture2D playerWalkRight;
        public Texture2D playerWalkLeft;
        public Texture2D playerIdle;
        public Texture2D playerIdleLeft;

        private Animation playerWalkAnimation;
        private Animation playerIdleAnimation;
        #endregion
        SpriteFont highlight;
        SpriteFont text;

        MovingPlatform movingPlatform;
        Texture2D movingPlatformTexture;

        private Dictionary<string, Texture2D> animationSprites;

        private Dictionary<Texture2D, Animation> animations;

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
            

            windowHeight = Window.ClientBounds.Height;
            windowWidth = Window.ClientBounds.Width;

            floorSize = new Point(windowWidth, 100);
            platformSize = new Point(100, 30);
            movingPlatformSize = new Point(30, 5);
            trapSize = new Point(10, 10);

            gravity = 3.75f;

            floorPlatform = new Platform(floorSize, new Point(0, windowHeight - floorSize.Y));

            bottomPlatform = new Platform(platformSize, new Point(windowWidth / 2, windowHeight - floorSize.Y - Player.Size.Y));

            bottomPlatform1 = new Platform(platformSize, new Point(windowWidth / 2 + 100, windowHeight - floorSize.Y - Player.Size.Y - 30));

            finalPlatform = new Platform(platformSize, new Point(windowWidth / 2 + 300, windowHeight - floorSize.Y - Player.Size.Y - 30));

            movingPlatform = new MovingPlatform(movingPlatformSize, new Point(windowWidth / 2 + 100 + platformSize.X, windowHeight - floorSize.Y - bottomPlatform.Size.Y - bottomPlatform1.Size.Y - 10),
                windowWidth / 2 + 100 + platformSize.X, windowWidth / 2 + 100 + platformSize.X + 70);

            platforms = new Platform[] { bottomPlatform1, bottomPlatform, floorPlatform, finalPlatform,movingPlatform };

            trap = new Trap(new Point(100, 10), new Point(windowWidth/2 + 200, windowHeight - floorSize.Y - Player.Size.Y + 1 + 20));

            playerWalkAnimation = new Animation(playerWalkRight, 32, 32, currentFrame, spriteSize);

            playerIdleAnimation = new Animation(playerIdle, 32, 32, currentFrameIdle, spriteSizeIdle);

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

            trapTexture = Content.Load<Texture2D>("trap");

            playerWalkRight = Content.Load<Texture2D>("Dude_Monster_Walk_6");
            playerWalkLeft = Content.Load<Texture2D>("Dude_Monster_Walk_Left");
            playerIdle = Content.Load<Texture2D>("Dude_Monster_Idle_4");
            playerIdleLeft = Content.Load<Texture2D>("Dude_Monster_Idle_Left");

            highlight = Content.Load<SpriteFont>("myText1");
            text = Content.Load<SpriteFont>("File");

            skyBackground = Content.Load<Texture2D>("sky");
            animationSprites = new Dictionary<string, Texture2D>()
            {
                { "playerWalkLeft", playerWalkLeft},
                { "playerWalkRight", playerWalkRight},
                { "playerIdleLeft", playerIdleLeft },
                { "playerIdleRight", playerIdle }
            };

            animations = new Dictionary<Texture2D, Animation>()
            {
                { playerWalkLeft, playerWalkAnimation},
                { playerWalkRight, playerWalkAnimation},
                { playerIdleLeft, playerIdleAnimation },
                { playerIdle, playerIdleAnimation },
            };

            Player.InicializeSprites(playerWalkRight, playerWalkRight, playerIdle);
        }


        private GameState state;
        protected override void Update(GameTime gameTime)
        {
            switch (state)
            {
                case GameState.Menu:
                    UpdateMenu(gameTime);
                    break;
                case GameState.GamePlay:
                    UpdateGamePlay(gameTime);
                    break;
                case GameState.GameOver:
                    UpdateGameOver(gameTime);
                    break;
            }

            base.Update(gameTime);
        }

        private void UpdateGameOver(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                state = GameState.GamePlay;
        }

        private void UpdateMenu(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                state = GameState.GamePlay;
        }

        private void UpdateGamePlay(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (trap.CollideWithTrap(Player.Vector, Player.Size))
            {
                state = GameState.GameOver;
                Player.StartAgain();
            }

            Player.Vector.Y += gravity;

            movingPlatform.Move();

            Player.Move(currentFrame, gameTime);

            foreach (var platform in platforms)
            {
                if (platform.Collide(Player.Vector, Player.Size) != CollideState.Fall)
                    Player.CollideWithPlatforms(platforms, gravity);
            }
        }

        protected override void Draw(GameTime gameTime)
        {

            switch (state)
            {
                case GameState.Menu:
                    DrawMenu(gameTime);
                    break;
                case GameState.GamePlay:
                    DrawGamePlay(gameTime);
                    break;
                case GameState.GameOver:
                    DrawGameOver(gameTime);
                    break;
            }
            base.Draw(gameTime);
        }

        private void DrawGameOver(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(skyBackground, new Rectangle(0, 0, windowWidth, windowHeight), Color.White);
            _spriteBatch.DrawString(highlight, "Game over!", new Vector2(100, 50), Color.Black);
            _spriteBatch.DrawString(text, "Press space to start", new Vector2(100, windowHeight - 50), Color.Black);
            _spriteBatch.End();
        }

        private void DrawGamePlay(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(skyBackground, new Rectangle(0, 0, windowWidth, windowHeight), Color.White);
            _spriteBatch.Draw(floor, new Rectangle(floorPlatform.SpawnPoint, floorPlatform.Size), Color.White);

            //if (!Player.GoLeft && Player.IsMove)
            //    _spriteBatch.Draw(playerWalkRight,
            //        new Rectangle((int)Player.Vector.X, (int)Player.Vector.Y - 10, Player.Size.X + 10, Player.Size.Y + 10),
            //        Player.currentAnimation.CreateRectangle(playerWalkAnimation.frameWidth),
            //        Color.White);
            //else if (Player.GoLeft && Player.IsMove)
            //    _spriteBatch.Draw(playerWalkLeft,
            //        new Rectangle((int)Player.Vector.X, (int)Player.Vector.Y - 10, Player.Size.X + 10, Player.Size.Y + 10),
            //        Player.currentAnimation.CreateRectangle(playerWalkAnimation.frameWidth),
            //        Color.White);
            //else if (!Player.IsMove && !Player.GoLeft)
            //    _spriteBatch.Draw(playerIdle,
            //        new Rectangle((int)Player.Vector.X, (int)Player.Vector.Y - 10, Player.Size.X + 10, Player.Size.Y + 10),
            //        Player.currentAnimation.CreateRectangle(playerIdleAnimation.frameWidth),
            //        Color.White);
            //else if (!Player.IsMove && Player.GoLeft)
            //    _spriteBatch.Draw(playerIdleLeft,
            //        new Rectangle((int)Player.Vector.X, (int)Player.Vector.Y - 10, Player.Size.X + 10, Player.Size.Y + 10),
            //        Player.currentAnimation.CreateRectangle(playerIdleAnimation.frameWidth),
            //        Color.White);

            Player.DrawPlayerAnimation(_spriteBatch, animationSprites, animations);

            _spriteBatch.Draw(platform, new Rectangle(bottomPlatform.SpawnPoint, bottomPlatform.Size), Color.White);

            _spriteBatch.Draw(platform, new Rectangle(finalPlatform.SpawnPoint, finalPlatform.Size), Color.White);

            _spriteBatch.Draw(platform1, new Rectangle(bottomPlatform1.SpawnPoint, bottomPlatform1.Size), new Rectangle(0,0,18,18), Color.White);

            for (int i =  0; i < 10; i++)
            {
                _spriteBatch.Draw(trapTexture, new Rectangle(trap.SpawnPoint.X + i * trapSize.X, trap.SpawnPoint.Y, trapSize.X, trapSize.Y), Color.White);
            }
            

            for (int i = 0; i < 3; i++)
            {
                _spriteBatch.Draw(cubeTexture, new Rectangle(bottomPlatform.SpawnPoint.X + 33*i, bottomPlatform.SpawnPoint.Y, 30, 30), new Rectangle(0, 0, 18, 18), Color.White);
            }

            _spriteBatch.Draw(movingPlatformTexture, new Rectangle((int)movingPlatform.Vector.X, movingPlatform.SpawnPoint.Y, movingPlatform.Size.X, movingPlatform.Size.Y), Color.White);
            
            _spriteBatch.End();
        }

        private void DrawMenu(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(skyBackground, new Rectangle(0,0,windowWidth, windowHeight), Color.White);
            _spriteBatch.DrawString(highlight, "Pixel Adventure", new Vector2(100, 50), Color.Black);
            _spriteBatch.DrawString(text, "Press space to start", new Vector2(100, windowHeight - 50), Color.Black);
            _spriteBatch.End();
        }
    }
}