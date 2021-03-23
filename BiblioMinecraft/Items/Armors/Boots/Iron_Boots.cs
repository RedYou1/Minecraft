using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiblioMinecraft.Attributes;
using BiblioMinecraft.Entities;
using BiblioMinecraft.Damages;

namespace BiblioMinecraft.Items.Armors
{
    public class Iron_Boots : Boots
    {
        public override void EntityDied(Entity entity)
        {
            if (entity is Player)
            {
                ((Player)entity).DropItem(this);
            }
        }

        public override string id()
        {
            return "Iron_Boots";
        }

        public override void Reduce(Damage damage)
        {
            if (damage.id() == new PhysicalDamage(0).id())
            {
                damage.damage-=2;
            }
        }
    }
}
