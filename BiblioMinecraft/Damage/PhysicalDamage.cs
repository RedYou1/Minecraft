using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiblioMinecraft.Damages
{
    public class PhysicalDamage : Damage
    {
        public override String id()
        {
            return "Physical";
        }

        public PhysicalDamage(int damage) : base(damage) { }
    }
}
