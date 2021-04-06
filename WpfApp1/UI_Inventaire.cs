using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Controls;
using System.Windows.Markup;
using CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Items;
using System.Windows.Media.Imaging;
using CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting;
using CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Attributes;
using CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Entities;
using CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.World_System.Blocks;

namespace Minecraft
{
    public class UI_Inventaire : FrameworkElement
    {
        public List<UI_Item> items;
        private ImageSource image;
        private double width;
        private double height;
        private double itwidth;
        private double itheight;
        private Canvas canvas;
        public Player caster;
        public object something2;
        public object inv;
        public int act = 0;

        public UI_Inventaire(Player caster, object something, object something2, double itwidth, double itheight, Canvas canvas)
        {
            this.something2 = something2;
            this.caster = caster;
            this.canvas = canvas;
            width = Helper.Width;
            height = Helper.Height;
            this.itwidth = itwidth;
            this.itheight = itheight;
            items = new List<UI_Item>();

            items.Add(new UI_Item(null, act++, width / 4 + (1 * 41) + 20, height - height / 4 - (1 * 41) + 5, itwidth, itheight, 0, 0));

            if (something is Player player)
            {
                BitmapImage image = new BitmapImage(new Uri(Helper.ImageFile + "Player_Inventory.png"));
                DrawingGroup i = new DrawingGroup();
                DrawingContext j = i.Open();
                j.DrawImage(image, new Rect(new Point(0, 0), new Point(177, 166)));
                j.Close();
                this.image = new DrawingImage(i);
                this.inv = player;
                for (int x = 0; x < player.Inventaire.Width; x++)
                {
                    for (int y = 0; y < player.Inventaire.Height; y++)
                    {
                        Item it = player.Inventaire.GetItem(x + (y * player.Inventaire.Width));
                        items.Add(new UI_Item(it, act++, width / 4 + (x * 41) + 20, height - height / 4 - (y * 41) + 5, itwidth, itheight, x, y));
                    }
                }
                items.Add(new UI_Item(player.Helmet, act++, width / 4 + 20, height / 6, itwidth, itheight, 0, 7));
                items.Add(new UI_Item(player.ChestPlate, act++, width / 4 + 20, height / 6 + 34, itwidth, itheight, 0, 6));
                items.Add(new UI_Item(player.Legging, act++, width / 4 + 20, height / 6 + 68, itwidth, itheight, 0, 5));
                items.Add(new UI_Item(player.Boots, act++, width / 4 + 20, height / 6 + 110, itwidth, itheight, 0, 4));
            }
            if (something is Merchand mer)
            {
                this.inv = mer;
                BitmapImage slot = new BitmapImage(new Uri(Helper.ImageFile + "Slot.png"));
                BitmapImage image = new BitmapImage(new Uri(Helper.ImageFile + "Other_Inventory.png"));
                DrawingGroup i = new DrawingGroup();
                DrawingContext j = i.Open();
                j.DrawImage(image, new Rect(new Point(0, 0), new Point(177, 166)));

                for (int x = 0; x < caster.Inventaire.Width; x++)
                {
                    for (int y = 0; y < caster.Inventaire.Height; y++)
                    {
                        Item it = caster.Inventaire.GetItem(x + (y * caster.Inventaire.Width));
                        items.Add(new UI_Item(it, act++, width / 4 + (x * 41) + 20, height - height / 4 - (y * 41) + 5, itwidth, itheight, x, y));
                    }
                }

                int tx = 0;
                int ty = 0;
                foreach (Trade trade in mer.Trades)
                {
                    UI_Item wanted = new UI_Item(trade.wanted(), 0, 0, 0, itwidth, itheight, tx, ty);
                    UI_Item giving = new UI_Item(trade.giving(), 0, 0, 0, itwidth, itheight, tx, ty);

                    j.DrawImage(wanted.GetImage(), new Rect(
                            new Point((tx * 16) + 7, 7 + (ty * 18)),
                            new Point(((tx + 1) * 16) + 7, 7 + ((ty + 1) * 18))));
                    j.DrawImage(giving.GetImage(), new Rect(
                            new Point(((tx + 3) * 16) + 7, 7 + (ty * 18)),
                            new Point(((tx + 4) * 16) + 7, 7 + ((ty + 1) * 18))));

                    j.DrawImage(slot, new Rect(
                            new Point(((tx + 1) * 16) + 7, 7 + (ty * 18)),
                            new Point(((tx + 2) * 16) + 7, 7 + ((ty + 1) * 18))));
                    j.DrawImage(slot, new Rect(
                            new Point(((tx + 4) * 16) + 7, 7 + (ty * 18)),
                            new Point(((tx + 5) * 16) + 7, 7 + ((ty + 1) * 18))));

                    items.Add(new UI_Item(null, act++, width / 4 + ((tx + 1) * 36) + 17, height - height / 4 - ((2 - ty) * 36) - height / 6 - 115, itwidth, itheight, 0, ty + (tx / 5 * 4) + caster.Inventaire.Height));

                    ty++;
                    if (ty > 3)
                    {
                        ty = 0;
                        tx += 5;
                        if (tx > 10)
                        {
                            break;
                        }
                    }
                }

                j.Close();
                this.image = new DrawingImage(i);
            }
            if (something is Inventaire inv)
            {
                this.inv = inv;
                BitmapImage slot = new BitmapImage(new Uri(Helper.ImageFile + "Slot.png"));
                BitmapImage image = new BitmapImage(new Uri(Helper.ImageFile + "Other_Inventory.png"));
                DrawingGroup i = new DrawingGroup();
                DrawingContext j = i.Open();
                j.DrawImage(image, new Rect(new Point(0, 0), new Point(177, 166)));
                for (int x = 0; x < caster.Inventaire.Width; x++)
                {
                    for (int y = 0; y < caster.Inventaire.Height; y++)
                    {
                        Item it = caster.Inventaire.GetItem(x + (y * caster.Inventaire.Width));
                        items.Add(new UI_Item(it, act++, width / 4 + (x * 41) + 20, height - height / 4 - (y * 41) + 5, itwidth, itheight, x, y));
                    }
                }
                for (int x = 0; x < inv.Width; x++)
                {
                    for (int y = 0; y < inv.Height; y++)
                    {
                        j.DrawImage(slot, new Rect(
                            new Point((x * 18) + 7, 47 - (y * 18)),
                            new Point(((x + 1) * 18) + 7, 47 - (y * 18) + 18)));
                        Item it = inv.GetItem(x + (y * inv.Width));
                        items.Add(new UI_Item(it,act++, width / 4 + (x * 41) + 20, height - height / 4 - (y * 41) + 5 - height / 6 - 110, itwidth, itheight, x, y + caster.Inventaire.Height));
                    }
                }
                j.Close();
                this.image = new DrawingImage(i);
            }
            canvas.Children.Add(this);
            foreach (UI_Item it in items)
            {
                canvas.Children.Add(it);
            }
        }

        
        public UI_Item GetItem(int id)
        {
            foreach (UI_Item it in items)
            {
                if (it.ID == id)
                {
                    return it;
                }
            }
            throw new KeyNotFoundException();
        }
        

