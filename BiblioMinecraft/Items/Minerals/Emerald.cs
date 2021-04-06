using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Items
{
    public class Emerald : CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Attributes.Mineral
    {
        public Emerald(int quantity) : base(quantity,64)
        {

        }
        public override String id()
        {
            return "Emerald";
        }
    }
}
