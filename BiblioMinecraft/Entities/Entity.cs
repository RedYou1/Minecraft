using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Items;
using CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Damages;
using CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.World_System;
using System.Windows.Media.Media3D;

namespace CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Entities
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
            if (Helper.group != null) { Helper.group.RemoveEntity(this); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>get the 3D model</returns>
        public virtual Game_Model Model()
        {
            Game_Model a = EntityModel();
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

        protected abstract Game_Model EntityModel();

        public bool InHitbox(Location locp)
        {
            Rect3D hit = HitBox();
            return locp.X >= loc.X + hit.X && locp.X <= loc.X + hit.SizeX
                && locp.Y >= loc.Y + hit.Y && locp.Y <= loc.Y + hit.SizeY
                && locp.Z >= loc.Z + hit.Z && locp.Z <= loc.Z + hit.SizeZ;
        }

        protected abstract Rect3D HitBox();

        public void Move(float x, float y, float z, float pitch, float yaw)
        {

            if (Helper.group != null) { Helper.group.RemoveEntity(this); }
            loc.Move(x, y, z, pitch, yaw);
            if (Helper.group != null) { Helper.group.AddEntity(this); }
        }
        public void TP(float x, float y, float z, float pitch, float yaw)
        {
            if (Helper.group != null) { Helper.group.RemoveEntity(this); }
            loc.TP(x, y, z, pitch, yaw);
            if (Helper.group != null) { Helper.group.AddEntity(this); }
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
                    if (ent.InHitbox(nloc))
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
