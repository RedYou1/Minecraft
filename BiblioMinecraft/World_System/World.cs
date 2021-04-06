using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Entities;

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
            Chunk c = GetChunk((int)(x / 16), (int)(y / 16), (int)(z / 16));

            if (c == null)
            {
                c = new Chunk((int)(x / 16), (int)(y / 16), (int)(z / 16));
                chunks.Add(c);
            }

            int nx = (int)(x % 16);
            int ny = (int)(y % 16);
            int nz = (int)(z % 16);
            if (nx < 0) { nx *= -1; }
            if (ny < 0) { ny *= -1; }
            if (nz < 0) { nz *= -1; }

            c.SetBlock(nx, ny, nz, block);
        }
        public Block GetBlock(int x, int y, int z)
        {
            if (x < 0) { x -= 16; }
            if (y < 0) { y -= 16; }
            if (z < 0) { z -= 16; }
            Chunk c = GetChunk((int)(x / 16), (int)(y / 16), (int)(z / 16));
            if (c != null)
            {
                int nx = (int)(x % 16);
                int ny = (int)(y % 16);
                int nz = (int)(z % 16);
                if (nx < 0) { nx *= -1; }
                if (ny < 0) { ny *= -1; }
                if (nz < 0) { nz *= -1; }
                Block b = c.GetBlock(nx, ny, nz);
                return b;
            }
            return null;
        }

        public Chunk GetChunk(int x, int y, int z)
        {
            foreach (Chunk chunk in chunks)
            {
                if (chunk.X == x && chunk.Y == y && chunk.Z == z)
                {
                    return chunk;
                }
            }
            return null;
        }

        public String Name { get => name; }
        public Chunk[] Chunks { get => chunks.ToArray(); }
        public Block[] Blocks
        {
            get
            {
                List<Block> blocks = new List<Block>();
                foreach (Chunk chunk in chunks)
                {
                    foreach (Block block in chunk.Blocks)
                    {
                        if (block != null)
                        {
                            blocks.Add(block);
                        }
                    }
                }
                return blocks.ToArray();
            }
        }
        public Entity[] Entities { get => entities.ToArray(); }
    }
}
