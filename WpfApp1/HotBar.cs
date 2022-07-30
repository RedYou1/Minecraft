using CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting;
using CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Entities;
using CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Items;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Minecraft
{
    public class HotBar : FrameworkElement
    {
        private Player player;
        public double itwidth;
        public double itheight;
        public double width;
        public double height;
        public HotBar(Player player, double itwidth, double itheight)
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
            drawingContext.DrawImage(new BitmapImage(new Uri(Helper.ImageFile + "Selector.png")), new Rect(width / 4 + itwidth * player.itemSelected * 1.355, height - height / 5.7 - 5, itwidth + 20, itheight + 15));
            for (int i = 0; i < 9; i++)
            {
                Item item = player.Inventaire.GetItem(i);
                if (item != null)
                {
                    double x = width / 4 + 10 + itwidth * i * 1.355;
                    double y = height - height / 5.7;
                    if (!double.IsInfinity(x) && !double.IsInfinity(y))
                    {
                        drawingContext.DrawImage(new BitmapImage(new Uri(Helper.ImageFile + "Items\\" + item.id() + ".png")),
                            new Rect(new Point(x, y), new Point(x + itwidth, y + itheight)));
                        if (item.Quantity != 1)
                        {
                            drawingContext.DrawRectangle(Brushes.White, new Pen(),
                                new Rect(x + itwidth / 1.5f, y + itheight / 2f, itwidth - itwidth / 2f, itheight / 2f));
                            drawingContext.DrawText(new FormattedText("" + item.Quantity, new System.Globalization.CultureInfo("NA"), FlowDirection, new Typeface("type"), itwidth / 2f, Brushes.Black, 1.25),
                                new Point(x + itwidth / 1.5f, y + itheight / 2f));
                        }
                    }
                }
            }

        }
    }
}
