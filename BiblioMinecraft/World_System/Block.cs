using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiblioMinecraft.World_System
{
    public class Block
    {
        private float x;
        private float y;
        private float z;

        public Block(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public double[][] Box()
        {
            double[][] v = new double[8][];
            v[0] = new double[3] { -0.5 + x, 0.5 + y, 0.5 + z };
            v[1] = new double[3] { 0.5 + x, -0.5 + y, 0.5 + z };
            v[2] = new double[3] { 0.5 + x, 0.5 + y, 0.5 + z };
            v[3] = new double[3] { -0.5 + x, -0.5 + y, 0.5 + z };
            v[4] = new double[3] { -0.5 + x, 0.5 + y, -0.5 + z };
            v[5] = new double[3] { 0.5 + x, -0.5 + y, -0.5 + z };
            v[6] = new double[3] { 0.5 + x, 0.5 + y, -0.5 + z };
            v[7] = new double[3] { -0.5 + x, -0.5 + y, -0.5 + z };
            y += 0.01f;
            return v;
        }

        public float X { get => x; }
        public float Y { get => y; }
        public float Z { get => z; }
    }
}
