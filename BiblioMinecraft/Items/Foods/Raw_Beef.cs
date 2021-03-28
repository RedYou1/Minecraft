using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiblioMinecraft.Attributes;

namespace BiblioMinecraft.Items
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
