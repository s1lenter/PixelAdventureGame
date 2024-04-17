using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelAdventure
{
    internal class Button
    {
        private string Name;
        private Texture2D Texture;
        private int X { get; set; }
        private int Y { get; set; }

        public Button(string name, Texture2D texture, int x, int y)
        {
            Texture = texture;
            Name = name;
            X = x;
            Y = y;
        }
    }
}
