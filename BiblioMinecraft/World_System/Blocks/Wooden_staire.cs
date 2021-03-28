using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using BiblioMinecraft.World_System.Models;

namespace BiblioMinecraft.World_System.Blocks
{
    public class Wooden_staire : Staire
    {
        public Wooden_staire(Location loc) : base(loc, Game_Model.GetImage(Other.ImageFile + "oak_planks.PNG")) { }
    }
}
