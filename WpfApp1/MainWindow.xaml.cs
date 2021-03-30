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
using BiblioMinecraft.Attributes;

namespace Minecraft
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Block[] blocks;
        public static Player player = new Player(new Location(-20, 15, 20, -(float)Math.PI / 6, (float)Math.PI - (float)Math.PI / 4, new World()));
        public static int selected = -1;
        public MainWindow()
        {
            InitializeComponent();
            Helper.ImageFile = @"C:\Users\jcdem\source\repos\Minecraft\Images\";

            Canvas.MouseDown += ItemClickInv;

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

            player.Inventaire.SetItem(new Emerald(0 + 2), 0);
            for (int i = 1; i < player.Inventaire.Length - player.Inventaire.Width; i++)
            {
                player.Inventaire.SetItem(new Steak(i + 2), i);
            }

            Diamond_Helmet hel2 = new Diamond_Helmet();
            player.Inventaire.AddItem(hel2);
            Diamond_ChestPlate che2 = new Diamond_ChestPlate();
            player.Inventaire.AddItem(che2);
            Diamond_Legging leg2 = new Diamond_Legging();
            player.Inventaire.AddItem(leg2);
            Diamond_Boots boo2 = new Diamond_Boots();
            player.Inventaire.AddItem(boo2);

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

        private UI_Inventaire inv;

        public void ShowInventory(object cont)
        {
            RemoveInventory();
            Helper.Width = Width;
            Helper.Height = Height;
            if (cont is Player player)
            {
                inv = new UI_Inventaire(player, 32, 32);
                Canvas.Children.Add(inv);
            }
            if (cont is Inventaire chest)
            {
                Image image = new Image();
                image.Width = Width;
                image.Height = Height / 2;
                image.Source = new BitmapImage(new Uri(Helper.ImageFile + "inventory.png"));
                Canvas.Children.Add(image);
                Canvas.SetTop(image, Height / 5);
            }
        }

        private void ItemClickInv(object sender, MouseEventArgs e)
        {
            Point pos = e.GetPosition(Canvas);

            UI_Item[] its = inv.GetItem(pos.X, pos.Y);
            if (its.Length > 0)
            {
                if (selected == -1)
                {
                    player.Inventaire.RemoveItem(its[0].item);
                    selected = inv.GetIndex(its[0].item);

                    Point p = Mouse.GetPosition(Canvas);
                    its[0].x = p.X - inv.ItWidth / 2;
                    its[0].y = p.Y - inv.ItHeight / 2;
                }
                else if (its.Length > 1)
                {
                    for (int i = 0; i < its.Length; i++)
                    {
                        int h = inv.GetIndex(its[i].item);
                        if (h != selected)
                        {
                            UI_Item it = inv.GetItem(h);

                            int x = (int)((it.x - Helper.Width / 4 - 20) / 41);
                            int y = (int)((it.y - Helper.Height + Helper.Height / 4 - 5) / -41 + 1);
                            player.Inventaire.SetItem(inv.GetItem(selected).item, x + (y * player.Inventaire.Width));

                            inv.Move(selected, it.x, it.y);
                            selected = h;
                            player.Inventaire.RemoveItem(inv.GetItem(selected).item);
                            break;
                        }
                    }
                }
                else
                {
                    UI_Item it = inv.GetItem(selected);
                    int x = (int)((it.x - Helper.Width / 4 - 20) / 41);
                    int y = (int)((it.y - Helper.Height + Helper.Height / 4 - 5) / -41+1);
                    player.Inventaire.SetItem(it.item,x+(y*player.Inventaire.Width));
                    inv.Move(selected, Helper.Width / 4 + (x * 41) + 20, Helper.Height - Helper.Height / 4 - (y * 41) + 5);
                    selected = -1;
                }
                Canvas.Children.Remove(inv);
                Canvas.Children.Add(inv);
            }

            /*
            if (Height / 2.3f > pos.Y)
            {
                //TODO: crafting
                int x = (int)((pos.X - Width / 3 - 25) / 24.5);
                if (x == 0)
                {
                    int y = (int)(((pos.Y - Height / 2 + Height / 8 - 2) / 27) - 5) * -1;
                    foreach (UIElement a in Canvas.Children)
                    {
                        if (a is Image img)
                        {
                            if (img.Width > 128)
                            {
                                continue;
                            }
                            int y1 = (int)(((Canvas.GetTop(a) - Height / 2 + Height / 8 - 2) / 27) - 5) * -1;
                            if (y1 == y)
                            {
                                int b = Canvas.Children.IndexOf(a);
                                switch (y)
                                {
                                    //boots
                                    case 4:
                                        {
                                            if (selected is Boots sel)
                                            {
                                                SwapWithSelect(b, selected == null);
                                                selected = player.Boots;
                                                player.Boots = sel;
                                                if (selected == null)
                                                {
                                                    Canvas.SetTop(Canvas.Children[b], Height / 5 + 83);
                                                    Canvas.SetLeft(Canvas.Children[b], Width / 3 + 25);
                                                }
                                            }
                                            else if (selected == null)
                                            {
                                                SwapWithSelect(b, selected == null);
                                                selected = player.Boots;
                                                player.Boots = null;
                                            }
                                        }
                                        return;
                                    //leggings
                                    case 5:
                                        {
                                            if (selected is Legging sel)
                                            {
                                                SwapWithSelect(b, selected == null);
                                                selected = player.Legging;
                                                player.Legging = sel;
                                                if (selected == null)
                                                {
                                                    Canvas.SetTop(Canvas.Children[b], Height / 5 + 59);
                                                    Canvas.SetLeft(Canvas.Children[b], Width / 3 + 25);
                                                }
                                            }
                                            else if (selected == null)
                                            {
                                                SwapWithSelect(b, selected == null);
                                                selected = player.Legging;
                                                player.Legging = null;
                                            }
                                        }
                                        return;
                                    //chestplate
                                    case 6:
                                        {
                                            if (selected is ChestPlate sel)
                                            {
                                                SwapWithSelect(b, selected == null);
                                                selected = player.ChestPlate;
                                                player.ChestPlate = sel;
                                                if (selected == null)
                                                {
                                                    Canvas.SetTop(Canvas.Children[b], Height / 5 + 34);
                                                    Canvas.SetLeft(Canvas.Children[b], Width / 3 + 25);
                                                }
                                            }
                                            else if (selected == null)
                                            {
                                                SwapWithSelect(b, selected == null);
                                                selected = player.ChestPlate;
                                                player.ChestPlate = null;
                                            }
                                        }
                                        return;
                                    //helmet
                                    case 7:
                                        {
                                            if (selected is Helmet sel)
                                            {
                                                SwapWithSelect(b, selected == null);
                                                selected = player.Helmet;
                                                player.Helmet = sel;
                                                if (selected == null)
                                                {
                                                    Canvas.SetTop(Canvas.Children[b], Height / 5 + 10);
                                                    Canvas.SetLeft(Canvas.Children[b], Width / 3 + 25);
                                                }
                                            }
                                            else if (selected == null)
                                            {
                                                SwapWithSelect(b, selected == null);
                                                selected = player.Helmet;
                                                player.Helmet = null;
                                            }
                                        }
                                        return;
                                }
                            }
                        }
                    }

                }
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
                        if (a is Image img)
                        {
                            int y1 = (int)(((Canvas.GetTop(a) - Height / 2 + Height / 8 - 2) / 27) - 5) * -1;
                            int x1 = (int)((Canvas.GetLeft(a) - Width / 3 - 25) / 24.5);
                            if (y1 == y && x1 == x)
                            {
                                int b = Canvas.Children.IndexOf(a);

                                Item s = player.Inventaire.GetItem(x + (y * player.Inventaire.Width));
                                if (selected != null && s != null)
                                {
                                    if (s.id() == selected.id()) {
                                        if (s.MaxQuantity >= s.Quantity + selected.Quantity)
                                        {
                                            s.Quantity += selected.Quantity;
                                            Canvas.Children.RemoveAt(Canvas.Children.Count - 1);
                                            selected = null;
                                        }
                                        else
                                        {
                                            int an = s.MaxQuantity - s.Quantity;
                                            s.Quantity = s.MaxQuantity;
                                            selected.Quantity -= an;
                                        }
                                        return;
                                    }
                                }

                                player.Inventaire.SetItem(selected, x + (y * player.Inventaire.Width));

                                SwapWithSelect(b, selected == null);
                                selected = item;
                                break;
                            }
                        }
                    }
                }
                else if (selected != null)
                {
                    Canvas.SetTop(Canvas.Children[Canvas.Children.Count - 1], Height / 2 + Height / 8 - (y * 27) + 2);
                    Canvas.SetLeft(Canvas.Children[Canvas.Children.Count - 1], Width / 3 + (x * 24.5) + 25);
                    player.Inventaire.SetItem(selected, x + (y * player.Inventaire.Width));
                    selected = null;
                }
            }
            */
        }

        private void SelectedMove(object sender, MouseEventArgs e)
        {
            if (selected != -1 && inv != null)
            {
                Point p = e.GetPosition(Canvas);
                inv.Move(selected, p.X-inv.ItWidth/2, p.Y - inv.ItHeight / 2);
                Canvas.Children.Remove(inv);
                Canvas.Children.Add(inv);
            }
        }

        public void RemoveInventory()
        {
            if (inv != null)
            {
                if (selected != -1)
                {
                    Item it = inv.GetItem(selected).item;
                    player.Inventaire.RemoveItem(it);
                    player.Location.World.SpawnEntity(new Item_Entity(player.Location, it));
                    group.Children.Add(Model(new Item_Entity(player.Location, it).Model()));
                    selected = -1;
                }
                Canvas.Children.Clear();
            }
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
                    group.Children[index] = Model(block.Model());
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
                    group.Children.Add(Model(block.Model()));
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
                group.Children.Add(Model(block.Model()));
            }
            foreach (Entity ent in player.Location.World.Entities)
            {
                group.Children.Add(Model(ent.Model()));
            }
        }

        public static GeometryModel3D Model(Game_Model a)
        {
            MeshGeometry3D mesh = new MeshGeometry3D();

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
