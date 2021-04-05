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

namespace Minecraft_Tests.World_System
{
    [TestClass]
    public class WorldClass
    {
        [TestMethod]
        public void WorldMethod()
        {
            World w = new World("Overworld");
            Assert.AreEqual(w.Name, "Overworld");

            Block bl = new CobbleStone_Block(new Location(0,0,0,w));
            w.SetBlock(bl);
            Assert.AreEqual(w.GetBlock(0,0,0),bl);
            Assert.AreEqual(w.Blocks.Length,1);
            Assert.AreEqual(w.Blocks[0], bl);
            Assert.AreEqual(w.Chunks.Length,1);
            Assert.AreEqual(w.Chunks[0].X, 0);
            Assert.AreEqual(w.Chunks[0].Y, 0);
            Assert.AreEqual(w.Chunks[0].Z, 0);
            Assert.AreEqual(w.Chunks[0].GetBlock(0, 0, 0), bl);
            Assert.IsNull(w.GetBlock(0,0,1));

            w = new World();
            bl = new CobbleStone_Block(new Location(-16, -16, -16, w));
            w.SetBlock(bl);
            Assert.AreEqual(w.GetBlock(-16, -16, -16), bl);
            Assert.AreEqual(w.Blocks.Length, 1);
            Assert.AreEqual(w.Blocks[0], bl);
            Assert.AreEqual(w.Chunks.Length, 1);
            Assert.AreEqual(w.Chunks[0].X, -2);
            Assert.AreEqual(w.Chunks[0].Y, -2);
            Assert.AreEqual(w.Chunks[0].Z, -2);
            Assert.AreEqual(w.Chunks[0].GetBlock(0,0,0), bl);
            Assert.IsNull(w.GetBlock(0, 0, 0));

            w = new World();
            bl = new CobbleStone_Block(new Location(-5, -5, -5, w));
            w.SetBlock(bl);
            Assert.AreEqual(w.GetBlock(-5, -5, -5), bl);
            Assert.AreEqual(w.Blocks.Length, 1);
            Assert.AreEqual(w.Blocks[0], bl);
            Assert.AreEqual(w.Chunks.Length, 1);
            Assert.AreEqual(w.Chunks[0].X, -1);
            Assert.AreEqual(w.Chunks[0].Y, -1);
            Assert.AreEqual(w.Chunks[0].Z, -1);
            Assert.AreEqual(w.Chunks[0].GetBlock(5, 5, 5), bl);
            Assert.IsNull(w.GetBlock(0, 0, 0));
        }
    }
}
