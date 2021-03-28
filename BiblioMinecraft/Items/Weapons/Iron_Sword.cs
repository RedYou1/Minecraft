using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiblioMinecraft.Attributes;
using BiblioMinecraft.Damages;

namespace BiblioMinecraft.Items
{
    public class Iron_Sword : Sword
    {
        public override Damage Damge()
        {
            return new PhysicalDamage(6);
        }

        public override string id()
        {
            return "Iron_Sword";
        }
    }
}
