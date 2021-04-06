using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting;
using CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Items;
using CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Items.Armors;
using CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Entities;
using CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Attributes;
using CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Damages;
using CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.World_System;

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
