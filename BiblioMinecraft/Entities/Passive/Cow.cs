﻿using System;
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
                loc.World.SpawnEntity(new Item_Entity(this.loc,new Steak()));
            }
            loc.World.RemoveEntity(this);
        }

        public Cow(Location loc) : base(loc, 10)
        {

        }

        public override string id()
        {
            return "Cow";
        }
    }
}
