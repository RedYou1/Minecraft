using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Entities;

namespace CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Attributes
{
    public abstract class Food : Attribute
    {

        public Food(int quantity, int maxQuantity) : base(quantity,maxQuantity)
        {

        }

        public override object Right_Click(Player player, object to)
        {
            player.Eat(this);
            quantity--;
            if (quantity <= 0)
            {
                player.Inventaire.RemoveItem(this);
            }
            return FOOD;
        }

        public static String FOOD { get => "Food Restored"; }

        public override string Attribute_id() { return "Food"; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>the amount of food to restore</returns>
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
