using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PixelAdventure
{
    internal class MapCreator
    {
        //private int width;
        //private int height;
        private int[,] pixelsMatrix;
        public MapCreator(int windowWidth, int windowHeight)
        {
            //width = windowWidth;
            //height = windowHeight;
            //pixelsMatrix = new int[windowWidth/25, windowHeight/40];
        }

        public void DrawTexture(SpriteBatch _spriteBatch, Texture2D texture, Point size, Point spawn)
        {
            _spriteBatch.Draw(texture, new Rectangle(spawn.X, spawn.Y + size.Y -30, 30, 30), new Rectangle(19, 133, 18, 18), Color.White);
            _spriteBatch.Draw(texture, new Rectangle(spawn.X+size.X -30, spawn.Y +size.Y- 30, 30, 30), new Rectangle(57, 133, 18, 18), Color.White);

            for (int x = spawn.X; x < spawn.X + size.X - 30; x += 30)
                _spriteBatch.Draw(texture, new Rectangle(x, spawn.Y + size.Y - 30, 30, 30), new Rectangle(38, 133, 18, 18), Color.White);

            for (int y = spawn.Y + 30; y < spawn.Y + size.Y - 30; y += 30)
                _spriteBatch.Draw(texture, new Rectangle(spawn.X, y, 30, 30), new Rectangle(19, 114, 18, 18), Color.White);

            for (int y = spawn.Y + 30; y < spawn.Y+size.Y - 30; y += 30)
                _spriteBatch.Draw(texture, new Rectangle(size.X - 30, y, 30, 30), new Rectangle(57, 114, 18, 18), Color.White);

            for (int i = spawn.X+30; i < size.X - 30; i += 30)
                for (int j = spawn.Y+30; j < spawn.Y + size.Y - 30; j += 30)
                {
                    _spriteBatch.Draw(texture, new Rectangle(i, j, 30, 30), new Rectangle(38, 114, 18, 18), Color.White);
                }

            for (int x = spawn.X; x < spawn.X + size.X - 30; x += 30)
                _spriteBatch.Draw(texture, new Rectangle(x, spawn.Y, 30, 30), new Rectangle(38, 19, 18, 18), Color.White);
            _spriteBatch.Draw(texture, new Rectangle(spawn.X, spawn.Y, 30, 30), new Rectangle(19, 19, 18, 18), Color.White);
            _spriteBatch.Draw(texture, new Rectangle(spawn.X + size.X - 30, spawn.Y, 30, 30), new Rectangle(57, 19, 18, 18), Color.White);
        }
    }
}
