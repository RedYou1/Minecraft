using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiblioMinecraft.World_System
{
    public abstract class Block : Model
    {
        protected float x;
        protected float y;
        protected float z;

        public Block(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public abstract Game_Model Model();

        public float X { get => x; }
        public float Y { get => y; }
        public float Z { get => z; }
    }
}
