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
    public class HotBar : FrameworkElement
    {
        private Player player;
        private int itwidth;
        private int itheight;
        private double width;
        private double height;
        public HotBar(Player player, int itwidth, int itheight)
        {
            this.player = player;
            this.itwidth = itwidth;
            this.itheight = itheight;
            this.width = Helper.Width;
            this.height = Helper.Height;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            drawingContext.DrawImage(new BitmapImage(new Uri(Helper.ImageFile + "hotbar.png")), new Rect(width / 4, height - height / 4, width / 2, height / 6));
            drawingContext.DrawImage(new BitmapImage(new Uri(Helper.ImageFile + "Selector.png")), new Rect(width / 4 + itwidth * player.itemSelected * 1.355, height - height / 5.7-5, itwidth+20, itheight+15));
            for (int i = 0; i < 9; i++)
            {
                Item item = player.Inventaire.GetItem(i);
                if (item != null)
                {
                    double x = width / 4 + 10 + itwidth * i * 1.355;
                    double y = height - height / 5.7;
                    drawingContext.DrawImage(new BitmapImage(new Uri(Helper.ImageFile + "Items\\" + item.id() + ".png")),
                        new Rect(new Point(x, y), new Point(x + itwidth, y + itheight)));
                    if (item.Quantity != 1)
                    {
                        drawingContext.DrawText(new FormattedText("" + item.Quantity, new System.Globalization.CultureInfo("NA"), FlowDirection, new Typeface("type"),width / 2, Brushes.Black, 1.25),
                            new Point(x + width / 1.25f, y + height / 2));
                    }
                }
            }
            
        }
    }
}
