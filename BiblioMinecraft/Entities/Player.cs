using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiblioMinecraft.Attributes;
using BiblioMinecraft.Items;
using BiblioMinecraft.Damages;
using BiblioMinecraft.World_System;

namespace BiblioMinecraft.Entities
{
    public class Player : Entity
    {
        private Helmet helmet = null;
        private ChestPlate chestPlate = null;
        private Legging legging = null;
        private Boots boots = null;

        private float hunger = 20;

        private Inventaire inventaire = new Inventaire(9, 4);

        protected float distOfItemTaking = 1;

        public Player(Location loc) : base(loc, 20)
        { }

        public override string id()
        {
            return "Player";
        }

        public override void Die()
        {
            foreach (Item item in inventaire.Items)
            {
                if (item != null)
                {
                    item.EntityDied(this);
                }
            }
            if (helmet != null)
            {
                helmet.EntityDied(this);
            }
            if (chestPlate != null)
            {
                chestPlate.EntityDied(this);
            }
            if (legging != null)
            {
                legging.EntityDied(this);
            }
            if (boots != null)
            {
                boots.EntityDied(this);
            }
        }

        public override bool TakeDamage(Damage damage)
        {
            if (helmet != null) { helmet.Reduce(damage); }
            if (chestPlate != null) { chestPlate.Reduce(damage); }
            if (legging != null) { legging.Reduce(damage); }
            if (boots != null) { boots.Reduce(damage); }
            base.TakeDamage(damage);
            return false;
        }

        public bool Equipe(Armor armor)
        {
            if (inventaire.Contains(armor))
            {
                Armor pre = null;
                if (armor.Attribute_id() == "Helmet")
                {
                    if (helmet != null) { pre = helmet; }
                    helmet = (Helmet)armor;
                }
                else if (armor.Attribute_id() == "ChestPlate")
                {
                    if (chestPlate != null) { pre = chestPlate; }
                    chestPlate = (ChestPlate)armor;
                }
                else if (armor.Attribute_id() == "Legging")
                {
                    if (legging != null) { pre = legging; }
                    legging = (Legging)armor;
                }
                else if (armor.Attribute_id() == "Boots")
                {
                    if (boots != null) { pre = boots; }
                    boots = (Boots)armor;
                }
                inventaire.RemoveItem(armor);
                if (pre != null)
                {
                    inventaire.AddItem(pre);
                }
                return true;
            }
            return false;
        }

        public override void Update()
        {

        }

        public void TakeItemAroundHim()
        {
            List<Item_Entity> ie = new List<Item_Entity>();
            foreach (Entity entity in loc.World.Entities)
            {
                if (entity is Item_Entity item)
                {
                    if (Helper.Dist(this.loc, item.Location) <= distOfItemTaking)
                    {
                        if (inventaire.AddItem(item.Item))
                        {
                            ie.Add(item);
                        }
                    }
                }
            }
            foreach (Item_Entity item in ie)
            {
                item.Die();
            }
        }

        public void DropItem(Item item)
        {
            if (inventaire.Contains(item))
            {
                inventaire.RemoveItem(item);
                Item_Entity ie = new Item_Entity(this.loc, item);
                loc.World.SpawnEntity(ie);
            }
            else if (helmet != null && item == helmet)
            {
                helmet = null;
                Item_Entity ie = new Item_Entity(this.loc, item);
                loc.World.SpawnEntity(ie);
            }
            else if (chestPlate != null && item == chestPlate)
            {
                chestPlate = null;
                Item_Entity ie = new Item_Entity(this.loc, item);
                loc.World.SpawnEntity(ie);
            }
            else if (legging != null && item == legging)
            {
                legging = null;
                Item_Entity ie = new Item_Entity(this.loc, item);
                loc.World.SpawnEntity(ie);
            }
            else if (boots != null && item == boots)
            {
                boots = null;
                Item_Entity ie = new Item_Entity(this.loc, item);
                loc.World.SpawnEntity(ie);
            }
        }

        public void Eat(Food food)
        {
            if (inventaire.Contains(food))
            {
                hunger += food.Food_Restored();
                if (hunger > 20)
                {
                    hunger = 20;
                }
                inventaire.RemoveItem(food);
            }
        }

        public override string ToString()
        {
            String inv = inventaire.ToString();
            inv = inv.Replace("Invertory{", id() + " Invertory{");
            return inv;
        }

        protected override Game_Model EntityModel()
        {
            KeyValuePair<double[], double[]>[] model = new KeyValuePair<double[], double[]>[]{
            };
            return new Game_Model(model,Game_Model.GetImage(Helper.ImageFile+@"Player.png"));
        }

        public Inventaire Inventaire { get => inventaire; }
        public float Hunger { get => hunger; set => hunger = value; }
        public Helmet Helmet { get => helmet; set => helmet = value; }
        public ChestPlate ChestPlate { get => chestPlate; set => chestPlate = value; }
        public Legging Legging { get => legging; set => legging = value; }
        public Boots Boots { get => boots; set => boots = value; }
        public float DistOfItemTaking { get => distOfItemTaking; }
    }
}
