using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Media.Imaging;
using System.Windows.Controls;

namespace CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.World_System
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
            if (Helper.goodimage) {
                Image colors_brush = new Image();
                colors_brush.Source = new BitmapImage(new Uri(fullpath, UriKind.Relative));
                RenderOptions.SetCachingHint(colors_brush, CachingHint.Cache);
                RenderOptions.SetBitmapScalingMode(colors_brush, BitmapScalingMode.NearestNeighbor);
                DiffuseMaterial colors_material = new DiffuseMaterial(new VisualBrush(colors_brush));
                return colors_material;
            }
            else {
                ImageBrush colors_brush = new ImageBrush();
                colors_brush.ImageSource = new BitmapImage(new Uri(fullpath, UriKind.RelativeOrAbsolute));
                DiffuseMaterial colors_material = new DiffuseMaterial(colors_brush);
                return colors_material;
            }
        }
    }

    public interface Model
    {
        Game_Model Model();
    }
}
