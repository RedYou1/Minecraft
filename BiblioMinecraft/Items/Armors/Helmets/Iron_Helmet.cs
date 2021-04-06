using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Attributes;
using CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Entities;
using CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Damages;

namespace CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Items.Armors
{
    public class Iron_Helmet : Helmet
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
            return "Iron_Helmet";
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
