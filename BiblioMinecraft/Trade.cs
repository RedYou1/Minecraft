using CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Items;
using System;

namespace CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting
{
    public class Trade
    {
        public Func<Item> wanted;
        public Func<Item> giving;

        public Trade(Func<Item> wanted, Func<Item> giving)
        {
            this.wanted = wanted;
            this.giving = giving;
        }
    }
}
