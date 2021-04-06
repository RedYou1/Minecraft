using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Attributes;

namespace CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Items
{
    public class Raw_Beef : Food
    {
        public Raw_Beef(int quantity) : base(quantity, 64)
        {

        }

        public override String id() { return "Raw Beef"; }

        public override int Food_Restored()
        {
            return 3;
        }
    }
}
