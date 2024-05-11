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
    internal class Win
    {
        private const int windowWidth = 1920;
        private const int windowHeight = 1080;
        private SpriteFont highlight;
        private SpriteFont text;
        private Texture2D background;

        public Win(SpriteFont highlight, SpriteFont text, Texture2D background)
        {
            this.highlight = highlight;
            this.text = text;
            this.background = background;
        }

        public GameState Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                return GameState.Menu;
            return GameState.Win;
        }
        public void Draw(GameTime gameTime, SpriteBatch _spriteBatch)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(background, new Rectangle(0, 0, windowWidth, windowHeight), Color.White);
            _spriteBatch.DrawString(highlight, "You are win!", new Vector2(100, 50), Color.Black);
            _spriteBatch.DrawString(text, "Press SPACE to quit to menu", new Vector2(100, windowHeight - 70), Color.Black);
            _spriteBatch.End();
        }

    }
}
