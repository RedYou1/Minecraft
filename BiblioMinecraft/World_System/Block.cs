using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiblioMinecraft.World_System
{
    public abstract class Block : Model
    {
        protected Location loc;

        public Block(Location loc)
        {
            this.loc = loc;
        }

        public virtual Game_Model Model()
        {
            Game_Model a = BlockModel();
            for (int i = 0; i < a.model.Length; i++)
            {
                double[] rot = CoordToRot(a.model[i].Key);
                rot[0] += loc.Pitch;
                //rot[1] += yaw;
                double dist = Math.Sqrt(a.model[i].Key[1] * a.model[i].Key[1] + (a.model[i].Key[0] * a.model[i].Key[0] + a.model[i].Key[2] * a.model[i].Key[2]));
                double[] model = RotToCoord(rot, dist);
                a.model[i].Key[0] = model[0];
                a.model[i].Key[1] = model[1];
                a.model[i].Key[2] = model[2];

                a.model[i].Key[0] += loc.X;
                a.model[i].Key[1] += loc.Y;
                a.model[i].Key[2] += loc.Z;

            }
            //pitch += 0.01f;
            return a;
        }

        public void Move(float x, float y, float z, float pitch, float yaw)
        {
            if ((int)x != (int)loc.X || (int)y != (int)loc.Y || (int)z != (int)loc.Z)
            {
                ChangeCo((int)x, (int)y, (int)z);
            }
            loc.Move(x, y, z, pitch, yaw);
        }
        public void TP(float x, float y, float z, float pitch, float yaw)
        {
            if ((int)x != (int)loc.X || (int)y != (int)loc.Y || (int)z != (int)loc.Z)
            {
                ChangeCo((int)x, (int)y, (int)z);
            }
            loc.TP(x, y, z, pitch, yaw);
        }

        private void ChangeCo(int x, int y, int z)
        {
            loc.World.SetBlock(x, y, z, this);
            loc.World.SetBlock((int)Math.Round(loc.X), (int)Math.Round(loc.Y), (int)Math.Round(loc.Z), null);
        }

        public virtual void Destroy()
        {
            loc.World.SetBlock((int)Math.Round(loc.X), (int)Math.Round(loc.Y), (int)Math.Round(loc.Z), null);
            Helper.group.RemoveBlock(this);
        }

        public virtual void Left_Click(BiblioMinecraft.Entities.Player player)
        {
            Destroy();
        }

        public virtual object Right_Click(BiblioMinecraft.Entities.Player player, Block block, Location loc)
        {
            float x = loc.X - X;
            float z = loc.Z - Z;
            float yaw = (float)Math.Atan2(z, x);

            float pitch = (float)Math.Atan2(loc.Y - Y, Math.Sqrt(x * x + z * z));
            if (pitch > Math.PI / 4)
            {
                block.Location = new Location(X, Y + 1, Z, loc.World);
            }
            else if (pitch < -Math.PI / 4)
            {
                block.Location = new Location(X, Y - 1, Z, loc.World);
            }
            else if (yaw >= 2 * Math.PI - Math.PI / 4 || yaw <= Math.PI / 4)
            {
                block.Location = new Location(X, Y, Z + 1, loc.World);
            }
            else if (yaw >= Math.PI / 2 - Math.PI / 4 && yaw <= Math.PI / 2 + Math.PI / 4)
            {
                block.Location = new Location(X - 1, Y, Z, loc.World);
            }
            else if (yaw >= Math.PI - Math.PI / 4 && yaw <= Math.PI + Math.PI / 4)
            {
                block.Location = new Location(X, Y, Z - 1, loc.World);
            }
            else if (yaw >= Math.PI + Math.PI / 2 - Math.PI / 4 && yaw <= Math.PI + Math.PI / 2 + Math.PI / 4)
            {
                block.Location = new Location(X + 1, Y, Z, loc.World);
            }
            else
            {
                return null;
            }

            loc.World.SetBlock(block);
            return block;
        }

        public bool Equal(Block block)
        {
            return loc.Equals(block.Location);
        }

        protected abstract Game_Model BlockModel();

        protected double[] CoordToRot(double[] coord)
        {
            double[] rot = new double[2];
            rot[0] = Math.Atan2(coord[0], coord[2]);
            rot[1] = Math.Atan2(coord[1], Math.Sqrt(coord[0] * coord[0] + coord[2] * coord[2]));
            return rot;
        }

        protected double[] RotToCoord(double[] rot, double dist)
        {
            double[] coord = new double[3];
            coord[0] = Math.Cos(rot[1]) * Math.Sin(rot[0]) * dist;
            coord[1] = Math.Sin(rot[1]) * dist;
            coord[2] = Math.Cos(rot[1]) * Math.Cos(rot[0]) * dist;
            return coord;
        }

        public Location Location { get => loc; set => loc = value; }
        public float X { get => loc.X; }
        public float Y { get => loc.Y; }
        public float Z { get => loc.Z; }
    }
}
