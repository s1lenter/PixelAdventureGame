using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelAdventure.ObjectsScripts
{
    internal interface IObject
    {
        public Point Size { get; set; }
        public Point SpawnPoint { get; set; }
    }
}
