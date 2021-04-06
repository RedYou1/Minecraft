using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Entities;
using CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Attributes;

namespace CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Items
{
    public class Back_Pack : CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Attributes.Other
    {
        public Inventaire inventaire = new Inventaire(3,2);

        public Back_Pack() : base(1,1)
        {

        }

        public override string id()
        {
            return "Back_Pack";
        }

        public override object Right_Click(Player player, object to)
        {
            return inventaire;
        }
    }
}
