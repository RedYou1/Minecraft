using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiblioMinecraft.Damages
{
    public abstract class Damage : DamageSoure
    {
        public int damage;
        public abstract String id();

        public Damage(int damage)
        {
            this.damage = damage;
        }
    }
}
