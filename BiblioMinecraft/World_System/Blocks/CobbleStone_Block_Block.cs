using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using BiblioMinecraft.World_System.Models;
using System.Windows.Media.Imaging;

namespace BiblioMinecraft.World_System.Blocks
{
    public class CobbleStone_Block : Cube
    {
        public CobbleStone_Block(Location loc) : base(loc, Game_Model.GetImage(Helper.ImageFile + "CobbleStone.PNG")) { }
    }
}
