using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Media.Imaging;

namespace BiblioMinecraft.World_System
{
    public class Game_Model
    {
        public KeyValuePair<double[],double[]>[] model;
        public DiffuseMaterial mat;
        public Game_Model(KeyValuePair<double[], double[]>[] model, DiffuseMaterial mat)
        {
            this.model = model;
            this.mat = mat;
        }

        public static DiffuseMaterial GetImage(String fullpath)
        {
            ImageBrush colors_brush = new ImageBrush();
            colors_brush.ImageSource = new BitmapImage(new Uri(fullpath, UriKind.Relative));
            DiffuseMaterial colors_material = new DiffuseMaterial(colors_brush);
            return colors_material;
        }
    }

    public interface Model
    {
        Game_Model Model();
    }
}
