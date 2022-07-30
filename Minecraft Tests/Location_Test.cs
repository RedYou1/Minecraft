using CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting;
using CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.World_System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Minecraft_Tests
{
    [TestClass]
    public class LocationClass
    {
        [TestMethod]
        public void LocationMethod()
        {
            Location loc = new Location(0, 0, 0, new World());
            Assert.AreEqual(loc.X, 0);
            Assert.AreEqual(loc.Y, 0);
            Assert.AreEqual(loc.Z, 0);
            Assert.AreEqual(loc.AbsoluteEquals(loc.Clone()), true);
            Assert.AreEqual(loc.Equals(new Location(0, 0, 0, new World())), true);
            Assert.AreEqual(loc.AbsoluteEquals(new Location(0, 0, 0, loc.World)), true);
            Assert.AreEqual(loc.AbsoluteEquals(new Location(0, 0, 0, new World())), false);

            loc.Move(0, 0, 0, 0, 5 * (float)Math.PI);
            Assert.AreEqual(loc.Yaw, (float)Math.PI, 0.0001);
            loc.Move(0, 0, 0, 0, -5 * (float)Math.PI);
            Assert.AreEqual(loc.Yaw, 0, 0.0001);
        }
    }
}
