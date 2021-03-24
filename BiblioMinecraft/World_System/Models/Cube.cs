using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace BiblioMinecraft.World_System.Models
{
    public abstract class Cube : Block
    {
        private DiffuseMaterial mat;
        public Cube(float x, float y, float z, DiffuseMaterial mat) : base(x, y, z)
        {

            this.mat = mat;
        }

        public override Game_Model Model()
        {
            double[][] v = new double[8][];
            v[0] = new double[3] { -0.5 + x, 0.5 + y, 0.5 + z };
            v[1] = new double[3] { 0.5 + x, -0.5 + y, 0.5 + z };
            v[2] = new double[3] { 0.5 + x, 0.5 + y, 0.5 + z };
            v[3] = new double[3] { -0.5 + x, -0.5 + y, 0.5 + z };
            v[4] = new double[3] { -0.5 + x, 0.5 + y, -0.5 + z };
            v[5] = new double[3] { 0.5 + x, -0.5 + y, -0.5 + z };
            v[6] = new double[3] { 0.5 + x, 0.5 + y, -0.5 + z };
            v[7] = new double[3] { -0.5 + x, -0.5 + y, -0.5 + z };
            Game_Model gm = new Game_Model(v, mat);
            return gm;
        }
    }
}
