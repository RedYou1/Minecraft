using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiblioMinecraft.Entities;

namespace BiblioMinecraft.Attributes
{
    public abstract class Food : Attribute
    {

        public Food(int quantity, int maxQuantity) : base(quantity,maxQuantity)
        {

        }

        public override object Right_Click(Player player, object to)
        {
            player.Eat(this);
            return null;
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
