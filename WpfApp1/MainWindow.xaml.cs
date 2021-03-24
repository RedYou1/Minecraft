using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Media3D;
using BiblioMinecraft.World_System;
using BiblioMinecraft.Entities;

namespace Minecraft
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static GeometryModel3D mGeometry;
        public static Block[] blocks;
        public static Player player = new Player(0, 0, 5, (float)Math.PI, 0);
        public MainWindow()
        {
            InitializeComponent();
            player = new BiblioMinecraft.Entities.Player(0, 0, 5, (float)Math.PI, 0);
            blocks = new Block[2]{ new Block(0, 0, 0),new Block(0, 2, 0)};
            UpdateWorld();
            /*
            System.Threading.Thread t = new System.Threading.Thread( () =>
            {
                while (true)
                {
                    
                    System.Threading.Thread.Sleep(17);
                }
            });
            t.Start();
            */
        }

        private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
        {

        }

        public void UpdateWorld()
        {
            // Define 3D mesh object
            MeshGeometry3D mesh = new MeshGeometry3D();

            foreach (Block block in blocks)
            {

                int pre = mesh.Positions.Count;
                double[][] a = block.Box();
                for (int i = 0; i < a.Length; i++)
                {
                    mesh.Positions.Add(new Point3D(a[i][0], a[i][1], a[i][2]));
                }

                // Front face
                mesh.TriangleIndices.Add(pre + 2);
                mesh.TriangleIndices.Add(pre + 0);
                mesh.TriangleIndices.Add(pre + 1);
                mesh.TriangleIndices.Add(pre + 1);
                mesh.TriangleIndices.Add(pre + 0);
                mesh.TriangleIndices.Add(pre + 3);
                // Back face
                mesh.TriangleIndices.Add(pre + 6);
                mesh.TriangleIndices.Add(pre + 5);
                mesh.TriangleIndices.Add(pre + 4);
                mesh.TriangleIndices.Add(pre + 4);
                mesh.TriangleIndices.Add(pre + 5);
                mesh.TriangleIndices.Add(pre + 7);
                // Right face
                mesh.TriangleIndices.Add(pre + 6);
                mesh.TriangleIndices.Add(pre + 2);
                mesh.TriangleIndices.Add(pre + 5);
                mesh.TriangleIndices.Add(pre + 5);
                mesh.TriangleIndices.Add(pre + 2);
                mesh.TriangleIndices.Add(pre + 1);
                // Left face
                mesh.TriangleIndices.Add(pre + 4);
                mesh.TriangleIndices.Add(pre + 7);
                mesh.TriangleIndices.Add(pre + 0);
                mesh.TriangleIndices.Add(pre + 0);
                mesh.TriangleIndices.Add(pre + 7);
                mesh.TriangleIndices.Add(pre + 3);
                // Top face
                mesh.TriangleIndices.Add(pre + 4);
                mesh.TriangleIndices.Add(pre + 0);
                mesh.TriangleIndices.Add(pre + 6);
                mesh.TriangleIndices.Add(pre + 6);
                mesh.TriangleIndices.Add(pre + 0);
                mesh.TriangleIndices.Add(pre + 2);
                // Buttom face
                mesh.TriangleIndices.Add(pre + 7);
                mesh.TriangleIndices.Add(pre + 5);
                mesh.TriangleIndices.Add(pre + 3);
                mesh.TriangleIndices.Add(pre + 3);
                mesh.TriangleIndices.Add(pre + 5);
                mesh.TriangleIndices.Add(pre + 1);
            }

            // Geometry creation
            mGeometry = new GeometryModel3D(mesh, new DiffuseMaterial(Brushes.YellowGreen));
            mGeometry.Transform = new Transform3DGroup();
            group.Children.Add(mGeometry);
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            float pitch = player.Pitch;
            float yaw = player.Yaw;
            Key key = e.Key;
            float speed = 0.04f;
            switch (key)
            {
                case Key.W:
                    player.Move((float)Math.Cos(yaw) * (float)Math.Sin(pitch) * speed, (float)Math.Sin(yaw) * speed, (float)Math.Cos(yaw) * (float)Math.Cos(pitch) * speed, 0, 0);
                    break;
                case Key.S:
                    player.Move((float)Math.Cos(yaw) * (float)Math.Sin(pitch) * -speed, (float)Math.Sin(yaw) * -speed, (float)Math.Cos(yaw) * (float)Math.Cos(pitch) * -speed, 0, 0);
                    break;
                case Key.A:
                    player.Move(-speed, 0, 0, 0, 0);
                    break;
                case Key.D:
                    player.Move(speed, 0, 0, 0, 0);
                    break;
                case Key.Space:
                    player.Move(0, speed, 0, 0, 0);
                    break;
                case Key.LeftShift:
                    player.Move(0, -speed, 0, 0, 0);
                    break;
                case Key.E:
                    player.Move(0, 0, 0, -speed / 2, 0);
                    break;
                case Key.Q:
                    player.Move(0, 0, 0, speed / 2, 0);
                    break;
                case Key.K:
                    player.Move(0, 0, 0, 0, -speed / 2);
                    break;
                case Key.L:
                    player.Move(0, 0, 0, 0, speed / 2);
                    break;
            }
            camera.Position = new Point3D(
                    player.X,
                    player.Y,
                    player.Z);
            camera.LookDirection = new Vector3D(Math.Sin(player.Pitch), Math.Sin(player.Yaw), Math.Cos(player.Pitch));
        }
    }
}
