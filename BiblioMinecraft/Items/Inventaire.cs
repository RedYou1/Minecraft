using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiblioMinecraft.Items
{
    public class Inventaire
    {
        private Item[] items;
        public Inventaire(int width, int height)
        {
            items = new Item[width * height];
        }
        public Inventaire(Item[] items)
        {
            this.items = items;
        }

        public Item GetItem(int index)
        {
            if (index >= 0 && index < items.Length)
            {
                return items[index];
            }
            return null;
        }

        public void RemoveItem(Item item)
        {
            if (Contains(item))
            {
                SetItem(null, GetIndex(item));
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
                else {
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
            if (item == null) { return -1; }
            if (Contains(item))
            {
                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i] != null)
                    {
                        if (items[i].id() == item.id())
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
            return new Inventaire((Item[])items.Clone());
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
    }
}
