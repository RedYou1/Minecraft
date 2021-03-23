using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiblioMinecraft.Attributes;

namespace BiblioMinecraft.Items.Foods
{
    public class Raw_Beef : Food
    {
        public override String id() { return "Raw Beef"; }

        public override int Food_Restored()
        {
            return 3;
        }
    }
}
