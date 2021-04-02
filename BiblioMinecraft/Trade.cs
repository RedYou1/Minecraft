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
        public Func<Item> wanted;
        public Func<Item> giving;

        public Trade(Func<Item> wanted, Func<Item> giving)
        {
            this.wanted = wanted;
            this.giving = giving;
        }
    }
}
