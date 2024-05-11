﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelAdventure.Scenes
{
    internal class GameOver
    {
        private const int windowWidth = 1920;
        private const int windowHeight = 1080;
        private SpriteFont highlight;
        private SpriteFont text;
        private Texture2D background;

        public GameOver(SpriteFont highlight, SpriteFont text, Texture2D background)
        {
            this.highlight = highlight;
            this.text = text;
            this.background = background;
        }

        public GameState UpdateGameOver(GameTime gameTime, GameState currentLevel)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                return currentLevel;
            return GameState.GameOver;
        }

        public void DrawGameOver(GameTime gameTime, SpriteBatch _spriteBatch)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(background, new Rectangle(0, 0, windowWidth, windowHeight), Color.White);
            _spriteBatch.DrawString(highlight, "Game over!", new Vector2(100, 50), Color.Black);
            _spriteBatch.DrawString(text, "Press space to start", new Vector2(100, windowHeight - 50), Color.Black);
            _spriteBatch.End();
        }
    }
}
