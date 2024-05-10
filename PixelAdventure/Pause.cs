using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PixelAdventure.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelAdventure
{
    internal class Pause
    {
        private SpriteFont highlight;
        private SpriteFont text;
        private Texture2D background;

        public Pause(SpriteFont highlight, SpriteFont text, Texture2D background)
        {
            this.highlight = highlight;
            this.text = text;
            this.background = background;
        }

        public GameState UpdatePause(GameTime gameTime, GameState currentLevel)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                return GameState.Menu;
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                return currentLevel;
            return GameState.Pause;
        }
    }
}
