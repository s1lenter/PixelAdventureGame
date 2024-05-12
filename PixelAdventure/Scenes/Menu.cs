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
        private Texture2D select;

        private Vector2 selectVector;
        private int change = 100;

        private int countChoose = 0;

        private int currentTime;
        private int period;

        public Menu(SpriteFont highlight, SpriteFont text, Texture2D background, Texture2D select, int currentTime, int period)
        {
            this.highlight = highlight;
            this.text = text;
            this.background = background;
            this.select = select;
            selectVector = new Vector2(90, 200);

            this.currentTime = currentTime;
            this.period = period;
        }

        public GameState Update(GameTime gameTime, GameState currentLevel)
        {
            currentTime += gameTime.ElapsedGameTime.Milliseconds;
            if (currentTime > period)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Down) && countChoose == 0 &&
                selectVector.Y < 400)
                {
                    selectVector.Y += change;
                    countChoose++;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Up) && countChoose == 0 &&
                    selectVector.Y >= 300)
                {
                    selectVector.Y -= change;
                    countChoose++;
                }
                if (countChoose > 0)
                {
                    change = 0;
                    countChoose--;
                }
                else if (countChoose == 0)
                    change = 100;

                if (Keyboard.GetState().IsKeyDown(Keys.Enter) && selectVector.Y == 200)
                    return currentLevel;
                if (Keyboard.GetState().IsKeyDown(Keys.Enter) && selectVector.Y == 300)
                    return currentLevel;
                else if (Keyboard.GetState().IsKeyDown(Keys.Enter) && selectVector.Y == 400)
                    return GameState.Quit;
            }
            return GameState.Menu;
        }

        public void Draw(GameTime gameTime, SpriteBatch _spriteBatch)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(background, new Rectangle(0, 0, windowWidth, windowHeight), Color.White);
            _spriteBatch.Draw(select, new Rectangle((int)selectVector.X, (int)selectVector.Y, 150, 65), Color.White);
            _spriteBatch.DrawString(highlight, "Pixel Adventure", new Vector2(100, 50), Color.Black);
            _spriteBatch.DrawString(text, "Play", new Vector2(100, 200), Color.Black);
            _spriteBatch.DrawString(text, "Quit", new Vector2(100, 300), Color.Black);
            _spriteBatch.End();
        }
    }
}
