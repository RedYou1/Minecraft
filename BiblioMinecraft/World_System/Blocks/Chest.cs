using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiblioMinecraft.Items;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Media.Imaging;
using BiblioMinecraft.World_System.Models;
using BiblioMinecraft.Entities;

namespace BiblioMinecraft.World_System.Blocks
{
    public class Chest : Block
    {
        private Inventaire inv;
        private DiffuseMaterial mat = Game_Model.GetImage(Helper.ImageFile + "Chest.PNG");
        //public float opened = 0;
        public Chest(Location loc) : base(loc)
        {
            inv = new Inventaire(9, 3);
        }

        public override object Right_Click(BiblioMinecraft.Entities.Player player, Block block, Location loc)
        {
            return inv;
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
                double[] rot = CoordToRot(a[i].Key);
                rot[0] += loc.Pitch;
                //rot[1] += yaw;
                //TODO: +opened (yaw but not in origine)
                double dist = Math.Sqrt(a[i].Key[1] * a[i].Key[1] + (a[i].Key[0] * a[i].Key[0] + a[i].Key[2] * a[i].Key[2]));
                double[] model = RotToCoord(rot, dist);
                a[i].Key[0] = model[0];
                a[i].Key[1] = model[1];
                a[i].Key[2] = model[2];

                a[i].Key[0] += loc.X;
                a[i].Key[1] += loc.Y;
                a[i].Key[2] += loc.Z;
            }
            //pitch += 0.01f;

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
