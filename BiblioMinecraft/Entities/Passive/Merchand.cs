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
            trades.Add(new Trade(() => { return new Steak(1); }, () => { return new Emerald(1); }));
            trades.Add(new Trade(() => { return new Emerald(1); }, () => { return new Steak(1); }));
            trades.Add(new Trade(() => { return new Steak(2); }, () => { return new Emerald(2); }));
            trades.Add(new Trade(() => { return new Emerald(2); }, () => { return new Steak(2); }));
            trades.Add(new Trade(() => { return new Steak(3); }, () => { return new Emerald(3); }));
            trades.Add(new Trade(() => { return new Emerald(3); }, () => { return new Steak(3); }));
        }

        public override void Update()
        {

        }

        protected override Game_Model EntityModel()
        {
            KeyValuePair<double[], double[]>[] model = new KeyValuePair<double[], double[]>[]
            {
                // Front face
                new KeyValuePair<double[],double[]>(new double[]{ 0.5, 0.5, 0.5 },new double[]{ 1 , 0}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.5, 0.5, 0.5 },new double[]{ 0 , 0}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.5, -0.5, 0.5 },new double[]{ 1 , 1}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.5, -0.5, 0.5 },new double[]{ 1 , 1}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.5, 0.5, 0.5 },new double[]{ 0 , 0}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.5, -0.5, 0.5 },new double[]{ 0 , 1}),

                // Back face
                new KeyValuePair<double[],double[]>(new double[]{ 0.5, 0.5, -0.5 },new double[]{ 0 , 0}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.5, -0.5, -0.5 },new double[]{ 0 , 1}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.5, 0.5, -0.5 },new double[]{ 1 , 0}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.5, 0.5, -0.5 },new double[]{ 1 , 0}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.5, -0.5, -0.5 },new double[]{ 0 , 1}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.5, -0.5, -0.5 },new double[]{ 1 , 1}),

                // Right face
                new KeyValuePair<double[],double[]>(new double[]{ 0.5, 0.5, -0.5 },new double[]{ 1 , 0}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.5, 0.5, 0.5 },new double[]{ 0 , 0}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.5, -0.5, -0.5 },new double[]{ 1 , 1}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.5, -0.5, -0.5 },new double[]{ 1 , 1}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.5, 0.5, 0.5 },new double[]{ 0 , 0}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.5, -0.5, 0.5 },new double[]{ 0 , 1}),

                // Left face
                new KeyValuePair<double[],double[]>(new double[]{ -0.5, 0.5, -0.5 },new double[]{ 0 , 0}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.5, -0.5, -0.5 },new double[]{ 0 , 1}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.5, 0.5, 0.5 },new double[]{ 1 , 0}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.5, 0.5, 0.5 },new double[]{ 1 , 0}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.5, -0.5, -0.5 },new double[]{ 0 , 1}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.5, -0.5, 0.5 },new double[]{ 1 , 1}),

                // Top face
                new KeyValuePair<double[],double[]>(new double[]{ -0.5, 0.5, -0.5 },new double[]{ 0 , 1}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.5, 0.5, 0.5 },new double[]{ 0 , 0}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.5, 0.5, -0.5 },new double[]{ 1 , 1}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.5, 0.5, -0.5 },new double[]{ 1 , 1}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.5, 0.5, 0.5 },new double[]{ 0 , 0}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.5, 0.5, 0.5 },new double[]{ 1 , 0}),

                // Buttom face
                new KeyValuePair<double[],double[]>(new double[]{ -0.5, -0.5, -0.5 },new double[]{ 0 , 0}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.5, -0.5, -0.5 },new double[]{ 1 , 0}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.5, -0.5, 0.5 },new double[]{ 0 , 1}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.5, -0.5, 0.5 },new double[]{ 0 , 1}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.5, -0.5, -0.5 },new double[]{ 1 , 0}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.5, -0.5, 0.5 },new double[]{ 1 , 1})
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
            base.Die();
            foreach (Item item in inventaire.Items)
            {
                if (item != null)
                {
                    item.EntityDied(this);
                }
            }
        }

        /*
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
        */

        /*
        public override string ToString()
        {
            String inv = inventaire.ToString();
            inv = inv.Replace("Invertory{", id() + " Invertory{");
            return inv;
        }
        */

        public Inventaire Inventaire { get => inventaire; }
        public Trade[] Trades { get => trades.ToArray(); }
    }
}
