using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Items;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Media.Imaging;
using CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.World_System.Models;
using CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Entities;
using System.Threading;

namespace CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.World_System.Blocks
{
    public class Chest : Block
    {
        private Inventaire inv;
        private DiffuseMaterial mat = Game_Model.GetImage(Helper.ImageFile + "Chest.PNG");
        public float opened = 0;
        private float topened = 0;
        public bool opening = false;

        public Chest(Location loc) : base(loc)
        {
            inv = new Inventaire(9, 3);
        }

        public override object Right_Click(Player player, Block block, Location loc)
        {
            opening = true;
            Thread t = new Thread(() => {
                while (opened < Math.PI/2 && opening)
                {
                    opened += 0.003f;
                    Thread.Sleep(1);
                }
            });
            t.Start();
            return inv;
        }

        public override void Update()
        {
            if (topened != opened)
            {
                Helper.group.RemoveBlock(this);
                Helper.group.AddBlock(this);
                topened = opened;
            }
        }

        public override void Destroy()
        {
            base.Destroy();
            foreach (Item it in inv.Items)
            {
                Item_Entity ie = new Item_Entity(Location.Clone(), it);
                Location.World.SpawnEntity(ie);
                Helper.group.AddEntity(ie);
            }
        }

        public override Game_Model Model()
        {
            Game_Model Cbase = base.Model();
            KeyValuePair<double[], double[]>[] a = Top_Model();
            for (int i = 0; i < a.Length; i++)
            {
                double[,] coord = new double[1, 3];
                coord[0, 0] = a[i].Key[0];
                coord[0, 1] = a[i].Key[1] - 0.11458;
                coord[0, 2] = a[i].Key[2] + (0.46875 * Math.Cos(loc.Yaw));
                double[,] rotmat = new double[3, 3];
                rotmat[0, 0] = Math.Cos(loc.Yaw);
                rotmat[1, 0] = Math.Sin(loc.Yaw) * Math.Sin(loc.Pitch - opened);
                rotmat[2, 0] = Math.Sin(loc.Yaw) * Math.Cos(loc.Pitch - opened);
                rotmat[0, 1] = 0;
                rotmat[1, 1] = Math.Cos(loc.Pitch - opened);
                rotmat[2, 1] = -Math.Sin(loc.Pitch - opened);
                rotmat[0, 2] = -Math.Sin(loc.Yaw);
                rotmat[1, 2] = Math.Cos(loc.Yaw) * Math.Sin(loc.Pitch - opened);
                rotmat[2, 2] = Math.Cos(loc.Yaw) * Math.Cos(loc.Pitch - opened);

                double[,] newcoord = Helper.MultiplyMatrix(coord, rotmat);

                a[i].Key[0] = newcoord[0, 0] + loc.X;
                a[i].Key[1] = newcoord[0, 1] + loc.Y + 0.11458;
                a[i].Key[2] = newcoord[0, 2] + loc.Z - (0.46875 * Math.Cos(loc.Yaw));
            }

            List<KeyValuePair<double[], double[]>> li = Cbase.model.ToList();
            foreach (KeyValuePair<double[], double[]> c in a)
            {
                li.Add(c);
            }
            Cbase.model = li.ToArray();

            return Cbase;
        }

