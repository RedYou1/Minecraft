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
