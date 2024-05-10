using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelAdventure
{
    internal class Menu
    {
        private SpriteFont highlight;
        private SpriteFont text;
        private Texture2D background;

        public Menu(SpriteFont highlight, SpriteFont text, Texture2D background)
        {
            this.highlight = highlight;
            this.text = text;
            this.background = background;
        }

        public GameState UpdateMenu(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                return GameState.Level1;
            return GameState.Menu;
        }
    }
}
