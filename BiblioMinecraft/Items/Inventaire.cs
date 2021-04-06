using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Items
{
    public class Inventaire
    {
        private Item[] items;
        private int width;
        private int height;
        public Inventaire(int width, int height)
        {
            this.width = width;
            this.height = height;
            items = new Item[width * height];
        }
        public Inventaire(int width, int height, Item[] items)
        {
            this.width = width;
            this.height = height;
            this.items = items;
        }

        public Item GetItem(int index)
        {
            if (index >= 0 && index < items.Length)
            {
                return items[index];
            }
            throw new ArgumentOutOfRangeException();
        }

        public void RemoveItem(Item item)
        {
            if (Contains(item))
            {
                SetItem(null, GetIndex(item));
            }
            else
            {
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>true if get in<br/>false if couldnt get in or partialy</returns>
        public bool AddItem(Item item)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] == null)
                {
                    items[i] = item;
                    return true;
                }
                else
                {
                    if (items[i].id() == item.id() && items[i].MaxQuantity > items[i].Quantity)
                    {
                        if (items[i].MaxQuantity >= items[i].Quantity + item.Quantity)
                        {
                            items[i].Quantity += item.Quantity;
                            return true;
                        }
                        else
                        {
                            int a = items[i].MaxQuantity - items[i].Quantity;
                            items[i].Quantity = items[i].MaxQuantity;
                            item.Quantity -= a;
                        }
                    }
                }
            }
            return false;
        }

        public int GetIndex(Item item)
        {
            if (Contains(item))
            {
                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i] != null)
                    {
                        if (items[i].id() == item.id() && items[i].Quantity == item.Quantity)
                        {
                            return i;
                        }
                    }
                }
            }
            return -1;
        }

        public bool Contains(Item item)
        {
            if (item == null)
            {
                return false;
            }
            foreach (Item item2 in items)
            {
                if (item2 != null)
                {
                    if (item.id() == item2.id())
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void SetItem(Item item, int index)
        {
            if (index >= 0 && index < items.Length)
            {
                items[index] = item;
            }
        }

        public Inventaire Clone()
        {
            return new Inventaire(width, height, (Item[])items.Clone());
        }

        public Item[] Items
        {
            get
            {
                List<Item> a = new List<Item>();
                foreach (Item i in items)
                {
                    if (i != null)
                    {
                        a.Add(i);
                    }
                }
                return a.ToArray();
            }
        }

        /*
        public override String ToString()
        {
            String s = "Invertory{\n";
            foreach (Item item in items)
            {
                if (item != null)
                {
                    s += item.id() + "\n";
                }
            }
            s += "}";
            return s;
        }
        */

        public int Length { get => width * height; }
        public int Width { get => width; }
        public int Height { get => height; }
    }
}
