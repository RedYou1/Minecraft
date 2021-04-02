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
using System.Collections.Generic;

namespace Minecraft_Tests.Entities
{
    [TestClass]
    public class PlayerClass
    {
        [TestMethod]
        public void PlayerMethode()
        {
            Player p1 = new Player(new Location(0, 0, 0, new World()));
            Assert.AreEqual(p1.X, 0);
            Assert.AreEqual(p1.Y, 0);
            Assert.AreEqual(p1.Z, 0);
            Assert.AreEqual(p1.Pitch, 0);
            Assert.AreEqual(p1.Yaw, 0);
            p1.Move(1, 1, 1, 1, 1);
            Assert.AreEqual(p1.X, 1);
            Assert.AreEqual(p1.Y, 1);
            Assert.AreEqual(p1.Z, 1);
            Assert.AreEqual(p1.Pitch, 1);
            Assert.AreEqual(p1.Yaw, 1);
            p1.TP(0, 0, 0, 0, 0);
            Assert.AreEqual(p1.X, 0);
            Assert.AreEqual(p1.Y, 0);
            Assert.AreEqual(p1.Z, 0);
            Assert.AreEqual(p1.Pitch, 0);
            Assert.AreEqual(p1.Yaw, 0);

            Player player = new Player(new Location(0, 0, 0, new World()));
            Assert.AreEqual(player.Hunger, 20);
            player.Hunger -= 10;
            Assert.AreEqual(player.Hunger, 10);
            Assert.AreEqual(player.id(), "Player");
            Assert.AreEqual(player.DistOfItemTaking, 1);
            Steak steak = new Steak(1);
            player.Inventaire.AddItem(steak);
            Assert.AreEqual(player.Inventaire.GetIndex(steak), 0);
            player.Eat(steak);
            Assert.AreEqual(player.Inventaire.GetIndex(steak), -1);
            Assert.AreEqual(player.Hunger, 18);
            Steak nsteak = new Steak(1);
            player.Inventaire.AddItem(nsteak);
            Assert.AreEqual(player.Inventaire.GetIndex(nsteak), 0);
            player.Eat(nsteak);
            Assert.AreEqual(player.Inventaire.GetIndex(nsteak), -1);
            Assert.AreEqual(player.Hunger, 20);

            Steak nnsteak = new Steak(1);
            player.Inventaire.AddItem(nnsteak);
            Assert.AreEqual(player.Inventaire.GetIndex(nnsteak), 0);
            player.DropItem(nnsteak);
            Assert.AreEqual(player.Inventaire.GetIndex(nnsteak), -1);
            Assert.AreEqual(player.Location.World.Entities.Length, 1);
            Assert.AreEqual(player.Location.World.Entities[0].id(), "Item_Entity Steak");

            Helmet helmet = new Diamond_Helmet();
            player.Inventaire.AddItem(helmet);
            Assert.AreEqual(player.Inventaire.GetIndex(helmet), 0);
            player.Equipe(helmet);
            Assert.AreEqual(player.Inventaire.GetIndex(helmet), -1);
            Assert.AreEqual(player.Helmet, helmet);

            Helmet ihelmet = new Iron_Helmet();
            player.Inventaire.AddItem(ihelmet);
            Assert.AreEqual(player.Inventaire.GetIndex(ihelmet), 0);
            player.Equipe(ihelmet);
            Assert.AreEqual(player.Inventaire.GetIndex(ihelmet), -1);
            Assert.AreEqual(player.Inventaire.GetIndex(helmet), 0);
            Assert.AreEqual(player.Helmet, ihelmet);

            ChestPlate chestPlate = new Diamond_ChestPlate();
            player.Inventaire.AddItem(chestPlate);
            Assert.AreEqual(player.Inventaire.GetIndex(chestPlate), 1);
            player.Equipe(chestPlate);
            Assert.AreEqual(player.Inventaire.GetIndex(chestPlate), -1);
            Assert.AreEqual(player.ChestPlate, chestPlate);

            ChestPlate ichestPlate = new Iron_ChestPlate();
            player.Inventaire.AddItem(ichestPlate);
            Assert.AreEqual(player.Inventaire.GetIndex(ichestPlate), 1);
            player.Equipe(ichestPlate);
            Assert.AreEqual(player.Inventaire.GetIndex(ichestPlate), -1);
            Assert.AreEqual(player.Inventaire.GetIndex(chestPlate), 1);
            Assert.AreEqual(player.ChestPlate, ichestPlate);

            Legging legging = new Diamond_Legging();
            player.Inventaire.AddItem(legging);
            Assert.AreEqual(player.Inventaire.GetIndex(legging), 2);
            player.Equipe(legging);
            Assert.AreEqual(player.Inventaire.GetIndex(legging), -1);
            Assert.AreEqual(player.Legging, legging);

            Legging ilegging = new Iron_Legging();
            player.Inventaire.AddItem(ilegging);
            Assert.AreEqual(player.Inventaire.GetIndex(ilegging), 2);
            player.Equipe(ilegging);
            Assert.AreEqual(player.Inventaire.GetIndex(ilegging), -1);
            Assert.AreEqual(player.Inventaire.GetIndex(legging), 2);
            Assert.AreEqual(player.Legging, ilegging);

            Boots boots = new Diamond_Boots();
            player.Inventaire.AddItem(boots);
            Assert.AreEqual(player.Inventaire.GetIndex(boots), 3);
            player.Equipe(boots);
            Assert.AreEqual(player.Inventaire.GetIndex(boots), -1);
            Assert.AreEqual(player.Boots, boots);

            Boots iboots = new Iron_Boots();
            player.Inventaire.AddItem(iboots);
            Assert.AreEqual(player.Inventaire.GetIndex(iboots), 3);
            player.Equipe(iboots);
            Assert.AreEqual(player.Inventaire.GetIndex(iboots), -1);
            Assert.AreEqual(player.Inventaire.GetIndex(boots), 3);
            Assert.AreEqual(player.Boots, iboots);

            bool a = player.Equipe(iboots);
            Assert.AreEqual(a, false);

            player.TakeDamage(new PhysicalDamage(2));
            Assert.AreEqual(player.Hp, 20);
            player.TakeDamage(new PhysicalDamage(10));
            Assert.AreEqual(player.Hp, 18);
            player.TakeDamage(new PhysicalDamage(26));
            Assert.AreEqual(player.Hp, 0);

            Player np = new Player(new Location(0, 0, 0, player.Location.World));
            np.TakeItemAroundHim();
            Assert.AreEqual(np.Inventaire.Items.Length, 9);
            np.Inventaire.RemoveItem(iboots);
            Assert.AreEqual(np.Inventaire.Items.Length, 8);
        }
    }
}
