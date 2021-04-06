using CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Attributes
{
    public abstract class Sword : Weapon
    {
        public override object Right_Click(Player player, object to)
        {
            //TODO: parrer
            return null;
        }
        public override string Attribute_id()
        {
            return "Sword";
        }
    }
}
