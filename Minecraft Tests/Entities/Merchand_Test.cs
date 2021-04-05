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
    public class MerchandClass
    {
        [TestMethod]
        public void MerchandMethod()
        {
            World world = new World();
            Merchand mer = new Merchand(new Location(0,0,0,world));
            Assert.AreEqual(mer.id(), "Merchand");
            Player player = new Player(new Location(0,0,0,world));
            Steak steak = new Steak(1);
            player.Inventaire.AddItem(steak);
            Assert.AreEqual(player.Inventaire.GetItem(0).id(),steak.id());

            mer.Die();
        }
    }
}
