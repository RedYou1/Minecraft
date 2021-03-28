using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiblioMinecraft.Attributes;

namespace BiblioMinecraft.Items
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