        protected KeyValuePair<double[], double[]>[] Top_Model()
        {
            return new KeyValuePair<double[], double[]>[]{

                // Front face
                new KeyValuePair<double[],double[]>(new double[]{ 0.46875, 0.46875, 0.46875 },new double[]{ 0.49972, 0.325581}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.46875, 0.46875, 0.46875 },new double[]{ 0.25 , 0.325581}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.46875, 0.11458, 0.46875 },new double[]{ 0.49972, 0.441497}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.46875, 0.11458, 0.46875 },new double[]{ 0.49972, 0.441497}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.46875, 0.46875, 0.46875 },new double[]{ 0.25 , 0.325581}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.46875, 0.11458, 0.46875 },new double[]{ 0.25 , 0.441497}),

                new KeyValuePair<double[],double[]>(new double[]{ 0.085938, 0.261719, 0.5 },new double[]{ 0.053292, 0.023256}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.085938, 0.261719, 0.5 },new double[]{ 0.017857 , 0.023256}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.085938, -0.089844, 0.5 },new double[]{ 0.053292, 0.115916}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.085938, -0.089844, 0.5 },new double[]{ 0.053292, 0.115916}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.085938, 0.261719, 0.5 },new double[]{ 0.017857, 0.023256}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.085938, -0.089844, 0.5 },new double[]{ 0.017857, 0.115916}),

                // Back face
                new KeyValuePair<double[],double[]>(new double[]{ 0.46875, 0.46875, -0.46875 },new double[]{ 0.75 , 0.325581}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.46875, 0.11458, -0.46875 },new double[]{ 0.75 , 0.441497}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.46875, 0.46875, -0.46875 },new double[]{ 1 , 0.325581}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.46875, 0.46875, -0.46875 },new double[]{ 1 , 0.325581}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.46875, 0.11458, -0.46875 },new double[]{ 0.75 , 0.441497}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.46875, 0.11458, -0.46875 },new double[]{ 1 , 0.441497}),

                new KeyValuePair<double[],double[]>(new double[]{ 0.085938, 0.261719, 0.46875 },new double[]{ 0.071429 , 0.023256}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.085938, -0.089844, 0.46875 },new double[]{ 0.071429, 0.115916}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.085938, 0.261719, 0.46875 },new double[]{ 0.106864 , 0.023256}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.085938, 0.261719, 0.46875 },new double[]{ 0.106864, 0.023256}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.085938, -0.089844, 0.46875 },new double[]{ 0.071429, 0.115916}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.085938, -0.089844, 0.46875 },new double[]{ 0.106864, 0.115916}),

                // Right face
                new KeyValuePair<double[],double[]>(new double[]{ -0.46875, 0.46875, -0.46875 },new double[]{ 0 , 0.325581}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.46875, 0.11458, -0.46875 },new double[]{ 0 , 0.441497}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.46875, 0.46875, 0.46875 },new double[]{ 0.249721 , 0.325581}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.46875, 0.46875, 0.46875 },new double[]{ 0.249721, 0.325581}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.46875, 0.11458, -0.46875 },new double[]{ 0 , 0.441497}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.46875, 0.11458, 0.46875 },new double[]{ 0.249721, 0.441497}),

                new KeyValuePair<double[],double[]>(new double[]{ -0.085938, 0.261719, 0.46875 },new double[]{ 0 , 0.023256}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.085938, -0.089844, 0.46875 },new double[]{ 0 , 0.115916}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.085938, 0.261719, 0.5 },new double[]{ 0.017578 , 0.023256}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.085938, 0.261719, 0.5 },new double[]{ 0.017578, 0.023256}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.085938, -0.089844, 0.46875 },new double[]{ 0 , 0.115916}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.085938, -0.089844, 0.5 },new double[]{ 0.017578, 0.115916}),

                // Left face
                new KeyValuePair<double[],double[]>(new double[]{ 0.46875, 0.46875, -0.46875 },new double[]{ 0.731864, 0.325581}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.46875, 0.46875, 0.46875 },new double[]{ 0.5 , 0.325581}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.46875, 0.11458, -0.46875 },new double[]{ 0.731864, 0.441497}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.46875, 0.11458, -0.46875 },new double[]{ 0.731864 , 0.441497}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.46875, 0.46875, 0.46875 },new double[]{ 0.5 , 0.325581}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.46875, 0.11458, 0.46875 },new double[]{ 0.5 , 0.441497}),

                new KeyValuePair<double[],double[]>(new double[]{ 0.085938, 0.261719, 0.46875 },new double[]{ 0.07115, 0.023256}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.085938, 0.261719, 0.5 },new double[]{ 0.053571 , 0.023256}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.085938, -0.089844, 0.46875 },new double[]{ 0.07115, 0.115916}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.085938, -0.089844, 0.46875 },new double[]{ 0.07115, 0.115916}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.085938, 0.261719, 0.5 },new double[]{ 0.053571, 0.023256}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.085938, -0.089844, 0.5 },new double[]{ 0.053571, 0.115916}),

                // Top face
                new KeyValuePair<double[],double[]>(new double[]{ -0.46875, 0.46875, -0.46875 },new double[]{ 0.25 , 0.325218}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.46875, 0.46875, 0.46875 },new double[]{ 0.25 , 0}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.46875, 0.46875, -0.46875 },new double[]{ 0.499721 , 0.325218}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.46875, 0.46875, -0.46875 },new double[]{ 0.499721, 0.325218}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.46875, 0.46875, 0.46875 },new double[]{ 0.25 , 0}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.46875, 0.46875, 0.46875 },new double[]{ 0.499721, 0}),

                new KeyValuePair<double[],double[]>(new double[]{ -0.085938, 0.261719, 0.46875 },new double[]{ 0.017857, 0.022892}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.085938, 0.261719, 0.5 },new double[]{ 0.017857, 0}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.085938, 0.261719, 0.46875 },new double[]{ 0.053292, 0.022892}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.085938, 0.261719, 0.46875 },new double[]{ 0.053292, 0.022892}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.085938, 0.261719, 0.5 },new double[]{ 0.017857, 0}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.085938, 0.261719, 0.5 },new double[]{ 0.053292, 0}),

                // Buttom face
                new KeyValuePair<double[],double[]>(new double[]{ -0.46875, 0.11458, -0.46875 },new double[]{ 0.25 , 0.44186}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.46875, 0.11458, -0.46875 },new double[]{ 0.499721, 0.44186}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.46875, 0.11458, 0.46875 },new double[]{ 0.25 , 0.767078}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.46875, 0.11458, 0.46875 },new double[]{ 0.25 , 0.767078}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.46875, 0.11458, -0.46875 },new double[]{ 0.499721, 0.44186}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.46875, 0.11458, 0.46875 },new double[]{ 0.499721, 0.767078}),

                new KeyValuePair<double[],double[]>(new double[]{ -0.085938, -0.089844, 0.46875 },new double[]{ 0.053571 , 0}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.085938, -0.089844, 0.46875 },new double[]{ 0.089007, 0}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.085938, -0.089844, 0.5 },new double[]{ 0.053571, 0.022892}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.085938, -0.089844, 0.5 },new double[]{ 0.053571, 0.022892}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.085938, -0.089844, 0.46875 },new double[]{ 0.089007, 0}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.085938, -0.089844, 0.5 },new double[]{ 0.089007, 0.022892})
            };
        }

        protected override Game_Model BlockModel()
        {
            KeyValuePair<double[], double[]>[] model = new KeyValuePair<double[], double[]>[]{
                // Front face
                new KeyValuePair<double[],double[]>(new double[]{ 0.46875, 0.11458, 0.46875 },new double[]{ 0.49972, 0.767442}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.46875, 0.11458, 0.46875 },new double[]{ 0.25, 0.767442}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.46875, -0.5, 0.46875 },new double[]{ 0.49972, 1}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.46875, -0.5, 0.46875 },new double[]{ 0.49972, 1}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.46875, 0.11458, 0.46875 },new double[]{ 0.25, 0.767442}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.46875, -0.5, 0.46875 },new double[]{ 0.25, 1}),

                // Back face
                new KeyValuePair<double[],double[]>(new double[]{ 0.46875, 0.11458, -0.46875 },new double[]{ 0.75, 0.767442}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.46875, -0.5, -0.46875 },new double[]{ 0.75, 1}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.46875, 0.11458, -0.46875 },new double[]{ 1 , 0.767442}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.46875, 0.11458, -0.46875 },new double[]{ 1 , 0.767442}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.46875, -0.5, -0.46875 },new double[]{ 0.75, 1}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.46875, -0.5, -0.46875 },new double[]{ 1 , 1}),

                // Right face
                new KeyValuePair<double[],double[]>(new double[]{ -0.46875, 0.11458, -0.46875 },new double[]{ 0 , 0.767442}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.46875, -0.5, -0.46875 },new double[]{ 0 , 1}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.46875, 0.11458, 0.46875 },new double[]{ 0.249721, 0.767442}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.46875, 0.11458, 0.46875 },new double[]{ 0.249721, 0.767442}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.46875, -0.5, -0.46875 },new double[]{ 0 , 1}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.46875, -0.5, 0.46875 },new double[]{ 0.249721, 1}),

                // Left face
                new KeyValuePair<double[],double[]>(new double[]{ 0.46875, 0.11458, -0.46875 },new double[]{ 0.731864, 0.767442}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.46875, 0.11458, 0.46875 },new double[]{ 0.5 , 0.767442}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.46875, -0.5, -0.46875 },new double[]{ 0.731864, 1}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.46875, -0.5, -0.46875 },new double[]{ 0.731864, 1}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.46875, 0.11458, 0.46875 },new double[]{ 0.5 , 0.767442}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.46875, -0.5, 0.46875 },new double[]{ 0.5 , 1}),

                // Top face
                new KeyValuePair<double[],double[]>(new double[]{ -0.46875, 0.11458, -0.46875 },new double[]{ 0.5 , 0.325218}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.46875, 0.11458, 0.46875 },new double[]{ 0.5 , 0}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.46875, 0.11458, -0.46875 },new double[]{ 0.749721 , 0.325218}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.46875, 0.11458, -0.46875 },new double[]{ 0.749721, 0.325218}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.46875, 0.11458, 0.46875 },new double[]{ 0.5 , 0}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.46875, 0.11458, 0.46875 },new double[]{ 0.749721, 0}),

                // Buttom face
                new KeyValuePair<double[],double[]>(new double[]{ -0.46875, -0.5, -0.46875 },new double[]{ 0.5 , 0.44186}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.46875, -0.5, -0.46875 },new double[]{ 0.749721, 0.44186}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.46875, -0.5, 0.46875 },new double[]{ 0.5 , 0.767078}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.46875, -0.5, 0.46875 },new double[]{ 0.5 , 0.767078}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.46875, -0.5, -0.46875 },new double[]{ 0.749721, 0.44186}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.46875, -0.5, 0.46875 },new double[]{ 0.749721, 0.767078})
            };

            Game_Model gm = new Game_Model(model, mat);
            return gm;
        }

        public Inventaire Inventaire { get => inv; }
    }
}
