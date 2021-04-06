using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Entities;
using CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Damages;

namespace CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Attributes
{
    public abstract class Armor : Attribute
    {

        public Armor() : base(1,1) { }

        /// <summary>
        /// edit the damage with his damage reduction
        /// </summary>
        /// <param name="damage"></param>
        public abstract void Reduce(Damage damage);

        public override object Right_Click(Player player,object to)
        {
            player.Equipe(this);
            return null;
        }

        public override bool HaveAttribute(Attribute attribute)
        {
            return (attribute.Attribute_id() == Attribute_id());
        }
    }
}
