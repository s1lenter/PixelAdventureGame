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
        private int windowWidth;
        private int windowHeight;
        private int[,] pixelsMatrix;
        public MapCreator(int windowWidth, int windowHeight)
        {
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;
            pixelsMatrix = new int[windowWidth / 30, windowHeight / 30];
        }

        public void DrawTexture()
        {

        }
    }
}
