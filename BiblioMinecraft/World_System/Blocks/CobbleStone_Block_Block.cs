using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.World_System.Models;
using System.Windows.Media.Imaging;

namespace CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.World_System.Blocks
{
    public class CobbleStone_Block : Cube
    {
        public CobbleStone_Block(Location loc) : base(loc, Game_Model.GetImage(Helper.ImageFile + "CobbleStone.PNG")) { }
    }
}
