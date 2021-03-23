using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Media3D;

namespace Minecraft
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GeometryModel3D mGeometry;
        public MainWindow()
        {
            InitializeComponent();
            // Define 3D mesh object
            MeshGeometry3D mesh = new MeshGeometry3D();
            // Front face
            mesh.Positions.Add(new Point3D(-0.5, -0.5, 1));
            mesh.Positions.Add(new Point3D(0.5, -0.5, 1));
            mesh.Positions.Add(new Point3D(0.5, 0.5, 1));
            mesh.Positions.Add(new Point3D(-0.5, 0.5, 1));
            // Back face
            mesh.Positions.Add(new Point3D(-1, -1, -1));
            mesh.Positions.Add(new Point3D(1, -1, -1));
            mesh.Positions.Add(new Point3D(1, 1, -1));
            mesh.Positions.Add(new Point3D(-1, 1, -1));
            // Front face
            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(3);
            mesh.TriangleIndices.Add(0);
            // Back face
            mesh.TriangleIndices.Add(6);
            mesh.TriangleIndices.Add(5);
            mesh.TriangleIndices.Add(4);
            mesh.TriangleIndices.Add(4);
            mesh.TriangleIndices.Add(7);
            mesh.TriangleIndices.Add(6);
            // Right face
            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(5);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(5);
            mesh.TriangleIndices.Add(6);
            mesh.TriangleIndices.Add(2);
            // Other faces (see complete source code)...

            // Geometry creation
            mGeometry = new GeometryModel3D(mesh, new DiffuseMaterial(Brushes.YellowGreen));
            mGeometry.Transform = new Transform3DGroup();
            group.Children.Add(mGeometry);
        }

        private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            char key = e.Key.ToString()[0];
            float speed = 0.02f;
            switch (key)
            {
                case 'W':
                    Main_Thread.player.Move(0,0,-speed,0,0);
                    break;
                case 'S':
                    Main_Thread.player.Move(0, 0, speed, 0, 0);
                    break;
                case 'A':
                    Main_Thread.player.Move(-speed, 0, 0, 0, 0);
                    break;
                case 'D':
                    Main_Thread.player.Move(speed, 0, 0, 0, 0);
                    break;
            }
            camera.Position = new Point3D(
                    Main_Thread.player.X,
                    Main_Thread.player.Y,
                    Main_Thread.player.Z);
        }
    }
}
