using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelAdventure.Scenes
{
    internal class Menu
    {
        private const int windowWidth = 1920;
        private const int windowHeight = 1080;
        private SpriteFont highlight;
        private SpriteFont text;
        private Texture2D background;

        public Menu(SpriteFont highlight, SpriteFont text, Texture2D background)
        {
            this.highlight = highlight;
            this.text = text;
            this.background = background;
        }

        public GameState Update(GameTime gameTime, GameState currentlevel)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                return currentlevel;
            return GameState.Menu;
        }

        public void Draw(GameTime gameTime, SpriteBatch _spriteBatch)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(background, new Rectangle(0, 0, windowWidth, windowHeight), Color.White);
            _spriteBatch.DrawString(highlight, "Pixel Adventure", new Vector2(100, 50), Color.Black);
            _spriteBatch.DrawString(text, "Press SPACE to start", new Vector2(100, windowHeight - 50), Color.Black);
            _spriteBatch.End();
        }
    }
}
