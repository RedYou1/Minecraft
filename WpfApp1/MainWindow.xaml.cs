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
using CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting;
using System.Windows.Media.Media3D;
using CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.World_System;
using CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Items;
using CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Entities;
using CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.World_System.Models;
using CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.World_System.Blocks;
using CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Damages;
using System.Threading;
using CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Items.Armors;
using CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Attributes;

namespace Minecraft
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static int selected = 0;
        private UI_Inventaire inv;
        public static bool open = true;
        public MainWindow()
        {
            InitializeComponent();

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

            Helper.player.Inventaire.SetItem(new Back_Pack(), 0);
            Helper.player.Inventaire.SetItem(new Emerald(0 + 2), 1);
            Helper.player.Inventaire.SetItem(new Back_Pack(), 2);
            Helper.player.Inventaire.SetItem(new Back_Pack(), 3);
            for (int i = 4; i < Helper.player.Inventaire.Length - Helper.player.Inventaire.Width; i++)
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
            foreach (Block block in Helper.player.Location.World.Blocks)
            {
                Helper.group.AddBlock(block);
            }

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
                        if (open) { Dispatcher.Invoke(() => { ent.Update(); }); } else { return; }
                    }
                    foreach (Block block in Helper.player.Location.World.Blocks)
                    {
                        if (open) { Dispatcher.Invoke(() => { block.Update(); }); } else { return; }
                    }
                    Thread.Sleep(20);
                }
            });
            thread.Start();
            ShowHotbar(Helper.player);
        }

        public void ShowInventory(object cont, object something)
        {
            Canvas.Children.Clear();
            selected = 0;
            Helper.Width = Width;
            Helper.Height = Height;
            inv = new UI_Inventaire(Helper.player, cont, something, 32, 32, Canvas);
            Point t = Mouse.GetPosition(Canvas);
            inv.Move(selected, t.X, t.Y);
        }

        public void ShowHotbar(Player player)
        {
            Canvas.Children.Clear();
            Helper.Width = Width;
            Helper.Height = Height;
            HotBar hb = new HotBar(player, 32, 32);
            Canvas.Children.Add(hb);
        }

        private void ItemClickInv(object sender, MouseEventArgs e)
        {
            if (inv != null)
            {
                Point pos = e.GetPosition(Canvas);
                KeyValuePair<UI_Item, int>[] its = inv.GetItem(pos.X, pos.Y);
                if (its.Length > 1)
                {
                    for (int i = 0; i < its.Length; i++)
                    {
                        int h = inv.items[its[i].Value].ID;
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

                                        if (uit.item == null)
                                        {
                                            doit = true;
                                            uit.x = it.x;
                                            uit.y = it.y;
                                        }
                                        else
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
                                                        inv.AddItem(new UI_Item(trade.giving(), inv.act++, it.pix + (3 * 36), it.piy, inv.ItWidth, inv.ItHeight, 1, it.y));
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (uit.item == null)
                                        {
                                            List<KeyValuePair<UI_Item, int>> l = inv.GetItem(0, it.y);
                                            foreach (KeyValuePair<UI_Item, int> a in l)
                                            {
                                                if (a.Key.item != null)
                                                {
                                                    Item wanted = trade.wanted();

                                                    inv.RemoveItem(a.Key);
                                                    if (wanted.Quantity < a.Key.item.Quantity)
                                                    {
                                                        a.Key.item.Quantity -= wanted.Quantity;
                                                        inv.AddItem(a.Key);
                                                    }

                                                    selected = it.ID;
                                                    inv.RemoveItem(uit);

                                                    if (wanted.Quantity <= a.Key.item.Quantity)
                                                    {
                                                        inv.AddItem(new UI_Item(trade.giving(), inv.act++, it.pix, it.piy, inv.ItWidth, inv.ItHeight, 1, it.y));
                                                    }
                                                    return;
                                                }
                                            }
                                            return;
                                        }
                                        else if (uit.item.id() == trade.giving().id())
                                        {
                                            Item giving = trade.giving();
                                            if (uit.item.Quantity + giving.Quantity <= uit.item.MaxQuantity)
                                            {
                                                inv.RemoveItem(it);

                                                inv.RemoveItem(uit);
                                                uit.item.Quantity += giving.Quantity;
                                                inv.AddItem(uit);

                                                Item wanted = trade.wanted();
                                                List<KeyValuePair<UI_Item, int>> l = inv.GetItem(0, it.y);
                                                if (l.Count > 0)
                                                {
                                                    if (wanted.Quantity < l[0].Key.item.Quantity)
                                                    {
                                                        inv.AddItem(new UI_Item(trade.giving(), inv.act++, it.pix, it.piy, inv.ItWidth, inv.ItHeight, 1, it.y));

                                                        inv.RemoveItem(l[0].Key);
                                                        l[0].Key.item.Quantity -= wanted.Quantity;
                                                        if (l[0].Key.item.Quantity > 1)
                                                        {
                                                            inv.AddItem(l[0].Key);
                                                        }
                                                    }
                                                }
                                                return;
                                            }
                                        }
                                    }
                                }
                                if (inv.inv is Inventaire other)
                                {
                                    if (!(inv.something2 is Item tit && tit == uit.item))
                                    {
                                        doit = true;
                                        uit.x = it.x;
                                        uit.y = it.y;
                                        other.SetItem(uit.item, it.x + ((it.y - Helper.player.Inventaire.Height) * other.Width));
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
                            }
                            if (doit == true)
                            {
                                double x = uit.pix;
                                double y = uit.piy;
                                inv.Move(inv.items.IndexOf(inv.GetItem(selected)), it.pix, it.piy);
                                selected = h;
                                inv.Move(inv.items.IndexOf(inv.GetItem(selected)), x, y);
                            }
                            break;
                        }

                    }
                }
            }
        }

        private void SelectedMove(object sender, MouseEventArgs e)
        {
            if (inv != null)
            {
                Point p = e.GetPosition(Canvas);
                inv.Move(inv.items.IndexOf(inv.GetItem(selected)), p.X - inv.ItWidth / 2, p.Y - inv.ItHeight / 2);
            }
        }

        public void RemoveInventory()
        {
            if (inv != null)
            {
                if (inv.something2 is Chest chest)
                {
                    chest.opening = false;
                    Thread t = new Thread(() =>
                    {
                        while (chest.opened > 0 && !chest.opening)
                        {
                            chest.opened -= 0.003f;
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
                ShowHotbar(Helper.player);
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

        private void Game_Closing(object sender, EventArgs e)
        {
            open = false;
        }

        private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            Helper.player.itemSelected += (e.Delta > 0 ? 1 : -1);
            if (Helper.player.itemSelected >= Helper.player.Inventaire.Width)
            {
                Helper.player.itemSelected -= Helper.player.Inventaire.Width;
            }
            if (Helper.player.itemSelected < 0)
            {
                Helper.player.itemSelected += Helper.player.Inventaire.Width;
            }
            ShowHotbar(Helper.player);
        }

        public void Grid_MousePress(object sender, MouseButtonEventArgs e)
        {
            if (inv == null)
            {
                MouseButton button = e.ChangedButton;
                object qqch = Helper.player.GetInFrontOfHim(20);
                switch (button)
                {
                    case MouseButton.Right:
                        {
                            Item itSel = Helper.player.Inventaire.GetItem(Helper.player.itemSelected);
                            if (itSel != null)
                            {
                                object temp = itSel.Right_Click(Helper.player, qqch);
                                if (temp is Inventaire inv)
                                {
                                    ShowInventory(inv, itSel);
                                    return;
                                }
                                if (temp is String s && s == Food.FOOD)
                                {
                                    ShowHotbar(Helper.player);
                                    return;
                                }
                            }

                            if (qqch != null)
                            {
                                if (qqch is Merchand mer)
                                {
                                    ShowInventory(mer, mer);
                                }
                                if (qqch is KeyValuePair<Block, Location> bl)
                                {
                                    object ob = bl.Key.Right_Click(Helper.player, new Wooden_Block(bl.Value), bl.Value);
                                    if (ob != null)
                                    {

                                        if (ob is Inventaire inv)
                                        {
                                            ShowInventory(inv, bl.Key);
                                        }
                                    }
                                    return;
                                }
                            }
                        }
                        break;
                    case MouseButton.Left:
                        {
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
                        {
                            //TODO: item du block
                        }
                        break;
                }
            }
        }

        private static bool ctrl = true;

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Controle.inventaire)
            {
                if (inv != null)
                {
                    RemoveInventory();
                }
                else
                {
                    ShowInventory(Helper.player, Helper.player);
                }
                return;
            }
            if (inv == null)
            {
                Func<int, bool> slot = (s) =>
                 {
                     Helper.player.itemSelected = s;
                     ShowHotbar(Helper.player);
                     return true;
                 };

                float pitch = Helper.player.Pitch;
                float yaw = Helper.player.Yaw;
                float speed = 0.12f;
                float speedrot = 0.02f;

                switch (e.Key)
                {
                    case Controle.Slot1: { slot(0); return; }
                    case Controle.Slot2: { slot(1); return; }
                    case Controle.Slot3: { slot(2); return; }
                    case Controle.Slot4: { slot(3); return; }
                    case Controle.Slot5: { slot(4); return; }
                    case Controle.Slot6: { slot(5); return; }
                    case Controle.Slot7: { slot(6); return; }
                    case Controle.Slot8: { slot(7); return; }
                    case Controle.Slot9: { slot(8); return; }

                    case Controle.tourneBlock_WoodenPlank:
                        {
                            Block[] b = Helper.player.Location.World.Blocks;
                            Helper.group.RemoveBlock(b[b.Length - 1]);
                            b[b.Length - 1].Location.Move(0, 0, 0, 0.1f, 0.1f);
                            Helper.group.AddBlock(b[b.Length - 1]);
                        }
                        break;
                    case Controle.marchandLeveTete:
                        {
                            Entity[] b = Helper.player.Location.World.Entities;
                            b[b.Length - 1].Move(0, 0, 0, -0.1f, 0);
                        }
                        break;
                    case Controle.marchandPencheTete:
                        {
                            Entity[] b = Helper.player.Location.World.Entities;
                            b[b.Length - 1].Move(0, 0, 0, 0.1f, 0);
                        }
                        break;
                    case Controle.marchandTourneDroitTete:
                        {
                            Entity[] b = Helper.player.Location.World.Entities;
                            b[b.Length - 1].Move(0, 0, 0, 0, -0.1f);
                        }
                        break;
                    case Controle.marchandTourneGaucheTete:
                        {
                            Entity[] b = Helper.player.Location.World.Entities;
                            b[b.Length - 1].Move(0, 0, 0, 0, 0.1f);
                        }
                        break;
                    case Controle.flyingMode:
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
                        }
                        break;
                    case Controle.devant:
                        {
                            if (ctrl)
                            {
                                Helper.player.Move((float)Math.Sin(yaw) * speed, 0, (float)Math.Cos(yaw) * speed, 0, 0);
                            }
                            else
                            {
                                Helper.player.Move((float)Math.Cos(pitch) * (float)Math.Sin(yaw) * speed, (float)Math.Sin(pitch) * speed, (float)Math.Cos(pitch) * (float)Math.Cos(yaw) * speed, 0, 0);
                            }
                        }
                        break;
                    case Controle.derriere:
                        {
                            if (ctrl)
                            {
                                Helper.player.Move((float)Math.Sin(yaw) * -speed, 0, (float)Math.Cos(yaw) * -speed, 0, 0);
                            }
                            else
                            {
                                Helper.player.Move((float)Math.Cos(pitch) * (float)Math.Sin(yaw) * -speed, (float)Math.Sin(pitch) * -speed, (float)Math.Cos(pitch) * (float)Math.Cos(yaw) * -speed, 0, 0);
                            }
                        }
                        break;
                    case Controle.gauche:
                        {
                            Helper.player.Move((float)Math.Sin(yaw + (Math.PI / 2)) * speed, 0, (float)Math.Cos(yaw + (Math.PI / 2)) * speed, 0, 0);
                        }
                        break;
                    case Controle.droite:
                        {
                            Helper.player.Move((float)Math.Sin(yaw + (Math.PI / 2)) * -speed, 0, (float)Math.Cos(yaw + (Math.PI / 2)) * -speed, 0, 0);
                        }
                        break;
                    case Controle.dessus:
                        {
                            Helper.player.Move(0, speed, 0, 0, 0);
                        }
                        break;
                    case Controle.dessous:
                        {
                            Helper.player.Move(0, -speed, 0, 0, 0);
                        }
                        break;
                    case Controle.regardDessous:
                        {
                            if (Helper.player.Location.Pitch - speedrot >= -Math.PI / 2)
                            {
                                Helper.player.Move(0, 0, 0, -speedrot, 0);
                            }
                        }
                        break;
                    case Controle.regardDessus:
                        {
                            if (Helper.player.Location.Pitch + speedrot <= Math.PI / 2)
                            {
                                Helper.player.Move(0, 0, 0, speedrot, 0);
                            }
                        }
                        break;
                    case Controle.regardDroite:
                        {
                            Helper.player.Move(0, 0, 0, 0, -speedrot);
                        }
                        break;
                    case Controle.regardGauche:
                        {
                            Helper.player.Move(0, 0, 0, 0, speedrot);
                        }
                        break;
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
