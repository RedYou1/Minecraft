using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiblioMinecraft.Items;
using BiblioMinecraft.Damages;
using BiblioMinecraft.World_System;

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

        public abstract void Die();

        public void Move(float x, float y, float z, float pitch, float yaw)
        {
            loc.Move(x, y, z, pitch, yaw);
        }
        public void TP(float x, float y, float z, float pitch, float yaw)
        {
            loc.TP(x, y, z, pitch, yaw);
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
                /*
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
                */
                Block block = loc.World.GetBlock((int)Math.Floor(nloc.X+0.5f), (int)Math.Floor(nloc.Y+0.5f), (int)Math.Floor(nloc.Z+0.5f));
                if (block != null)
                {
                    return block;
                }
            }
            return null;
        }

        public bool Equal(Entity entity)
        {
            return id() == entity.id() && loc.Equals(entity.loc);
        }

        public virtual object Right_Clicked(Player player,Item with)
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
