using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BiblioMinecraft;
using BiblioMinecraft.Items;
using BiblioMinecraft.Items.Armors;
using BiblioMinecraft.Items.Foods;
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
        }
    }
}
