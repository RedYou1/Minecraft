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
using BiblioMinecraft;
using System.Windows.Media.Media3D;
using BiblioMinecraft.World_System;
using BiblioMinecraft.Items;
using BiblioMinecraft.Entities;
using BiblioMinecraft.World_System.Models;
using BiblioMinecraft.World_System.Blocks;
using BiblioMinecraft.Damages;
using System.Threading;
using BiblioMinecraft.Items.Armors;

namespace Minecraft
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Block[] blocks;
        public static Player player = new Player(new Location(-20, 15, 20, -(float)Math.PI / 6, (float)Math.PI - (float)Math.PI / 4, new World()));
        public static Item selected = null;
        public MainWindow()
        {
            InitializeComponent();
            Other.ImageFile = @"C:\Users\jcdem\source\repos\Minecraft\Images\";

            Iron_Helmet hel = new Iron_Helmet();
            player.Inventaire.AddItem(hel);
            player.Equipe(hel);
            Iron_ChestPlate che = new Iron_ChestPlate();
            player.Inventaire.AddItem(che);
            player.Equipe(che);
            Iron_Legging leg = new Iron_Legging();
            player.Inventaire.AddItem(leg);
            player.Equipe(leg);
            Iron_Boots boo = new Iron_Boots();
            player.Inventaire.AddItem(boo);
            player.Equipe(boo);

            for (int i = 0; i < player.Inventaire.Length; i++)
            {
                player.Inventaire.SetItem(new Steak(i + 2), i);
            }

            //player = new Player(new Location(0, 0, 5, 0, (float)Math.PI, new World()));
            GenerateWorld(player.Location.World);
            RegenerateWorld();

            World world = player.Location.World;

            camera.Position = new Point3D(
                    player.X,
                    player.Y,
                    player.Z);
            camera.LookDirection = new Vector3D((float)Math.Cos(player.Pitch) * (float)Math.Sin(player.Yaw), (float)Math.Sin(player.Pitch), (float)Math.Cos(player.Pitch) * (float)Math.Cos(player.Yaw));

            //Dispatcher.Invoke(() => { UpdateWorld(); });


        }

        public void ShowInventory(object cont)
        {
            RemoveInventory();
            if (cont is Player player)
            {
                Image image = new Image();
                image.Width = Width;
                image.Height = Height / 2;
                image.Source = new BitmapImage(new Uri(Other.ImageFile + "inventory.png"));
                Canvas.Children.Add(image);
                Canvas.SetTop(image, Height / 5);

                Func<string, double, bool> AddArmor = (id, i) =>
                  {
                      Image itImage = new Image();
                      itImage.Width = 64 / 3;
                      itImage.Height = 64 / 3;
                      itImage.Source = new BitmapImage(new Uri(Other.ImageFile + "Items\\Armors\\" + id + ".png"));
                      itImage.MouseDown += ItemClickInv;
                      Canvas.Children.Add(itImage);
                      Canvas.SetTop(itImage, Height / 5 + i);
                      Canvas.SetLeft(itImage, Width / 3 + 25);
                      return true;
                  };

                if (player.Helmet != null)
                {
                    AddArmor(player.Helmet.id(), 10);
                }
                if (player.ChestPlate != null)
                {
                    AddArmor(player.ChestPlate.id(), 34);
                }
                if (player.Legging != null)
                {
                    AddArmor(player.Legging.id(), 59);
                }
                if (player.Boots != null)
                {
                    AddArmor(player.Boots.id(), 83);
                }

                Inventaire inv = player.Inventaire;
                List<Func<bool>> tempqt = new List<Func<bool>>();
                for (int i = 0; i < 4 * 9; i++)
                {
                    Item it = inv.GetItem(i);
                    if (it != null)
                    {
                        int x = i % inv.Width;
                        int y = (int)(i / inv.Width);

                        tempqt.Add(() =>
                        {
                            Image itImage = new Image();
                            itImage.Width = 64 / 3;
                            itImage.Height = 64 / 3;
                            itImage.Source = new BitmapImage(new Uri(Other.ImageFile + "Items\\" + it.id() + ".png"));
                            itImage.MouseDown += ItemClickInv;
                            Canvas.Children.Add(itImage);
                            Canvas.SetTop(itImage, Height / 2 + Height / 8 - (y * 27) + 2);
                            Canvas.SetLeft(itImage, Width / 3 + (x * 24.5) + 25);
                            return true;
                        });

                        if (it.Quantity != 1)
                        {
                            TextBlock tb = new TextBlock();
                            tb.Text = "" + it.Quantity;
                            tb.Background = Brushes.White;
                            tb.Foreground = Brushes.Black;
                            tb.Width = 25;
                            tb.Height = 20;
                            Canvas.Children.Add(tb);
                            Canvas.SetTop(tb, Height / 2 + Height / 8 - ((y - 1) * 27) - 10);
                            Canvas.SetLeft(tb, Width / 3 + ((x + 1) * 24.5) + 10);
                        }
                    }
                }
                foreach (Func<bool> a in tempqt)
                {
                    a();
                }
            }
            if (cont is Inventaire chest)
            {
                Image image = new Image();
                image.Width = Width;
                image.Height = Height / 2;
                image.Source = new BitmapImage(new Uri(Other.ImageFile + "inventory.png"));
                Canvas.Children.Add(image);
                Canvas.SetTop(image, Height / 5);
            }
        }

        private void ItemClickInv(object sender, MouseEventArgs e)
        {
            Point pos = e.GetPosition(Canvas);

            if (Height / 2.3f > pos.Y)
            {
                //armor...
            }
            else
            {
                int x = (int)((pos.X - Width / 3 - 25) / 24.5);
                int y = (int)(((pos.Y - Height / 2 + Height / 8 - 2) / 27) - 5) * -1;
                Item item = player.Inventaire.GetItem(x + (y * player.Inventaire.Width));
                if (item != null)
                {
                    foreach (UIElement a in Canvas.Children)
                    {
                        double y1 = Canvas.GetTop(a);
                        double y2 = Height / 2 + Height / 8 - (y * 27) + 2;
                        int x1 = (int)((Canvas.GetLeft(a) + 10) / 10);
                        int x2 = (int)((Width / 3 + (x * 24.5) + 10) / 10);
                        if (y1 == y2 && x1 == x2)
                        {
                            int b = Canvas.Children.IndexOf(a);
                            player.Inventaire.SetItem(selected, x + (y * player.Inventaire.Width));
                            selected = item;

                            if (selected != null)
                            {
                                UIElement t = Canvas.Children[Canvas.Children.Count - 1];
                                //Canvas.Children[Canvas.Children.Count - 1].
                                List<UIElement> ui = new List<UIElement>();
                                foreach(UIElement uiin in Canvas.Children)
                                {
                                    ui.Add(uiin);
                                }
                                ui[ui.Count - 1] = ui[b];
                                ui[b] = t;
                                Canvas.Children.Clear();
                                foreach (UIElement uiin in ui)
                                {
                                    Canvas.Children.Add(uiin);
                                }
                                return;
                            }
                            else
                            {
                                Image iImage = new Image();
                                iImage.Width = 64 / 3;
                                iImage.Height = 64 / 3;
                                iImage.Source = new BitmapImage(new Uri(Other.ImageFile + "Items\\" + selected.id() + ".png"));
                                Canvas.Children.Add(iImage);
                            }
                        }
                    }
                    
                    /*
                    foreach (UIElement a in Canvas.Children)
                    {
                        double y1 = Canvas.GetTop(a);
                        double y2 = Height / 2 + Height / 8 - (y * 27) + 2;
                        int x1 = (int)((Canvas.GetLeft(a)+10)/10);
                        int x2 = (int)((Width / 3 + (x * 24.5) + 10)/10);
                        if (y1 == y2 && x1 ==  x2)
                        {
                            Canvas.MouseMove += SelectedMove;
                        }
                    }
                    */
                }
                else if (selected != null)
                {
                    player.Inventaire.SetItem(selected, x + (y * player.Inventaire.Width));
                    /*
                    foreach (UIElement a in Canvas.Children)
                    {
                        if (Canvas.GetTop(a) == Height / 2 + Height / 8 - (y * 27) + 2 && Canvas.GetLeft(a) == Width / 3 + ((x + 1) * 24.5) + 10)
                        {
                            a.MouseMove -= SelectedMove;
                        }
                    }
                    */
                }
            }
        }

        private void SelectedMove(object sender, MouseEventArgs e)
        {
            if (selected != null)
            {
                Canvas.SetTop(Canvas.Children[Canvas.Children.Count-1], e.GetPosition(Canvas).Y);
                Canvas.SetLeft(Canvas.Children[Canvas.Children.Count - 1], e.GetPosition(Canvas).X);
            }
        }

        public void RemoveInventory()
        {
            if (selected != null)
            {
                player.Location.World.SpawnEntity(new Item_Entity(player.Location, selected));
                selected = null;
            }
            Canvas.Children.Clear();
        }

        public void GenerateWorld(World world)
        {
            Noise noise = new Noise();
            for (int x = -20; x <= 20; x++)
            {
                for (int z = -20; z <= 20; z++)
                {
                    world.SetBlock(new CobbleStone_Block(new Location(x, (int)(noise.Evaluate((float)x / 10, (float)z / 10) * 4 - 4), z, world)));
                }
            }
            world.SetBlock(new Wooden_staire(new Location(-2, 0, 0, world)));
            world.SetBlock(new Dirt_Block(new Location(0, 2, 0, world)));
            world.SetBlock(new CobbleStone_Block(new Location(0, 0, 0, world)));
            world.SetBlock(new Wooden_Block(new Location(2, 0, 0, world)));
            world.SetBlock(new Chest(new Location(-2, 2, 0, world)));
        }

        public void RegenerateWorld()
        {
            blocks = player.Location.World.Blocks;
            UpdateWorld();
        }

        public static bool open = true;

        private void Game_Closing(object sender, EventArgs e)
        {
            open = false;
        }

        private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
        {

        }

        public void UpdateBlock(int index, Block block)
        {
            if (index > 1 && index < group.Children.Count)
            {
                if (block.Location.World.Name == player.Location.World.Name)
                {
                    group.Children[index] = BlockToModel(block);
                }
                else
                {
                    group.Children.RemoveAt(index);
                }
            }
            else
            {
                if (block.Location.World.Name == player.Location.World.Name)
                {
                    group.Children.Add(BlockToModel(block));
                }
            }
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
                group.Children.Add(BlockToModel(block));
            }
        }

        public static GeometryModel3D BlockToModel(Block block)
        {
            MeshGeometry3D mesh = new MeshGeometry3D();

            Game_Model a = block.Model();
            PointCollection pc = new PointCollection();
            for (int i = 0; i < a.model.Length; i++)
            {
                KeyValuePair<double[], double[]> paire = a.model[i];
                double[] vertex = paire.Key;
                double[] tex = paire.Value;
                mesh.Positions.Add(new Point3D(vertex[0], vertex[1], vertex[2]));
                pc.Add(new Point(tex[0], tex[1]));
                mesh.TriangleIndices.Add(i);
            }
            mesh.TextureCoordinates = pc;

            GeometryModel3D mGeometry = new GeometryModel3D(mesh, a.mat);
            Transform3DGroup trans = new Transform3DGroup();
            mGeometry.Transform = trans;
            return mGeometry;
        }

        public void Grid_MousePress(object sender, MouseButtonEventArgs e)
        {
            MouseButton button = e.ChangedButton;
            switch (button)
            {
                case MouseButton.Right:
                    {
                        object qqch = player.GetInFrontOfHim(20);
                        if (qqch != null)
                        {
                            if (qqch is Block bl)
                            {
                                object ob = bl.Right_Click(player, new Wooden_Block(bl.Location), bl.Location);
                                if (ob != null)
                                {
                                    if (ob is Block blo)
                                    {
                                        List<Block> blockss = blocks.ToList();
                                        blockss.Add(blo);
                                        blocks = blockss.ToArray();
                                        UpdateBlock(group.Children.Count, blo);
                                    }
                                    if (ob is Inventaire inv)
                                    {
                                        ShowInventory(inv);
                                    }
                                }
                                return;
                            }
                        }
                    }
                    break;
                case MouseButton.Left:
                    {
                        object qqch = player.GetInFrontOfHim(20);
                        if (qqch != null)
                        {
                            if (qqch is Entity ent)
                            {
                                bool died = ent.TakeDamage(new PhysicalDamage(1));

                                // TODO: enlever model losque sera implementer
                            }
                            if (qqch is Block block)
                            {
                                block.Left_Click(player);

                                List<Block> bl = blocks.ToList();
                                int i = bl.IndexOf(block);
                                group.Children.RemoveAt(i + 2);
                                bl.Remove(block);
                                blocks = bl.ToArray();
                            }
                        }
                    }
                    break;
                case MouseButton.Middle:
                    break;
            }
        }

        private static bool ctrl = true;

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            float pitch = player.Pitch;
            float yaw = player.Yaw;
            Key key = e.Key;
            float speed = 0.12f;
            float speedrot = 0.02f;

            if (key == Key.F)
            {
                if (Canvas.Children.Count > 0)
                {
                    RemoveInventory();
                }
                else
                {
                    ShowInventory(player);
                }
                return;
            }

            if (key == Key.Delete)
            {
                player.Location.ChangeWorld(player.X, player.Y, player.Z, player.Pitch, player.Yaw, new World());
                GenerateWorld(player.Location.World);
                RegenerateWorld();
                return;
            }

            if (key == Key.Y)
            {
                blocks[blocks.Length - 1].Move(0, 0, 0, 0.2f, 0);
                UpdateBlock(group.Children.Count - 1, blocks[blocks.Length - 1]);
                return;
            }

            if (key == Key.LeftCtrl)
            {
                ctrl = !ctrl;
                if (ctrl)
                {
                    button.Content = "Creative";
                }
                else
                {
                    button.Content = "Spectator";
                }
                return;
            }
            if (ctrl)
            {
                switch (key)
                {
                    case Key.W:
                        player.Move((float)Math.Sin(yaw) * speed, 0, (float)Math.Cos(yaw) * speed, 0, 0);
                        break;
                    case Key.S:
                        player.Move((float)Math.Sin(yaw) * -speed, 0, (float)Math.Cos(yaw) * -speed, 0, 0);
                        break;
                    case Key.A:
                        player.Move((float)Math.Sin(yaw + (Math.PI / 2)) * speed, 0, (float)Math.Cos(yaw + (Math.PI / 2)) * speed, 0, 0);
                        break;
                    case Key.D:
                        player.Move((float)Math.Sin(yaw + (Math.PI / 2)) * -speed, 0, (float)Math.Cos(yaw + (Math.PI / 2)) * -speed, 0, 0);
                        break;
                    case Key.Space:
                        player.Move(0, speed, 0, 0, 0);
                        break;
                    case Key.LeftShift:
                        player.Move(0, -speed, 0, 0, 0);
                        break;
                    case Key.K:
                        player.Move(0, 0, 0, -speedrot, 0);
                        break;
                    case Key.L:
                        player.Move(0, 0, 0, speedrot, 0);
                        break;
                    case Key.E:
                        player.Move(0, 0, 0, 0, -speedrot);
                        break;
                    case Key.Q:
                        player.Move(0, 0, 0, 0, speedrot);
                        break;
                }
            }
            else
            {
                switch (key)
                {
                    case Key.W:
                        player.Move((float)Math.Cos(pitch) * (float)Math.Sin(yaw) * speed, (float)Math.Sin(pitch) * speed, (float)Math.Cos(pitch) * (float)Math.Cos(yaw) * speed, 0, 0);
                        break;
                    case Key.S:
                        player.Move((float)Math.Cos(pitch) * (float)Math.Sin(yaw) * -speed, (float)Math.Sin(pitch) * -speed, (float)Math.Cos(pitch) * (float)Math.Cos(yaw) * -speed, 0, 0);
                        break;
                    case Key.A:
                        player.Move((float)Math.Sin(yaw + (Math.PI / 2)) * speed, 0, (float)Math.Cos(yaw + (Math.PI / 2)) * speed, 0, 0);
                        break;
                    case Key.D:
                        player.Move((float)Math.Sin(yaw + (Math.PI / 2)) * -speed, 0, (float)Math.Cos(yaw + (Math.PI / 2)) * -speed, 0, 0);
                        break;
                    case Key.Space:
                        player.Move(0, speed, 0, 0, 0);
                        break;
                    case Key.LeftShift:
                        player.Move(0, -speed, 0, 0, 0);
                        break;
                    case Key.K:
                        player.Move(0, 0, 0, -speedrot, 0);
                        break;
                    case Key.L:
                        player.Move(0, 0, 0, speedrot, 0);
                        break;
                    case Key.E:
                        player.Move(0, 0, 0, 0, -speedrot);
                        break;
                    case Key.Q:
                        player.Move(0, 0, 0, 0, speedrot);
                        break;
                }
            }
            camera.Position = new Point3D(
                    player.X,
                    player.Y,
                    player.Z);
            camera.LookDirection = new Vector3D((float)Math.Cos(player.Pitch) * (float)Math.Sin(player.Yaw), (float)Math.Sin(player.Pitch), (float)Math.Cos(player.Pitch) * (float)Math.Cos(player.Yaw));
        }
    }
}
