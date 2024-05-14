using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelAdventure.Scenes.UI
{
    internal class LevelSelector
    {
        private const int windowWidth = 1920;
        private const int windowHeight = 1080;
        private SpriteFont highlight;
        private SpriteFont text;

        private Texture2D background;
        private Texture2D select;

        private Vector2 selectVector;
        private int change = 350;
        private int countChoose = 0;

        private int currentTime;
        private int period;

        public LevelSelector(SpriteFont highlight, SpriteFont text, Texture2D background, Texture2D select, int currentTime, int period)
        {
            this.highlight = highlight;
            this.text = text;
            this.background = background;
            selectVector = new Vector2(100, 400);
            this.select = select;

            this.currentTime = currentTime;
            this.period = period;
        }

        public GameState Update(GameTime gameTime)
        {
            currentTime += gameTime.ElapsedGameTime.Milliseconds;

            if (currentTime > period)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Right) && countChoose == 0 &&
                    selectVector.X < 800)
                {
                    selectVector.X += change;
                    countChoose++;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Left) && countChoose == 0 &&
                    selectVector.X > 100)
                {
                    selectVector.X -= change;
                    countChoose++;
                }

                if (countChoose > 0)
                {
                    change = 0;
                    countChoose--;
                }
                else if (countChoose == 0)
                    change = 350;
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    return GameState.Menu;

                if (Keyboard.GetState().IsKeyDown(Keys.Enter) && selectVector.X == 100)
                    return GameState.Level1;
                else if (Keyboard.GetState().IsKeyDown(Keys.Enter) && selectVector.X == 450)
                    return GameState.Level2;
                else if (Keyboard.GetState().IsKeyDown(Keys.Enter) && selectVector.X == 800)
                    return GameState.Level3;
            }
            return GameState.LevelSelector;
        }

        public void Draw(GameTime gameTime, SpriteBatch _spriteBatch, Texture2D level1, Texture2D level2, Texture2D level3)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(background, new Rectangle(0, 0, windowWidth, windowHeight), Color.White);
            _spriteBatch.Draw(level1, new Rectangle(100, 200, 300, 170), Color.White);
            _spriteBatch.Draw(level2, new Rectangle(450, 200, 300, 170), Color.White);
            _spriteBatch.Draw(level3, new Rectangle(800, 200, 300, 170), Color.White);
            _spriteBatch.Draw(select, new Rectangle((int)selectVector.X, (int)selectVector.Y, 300, 10), Color.White);
            _spriteBatch.DrawString(highlight, "Choose level", new Vector2(100, 50), Color.Black);
            _spriteBatch.DrawString(text, "Press ESC to quit menu", new Vector2(100, 900), Color.Black);
            _spriteBatch.End();
        }
    }
}
