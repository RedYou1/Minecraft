using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiblioMinecraft.World_System;
using BiblioMinecraft.Items;

namespace BiblioMinecraft.Entities
{
    public class Cow : Entity
    {
        public override void Die()
        {
            base.Die();
            Random r = new Random();
            int a = r.Next(1,4);
            loc.World.SpawnEntity(new Item_Entity(this.loc,new Steak(a)));
        }

        public Cow(Location loc) : base(loc, 10)
        {

        }

        public override void Update()
        {

        }

        protected override Game_Model EntityModel()
        {
            KeyValuePair<double[], double[]>[] model = new KeyValuePair<double[], double[]>[]
            {
            };
            return new Game_Model(model, Game_Model.GetImage(Helper.ImageFile+id()+".png"));
        }

        public override string id()
        {
            return "Cow";
        }
    }
}
