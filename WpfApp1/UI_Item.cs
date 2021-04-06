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
        private int id;
        private double width;
        private double height;
        public UI_Item(Item item,int id, double pix, double piy, double width, double height, int x, int y)
        {
            this.id = id;
            this.x = x;
            this.y = y;
            this.item = item;
            if (item != null)
            {
                image = new BitmapImage(new Uri(Helper.ImageFile + "Items\\" + (item is Armor ? "Armors\\" : "") + item.id() + ".png"));
            }
            this.pix = pix;
            this.piy = piy;
            this.width = width;
            this.height = height;
        }
        public BitmapImage image;

        public DrawingImage GetImage()
        {
            DrawingGroup i = new DrawingGroup();
            DrawingContext j = i.Open();
            if (item != null)
            {
                j.DrawImage(image, new Rect(new Point(pix, piy), new Point(pix + width, piy + height)));
                if (item.Quantity != 1)
                {
                    j.DrawText(new FormattedText("" + item.Quantity, new System.Globalization.CultureInfo("NA"), FlowDirection, new Typeface("type"), width / 2, Brushes.Black, 1.25), new Point(pix + width / 1.25f, piy + height / 2));
                }
            }
            j.Close();
            return new DrawingImage(i);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            //drawingContext.DrawRectangle(Brushes.Black,new Pen(), new Rect(new Point(pix, piy), new Point(pix + width, piy + height)));
            if (item != null)
            {
                drawingContext.DrawImage(image, new Rect(new Point(pix, piy), new Point(pix + width, piy + height)));
                if (item.Quantity != 1)
                {
                    drawingContext.DrawRectangle(Brushes.White, new Pen(),
                            new Rect(pix + width / 1.5f, piy + height / 2f, width - width / 2f, height / 2f));
                    drawingContext.DrawText(new FormattedText("" + item.Quantity, new System.Globalization.CultureInfo("NA"), FlowDirection, new Typeface("type"), width / 2, Brushes.Black, 1.25),
                        new Point(pix + width / 1.5f, piy + height / 2));
                }
            }
        }
        public int x;
        public int y;
        public double pix;
        public double piy;

        public int ID { get => id; }
    }
}
