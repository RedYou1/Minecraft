using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BiblioMinecraft.Items;
using BiblioMinecraft.Items.Armors;
using BiblioMinecraft.Items.Foods;
using BiblioMinecraft.Entities;
using BiblioMinecraft.Attributes;
using BiblioMinecraft.Damages;
using BiblioMinecraft.World_System;

namespace Minecraft_Tests
{
    [TestClass]
    public class Entity
    {
        [TestMethod]
        public void Player()
        {
            Player player = new Player(0, 0, 0);
            Assert.AreEqual(player.Hunger, 20);
            player.Hunger -= 10;
            Assert.AreEqual(player.Hunger, 10);
            Assert.AreEqual(player.id(), "Player");
            Assert.AreEqual(player.DistOfItemTaking, 1);
            Steak steak = new Steak();
            player.AddItem(steak);
            Assert.AreEqual(player.Inventaire.GetIndex(steak), 0);
            player.Eat(steak);
            Assert.AreEqual(player.Inventaire.GetIndex(steak), -1);
            Assert.AreEqual(player.Hunger, 18);
            Steak nsteak = new Steak();
            player.AddItem(nsteak);
            Assert.AreEqual(player.Inventaire.GetIndex(nsteak), 0);
            player.Eat(nsteak);
            Assert.AreEqual(player.Inventaire.GetIndex(nsteak), -1);
            Assert.AreEqual(player.Hunger, 20);

            Steak nnsteak = new Steak();
            player.AddItem(nnsteak);
            Assert.AreEqual(player.Inventaire.GetIndex(nnsteak), 0);
            player.DropItem(nnsteak);
            Assert.AreEqual(player.Inventaire.GetIndex(nnsteak), -1);
            Assert.AreEqual(World.Entities.Length, 1);
            Assert.AreEqual(World.Entities[0].id(), "Item_Entity Steak");

            Helmet helmet = new Diamond_Helmet();
            player.AddItem(helmet);
            Assert.AreEqual(player.Inventaire.GetIndex(helmet), 0);
            player.Equipe(helmet);
            Assert.AreEqual(player.Inventaire.GetIndex(helmet), -1);
            Assert.AreEqual(player.Helmet, helmet);

            Helmet ihelmet = new Iron_Helmet();
            player.AddItem(ihelmet);
            Assert.AreEqual(player.Inventaire.GetIndex(ihelmet), 0);
            player.Equipe(ihelmet);
            Assert.AreEqual(player.Inventaire.GetIndex(ihelmet), -1);
            Assert.AreEqual(player.Inventaire.GetIndex(helmet), 1);
            Assert.AreEqual(player.Helmet, ihelmet);

            ChestPlate chestPlate = new Diamond_ChestPlate();
            player.AddItem(chestPlate);
            Assert.AreEqual(player.Inventaire.GetIndex(chestPlate), 0);
            player.Equipe(chestPlate);
            Assert.AreEqual(player.Inventaire.GetIndex(chestPlate), -1);
            Assert.AreEqual(player.ChestPlate, chestPlate);

            ChestPlate ichestPlate = new Iron_ChestPlate();
            player.AddItem(ichestPlate);
            Assert.AreEqual(player.Inventaire.GetIndex(ichestPlate), 0);
            player.Equipe(ichestPlate);
            Assert.AreEqual(player.Inventaire.GetIndex(ichestPlate), -1);
            Assert.AreEqual(player.Inventaire.GetIndex(chestPlate), 2);
            Assert.AreEqual(player.ChestPlate, ichestPlate);

            Legging legging = new Diamond_Legging();
            player.AddItem(legging);
            Assert.AreEqual(player.Inventaire.GetIndex(legging), 0);
            player.Equipe(legging);
            Assert.AreEqual(player.Inventaire.GetIndex(legging), -1);
            Assert.AreEqual(player.Legging, legging);

            Legging ilegging = new Iron_Legging();
            player.AddItem(ilegging);
            Assert.AreEqual(player.Inventaire.GetIndex(ilegging), 0);
            player.Equipe(ilegging);
            Assert.AreEqual(player.Inventaire.GetIndex(ilegging), -1);
            Assert.AreEqual(player.Inventaire.GetIndex(legging), 3);
            Assert.AreEqual(player.Legging, ilegging);

            Boots boots = new Diamond_Boots();
            player.AddItem(boots);
            Assert.AreEqual(player.Inventaire.GetIndex(boots), 0);
            player.Equipe(boots);
            Assert.AreEqual(player.Inventaire.GetIndex(boots), -1);
            Assert.AreEqual(player.Boots, boots);

            Boots iboots = new Iron_Boots();
            player.AddItem(iboots);
            Assert.AreEqual(player.Inventaire.GetIndex(iboots), 0);
            player.Equipe(iboots);
            Assert.AreEqual(player.Inventaire.GetIndex(iboots), -1);
            Assert.AreEqual(player.Inventaire.GetIndex(boots), 4);
            Assert.AreEqual(player.Boots, iboots);

            bool a = player.Equipe(iboots);
            Assert.AreEqual(a, false);

            player.TakeDamage(new PhysicalDamage(2));
            Assert.AreEqual(player.Hp, 20);
            player.TakeDamage(new PhysicalDamage(10));
            Assert.AreEqual(player.Hp, 18);
            player.TakeDamage(new PhysicalDamage(26));
            Assert.AreEqual(player.Hp, 0);

            Player np = new Player(0, 0, 0);
            np.TakeItemAroundHim();
            Assert.AreEqual(np.Inventaire.Items.Length, 9);
            np.RemoveItem(iboots);
            Assert.AreEqual(np.Inventaire.Items.Length, 8);
        }
    }
}
