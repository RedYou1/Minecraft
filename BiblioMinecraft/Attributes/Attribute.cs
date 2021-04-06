using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Items;

namespace CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Attributes
{
    public abstract class Attribute : Item
    {

        public Attribute(int quantity, int maxQuantity) : base(quantity, maxQuantity) { }

        public abstract String Attribute_id();
    }
}
