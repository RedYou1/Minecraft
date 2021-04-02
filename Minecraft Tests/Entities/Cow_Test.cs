using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BiblioMinecraft;
using BiblioMinecraft.Items;
using BiblioMinecraft.Items.Armors;
using BiblioMinecraft.Entities;
using BiblioMinecraft.Attributes;
using BiblioMinecraft.Damages;
using BiblioMinecraft.World_System;

namespace Minecraft_Tests.Entities
{
    [TestClass]
    public class CowClass
    {
        [TestMethod]
        public void CowMethode()
        {
            Cow cow = new Cow(new Location(0, 0, 0,new World()));

            Assert.AreEqual(cow.id(), "Cow");

            cow.Die();

            Cow cow2 = new Cow(new Location(0, 0, 0, new World()));
            Assert.AreEqual(cow2.Hp,10);
            Player bucher = new Player(new Location(0, 0, 0, cow2.Location.World));
            Iron_Sword irons = new Iron_Sword();
            bucher.Inventaire.AddItem(irons);
            irons.Left_Click(bucher,cow2);
            Assert.AreEqual(cow2.Hp, 4);
        }
    }
}
