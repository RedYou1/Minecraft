using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Damages
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
