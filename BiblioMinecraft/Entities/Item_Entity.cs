using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiblioMinecraft.Items;

namespace BiblioMinecraft.Entities
{
    public class Item_Entity : Entity
    {
        public override void Die()
        {
            World_System.World.RemoveEntity(this);
        }

        private Item item;

        public Item_Entity(float x, float y, float z, float pitch, float yaw,Item item) : base(x, y, z, pitch, yaw, 1)
        {
            this.item = item;
        }

        public override string id()
        {
            return "Item_Entity " + item.id();
        }

        public Item Item { get => item; }
    }
}
