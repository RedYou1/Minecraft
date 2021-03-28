using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiblioMinecraft.Entities;
using BiblioMinecraft.Damages;

namespace BiblioMinecraft.Attributes
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
