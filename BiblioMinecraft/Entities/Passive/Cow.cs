using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.World_System;
using CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Items;
using System.Windows.Media.Media3D;
using System.Windows;

namespace CegepVicto.TechInfo.H21.P2.DA2033220.Minecrafting.Entities
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

        protected override Rect3D HitBox()
        {
            return new Rect3D(-0.5, -0.5f, -0.5, 0.5, 0.5, 0.5);
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
