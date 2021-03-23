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
        public int wantedamount;
        public Item giving;
        public int givingamount;

        public Trade(Item wanted,int wantedamount,Item giving,int givingamount)
        {
            this.wanted = wanted;
            this.wantedamount = wantedamount;
            this.giving = giving;
            this.givingamount = givingamount;
        }
    }
}
