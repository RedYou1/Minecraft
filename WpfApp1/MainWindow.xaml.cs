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
using BiblioMinecraft.World_System.Models;
using BiblioMinecraft.World_System.Blocks;

namespace Minecraft
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Block[] blocks;
        public static Player player = new Player(0, 0, 5, (float)Math.PI, 0);
        public MainWindow()
        {
            InitializeComponent();
            player = new BiblioMinecraft.Entities.Player(0, 0, 5, (float)Math.PI, 0);
            blocks = new Block[3] { new Stone_Block(0, 0, 0), new Stone_Block(0, 2, 0), new Dirt_Block(0, 1, 0) };
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
            Model3D g1 = group.Children[0];
            Model3D g2 = group.Children[1];
            group.Children.Clear();
            group.Children.Add(g1);
            group.Children.Add(g2);
            foreach (Block block in blocks)
            {
                MeshGeometry3D mesh = new MeshGeometry3D();

                int pre = mesh.Positions.Count;
                Game_Model a = block.Model();
                for (int i = 0; i < a.model.Length; i++)
                {
                    mesh.Positions.Add(new Point3D(a.model[i][0], a.model[i][1], a.model[i][2]));
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

                GeometryModel3D mGeometry = new GeometryModel3D(mesh, a.mat);
                mGeometry.Transform = new Transform3DGroup();
                group.Children.Add(mGeometry);
            }
        }

        public void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MouseButton button = e.ChangedButton;
            switch (button)
            {
                case MouseButton.Right:
                    break;
                case MouseButton.Left:
                    UpdateWorld();
                    break;
                case MouseButton.Middle:
                    break;
            }
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
                    player.Move((float)Math.Sin(pitch + (Math.PI / 2)) * speed, 0, (float)Math.Cos(pitch + (Math.PI / 2)) * speed, 0, 0);
                    break;
                case Key.D:
                    player.Move((float)Math.Sin(pitch + (Math.PI / 2)) * -speed, 0, (float)Math.Cos(pitch + (Math.PI / 2)) * -speed, 0, 0);
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
