﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Attributes;
using CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Damages;

namespace CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Items
{
    public class Iron_Sword : Sword
    {
        public override Damage Damge()
        {
            return new PhysicalDamage(6);
        }

        public override string id()
        {
            return "Iron_Sword";
        }
    }
}
