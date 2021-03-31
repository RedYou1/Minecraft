using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiblioMinecraft.Items;
using BiblioMinecraft.Damages;
using BiblioMinecraft.World_System;
using System.Windows.Media.Media3D;

namespace BiblioMinecraft.Entities
{
    public abstract class Entity
    {
        protected Location loc;
        protected float hp;

        public Entity(Location loc, float hp)
        {
            this.loc = loc;
            this.hp = hp;
        }

        public abstract String id();

        public virtual void Die()
        {
            loc.World.RemoveEntity(this);
            Helper.group.RemoveEntity(this);
        }


        public virtual Game_Model Model()
        {
            Game_Model a = EntityModel();
            for (int i = 0; i < a.model.Length; i++)
            {
                double[] rot = CoordToRot(a.model[i].Key);
                rot[0] += loc.Yaw;
                double dist = Math.Sqrt(a.model[i].Key[1] * a.model[i].Key[1] + (a.model[i].Key[0] * a.model[i].Key[0] + a.model[i].Key[2] * a.model[i].Key[2]));
                double[] model = RotToCoord(rot, dist);
                a.model[i].Key[0] = model[0];
                a.model[i].Key[1] = model[1];
                a.model[i].Key[2] = model[2];

                a.model[i].Key[0] += loc.X;
                a.model[i].Key[1] += loc.Y;
                a.model[i].Key[2] += loc.Z;
            }
            return a;
        }

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

        public abstract void Update();

        protected abstract Game_Model EntityModel();

        public void Move(float x, float y, float z, float pitch, float yaw)
        {
            
            Helper.group.RemoveEntity(this);
            loc.Move(x, y, z, pitch, yaw);
            Helper.group.AddEntity(this);
        }
        public void TP(float x, float y, float z, float pitch, float yaw)
        {
            Helper.group.RemoveEntity(this);
            loc.TP(x, y, z, pitch, yaw);
            Helper.group.AddEntity(this);
        }

        /// <summary>
        /// reduce his hp
        /// </summary>
        /// <param name="damage"></param>
        /// <returns>if it die</returns>
        public virtual bool TakeDamage(Damage damage)
        {
            if (damage.damage > 0)
            {
                hp -= damage.damage;
                if (hp <= 0)
                {
                    Die();
                    return true;
                }
            }
            return false;
        }

        public virtual object GetInFrontOfHim(float range)
        {
            for (float i = 0; i <= range; i += 0.005f)
            {
                Location nloc = new Location((float)Math.Cos(Pitch) * (float)Math.Sin(Yaw) * i + X,
                    (float)Math.Sin(Pitch) * i + Y,
                    (float)Math.Cos(Pitch) * (float)Math.Cos(Yaw) * i + Z,
                    Pitch, Yaw, loc.World);
                
                foreach (Entity ent in loc.World.Entities)
                {
                    if ((int)ent.X == (int)nloc.X && (int)ent.Y == (int)nloc.Y && (int)ent.Z == (int)nloc.Z)
                    {
                        if (!ent.Equal(this))
                        {
                            return ent;
                        }
                    }
                }
                
                Block block = loc.World.GetBlock((int)Math.Floor(nloc.X + 0.5f), (int)Math.Floor(nloc.Y + 0.5f), (int)Math.Floor(nloc.Z + 0.5f));
                if (block != null)
                {
                    return new KeyValuePair<Block, Location>(block, nloc);
                }
            }
            return null;
        }

        public bool Equal(Entity entity)
        {
            return id() == entity.id() && loc.Equals(entity.loc);
        }

        public virtual object Right_Clicked(Player player, Item with)
        {
            return null;
        }

        public Location Location { get => loc; }
        public float X { get => loc.X; }
        public float Y { get => loc.Y; }
        public float Z { get => loc.Z; }
        public float Pitch { get => loc.Pitch; }
        public float Yaw { get => loc.Yaw; }
        public float Hp { get => hp; }
    }
}
