using CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Items;
using CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Items.Armors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Minecraft_Tests.Items
{
    [TestClass]
    public class InventaireClass
    {
        [TestMethod]
        public void InventaireMethod()
        {
            Inventaire inv = new Inventaire(2, 2);
            Assert.AreEqual(inv.Width, 2);
            Assert.AreEqual(inv.Height, 2);
            Assert.AreEqual(inv.Length, 4);

            Inventaire i = inv.Clone();
            Assert.AreEqual(i.Width, 2);
            Assert.AreEqual(i.Height, 2);
            Assert.AreEqual(i.Length, 4);

            Assert.AreEqual(inv.GetItem(0), null);
            Assert.ThrowsException<ArgumentOutOfRangeException>(new Action(() => { inv.GetItem(5); }));
            Item it = new Diamond_Boots();
            inv.AddItem(it);
            Assert.AreEqual(inv.GetItem(0), it);
            Assert.ThrowsException<ArgumentOutOfRangeException>(new Action(() => { inv.GetItem(5); }));
            inv.RemoveItem(it);
            Assert.AreEqual(inv.GetItem(0), null);
            Assert.ThrowsException<ArgumentOutOfRangeException>(new Action(() => { inv.GetItem(5); }));
            Assert.ThrowsException<ArgumentException>(new Action(() => { inv.RemoveItem(it); }));

            it = new Emerald(1);
            inv.AddItem(it);
            Assert.AreEqual(inv.GetItem(0).id(), it.id());
            Assert.AreEqual(inv.GetItem(1), null);
            Assert.AreEqual(inv.GetItem(0).Quantity, 1);

            Assert.AreEqual(inv.Items.Length, 1);
            Assert.AreEqual(inv.Items[0].id(), it.id());
            Assert.AreEqual(inv.Items[0].Quantity, it.Quantity);

            inv.AddItem(it);
            Assert.AreEqual(inv.GetItem(0).id(), it.id());
            Assert.AreEqual(inv.GetItem(1), null);
            Assert.AreEqual(inv.GetItem(0).Quantity, 2);

            it = new Emerald(64);
            inv.AddItem(it);
            Assert.AreEqual(inv.GetItem(0).id(), it.id());
            Assert.AreEqual(inv.GetItem(1).id(), it.id());
            Assert.AreEqual(inv.GetItem(0).Quantity, 64);
            Assert.AreEqual(inv.GetItem(1).Quantity, 2);

            it = new Emerald(64);
            inv.AddItem(it);
            Assert.AreEqual(inv.GetItem(1).Quantity, 64);
            Assert.AreEqual(inv.GetItem(2).Quantity, 2);
            it = new Emerald(64);
            inv.AddItem(it);
            Assert.AreEqual(inv.GetItem(2).Quantity, 64);
            Assert.AreEqual(inv.GetItem(3).Quantity, 2);
            it = new Emerald(64);
            Assert.AreEqual(inv.AddItem(it), false);
            Assert.AreEqual(inv.GetItem(3).Quantity, 64);
        }
    }
}
