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
        private List<UI_Item> items;
        private BitmapImage image;
        private double width;
        private double height;

        public UI_Item GetItem(int index)
        {
            return items[index];
        }

        public void Move(int index,double x,double y)
        {
            if (index >= 0 && index < items.Count && items.Count > 0)
            {
                items[index].x = x;
                items[index].y = y;
            }
        }

        public int GetIndex(Item item)
        {
            for (int i = 0; i < items.Count;i++)
            {
                UI_Item it = items[i];
                if (it.item == item)
                {
                    return i;
                }
            }
            return -1;
        }

        public void Swap(int i1,int i2)
        {
            UI_Item t = items[i1];
            items[i1] = items[i2];
            items[i2] = t;
        }

        public UI_Item[] GetItem(double x,double y)
        {
            List<UI_Item> its = new List<UI_Item>();
            foreach (UI_Item it in items)
            {
                if (x >= it.x && x <= it.x + itwidth && y >= it.y && y <= it.y + itheight)
                {
                    its.Add(it);
                }
            }
            return its.ToArray();
        }

        private double itwidth;
        private double itheight;

        public UI_Inventaire(object something,double itwidth,double itheight)
        {
            width = Helper.Width;
            height = Helper.Height;
            this.itwidth = itwidth;
            this.itheight = itheight;
            image = new BitmapImage(new Uri(Helper.ImageFile + "inventory.png"));
            items = new List<UI_Item>();
            if (something is Player player)
            {
                for (int x = 0; x < player.Inventaire.Width; x++)
                {
                    for (int y = 0; y < player.Inventaire.Height; y++)
                    {
                        Item it = player.Inventaire.GetItem(x + (y * player.Inventaire.Width));
                        if (it != null)
                        {
                            items.Add(new UI_Item(it, width / 4 + (x * 41) + 20, height - height / 4 - (y * 41) + 5, itwidth, itheight));
                        }
                    }
                }
                if (player.Helmet != null)
                {
                    items.Add(new UI_Item(player.Helmet, width / 4 + 20,height / 6, itwidth, itheight));
                }
                if (player.ChestPlate != null)
                {
                    items.Add(new UI_Item(player.ChestPlate, width / 4 + 20, height / 6 + 34, itwidth, itheight));
                }
                if (player.Legging != null)
                {
                    items.Add(new UI_Item(player.Legging, width / 4 + 20, height / 6 + 68, itwidth, itheight));
                }
                if (player.Boots != null)
                {
                    items.Add(new UI_Item(player.Boots, width / 4 + 20, height / 6 + 110, itwidth, itheight));
                }
            }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            drawingContext.DrawImage(image,
                new Rect(new Point(width / 4, height / 8),
                new Point(width - width / 4, height - height / 8)));

            foreach (UI_Item it in items)
            {
                it.Render(drawingContext);
            }
        }

        public double ItWidth { get => itwidth; }
        public double ItHeight { get => itheight; }
    }
}
