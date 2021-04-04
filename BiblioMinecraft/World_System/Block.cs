using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

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
                double[,] coord = new double[1, 3];
                coord[0, 0] = a.model[i].Key[0];
                coord[0, 1] = a.model[i].Key[1];
                coord[0, 2] = a.model[i].Key[2];
                double[,] rotmat = new double[3, 3];
                rotmat[0, 0] = Math.Cos(loc.Yaw);
                rotmat[1, 0] = Math.Sin(loc.Yaw) * Math.Sin(loc.Pitch);
                rotmat[2, 0] = Math.Sin(loc.Yaw) * Math.Cos(loc.Pitch);
                rotmat[0, 1] = 0;
                rotmat[1, 1] = Math.Cos(loc.Pitch);
                rotmat[2, 1] = -Math.Sin(loc.Pitch);
                rotmat[0, 2] = -Math.Sin(loc.Yaw);
                rotmat[1, 2] = Math.Cos(loc.Yaw) * Math.Sin(loc.Pitch);
                rotmat[2, 2] = Math.Cos(loc.Yaw) * Math.Cos(loc.Pitch);

                double[,] newcoord = Helper.MultiplyMatrix(coord, rotmat);

                a.model[i].Key[0] = newcoord[0, 0] + loc.X;
                a.model[i].Key[1] = newcoord[0, 1] + loc.Y;
                a.model[i].Key[2] = newcoord[0, 2] + loc.Z;

            }
            return a;
        }

        public virtual void Update()
        {

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

            loc.World.SetBlock(block);
            Helper.group.AddBlock(block);
            return null;
        }

        public bool Equal(Block block)
        {
            return loc.Equals(block.Location);
        }

        protected abstract Game_Model BlockModel();

        public Location Location { get => loc; set => loc = value; }
        public float X { get => loc.X; }
        public float Y { get => loc.Y; }
        public float Z { get => loc.Z; }
    }
}
