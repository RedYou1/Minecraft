using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiblioMinecraft.Attributes;
using BiblioMinecraft.Entities;

namespace BiblioMinecraft.Items
{
    public abstract class Item
    {
        public abstract String id();
        public virtual bool HaveAttribute(BiblioMinecraft.Attributes.Attribute attribute)
        {
            return false;
        }

        public virtual void EntityDied(Entity entity)
        {
            World_System.World.SpawnEntity(new Item_Entity(entity.X,entity.Y,entity.Z,entity.Pitch,entity.Yaw,this));
        }

        public virtual void Right_Click(Player player)
        {

        }
        public virtual void Left_Click(Player player)
        {

        }
    }
}
