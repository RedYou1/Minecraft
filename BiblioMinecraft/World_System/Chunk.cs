using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiblioMinecraft.World_System
{
    public class Chunk
    {
        private int x;
        private int y;
        private int z;
        private List<Block> blocks;

        public Chunk(int x,int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            blocks = new List<Block>();
        }

        public Block[] Blocks { get => blocks.ToArray(); }

        public int X { get => x; }
        public int Y { get => y; }
        public int Z { get => z; }
    }
}
