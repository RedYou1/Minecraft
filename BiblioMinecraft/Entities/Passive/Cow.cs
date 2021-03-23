using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiblioMinecraft.World_System;
using BiblioMinecraft.Items.Foods;

namespace BiblioMinecraft.Entities
{
    public class Cow : Entity
    {
        public override void Die()
        {
            Random r = new Random();
            int a = r.Next(1,4);
            for (int i = 0; i < a;i++)
            {
                World.SpawnEntity(new Item_Entity(this.x,this.y,this.z,this.pitch,this.yaw,new Steak()));
            }
            World.RemoveEntity(this);
        }

        public Cow(float x, float y, float z, float pitch, float yaw) : base(x, y, z, pitch, yaw, 10)
        {

        }

        public override string id()
        {
            return "Cow";
        }
    }
}
