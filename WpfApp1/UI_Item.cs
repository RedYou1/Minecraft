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

namespace Minecraft
{
    public class UI_Item : FrameworkElement
    {
        public Item item;
        private double width;
        private double height;
        public UI_Item(Item item, double pix, double piy,double width,double height)
        {
            this.item = item;
            image = new BitmapImage(new Uri(Helper.ImageFile + "Items\\" + (item is Armor?"Armors\\":"") + item.id() + ".png"));
            x = pix;
            y = piy;
            this.width = width;
            this.height = height;
        }
        public BitmapImage image;
        public void Render(DrawingContext drawingContext)
        {
            drawingContext.DrawImage(image, new Rect(new Point(x, y), new Point(x + width, y + height)));
            drawingContext.DrawText(new FormattedText("" + item.Quantity, new System.Globalization.CultureInfo("NA"), FlowDirection, new Typeface("type"), width / 2, Brushes.Black, 1.25), new Point(x + width / 1.25f, y + height / 2));
        }
        public double x;
        public double y;
    }
}
