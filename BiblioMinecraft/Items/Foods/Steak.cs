using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Attributes;

namespace CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Items
{
    public class Steak : Food
    {
        public Steak(int quantity) : base(quantity,64)
        {

        }

        public override String id() { return "Steak"; }
        
        public override int Food_Restored()
        {
            return 8;
        }
    }
}
