using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using BiblioMinecraft.World_System.Models;

namespace BiblioMinecraft.World_System.Blocks
{
    public class Stone_Block : Cube
    {
        public Stone_Block(float x, float y, float z) : base(x, y, z, new DiffuseMaterial(Brushes.YellowGreen)) { }
    }
}
