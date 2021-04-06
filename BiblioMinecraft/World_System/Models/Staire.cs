using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.World_System.Models
{
    public abstract class Staire : Block
    {
        private DiffuseMaterial mat;
        public Staire(Location loc, DiffuseMaterial mat) : base(loc)
        {
            this.mat = mat;
        }

        protected override Game_Model BlockModel()
        {
            KeyValuePair<double[], double[]>[] model = new KeyValuePair<double[], double[]>[]
            {
                // Front face
                new KeyValuePair<double[],double[]>(new double[]{ 0.5, 0, 0.5 },new double[]{ 1 , 0.5}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.5, 0, 0.5 },new double[]{ 0 , 0.5}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.5, -0.5, 0.5 },new double[]{ 1 , 1}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.5, -0.5, 0.5 },new double[]{ 1 , 1}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.5, 0, 0.5 },new double[]{ 0 , 0.5}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.5, -0.5, 0.5 },new double[]{ 0 , 1}),

                new KeyValuePair<double[],double[]>(new double[]{ 0.5, 0.5, 0 },new double[]{ 1 , 0}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.5, 0.5, 0 },new double[]{ 0 , 0}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.5, 0, 0 },new double[]{ 1 , 0.5}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.5, 0, 0 },new double[]{ 1 , 0.5}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.5, 0.5, 0 },new double[]{ 0 , 0}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.5, 0, 0 },new double[]{ 0 , 0.5}),

                // Back face
                new KeyValuePair<double[],double[]>(new double[]{ 0.5, 0.5, -0.5 },new double[]{ 0 , 0}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.5, -0.5, -0.5 },new double[]{ 0 , 1}),
                new KeyValuePair<double[], double[]>(new double[]{ -0.5, 0.5, -0.5 },new double[]{ 1 , 0}),
                new KeyValuePair<double[], double[]>(new double[]{ -0.5, 0.5, -0.5 },new double[]{ 1 , 0}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.5, -0.5, -0.5 },new double[]{ 0 , 1}),
                new KeyValuePair<double[], double[]>(new double[]{ -0.5, -0.5, -0.5 },new double[]{ 1 , 1}),

                // Right face
                new KeyValuePair<double[], double[]>(new double[]{ 0.5, 0.5, -0.5 },new double[]{ 1 , 0}),
                new KeyValuePair<double[], double[]>(new double[]{ 0.5, -0.5, 0.5 },new double[]{ 0 , 1}),
                new KeyValuePair<double[], double[]>(new double[]{ 0.5, -0.5, -0.5 },new double[]{ 1 , 1}),

                new KeyValuePair<double[], double[]>(new double[]{ 0.5, 0.5, -0.5 },new double[]{ 1 , 0}),
                new KeyValuePair<double[], double[]>(new double[]{ 0.5, 0.5, 0 },new double[]{ 0.5 , 0}),
                new KeyValuePair<double[], double[]>(new double[]{ 0.5, 0, 0 },new double[]{ 0.5 , 0.5}),

                new KeyValuePair<double[], double[]>(new double[]{ 0.5, 0, 0 },new double[]{ 0.5 , 0.5}),
                new KeyValuePair<double[], double[]>(new double[]{ 0.5, 0, 0.5 },new double[]{ 0 , 0.5}),
                new KeyValuePair<double[], double[]>(new double[]{ 0.5, -0.5, 0.5 },new double[]{ 0 , 1}),

                // Left face
                new KeyValuePair<double[], double[]>(new double[]{ -0.5, 0.5, -0.5 },new double[]{ 0 , 0}),
                new KeyValuePair<double[], double[]>(new double[]{ -0.5, -0.5, -0.5 },new double[]{ 0 , 1}),
                new KeyValuePair<double[], double[]>(new double[]{ -0.5, -0.5, 0.5 },new double[]{ 1 , 1}),

                new KeyValuePair<double[], double[]>(new double[]{ -0.5, 0.5, -0.5 },new double[]{ 0 , 0}),
                new KeyValuePair<double[], double[]>(new double[]{ -0.5, 0, 0 },new double[]{ 0.5 , 0.5}),
                new KeyValuePair<double[], double[]>(new double[]{ -0.5, 0.5, 0 },new double[]{ 0.5 , 0}),

                new KeyValuePair<double[], double[]>(new double[]{ -0.5, 0, 0 },new double[]{ 0.5 , 0.5}),
                new KeyValuePair<double[], double[]>(new double[]{ -0.5, -0.5, 0.5 },new double[]{ 1 , 1}),
                new KeyValuePair<double[], double[]>(new double[]{ -0.5, 0, 0.5 },new double[]{ 1 , 0.5}),

                // Top face
                new KeyValuePair<double[],double[]>(new double[]{ -0.5, 0.5, -0.5 },new double[]{ 0 , 0}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.5, 0.5, 0 },new double[]{ 0 , 0.5}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.5, 0.5, -0.5 },new double[]{ 1 , 0}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.5, 0.5, -0.5 },new double[]{ 1 , 0}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.5, 0.5, 0 },new double[]{ 0 , 0.5}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.5, 0.5, 0 },new double[]{ 1 , 0.5}),

                new KeyValuePair<double[],double[]>(new double[]{ -0.5, 0, 0 },new double[]{ 0 , 0.5}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.5, 0, 0.5 },new double[]{ 0 , 1}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.5, 0, 0 },new double[]{ 1 , 0.5}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.5, 0, 0 },new double[]{ 1 , 0.5}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.5, 0, 0.5 },new double[]{ 0 , 1}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.5, 0, 0.5 },new double[]{ 1 , 1}),

                // Buttom face
                new KeyValuePair<double[],double[]>(new double[]{ -0.5, -0.5, -0.5 },new double[]{ 0 , 0}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.5, -0.5, -0.5 },new double[]{ 1 , 0}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.5, -0.5, 0.5 },new double[]{ 0 , 1}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.5, -0.5, 0.5 },new double[]{ 0 , 1}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.5, -0.5, -0.5 },new double[]{ 1 , 0}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.5, -0.5, 0.5 },new double[]{ 1 , 1})
            };

            Game_Model gm = new Game_Model(model, mat);
            return gm;
        }
    }
}
