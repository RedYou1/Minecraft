using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiblioMinecraft.Items;
using BiblioMinecraft.World_System;
using BiblioMinecraft.Attributes;

namespace BiblioMinecraft.Entities
{
    public class Item_Entity : Entity
    {
        public override void Die()
        {
            loc.World.RemoveEntity(this);
        }

        private Item item;

        public Item_Entity(Location loc,Item item) : base(loc, 1)
        {
            this.item = item;
        }

        public override string id()
        {
            return "Item_Entity " + item.id();
        }

        protected override Game_Model EntityModel()
        {
            KeyValuePair<double[], double[]>[] model = new KeyValuePair<double[], double[]>[]{
                // Front face
                new KeyValuePair<double[],double[]>(new double[]{ 0.5, 0.5, 0.03125 },new double[]{ 1 , 0}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.5, 0.5, 0.03125 },new double[]{ 0 , 0}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.5, -0.5, 0.03125 },new double[]{ 1 , 1}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.5, -0.5, 0.03125 },new double[]{ 1 , 1}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.5, 0.5, 0.03125 },new double[]{ 0 , 0}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.5, -0.5, 0.03125 },new double[]{ 0 , 1}),

                // Back face
                new KeyValuePair<double[],double[]>(new double[]{ 0.5, 0.5, -0.03125 },new double[]{ 0 , 0}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.5, -0.5, -0.03125 },new double[]{ 0 , 1}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.5, 0.5, -0.03125 },new double[]{ 1 , 0}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.5, 0.5, -0.03125 },new double[]{ 1 , 0}),
                new KeyValuePair<double[],double[]>(new double[]{ 0.5, -0.5, -0.03125 },new double[]{ 0 , 1}),
                new KeyValuePair<double[],double[]>(new double[]{ -0.5, -0.5, -0.03125 },new double[]{ 1 , 1})
            };
            return new Game_Model(model, Game_Model.GetImage(Helper.ImageFile + "Items\\" + (item is Armor? "Armors\\" : "") + item.id() +".png"));
        }

        public Item Item { get => item; }
    }
}
