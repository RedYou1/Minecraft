using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiblioMinecraft.Items;
using BiblioMinecraft;
using BiblioMinecraft.World_System;

namespace BiblioMinecraft.Entities
{
    public class Merchand : Entity
    {
        private Inventaire inventaire;
        private List<Trade> trades;

        public Merchand(Location loc) : base(loc, 20)
        {
            inventaire = new Inventaire(5, 1);
            inventaire.AddItem(new Emerald(1));
            inventaire.AddItem(new Steak(1));
            trades = new List<Trade>();
            trades.Add(new Trade(new Steak(1), 1, new Emerald(1), 1));
            trades.Add(new Trade(new Emerald(1), 1, new Steak(1), 1));
        }

        protected override Game_Model EntityModel()
        {
            KeyValuePair<double[], double[]>[] model = new KeyValuePair<double[], double[]>[]
            {
            };
            return new Game_Model(model, Game_Model.GetImage(Helper.ImageFile + id() + ".png"));
        }

        public override object Right_Clicked(Player player, Item with)
        {
            return inventaire;
        }

        public override string id()
        {
            return "Merchand";
        }

        public override void Die()
        {
            foreach (Item item in inventaire.Items)
            {
                if (item != null)
                {
                    item.EntityDied(this);
                }
            }
        }

        public void MakeATrade(Player player, Trade trade)
        {
            if (trades.Contains(trade))
            {
                if (player.Inventaire.Contains(trade.wanted) && inventaire.Contains(trade.giving))
                {
                    player.Inventaire.RemoveItem(trade.wanted);
                    player.Inventaire.AddItem(trade.giving);
                    inventaire.RemoveItem(trade.giving);
                    inventaire.AddItem(trade.wanted);
                }
            }
        }

        public override string ToString()
        {
            String inv = inventaire.ToString();
            inv = inv.Replace("Invertory{", id() + " Invertory{");
            return inv;
        }

        public Inventaire Inventaire { get => inventaire.Clone(); }
        public Trade[] Trades { get => trades.ToArray(); }
    }
}
