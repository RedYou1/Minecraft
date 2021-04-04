using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiblioMinecraft.Items;
using BiblioMinecraft;
using BiblioMinecraft.World_System;
using System.Windows.Media.Media3D;

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



        protected override Rect3D HitBox()
        {
            return new Rect3D(-0.5, 0, -0.5, 0.5, 2, 0.5);
        }

        public override Game_Model Model()
        {
            List<KeyValuePair<double[], double[]>> tot = new List<KeyValuePair<double[], double[]>>();
            KeyValuePair<double[], double[]>[] a = HeadModel();
            for (int i = 0; i < a.Length; i++)
            {
                double[,] coord = new double[1, 3];
                coord[0, 0] = a[i].Key[0];
                coord[0, 1] = a[i].Key[1] - 1.5;
                coord[0, 2] = a[i].Key[2];
                double[,] rotmat = new double[3, 3];
                rotmat[0, 0] = Math.Cos(loc.Yaw);
                rotmat[1, 0] = Math.Sin(loc.Yaw) * Math.Sin(loc.Pitch);
                rotmat[2, 0] = Math.Sin(loc.Yaw) * Math.Cos(loc.Pitch);
                rotmat[0, 1] = 0;
                rotmat[1, 1] = Math.Cos(loc.Pitch);
                rotmat[2, 1] = -Math.Sin(loc.Pitch);
                rotmat[0, 2] = -Math.Sin(loc.Yaw);
                rotmat[1, 2] = Math.Cos(loc.Yaw) * Math.Sin(loc.Pitch);
                rotmat[2, 2] = Math.Cos(loc.Yaw) * Math.Cos(loc.Pitch);

                double[,] newcoord = Helper.MultiplyMatrix(coord, rotmat);

                a[i].Key[0] = newcoord[0, 0] + loc.X;
                a[i].Key[1] = newcoord[0, 1] + loc.Y + 1.5;
                a[i].Key[2] = newcoord[0, 2] + loc.Z;
                tot.Add(a[i]);
            }
            foreach (KeyValuePair<double[], double[]> l in EntityModel().model)
            {
                l.Key[0] += loc.X;
                l.Key[1] += loc.Y;
                l.Key[2] += loc.Z;
                tot.Add(l);
            }
            return new Game_Model(tot.ToArray(), Game_Model.GetImage(Helper.ImageFile + id() + ".png"));
        }

        protected override Game_Model EntityModel()
        {
            List<KeyValuePair<double[], double[]>> a = new List<KeyValuePair<double[], double[]>>();
            foreach (KeyValuePair<double[], double[]> l in LegModel())
            {
                l.Key[0] -= 0.125;
                a.Add(l);
            }
            foreach (KeyValuePair<double[], double[]> l in LegModel())
            {
                l.Key[0] += 0.125;
                a.Add(l);
            }
            foreach (KeyValuePair<double[], double[]> l in ChestModel())
            {
                a.Add(l);
            }
            foreach (KeyValuePair<double[], double[]> l in ClothModel())
            {
                a.Add(l);
            }
            foreach (KeyValuePair<double[], double[]> l in ArmModel())
            {
                l.Key[0] -= 0.375;
                a.Add(l);
            }
            foreach (KeyValuePair<double[], double[]> l in ArmModel())
            {
                l.Key[0] += 0.375; 
                a.Add(l);
            }
            foreach (KeyValuePair<double[], double[]> l in HandsModel())
            {
                a.Add(l);
            }
            return new Game_Model(a.ToArray(), Game_Model.GetImage(Helper.ImageFile + id() + ".png"));
        }

        protected KeyValuePair<double[], double[]>[] HandsModel()
        {
            KeyValuePair<double[], double[]>[] model = new KeyValuePair<double[], double[]>[]
            { 
                // Front face
                new KeyValuePair<double[],double[]>(new double[]{ 0.25, 1.1767767, 0.374277 },new double[]{ 0.933594, 0.59375}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.25, 1.1767767, 0.374277 },new double[]{ 0.8125, 0.59375}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.25, 1, 0.1975 },new double[]{ 0.933594, 0.652344}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.25, 1, 0.1975 },new double[]{ 0.933594, 0.652344}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.25, 1.1767767, 0.374277 },new double[]{ 0.8125, 0.59375}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.25, 1, 0.1975 },new double[]{ 0.8125, 0.652344}),

                // Back face
                new KeyValuePair<double[],double[]>(new double[]{ 0.25, 1.32, 0.1975 },new double[]{ 0.6875, 0.59375}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.25, 1.1767767, 0.0207233 },new double[]{ 0.6875, 0.652344}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.25, 1.32, 0.1975 },new double[]{ 0.808594 , 0.59375}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.25, 1.32, 0.1975 },new double[]{ 0.808594, 0.59375}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.25, 1.1767767, 0.0207233 },new double[]{ 0.6875, 0.652344}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.25, 1.1767767, 0.0207233 },new double[]{ 0.808594, 0.652344}),

                // Right face
                new KeyValuePair<double[],double[]>(new double[]{ 0.25, 1.32, 0.1975 },new double[]{ 0.683594, 0.65625}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.25, 1.1767767, 0.374277 },new double[]{ 0.625 , 0.65625}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.25, 1.1767767, 0.0207233 },new double[]{ 0.683594, 0.714844}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.25, 1.1767767, 0.0207233 },new double[]{ 0.683594, 0.714844}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.25, 1.1767767, 0.374277 },new double[]{ 0.625, 0.65625}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.25, 1, 0.1975 },new double[]{ 0.625, 0.714844}),

                // Left face
                new KeyValuePair<double[],double[]>(new double[]{ -0.25, 1.32, 0.1975 },new double[]{ 0.8125 , 0.65625}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.25, 1.1767767, 0.0207233 },new double[]{ 0.8125, 0.714844}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.25, 1.1767767, 0.374277 },new double[]{ 0.871094 , 0.65625}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.25, 1.1767767, 0.374277 },new double[]{ 0.871094, 0.65625}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.25, 1.1767767, 0.0207233 },new double[]{ 0.8125, 0.714844}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.25, 1, 0.1975 },new double[]{ 0.871094, 0.714844}),

                // Top face
                new KeyValuePair<double[],double[]>(new double[]{ -0.25, 1.32, 0.1975 },new double[]{ 0.6875, 0.714844}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.25, 1.1767767, 0.374277 },new double[]{ 0.6875, 0.65625}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.25, 1.32, 0.1975 },new double[]{ 0.808594, 0.714844}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.25, 1.32, 0.1975 },new double[]{ 0.808594, 0.714844}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.25, 1.1767767, 0.374277 },new double[]{ 0.6875, 0.65625}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.25, 1.1767767, 0.374277 },new double[]{ 0.808594, 0.65625}),

                // Buttom face
                new KeyValuePair<double[],double[]>(new double[]{ -0.25, 1.1767767, 0.0207233 },new double[]{ 0.875 , 0.65625}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.25, 1.1767767, 0.0207233 },new double[]{ 1 , 0.65625}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.25, 1, 0.1975 },new double[]{ 0.875, 0.714844}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.25, 1, 0.1975 },new double[]{ 0.875, 0.714844}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.25, 1.1767767, 0.0207233 },new double[]{ 1, 0.65625}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.25, 1, 0.1975 },new double[]{ 1, 0.714844})
            };
            return model;
        }

        protected KeyValuePair<double[], double[]>[] HeadModel()
        {
            KeyValuePair<double[], double[]>[] model = new KeyValuePair<double[], double[]>[]
            {
                // Front face
                new KeyValuePair<double[],double[]>(new double[]{ 0.25, 2.125, 0.25 },new double[]{ 0.246094 , 0.125}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.25, 2.125, 0.25 },new double[]{ 0.125 , 0.125}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.25, 1.5, 0.25 },new double[]{ 0.246094, 0.277344}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.25, 1.5, 0.25 },new double[]{ 0.246094, 0.277344}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.25, 2.125, 0.25 },new double[]{ 0.125, 0.125}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.25, 1.5, 0.25 },new double[]{ 0.125, 0.277344}),

                new KeyValuePair<double[],double[]>(new double[]{ 0.0625, 1.6875, 0.375 },new double[]{ 0.433594 , 0.03125}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.0625, 1.6875, 0.375 },new double[]{ 0.40625 , 0.03125}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.0625, 1.4375, 0.375 },new double[]{ 0.433594, 0.089844}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.0625, 1.4375, 0.375 },new double[]{ 0.433594, 0.089844}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.0625, 1.6875, 0.375 },new double[]{ 0.40625, 0.03125}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.0625, 1.4375, 0.375 },new double[]{ 0.40625, 0.089844}),

                // Back face
                new KeyValuePair<double[],double[]>(new double[]{ 0.25, 2.125, -0.25 },new double[]{ 0.375 , 0.125}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.25, 1.5, -0.25 },new double[]{ 0.375, 0.277344}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.25, 2.125, -0.25 },new double[]{ 0.496094 , 0.125}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.25, 2.125, -0.25 },new double[]{ 0.496094, 0.125}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.25, 1.5, -0.25 },new double[]{ 0.375, 0.277344}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.25, 1.5, -0.25 },new double[]{ 0.496094, 0.277344}),

                new KeyValuePair<double[],double[]>(new double[]{ 0.0625, 1.6875, 0.25 },new double[]{ 0.46875 , 0.03125}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.0625, 1.4375, 0.25 },new double[]{ 0.46875, 0.089844}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.0625, 1.6875, 0.25 },new double[]{ 0.496094 , 0.03125}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.0625, 1.6875, 0.25 },new double[]{ 0.496094, 0.03125}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.0625, 1.4375, 0.25 },new double[]{ 0.46875, 0.089844}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.0625, 1.4375, 0.25 },new double[]{ 0.496094, 0.089844}),

                // Right face
                new KeyValuePair<double[],double[]>(new double[]{ 0.25, 2.125, -0.25 },new double[]{ 0.121094 , 0.125}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.25, 2.125, 0.25 },new double[]{ 0 , 0.125}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.25, 1.5, -0.25 },new double[]{ 0.121094, 0.277344}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.25, 1.5, -0.25 },new double[]{ 0.121094, 0.277344}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.25, 2.125, 0.25 },new double[]{ 0 , 0.125}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.25, 1.5, 0.25 },new double[]{ 0 , 0.277344}),

                new KeyValuePair<double[],double[]>(new double[]{ 0.0625, 1.6875, 0.25 },new double[]{ 0.402344 , 0.03125}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.0625, 1.6875, 0.375 },new double[]{ 0.375, 0.03125}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.0625, 1.4375, 0.25 },new double[]{ 0.402344, 0.089844}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.0625, 1.4375, 0.25 },new double[]{ 0.402344, 0.089844}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.0625, 1.6875, 0.375 },new double[]{ 0.375, 0.03125}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.0625, 1.4375, 0.375 },new double[]{ 0.375, 0.089844}),

                // Left face
                new KeyValuePair<double[],double[]>(new double[]{ -0.25, 2.125, -0.25 },new double[]{ 0.25 , 0.125}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.25, 1.5, -0.25 },new double[]{ 0.25 , 0.277344}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.25, 2.125, 0.25 },new double[]{ 0.371094 , 0.125}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.25, 2.125, 0.25 },new double[]{ 0.371094, 0.125}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.25, 1.5, -0.25 },new double[]{ 0.25 , 0.277344}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.25, 1.5, 0.25 },new double[]{ 0.371094, 0.277344}),

                new KeyValuePair<double[],double[]>(new double[]{ -0.0625, 1.6875, 0.25 },new double[]{ 0.4375 , 0.03125}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.0625, 1.4375, 0.25 },new double[]{ 0.4375, 0.089844}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.0625, 1.6875, 0.375 },new double[]{ 0.464844 , 0.03125}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.0625, 1.6875, 0.375 },new double[]{ 0.464844, 0.03125}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.0625, 1.4375, 0.25 },new double[]{ 0.4375, 0.089844}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.0625, 1.4375, 0.375 },new double[]{ 0.464844, 0.089844}),

                // Top face
                new KeyValuePair<double[],double[]>(new double[]{ -0.25, 2.125, -0.25 },new double[]{ 0.125, 0.121094}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.25, 2.125, 0.25 },new double[]{ 0.125, 0}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.25, 2.125, -0.25 },new double[]{ 0.246094, 0.121094}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.25, 2.125, -0.25 },new double[]{ 0.246094, 0.121094}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.25, 2.125, 0.25 },new double[]{ 0.125, 0}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.25, 2.125, 0.25 },new double[]{ 0.246094, 0}),

                new KeyValuePair<double[],double[]>(new double[]{ -0.0625, 1.6875, 0.25 },new double[]{ 0.40625, 0.027344}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.0625, 1.6875, 0.375 },new double[]{ 0.40625, 0}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.0625, 1.6875, 0.25 },new double[]{ 0.433594, 0.027344}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.0625, 1.6875, 0.25 },new double[]{ 0.433594, 0.027344}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.0625, 1.6875, 0.375 },new double[]{ 0.40625, 0}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.0625, 1.6875, 0.375 },new double[]{ 0.433594, 0}),

                // Buttom face
                new KeyValuePair<double[],double[]>(new double[]{ -0.25, 1.5, -0.25 },new double[]{ 0.25, 0}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.25, 1.5, -0.25 },new double[]{ 0.371094, 0}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.25, 1.5, 0.25 },new double[]{ 0.25, 0.121094}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.25, 1.5, 0.25 },new double[]{ 0.25, 0.121094}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.25, 1.5, -0.25 },new double[]{ 0.371094, 0}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.25, 1.5, 0.25 },new double[]{ 0.371094, 0.121094}),

                new KeyValuePair<double[],double[]>(new double[]{ -0.0625, 1.4375, 0.25 },new double[]{ 0.4375, 0}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.0625, 1.4375, 0.25 },new double[]{ 0.464844, 0}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.0625, 1.4375, 0.375 },new double[]{ 0.4375, 0.027344}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.0625, 1.4375, 0.375 },new double[]{ 0.4375, 0.027344}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.0625, 1.4375, 0.25 },new double[]{ 0.464844, 0}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.0625, 1.4375, 0.375 },new double[]{ 0.464844, 0.027344})
            };
            return model;
        }

        protected KeyValuePair<double[], double[]>[] ArmModel()
        {
            KeyValuePair<double[], double[]>[] model = new KeyValuePair<double[], double[]>[]
            { 
                // Front face
                new KeyValuePair<double[],double[]>(new double[]{ 0.125, 1.1767767, 0.374277 },new double[]{ 0.871094, 0.34375}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.125, 1.1767767, 0.374277 },new double[]{ 0.8125, 0.34375}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.125, 1, 0.1975 },new double[]{ 0.871094, 0.402344}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.125, 1, 0.1975 },new double[]{ 0.871094, 0.402344}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.125, 1.1767767, 0.374277 },new double[]{ 0.8125, 0.34375}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.125, 1, 0.1975 },new double[]{ 0.8125, 0.402344}),

                // Back face
                new KeyValuePair<double[],double[]>(new double[]{ 0.125, 1.46875, 0.03125 },new double[]{ 0.75 , 0.34375}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.125, 1.3125, -0.125 },new double[]{ 0.75, 0.402344}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.125, 1.46875, 0.03125 },new double[]{ 0.808594 , 0.34375}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.125, 1.46875, 0.03125 },new double[]{ 0.808594, 0.34375}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.125, 1.3125, -0.125 },new double[]{ 0.75, 0.402344}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.125, 1.3125, -0.125 },new double[]{ 0.808594, 0.402344}),

                // Right face
                new KeyValuePair<double[],double[]>(new double[]{ 0.125, 1.46875, 0.03125 },new double[]{ 0.871094, 0.40625}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.125, 1.1767767, 0.374277 },new double[]{ 0.8125 , 0.40625}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.125, 1.3125, -0.125 },new double[]{ 0.871094, 0.527344}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.125, 1.3125, -0.125 },new double[]{ 0.871094, 0.527344}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.125, 1.1767767, 0.374277 },new double[]{ 0.8125, 0.40625}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.125, 1, 0.1975 },new double[]{ 0.8125, 0.527344}),

                // Left face
                new KeyValuePair<double[],double[]>(new double[]{ -0.125, 1.46875, 0.03125 },new double[]{ 0.6875 , 0.40625}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.125, 1.3125, -0.125 },new double[]{ 0.6875, 0.527344}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.125, 1.1767767, 0.374277 },new double[]{ 0.746094 , 0.40625}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.125, 1.1767767, 0.374277 },new double[]{ 0.746094, 0.40625}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.125, 1.3125, -0.125 },new double[]{ 0.6875, 0.527344}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.125, 1, 0.1975 },new double[]{ 0.746094, 0.527344}),

                // Top face
                new KeyValuePair<double[],double[]>(new double[]{ -0.125, 1.46875, 0.03125 },new double[]{ 0.75, 0.527344}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.125, 1.1767767, 0.374277 },new double[]{ 0.75, 0.40625}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.125, 1.46875, 0.03125 },new double[]{ 0.808594, 0.527344}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.125, 1.46875, 0.03125 },new double[]{ 0.808594, 0.527344}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.125, 1.1767767, 0.374277 },new double[]{ 0.75, 0.40625}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.125, 1.1767767, 0.374277 },new double[]{ 0.808594, 0.40625}),

                // Buttom face
                new KeyValuePair<double[],double[]>(new double[]{ -0.125, 1.3125, -0.125 },new double[]{ 0.875 , 0.40625}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.125, 1.3125, -0.125 },new double[]{ 0.933594 , 0.40625}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.125, 1, 0.1975 },new double[]{ 0.875, 0.527344}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.125, 1, 0.1975 },new double[]{ 0.875, 0.527344}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.125, 1.3125, -0.125 },new double[]{ 0.933594, 0.40625}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.125, 1, 0.1975 },new double[]{ 0.933594, 0.527344})
            };
            return model;
        }

        protected KeyValuePair<double[], double[]>[] ClothModel()
        {
            KeyValuePair<double[], double[]>[] model = new KeyValuePair<double[], double[]>[]
            { 
                // Front face
                new KeyValuePair<double[],double[]>(new double[]{ 0.26, 1.51, 0.1975 },new double[]{ 0.214844, 0.6875}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.26, 1.51, 0.1975 },new double[]{ 0.09375, 0.6875}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.26, 0.2, 0.1975 },new double[]{ 0.214844, 1}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.26, 0.2, 0.1975 },new double[]{ 0.214844, 1}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.26, 1.51, 0.1975 },new double[]{ 0.09375, 0.6875}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.26, 0.2, 0.1975 },new double[]{ 0.09375, 1}),

                // Back face
                new KeyValuePair<double[],double[]>(new double[]{ 0.26, 1.51, -0.1975 },new double[]{ 0.3125 , 0.6875}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.26, 0.2, -0.1975 },new double[]{ 0.3125, 1}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.26, 1.51, -0.1975 },new double[]{ 0.433594 , 0.6875}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.26, 1.51, -0.1975 },new double[]{ 0.433594, 0.6875}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.26, 0.2, -0.1975 },new double[]{ 0.3125, 1}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.26, 0.2, -0.1975 },new double[]{ 0.433594, 1}),

                // Right face
                new KeyValuePair<double[],double[]>(new double[]{ 0.26, 1.51, -0.1975 },new double[]{ 0.089844 , 0.6875}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.26, 1.51, 0.1975 },new double[]{ 0 , 0.6875}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.26, 0.2, -0.1975 },new double[]{ 0.089844, 1}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.26, 0.2, -0.1975 },new double[]{ 0.089844, 1}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.26, 1.51, 0.1975 },new double[]{ 0 , 0.6875}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.26, 0.2, 0.1975 },new double[]{ 0 , 1}),

                // Left face
                new KeyValuePair<double[],double[]>(new double[]{ -0.26, 1.51, -0.1975 },new double[]{ 0.21875 , 0.6875}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.26, 0.2, -0.1975 },new double[]{ 0.21875, 1}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.26, 1.51, 0.1975 },new double[]{ 0.308594 , 0.6875}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.26, 1.51, 0.1975 },new double[]{ 0.308594, 0.6875}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.26, 0.2, -0.1975 },new double[]{ 0.21875, 1}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.26, 0.2, 0.1975 },new double[]{ 0.308594, 1}),

                // Top face
                new KeyValuePair<double[],double[]>(new double[]{ -0.26, 1.51, -0.1975 },new double[]{ 0.09375 , 0.683594}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.26, 1.51, 0.1975 },new double[]{ 0.09375, 0.59375}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.26, 1.51, -0.1975 },new double[]{ 0.214844 , 0.683594}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.26, 1.51, -0.1975 },new double[]{ 0.214844, 0.683594}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.26, 1.51, 0.1975 },new double[]{ 0.09375, 0.59375}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.26, 1.51, 0.1975 },new double[]{ 0.214844, 0.59375})
            };
            return model;
        }

        protected KeyValuePair<double[], double[]>[] ChestModel()
        {
            KeyValuePair<double[], double[]>[] model = new KeyValuePair<double[], double[]>[]
            {
                // Front face
                new KeyValuePair<double[],double[]>(new double[]{ 0.25, 1.5, 0.1875 },new double[]{ 0.464844 , 0.40625}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.25, 1.5, 0.1875 },new double[]{ 0.34375 , 0.40625}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.25, 0.75, 0.1875 },new double[]{ 0.464844, 0.589844}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.25, 0.75, 0.1875 },new double[]{ 0.464844, 0.589844}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.25, 1.5, 0.1875 },new double[]{ 0.34375, 0.40625}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.25, 0.75, 0.1875 },new double[]{ 0.34375, 0.589844}),

                // Back face
                new KeyValuePair<double[],double[]>(new double[]{ 0.25, 1.5, -0.1875 },new double[]{ 0.5625 , 0.40625}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.25, 0.75, -0.1875 },new double[]{ 0.5625, 0.589844}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.25, 1.5, -0.1875 },new double[]{ 0.683594 , 0.40625}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.25, 1.5, -0.1875 },new double[]{ 0.683594, 0.40625}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.25, 0.75, -0.1875 },new double[]{ 0.5625, 0.589844}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.25, 0.75, -0.1875 },new double[]{ 0.683594, 0.589844}),

                // Right face
                new KeyValuePair<double[],double[]>(new double[]{ 0.25, 1.5, -0.1875 },new double[]{ 0.339844 , 0.40625}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.25, 1.5, 0.1875 },new double[]{ 0.25 , 0.40625}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.25, 0.75, -0.1875 },new double[]{ 0.339844, 0.589844}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.25, 0.75, -0.1875 },new double[]{ 0.339844, 0.589844}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.25, 1.5, 0.1875 },new double[]{ 0.25, 0.40625}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.25, 0.75, 0.1875 },new double[]{ 0.25, 0.589844}),
                
                // Left face
                new KeyValuePair<double[],double[]>(new double[]{ -0.25, 1.5, -0.1875 },new double[]{ 0.46875 , 0.40625}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.25, 0.75, -0.1875 },new double[]{ 0.46875, 0.589844}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.25, 1.5, 0.1875 },new double[]{ 0.558594 , 0.40625}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.25, 1.5, 0.1875 },new double[]{ 0.558594, 0.40625}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.25, 0.75, -0.1875 },new double[]{ 0.46875, 0.589844}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.25, 0.75, 0.1875 },new double[]{ 0.558594, 0.589844}),

                // Top face
                new KeyValuePair<double[],double[]>(new double[]{ -0.25, 1.5, -0.1875 },new double[]{ 0.34375 , 0.402344}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.25, 1.5, 0.1875 },new double[]{ 0.34375, 0.3125}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.25, 1.5, -0.1875 },new double[]{ 0.464844 , 0.402344}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.25, 1.5, -0.1875 },new double[]{ 0.464844, 0.402344}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.25, 1.5, 0.1875 },new double[]{ 0.34375, 0.3125}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.25, 1.5, 0.1875 },new double[]{ 0.464844, 0.3125}),

                // Buttom face
                new KeyValuePair<double[],double[]>(new double[]{ -0.25, 0.75, -0.1875 },new double[]{ 0.46875 , 0.3125}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.25, 0.75, -0.1875 },new double[]{ 0.621094 , 0.3125}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.25, 0.75, 0.1875 },new double[]{ 0.46875, 0.402344}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.25, 0.75, 0.1875 },new double[]{ 0.46875, 0.402344}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.25, 0.75, -0.1875 },new double[]{ 0.621094, 0.3125}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.25, 0.75, 0.1875 },new double[]{ 0.621094, 0.402344})
            };
            return model;
        }

        protected KeyValuePair<double[], double[]>[] LegModel()
        {
            KeyValuePair<double[], double[]>[] model = new KeyValuePair<double[], double[]>[]
            {
                // Front face
                new KeyValuePair<double[],double[]>(new double[]{ 0.125, 0.75, 0.125 },new double[]{ 0.121094 , 0.40625}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.125, 0.75, 0.125 },new double[]{ 0.0625 , 0.40625}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.125, 0, 0.125 },new double[]{ 0.121094, 0.589844}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.125, 0, 0.125 },new double[]{ 0.121094, 0.589844}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.125, 0.75, 0.125 },new double[]{ 0.0625, 0.40625}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.125, 0, 0.125 },new double[]{ 0.0625, 0.589844}),

                // Back face
                new KeyValuePair<double[],double[]>(new double[]{ 0.125, 0.75, -0.125 },new double[]{ 0.1875 , 0.40625}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.125, 0, -0.125 },new double[]{ 0.1875, 0.589844}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.125, 0.75, -0.125 },new double[]{ 0.246094 , 0.40625}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.125, 0.75, -0.125 },new double[]{ 0.246094, 0.40625}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.125, 0, -0.125 },new double[]{ 0.1875, 0.589844}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.125, 0, -0.125 },new double[]{ 0.246094, 0.589844}),

                // Right face
                new KeyValuePair<double[],double[]>(new double[]{ 0.125, 0.75, -0.125 },new double[]{ 0.058594 , 0.40625}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.125, 0.75, 0.125 },new double[]{ 0 , 0.40625}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.125, 0, -0.125 },new double[]{ 0.058594, 0.589844}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.125, 0, -0.125 },new double[]{ 0.058594, 0.589844}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.125, 0.75, 0.125 },new double[]{ 0 , 0.40625}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.125, 0, 0.125 },new double[]{ 0 , 0.589844}),

                // Left face
                new KeyValuePair<double[],double[]>(new double[]{ -0.125, 0.75, -0.125 },new double[]{ 0.125 , 0.40625}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.125, 0, -0.125 },new double[]{ 0.125, 0.589844}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.125, 0.75, 0.125 },new double[]{ 0.183594 , 0.40625}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.125, 0.75, 0.125 },new double[]{ 0.183594, 0.40625}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.125, 0, -0.125 },new double[]{ 0.125, 0.589844}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.125, 0, 0.125 },new double[]{ 0.183594, 0.589844}),

                // Top face
                new KeyValuePair<double[],double[]>(new double[]{ -0.125, 0.75, -0.125 },new double[]{ 0.0625, 0.402344}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.125, 0.75, 0.125 },new double[]{ 0.0625, 0.34375}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.125, 0.75, -0.125 },new double[]{ 0.121094, 0.402344}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.125, 0.75, -0.125 },new double[]{ 0.121094, 0.402344}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.125, 0.75, 0.125 },new double[]{ 0.0625, 0.34375}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.125, 0.75, 0.125 },new double[]{ 0.121094, 0.34375}),

                // Buttom face
                new KeyValuePair<double[],double[]>(new double[]{ -0.125, 0, -0.125 },new double[]{ 0.125, 0.34375}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.125, 0, -0.125 },new double[]{ 0.183594, 0.34375}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.125, 0, 0.125 },new double[]{ 0.125, 0.402344}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.125, 0, 0.125 },new double[]{ 0.125, 0.402344}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.125, 0, -0.125 },new double[]{ 0.183594, 0.34375}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.125, 0, 0.125 },new double[]{ 0.183594, 0.402344})
            };
            return model;
        }
    }
}
