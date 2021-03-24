using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace BiblioMinecraft.World_System
{
    public class Game_Model
    {
        public double[][] model;
        public DiffuseMaterial mat;
        public Game_Model(double[][] model, DiffuseMaterial mat)
        {
            this.model = model;
            this.mat = mat;
        }
    }

    public interface Model
    {
        Game_Model Model();
    }
}
