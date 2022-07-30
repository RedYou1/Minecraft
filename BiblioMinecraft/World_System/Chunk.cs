using System;
using System.Collections.Generic;

namespace CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.World_System
{
    public class Chunk
    {
        private int x;
        private int y;
        private int z;
        private Block[,,] blocks;

        public Chunk(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            blocks = new Block[16, 16, 16];
        }

        public Block this[int x, int y, int z]
        {
            get
            {
                if (x >= 0 && x < 16 && y >= 0 && y < 16 && z >= 0 && z < 16)
                {
                    return blocks[x, y, z];
                }
                else
                {
                    throw new ArgumentOutOfRangeException("GetBlock in chunk out of array");
                }
            }
            set
            {
                if (x >= 0 && x < 16 && y >= 0 && y < 16 && z >= 0 && z < 16)
                {
                    blocks[x, y, z] = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("SetBlock in chunk out of array");
                }
            }
        }

        public Block[] Blocks
        {
            get
            {
                List<Block> block = new List<Block>();
                for (int x = 0; x < 16; x++)
                {
                    for (int y = 0; y < 16; y++)
                    {
                        for (int z = 0; z < 16; z++)
                        {
                            if (blocks[x, y, z] != null)
                            {
                                block.Add(blocks[x, y, z]);
                            }
                        }
                    }
                }
                return block.ToArray();
            }
        }

        public int X { get => x; }
        public int Y { get => y; }
        public int Z { get => z; }
    }
}
