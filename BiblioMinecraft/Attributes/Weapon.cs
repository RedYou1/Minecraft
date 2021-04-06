using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Entities;
using CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Damages;

namespace CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Attributes
{
    public abstract class Weapon : Attribute
    {
        public abstract Damage Damge();

        public Weapon() : base(1,1)
        {

        }

        public override object Left_Click(Player player, object to)
        {
            if (to is Entity ent)
            {
                ent.TakeDamage(Damge());
            }
            return null;
        }

        public override bool HaveAttribute(Attribute attribute)
        {
            return (attribute.Attribute_id() == Attribute_id());
        }
    }
}
