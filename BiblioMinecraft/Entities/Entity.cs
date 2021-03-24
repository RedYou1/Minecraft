using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiblioMinecraft.Items;
using BiblioMinecraft.Damages;

namespace BiblioMinecraft.Entities
{
    public abstract class Entity
    {
        protected float x;
        protected float y;
        protected float z;
        protected float pitch;
        protected float yaw;
        protected float hp;

        public Entity(float x, float y, float z, float pitch, float yaw, float hp)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.pitch = pitch;
            this.yaw = yaw;
            this.hp = hp;
        }

        public abstract String id();

        public abstract void Die();
        public virtual void Move(float x, float y, float z, float pitch, float yaw)
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

        public float X { get => x; }
        public float Y { get => y; }
        public float Z { get => z; }
        public float Pitch { get => pitch; }
        public float Yaw { get => yaw; }
        public float Hp { get => hp; }
    }
}
