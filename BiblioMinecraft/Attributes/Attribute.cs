using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiblioMinecraft.Items;

namespace BiblioMinecraft.Attributes
{
    public abstract class Attribute : Item
    {

        public Attribute(int quantity, int maxQuantity) : base(quantity, maxQuantity) { }

        public abstract String Attribute_id();
    }
}
