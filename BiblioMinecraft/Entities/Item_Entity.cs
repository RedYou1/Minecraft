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
            loc.World.RemoveEntity(this);
        }

        private Item item;

        public Item_Entity(Location loc,Item item) : base(loc, 1)
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
