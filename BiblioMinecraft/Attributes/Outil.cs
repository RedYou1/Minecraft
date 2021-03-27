using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiblioMinecraft.Entities;

namespace BiblioMinecraft.Attributes
{
    public abstract class Outils : Attribute
    {

        public override void Right_Click(Player player)
        {
            
        }

        public override string Attribute_id() { return "Food"; }

        public abstract int Food_Restored();

        public override bool HaveAttribute(Attribute attribute)
        {
            return attribute.id() == Attribute_id();
        }

        public override void EntityDied(Entity entity)
        {
            if (entity is Player) {
                ((Player)entity).DropItem(this);
            }
        }
    }
}
