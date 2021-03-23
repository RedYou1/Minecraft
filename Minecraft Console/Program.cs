using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiblioMinecraft;
using BiblioMinecraft.Items;
using BiblioMinecraft.Attributes;
using BiblioMinecraft.Items.Armors;
using BiblioMinecraft.Items.Foods;
using BiblioMinecraft.Entities;
using BiblioMinecraft.World_System;

namespace Minecraft_Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player(0, 0, 0, 0, 0);
            Iron_ChestPlate icp = new Iron_ChestPlate();
            icp.Right_Click(player);
            Console.WriteLine(player.ChestPlate.id());
            Leather_ChestPlate lcp = new Leather_ChestPlate();
            lcp.Right_Click(player);
            Console.WriteLine(player.ChestPlate.id());

            Steak steak = new Steak();
            Console.WriteLine(player.Hunger);
            player.Eat(steak);
            Console.WriteLine(player.Hunger);

            Console.WriteLine(World.Entities.Length);
            Cow cow = new Cow(0, 0, 0, 0, 0);
            World.SpawnEntity(cow);
            Console.WriteLine(World.Entities.Length);
            cow.Die();
            Console.WriteLine(World.Entities.Length);

            Console.WriteLine("");

            Merchand mer = new Merchand(0,0,0,0,0);
            player.AddItem(steak);
            Console.WriteLine(mer.ToString());
            Console.WriteLine(player.ToString());
            mer.MakeATrade(player, mer.Trades[0]);
            Console.WriteLine(mer.ToString());
            Console.WriteLine(player.ToString());
            mer.MakeATrade(player, mer.Trades[1]);
            Console.WriteLine(mer.ToString());
            Console.WriteLine(player.ToString());

            player.Die();
            Console.ReadLine();
        }
    }
}
