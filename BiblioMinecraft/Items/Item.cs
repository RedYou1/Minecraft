using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Attributes;
using CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Entities;

namespace CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Items
{
    public abstract class Item
    {
        protected int quantity;
        protected int maxQuantity;

        public Item(int quantity, int maxQuantity)
        {
            this.quantity = quantity;
            this.maxQuantity = maxQuantity;
        }

        public abstract String id();
        public virtual bool HaveAttribute(CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Attributes.Attribute attribute)
        {
            return false;
        }

        public virtual void EntityDied(Entity entity)
        {
            entity.Location.World.SpawnEntity(new Item_Entity(entity.Location, this));
        }

        public virtual object Right_Click(Player player, object to)
        {
            return null;
        }
        public virtual object Left_Click(Player player, object to)
        {
            if (to is Entity ent)
            {
                ent.TakeDamage(new Damages.PhysicalDamage(1));
            }
            return null;
        }

        public int Quantity
        {
            get => quantity;
            set
            {
                if (value <= maxQuantity)
                {
                    quantity = value;
                }
                if (value <= 0)
                {
                    //TODO: remove item from inventaire
                    
                }
            }
        }
        public int MaxQuantity { get => maxQuantity; }
    }
}