        public void AddItem(UI_Item item)
        {
            items.Add(item);
            canvas.Children.Add(item);
        }

        public void RemoveItem(UI_Item it)
        {
            items.Remove(it);
            canvas.Children.Remove(it);
        }

        public List<KeyValuePair<UI_Item, int>> GetItem(int x, int y)
        {
            List<KeyValuePair<UI_Item, int>> pairs = new List<KeyValuePair<UI_Item, int>>();
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].x == x && items[i].y == y)
                {
                    pairs.Add(new KeyValuePair<UI_Item, int>(items[i], i));
                }
            }
            return pairs;
        }

        public void Move(int index, double x, double y)
        {
            if (index >= 0 && index < items.Count && items.Count > 0)
            {
                items[index].pix = x;
                items[index].piy = y;

                canvas.Children.Remove(items[index]);
                canvas.Children.Add(items[index]);
            }
        }

        public KeyValuePair<UI_Item, int>[] GetItem(double x, double y)
        {
            List<KeyValuePair<UI_Item, int>> its = new List<KeyValuePair<UI_Item, int>>();
            for (int i = 0; i < items.Count; i++)
            {
                UI_Item it = items[i];
                if (x >= it.pix && x <= it.pix + itwidth && y >= it.piy && y <= it.piy + itheight)
                {
                    its.Add(new KeyValuePair<UI_Item, int>(it, i));
                }
            }
            return its.ToArray();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            drawingContext.DrawImage(image,
                new Rect(new Point(width / 4, height / 8),
                new Point(width - width / 4, height - height / 8)));
        }

        public double ItWidth { get => itwidth; }
        public double ItHeight { get => itheight; }
    }
}
