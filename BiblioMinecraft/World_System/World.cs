using CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.World_System
{
    public class World
    {
        private List<Chunk> chunks = new List<Chunk>();
        private List<Entity> entities = new List<Entity>();
        private String name;

        public void SpawnEntity(Entity entity)
        {
            entities.Add(entity);
        }

        public void RemoveEntity(Entity entity)
        {
            entities.Remove(entity);
        }

        public World() : this(RandomName()) { }
        private static String RandomName()
        {
            Random r = new Random();
            return "" + r.Next(Int32.MinValue, Int32.MaxValue);
        }
        public World(String name)
        {
            this.name = name;
        }

        public void SetBlock(Block block)
        {
            SetBlock((int)Math.Round(block.X), (int)Math.Round(block.Y), (int)Math.Round(block.Z), block);
        }

        public void SetBlock(int x, int y, int z, Block block)
        {
            if (x < 0) { x -= 16; }
            if (y < 0) { y -= 16; }
            if (z < 0) { z -= 16; }
            Chunk c = this[x / 16, y / 16, z / 16];

            if (c == null)
            {
                c = new Chunk(x / 16, y / 16, z / 16);
                chunks.Add(c);
            }

            int nx = x % 16;
            int ny = y % 16;
            int nz = z % 16;
            if (nx < 0) { nx *= -1; }
            if (ny < 0) { ny *= -1; }
            if (nz < 0) { nz *= -1; }

            c[nx, ny, nz] = block;
        }
        public Block GetBlock(int x, int y, int z)
        {
            if (x < 0) { x -= 16; }
            if (y < 0) { y -= 16; }
            if (z < 0) { z -= 16; }
            Chunk c = this[x / 16, y / 16, z / 16];
            if (c != null)
            {
                int nx = x % 16;
                int ny = y % 16;
                int nz = z % 16;
                if (nx < 0) { nx *= -1; }
                if (ny < 0) { ny *= -1; }
                if (nz < 0) { nz *= -1; }
                return c[nx, ny, nz];
            }
            return null;
        }

        public Chunk this[int x, int y, int z] => chunks.FirstOrDefault(c => c.X == x && c.Y == y && c.Z == z);
        public String Name => name;
        public Chunk[] Chunks => chunks.ToArray();
        public Block[] Blocks => chunks.SelectMany(c => c.Blocks).Where(b => b != null).ToArray();
        public Entity[] Entities => entities.ToArray();
    }
}
