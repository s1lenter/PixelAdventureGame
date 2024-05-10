using Microsoft.Xna.Framework.Graphics;
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
    }
}
