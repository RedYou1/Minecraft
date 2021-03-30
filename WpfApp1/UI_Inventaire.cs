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
using BiblioMinecraft.Items;
using System.Windows.Media.Imaging;
using BiblioMinecraft;
using BiblioMinecraft.Attributes;
using BiblioMinecraft.Entities;

namespace Minecraft
{
    public class UI_Inventaire : FrameworkElement
    {
        public List<UI_Item> items;
        private BitmapImage image;
        private double width;
        private double height;
        private double itwidth;
        private double itheight;
        private Canvas canvas;
        public Player caster;
        public object inv;

        public UI_Inventaire(Player caster, object something, double itwidth, double itheight, Canvas canvas)
        {
            this.caster = caster;
            this.canvas = canvas;
            width = Helper.Width;
            height = Helper.Height;
            this.itwidth = itwidth;
            this.itheight = itheight;
            image = new BitmapImage(new Uri(Helper.ImageFile + "inventory.png"));
            items = new List<UI_Item>();

            items.Add(new UI_Item(null, width / 4 + (1 * 41) + 20, height - height / 4 - (1 * 41) + 5, itwidth, itheight, 0, 0));

            if (something is Player player)
            {
                this.inv = player;
                for (int x = 0; x < player.Inventaire.Width; x++)
                {
                    for (int y = 0; y < player.Inventaire.Height; y++)
                    {
                        Item it = player.Inventaire.GetItem(x + (y * player.Inventaire.Width));
                        items.Add(new UI_Item(it, width / 4 + (x * 41) + 20, height - height / 4 - (y * 41) + 5, itwidth, itheight, x, y));
                    }
                }
                items.Add(new UI_Item(player.Helmet, width / 4 + 20, height / 6, itwidth, itheight, 0, 7));
                items.Add(new UI_Item(player.ChestPlate, width / 4 + 20, height / 6 + 34, itwidth, itheight, 0, 6));
                items.Add(new UI_Item(player.Legging, width / 4 + 20, height / 6 + 68, itwidth, itheight, 0, 5));
                items.Add(new UI_Item(player.Boots, width / 4 + 20, height / 6 + 110, itwidth, itheight, 0, 4));
            }
            if (something is Inventaire inv)
            {
                this.inv = inv;
                for (int x = 0; x < caster.Inventaire.Width; x++)
                {
                    for (int y = 0; y < caster.Inventaire.Height; y++)
                    {
                        Item it = caster.Inventaire.GetItem(x + (y * caster.Inventaire.Width));
                        items.Add(new UI_Item(it, width / 4 + (x * 41) + 20, height - height / 4 - (y * 41) + 5, itwidth, itheight, x, y));
                    }
                }
                for (int x = 0; x < inv.Width; x++)
                {
                    for (int y = 0; y < inv.Height; y++)
                    {
                        Item it = inv.GetItem(x + (y * inv.Width));
                        items.Add(new UI_Item(it, width / 4 + (x * 41) + 20, height - height / 4 - (y * 41) + 5 - height / 6 - 110, itwidth, itheight, x, y+3));
                    }
                }
            }
            canvas.Children.Add(this);
            foreach (UI_Item it in items)
            {
                canvas.Children.Add(it);
            }
        }

        public UI_Item GetItem(int index)
        {
            return items[index];
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

        public void Swap(int i1, int i2)
        {
            UI_Item t = items[i1];
            items[i1] = items[i2];
            items[i2] = t;

            List<UIElement> ele = new List<UIElement>();
            foreach (UIElement e in canvas.Children)
            {
                ele.Add(e);
            }

            UIElement tf = ele[i1 + 1];
            ele[i1 + 1] = ele[i2 + 1];
            ele[i2 + 1] = tf;

            canvas.Children.Clear();
            foreach (UIElement e in ele)
            {
                canvas.Children.Add(e);
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
