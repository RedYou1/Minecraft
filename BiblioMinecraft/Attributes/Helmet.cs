using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiblioMinecraft.Attributes
{
    public abstract class Helmet : Armor
    {
        public override String Attribute_id() { return "Helmet"; }
    }
}
