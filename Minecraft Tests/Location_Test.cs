using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BiblioMinecraft;
using BiblioMinecraft.Items;
using BiblioMinecraft.Items.Armors;
using BiblioMinecraft.Entities;
using BiblioMinecraft.Attributes;
using BiblioMinecraft.Damages;
using BiblioMinecraft.World_System;
using BiblioMinecraft.World_System.Blocks;

namespace Minecraft_Tests
{
    [TestClass]
    public class LocationClass
    {
        [TestMethod]
        public void LocationMethod()
        {
            Location loc = new Location(0,0,0,new World());
            Assert.AreEqual(loc.X,0);
            Assert.AreEqual(loc.Y, 0);
            Assert.AreEqual(loc.Z, 0);
            Assert.AreEqual(loc.AbsoluteEquals(loc.Clone()),true);
            Assert.AreEqual(loc.Equals(new Location(0, 0, 0, new World())), true);
            Assert.AreEqual(loc.AbsoluteEquals(new Location(0, 0, 0, loc.World)), true);
            Assert.AreEqual(loc.AbsoluteEquals(new Location(0,0,0,new World())), false);

            loc.Move(0,0,0,0,5*(float)Math.PI);
            Assert.AreEqual(loc.Yaw, (float)Math.PI,0.0001);
            loc.Move(0, 0, 0, 0, -5 * (float)Math.PI);
            Assert.AreEqual(loc.Yaw, 0, 0.0001);
        }
    }
}
