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
        public static int selected = 0;
        public MainWindow()
        {
            InitializeComponent();
            Helper.ImageFile = @"C:\Users\jcdem\source\repos\Minecraft\Images\";

            Canvas.MouseDown += ItemClickInv;
            Helper.group = new Model3DHandeler(group);

            Iron_Helmet hel = new Iron_Helmet();
            Helper.player.Inventaire.AddItem(hel);
            Helper.player.Equipe(hel);
            Iron_ChestPlate che = new Iron_ChestPlate();
            Helper.player.Inventaire.AddItem(che);
            Helper.player.Equipe(che);
            Iron_Legging leg = new Iron_Legging();
            Helper.player.Inventaire.AddItem(leg);
            Helper.player.Equipe(leg);
            Iron_Boots boo = new Iron_Boots();
            Helper.player.Inventaire.AddItem(boo);
            Helper.player.Equipe(boo);

            Helper.player.Inventaire.SetItem(new Emerald(0 + 2), 0);
            for (int i = 1; i < Helper.player.Inventaire.Length - Helper.player.Inventaire.Width; i++)
            {
                Helper.player.Inventaire.SetItem(new Steak(i + 2), i);
            }

            Diamond_Helmet hel2 = new Diamond_Helmet();
            Helper.player.Inventaire.AddItem(hel2);
            Diamond_ChestPlate che2 = new Diamond_ChestPlate();
            Helper.player.Inventaire.AddItem(che2);
            Diamond_Legging leg2 = new Diamond_Legging();
            Helper.player.Inventaire.AddItem(leg2);
            Diamond_Boots boo2 = new Diamond_Boots();
            Helper.player.Inventaire.AddItem(boo2);

            //Helper.player = new Helper.player(new Location(0, 0, 5, 0, (float)Math.PI, new World()));
            GenerateWorld(Helper.player.Location.World);
            RegenerateWorld();

            Merchand mer = new Merchand(new Location(-5, 5, 5, Helper.player.Location.World));
            Helper.player.Location.World.SpawnEntity(mer);
            Helper.group.AddEntity(mer);

            camera.Position = new Point3D(
                    Helper.player.X,
                    Helper.player.Y,
                    Helper.player.Z);
            camera.LookDirection = new Vector3D((float)Math.Cos(Helper.player.Pitch) * (float)Math.Sin(Helper.player.Yaw), (float)Math.Sin(Helper.player.Pitch), (float)Math.Cos(Helper.player.Pitch) * (float)Math.Cos(Helper.player.Yaw));


            Thread thread = new Thread(() =>
            {
                while (open)
                {
                    foreach (Entity ent in Helper.player.Location.World.Entities)
                    {
                        Dispatcher.Invoke(() => { ent.Update(); });
                    }
                    foreach (Block block in Helper.player.Location.World.Blocks)
                    {
                        Dispatcher.Invoke(() => { block.Update(); });
                    }
                    Thread.Sleep(20);
                }
            });
            thread.Start();
        }

        private UI_Inventaire inv;

        public void ShowInventory(object cont)
        {
            RemoveInventory();
            selected = 0;
            Helper.Width = Width;
            Helper.Height = Height;
            inv = new UI_Inventaire(Helper.player, cont, 32, 32, Canvas);
            Point t = Mouse.GetPosition(Canvas);
            inv.Move(selected, t.X, t.Y);
        }

        private void ItemClickInv(object sender, MouseEventArgs e)
        {
            Point pos = e.GetPosition(Canvas);

            KeyValuePair<UI_Item, int>[] its = inv.GetItem(pos.X, pos.Y);
            if (its.Length > 1)
            {
                for (int i = 0; i < its.Length; i++)
                {
                    int h = its[i].Value;
                    if (h != selected)
                    {
                        UI_Item it = inv.GetItem(h);
                        UI_Item uit = inv.GetItem(selected);

                        bool doit = false;
                        if (it.y >= Helper.player.Inventaire.Height)
                        {
                            if (inv.inv is Merchand mer)
                            {
                                Trade trade = mer.Trades[it.y - Helper.player.Inventaire.Height];
                                if (it.x == 0)
                                {
                                    Item wanted = trade.wanted();
                                    if (uit.item == null || uit.item.id() != wanted.id() || uit.item.Quantity < wanted.Quantity)
                                    {
                                        List<KeyValuePair<UI_Item, int>> tl = inv.GetItem(1, it.y);
                                        if (tl.Count >= 1)
                                        {
                                            inv.RemoveItem(tl[0].Key);
                                        }
                                    }

                                    if (uit.item != null)
                                    {
                                        if (uit.item.id() == wanted.id())
                                        {
                                            doit = true;
                                            uit.x = it.x;
                                            uit.y = it.y;
                                            if (uit.item.Quantity >= wanted.Quantity)
                                            {
                                                List<KeyValuePair<UI_Item, int>> l = inv.GetItem(1, it.y);
                                                if (l.Count == 0)
                                                {
                                                    inv.AddItem(new UI_Item(trade.giving(), it.pix + (3 * 36), it.piy, inv.ItWidth, inv.ItHeight, 1, it.y));
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        doit = true;
                                        uit.x = it.x;
                                        uit.y = it.y;
                                    }
                                }
                                if (it.x == 1)
                                {
                                    if (uit.item == null)
                                    {
                                        doit = true;
                                        List<KeyValuePair<UI_Item, int>> l = inv.GetItem(0, it.y);
                                        if (l.Count > 0)
                                        {
                                            Item wanted = trade.wanted();
                                            if (wanted.Quantity < l[0].Key.item.Quantity)
                                            {
                                                l[0].Key.item.Quantity -= wanted.Quantity;
                                            }
                                            else
                                            {
                                                l[0].Key.item = null;
                                            }
                                            if (wanted.Quantity <= l[0].Key.item.Quantity)
                                            {
                                                //inv.AddItem(new UI_Item(trade.giving, it.pix, it.piy, inv.ItWidth, inv.ItHeight, 1, it.y));
                                            }
                                            uit.x = it.x;
                                            uit.y = it.y;
                                            inv.Move(l[0].Value, l[0].Key.pix, l[0].Key.piy);
                                        }
                                    }
                                }
                            }

                            if (inv.inv is Inventaire other)
                            {
                                doit = true;
                                uit.x = it.x;
                                uit.y = it.y;
                                other.SetItem(uit.item, it.x + ((it.y - Helper.player.Inventaire.Height) * Helper.player.Inventaire.Width));
                                if (it.item != null)
                                {
                                    other.RemoveItem(it.item);
                                }
                            }
                            if (inv.inv is Player p)
                            {
                                Item ith = inv.GetItem(selected).item;
                                if (ith is Armor || ith == null)
                                {
                                    switch (it.y)
                                    {
                                        case 4:
                                            {
                                                if (ith is Boots boo)
                                                {
                                                    uit.x = 0;
                                                    uit.y = 4;
                                                    p.Boots = boo;
                                                    doit = true;
                                                }
                                                if (ith == null)
                                                {
                                                    uit.x = 0;
                                                    uit.y = 4;
                                                    p.Boots = null;
                                                    doit = true;
                                                }
                                            }
                                            break;
                                        case 5:
                                            {
                                                if (ith is Legging le)
                                                {
                                                    uit.x = 0;
                                                    uit.y = 5;
                                                    p.Legging = le;
                                                    doit = true;
                                                }
                                                if (ith == null)
                                                {
                                                    uit.x = 0;
                                                    uit.y = 5;
                                                    p.Legging = null;
                                                    doit = true;
                                                }
                                            }
                                            break;
                                        case 6:
                                            {
                                                if (ith is ChestPlate ch)
                                                {
                                                    uit.x = 0;
                                                    uit.y = 6;
                                                    p.ChestPlate = ch;
                                                    doit = true;
                                                }
                                                if (ith == null)
                                                {
                                                    uit.x = 0;
                                                    uit.y = 6;
                                                    p.ChestPlate = null;
                                                    doit = true;
                                                }
                                            }
                                            break;
                                        case 7:
                                            {
                                                if (ith is Helmet hel)
                                                {
                                                    uit.x = 0;
                                                    uit.y = 7;
                                                    p.Helmet = hel;
                                                    doit = true;
                                                }
                                                if (ith == null)
                                                {
                                                    uit.x = 0;
                                                    uit.y = 7;
                                                    p.Helmet = null;
                                                    doit = true;
                                                }
                                            }
                                            break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (it.item != null && uit.item != null)
                            {
                                if (it.item.id() == uit.item.id())
                                {
                                    if (it.item.Quantity < it.item.MaxQuantity)
                                    {
                                        if (it.item.MaxQuantity >= it.item.Quantity + uit.item.Quantity)
                                        {
                                            it.item.Quantity += uit.item.Quantity;
                                            uit.item = null;

                                            inv.Move(h, it.pix, it.piy);
                                            break;
                                        }
                                        else
                                        {
                                            uit.item.Quantity -= (it.item.MaxQuantity - it.item.Quantity);
                                            it.item.Quantity = it.item.MaxQuantity;
                                        }
                                    }
                                }
                            }
                            doit = true;
                            uit.x = it.x;
                            uit.y = it.y;
                            Helper.player.Inventaire.SetItem(uit.item, it.x + (it.y * Helper.player.Inventaire.Width));
                            if (it.item != null)
                            {
                                Helper.player.Inventaire.RemoveItem(it.item);
                            }
                        }
                        if (doit == true)
                        {
                            double x = uit.pix;
                            double y = uit.piy;
                            inv.Move(selected, it.pix, it.piy);
                            selected = h;
                            inv.Move(selected, x, y);
                        }
                        break;
                    }

                }
            }
        }

        private void SelectedMove(object sender, MouseEventArgs e)
        {
            if (inv != null)
            {
                Point p = e.GetPosition(Canvas);
                inv.Move(selected, p.X - inv.ItWidth / 2, p.Y - inv.ItHeight / 2);
            }
        }

        public void RemoveInventory()
        {
            if (inv != null)
            {
                if (inv.inv is Chest chest)
                {
                    chest.opening = false;
                    Thread t = new Thread(() => {
                        while (chest.opened > 0 && !chest.opening)
                        {
                            chest.opened -= 0.002f;
                            Thread.Sleep(1);
                        }
                    });
                    t.Start();
                }

                Item it = inv.GetItem(selected).item;
                if (it != null)
                {
                    Item_Entity ie = new Item_Entity(Helper.player.Location.Clone(), it);
                    Helper.player.Location.World.SpawnEntity(ie);
                    Helper.group.AddEntity(ie);
                }

                inv = null;
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

        public void UpdateWorld()
        {
            Model3D g1 = group.Children[0];
            Model3D g2 = group.Children[1];
            group.Children.Clear();
            group.Children.Add(g1);
            group.Children.Add(g2);
            foreach (Block block in Helper.player.Location.World.Blocks)
            {
                Helper.group.AddBlock(block);
            }
            foreach (Entity ent in Helper.player.Location.World.Entities)
            {
                Helper.group.AddEntity(ent);
            }
        }

        public void Grid_MousePress(object sender, MouseButtonEventArgs e)
        {
            if (inv == null)
            {
                MouseButton button = e.ChangedButton;
                switch (button)
                {
                    case MouseButton.Right:
                        {
                            object qqch = Helper.player.GetInFrontOfHim(20);
                            if (qqch != null)
                            {
                                if (qqch is Merchand mer)
                                {
                                    ShowInventory(mer);
                                }
                                if (qqch is KeyValuePair<Block, Location> bl)
                                {
                                    object ob = bl.Key.Right_Click(Helper.player, new Wooden_Block(bl.Value), bl.Value);
                                    if (ob != null)
                                    {
                                        if (ob is Chest chest)
                                        {
                                            ShowInventory(chest);
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
                            object qqch = Helper.player.GetInFrontOfHim(20);
                            if (qqch != null)
                            {
                                if (qqch is Entity ent)
                                {
                                    ent.TakeDamage(new PhysicalDamage(1));
                                }
                                if (qqch is KeyValuePair<Block, Location> block)
                                {
                                    block.Key.Left_Click(Helper.player);
                                }
                            }
                        }
                        break;
                    case MouseButton.Middle:
                        break;
                }
            }
        }

        private static bool ctrl = true;

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F)
            {
                if (Canvas.Children.Count > 0)
                {
                    RemoveInventory();
                }
                else
                {
                    ShowInventory(Helper.player);
                }
                return;
            }
            if (inv == null)
            {
                float pitch = Helper.player.Pitch;
                float yaw = Helper.player.Yaw;
                Key key = e.Key;
                float speed = 0.12f;
                float speedrot = 0.02f;

                if (key == Key.Delete)
                {
                    Helper.player.Location.ChangeWorld(Helper.player.X, Helper.player.Y, Helper.player.Z, Helper.player.Pitch, Helper.player.Yaw, new World());
                    GenerateWorld(Helper.player.Location.World);
                    RegenerateWorld();
                    return;
                }

                if (key == Key.Y)
                {
                    Block[] b = Helper.player.Location.World.Blocks;
                    Helper.group.RemoveBlock(b[b.Length-1]);
                    b[b.Length - 1].Location.Move(0,0,0,0.1f,0.1f);
                    Helper.group.AddBlock(b[b.Length - 1]);
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
                            Helper.player.Move((float)Math.Sin(yaw) * speed, 0, (float)Math.Cos(yaw) * speed, 0, 0);
                            break;
                        case Key.S:
                            Helper.player.Move((float)Math.Sin(yaw) * -speed, 0, (float)Math.Cos(yaw) * -speed, 0, 0);
                            break;
                        case Key.A:
                            Helper.player.Move((float)Math.Sin(yaw + (Math.PI / 2)) * speed, 0, (float)Math.Cos(yaw + (Math.PI / 2)) * speed, 0, 0);
                            break;
                        case Key.D:
                            Helper.player.Move((float)Math.Sin(yaw + (Math.PI / 2)) * -speed, 0, (float)Math.Cos(yaw + (Math.PI / 2)) * -speed, 0, 0);
                            break;
                        case Key.Space:
                            Helper.player.Move(0, speed, 0, 0, 0);
                            break;
                        case Key.LeftShift:
                            Helper.player.Move(0, -speed, 0, 0, 0);
                            break;
                        case Key.K:
                            if (Helper.player.Location.Pitch - speedrot >= -Math.PI / 2)
                            {
                                Helper.player.Move(0, 0, 0, -speedrot, 0);
                            }
                            break;
                        case Key.L:
                            if (Helper.player.Location.Pitch + speedrot <= Math.PI / 2)
                            {
                                Helper.player.Move(0, 0, 0, speedrot, 0);
                            }
                            break;
                        case Key.E:
                            Helper.player.Move(0, 0, 0, 0, -speedrot);
                            break;
                        case Key.Q:
                            Helper.player.Move(0, 0, 0, 0, speedrot);
                            break;
                    }
                }
                else
                {
                    switch (key)
                    {
                        case Key.W:
                            Helper.player.Move((float)Math.Cos(pitch) * (float)Math.Sin(yaw) * speed, (float)Math.Sin(pitch) * speed, (float)Math.Cos(pitch) * (float)Math.Cos(yaw) * speed, 0, 0);
                            break;
                        case Key.S:
                            Helper.player.Move((float)Math.Cos(pitch) * (float)Math.Sin(yaw) * -speed, (float)Math.Sin(pitch) * -speed, (float)Math.Cos(pitch) * (float)Math.Cos(yaw) * -speed, 0, 0);
                            break;
                        case Key.A:
                            Helper.player.Move((float)Math.Sin(yaw + (Math.PI / 2)) * speed, 0, (float)Math.Cos(yaw + (Math.PI / 2)) * speed, 0, 0);
                            break;
                        case Key.D:
                            Helper.player.Move((float)Math.Sin(yaw + (Math.PI / 2)) * -speed, 0, (float)Math.Cos(yaw + (Math.PI / 2)) * -speed, 0, 0);
                            break;
                        case Key.Space:
                            Helper.player.Move(0, speed, 0, 0, 0);
                            break;
                        case Key.LeftShift:
                            Helper.player.Move(0, -speed, 0, 0, 0);
                            break;
                        case Key.K:
                            if (Helper.player.Location.Pitch - speedrot >= -Math.PI / 2)
                            {
                                Helper.player.Move(0, 0, 0, -speedrot, 0);
                            }
                            break;
                        case Key.L:
                            if (Helper.player.Location.Pitch + speedrot <= Math.PI / 2)
                            {
                                Helper.player.Move(0, 0, 0, speedrot, 0);
                            }
                            break;
                        case Key.E:
                            Helper.player.Move(0, 0, 0, 0, -speedrot);
                            break;
                        case Key.Q:
                            Helper.player.Move(0, 0, 0, 0, speedrot);
                            break;
                    }
                }
                camera.Position = new Point3D(
                        Helper.player.X,
                        Helper.player.Y,
                        Helper.player.Z);
                camera.LookDirection = new Vector3D((float)Math.Cos(Helper.player.Pitch) * (float)Math.Sin(Helper.player.Yaw), (float)Math.Sin(Helper.player.Pitch), (float)Math.Cos(Helper.player.Pitch) * (float)Math.Cos(Helper.player.Yaw));
            }
        }
    }
}
