using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiblioMinecraft.World_System;

namespace BiblioMinecraft
{
    public class Location
    {
        private float x;
        private float y;
        private float z;
        private float pitch;
        private float yaw;
        private World world;

        public Location(float x, float y, float z, World world) : this(x, y, z, 0, 0, world) { }
        public Location(float x, float y, float z, float pitch, float yaw, World world)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.pitch = pitch;
            this.yaw = yaw;
            this.world = world;
        }


        public void Move(float x, float y, float z, float pitch, float yaw)
        {
            this.x += x;
            this.y += y;
            this.z += z;
            this.pitch += pitch;
            this.yaw += yaw;
        }

        public virtual void TP(float x, float y, float z, float pitch, float yaw)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.pitch = pitch;
            this.yaw = yaw;
        }

        public virtual void ChangeWorld(float x, float y, float z, float pitch, float yaw, World world)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.pitch = pitch;
            this.yaw = yaw;
            this.world = world;
        }

        public bool Equals(Location loc)
        {
            return X == loc.X && Y == loc.Y && Z == loc.Z && Pitch == loc.Pitch && Yaw == loc.Yaw;
        }

        public float X { get => x; }
        public float Y { get => y; }
        public float Z { get => z; }
        public float Pitch { get => pitch; }
        public float Yaw { get => yaw; }
        public World World { get => world; }
    }
}
