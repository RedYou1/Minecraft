using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiblioMinecraft.Items
{
    public class Emerald : BiblioMinecraft.Attributes.Mineral
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
