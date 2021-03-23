using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BiblioMinecraft.Items;
using BiblioMinecraft.Items.Armors;
using BiblioMinecraft.Items.Foods;
using BiblioMinecraft.Entities;
using BiblioMinecraft.Attributes;
using BiblioMinecraft.Damages;

namespace Minecraft_Tests
{
    [TestClass]
    public class Entity
    {
        [TestMethod]
        public void Player()
        {
            Player player = new Player(0, 0, 0);
            Steak steak = new Steak();
            player.AddItem(steak);
            Assert.AreEqual(player.Inventaire.GetIndex(steak), 0);
            player.Eat(steak);
            //Assert.AreEqual(player.Inventaire.GetIndex(steak), -1);

            Helmet helmet = new Diamond_Helmet();
            player.AddItem(helmet);
            Assert.AreEqual(player.Inventaire.GetIndex(helmet), 1);
            player.Equipe(helmet);
            Assert.AreEqual(player.Inventaire.GetIndex(helmet), -1);

            Helmet ihelmet = new Iron_Helmet();
            player.AddItem(ihelmet);
            Assert.AreEqual(player.Inventaire.GetIndex(ihelmet), 1);
            player.Equipe(ihelmet);
            Assert.AreEqual(player.Inventaire.GetIndex(ihelmet), -1);
            Assert.AreEqual(player.Inventaire.GetIndex(helmet), 2);

            ChestPlate chestPlate = new Diamond_ChestPlate();
            player.AddItem(chestPlate);
            Assert.AreEqual(player.Inventaire.GetIndex(chestPlate), 1);
            player.Equipe(chestPlate);
            Assert.AreEqual(player.Inventaire.GetIndex(chestPlate), -1);

            ChestPlate ichestPlate = new Iron_ChestPlate();
            player.AddItem(ichestPlate);
            Assert.AreEqual(player.Inventaire.GetIndex(ichestPlate), 1);
            player.Equipe(ichestPlate);
            Assert.AreEqual(player.Inventaire.GetIndex(ichestPlate), -1);
            Assert.AreEqual(player.Inventaire.GetIndex(chestPlate), 3);

            Legging legging = new Diamond_Legging();
            player.AddItem(legging);
            Assert.AreEqual(player.Inventaire.GetIndex(legging), 1);
            player.Equipe(legging);
            Assert.AreEqual(player.Inventaire.GetIndex(legging), -1);

            Legging ilegging = new Iron_Legging();
            player.AddItem(ilegging);
            Assert.AreEqual(player.Inventaire.GetIndex(ilegging), 1);
            player.Equipe(ilegging);
            Assert.AreEqual(player.Inventaire.GetIndex(ilegging), -1);
            Assert.AreEqual(player.Inventaire.GetIndex(legging), 4);

            Boots boots = new Diamond_Boots();
            player.AddItem(boots);
            Assert.AreEqual(player.Inventaire.GetIndex(boots), 1);
            player.Equipe(boots);
            Assert.AreEqual(player.Inventaire.GetIndex(boots), -1);

            Boots iboots = new Iron_Boots();
            player.AddItem(iboots);
            Assert.AreEqual(player.Inventaire.GetIndex(iboots), 1);
            player.Equipe(iboots);
            Assert.AreEqual(player.Inventaire.GetIndex(iboots), -1);
            Assert.AreEqual(player.Inventaire.GetIndex(boots), 5);

            bool a = player.Equipe(iboots);
            Assert.AreEqual(a,false);

            player.TakeDamage(new PhysicalDamage(2));
            player.TakeDamage(new PhysicalDamage(200));

            Player np = new Player(0,0,0);
            np.TakeItemAroundHim();
            Assert.AreEqual(np.Inventaire.Items.Length,8);
        }
    }
}
