using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiblioMinecraft.Items;

namespace BiblioMinecraft
{
    public class Trade
    {
        public Item wanted;
        public Item giving;

        public Trade(Item wanted,Item giving)
        {
            this.wanted = wanted;
            this.giving = giving;
        }
    }
}
