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

        public Player(float x, float y, float z) : this(x, y, z, 0, 0) { }

        public Player(float x, float y, float z, float pitch, float yaw) : base(x, y, z, pitch, yaw, 20)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.pitch = pitch;
            this.yaw = yaw;
            hp = 20;
        }

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

        public bool Equipe(Armor armor)
        {
            if (inventaire.Contains(armor))
            {
                if (armor.Attribute_id() == "Helmet")
                {
                    if (helmet != null) { inventaire.AddItem(helmet); }
                    helmet = (Helmet)armor;
                }
                else if (armor.Attribute_id() == "ChestPlate")
                {
                    if (chestPlate != null) { inventaire.AddItem(chestPlate); }
                    chestPlate = (ChestPlate)armor;
                }
                else if (armor.Attribute_id() == "Legging")
                {
                    if (legging != null) { inventaire.AddItem(legging); }
                    legging = (Legging)armor;
                }
                else if (armor.Attribute_id() == "Boots")
                {
                    if (boots != null) { inventaire.AddItem(boots); }
                    boots = (Boots)armor;
                }
                inventaire.RemoveItem(armor);
                return true;
            }
            return false;
        }

        public void TakeItemAroundHim()
        {
            List<Item_Entity> ie = new List<Item_Entity>();
            foreach (Entity entity in World_System.World.Entities)
            {
                if (entity is Item_Entity item)
                {
                    if (Other.Dist(this.x, item.X, this.y, item.Y, this.z, item.Z) <= distOfItemTaking)
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
                World_System.World.RemoveEntity(item);
            }
        }

        public void DropItem(Item item)
        {
            if (inventaire.Contains(item))
            {
                inventaire.RemoveItem(item);
                Item_Entity ie = new Item_Entity(this.x, this.y, this.z, this.pitch, this.yaw, item);
                World.SpawnEntity(ie);
            }
            else if (helmet != null && item == helmet)
            {
                helmet = null;
                Item_Entity ie = new Item_Entity(this.x, this.y, this.z, this.pitch, this.yaw, item);
                World.SpawnEntity(ie);
            }
            else if (chestPlate != null && item == chestPlate)
            {
                chestPlate = null;
                Item_Entity ie = new Item_Entity(this.x, this.y, this.z, this.pitch, this.yaw, item);
                World.SpawnEntity(ie);
            }
            else if (legging != null && item == legging)
            {
                legging = null;
                Item_Entity ie = new Item_Entity(this.x, this.y, this.z, this.pitch, this.yaw, item);
                World.SpawnEntity(ie);
            }
            else if (boots != null && item == boots)
            {
                boots = null;
                Item_Entity ie = new Item_Entity(this.x, this.y, this.z, this.pitch, this.yaw, item);
                World.SpawnEntity(ie);
            }
        }
        public void RemoveItem(Item item)
        {
            inventaire.RemoveItem(item);
        }
        public void AddItem(Item item)
        {
            inventaire.AddItem(item);
        }

        public void Eat(Food food)
        {
            hunger += food.Food_Restored();
        }

        public override string ToString()
        {
            String inv = inventaire.ToString();
            inv = inv.Replace("Invertory{", id() + " Invertory{");
            return inv;
        }

        public Inventaire Inventaire { get => inventaire.Clone(); }
        public float Hunger { get => hunger; }
        public Helmet Helmet { get => helmet; }
        public ChestPlate ChestPlate { get => chestPlate; }
        public Legging Legging { get => legging; }
        public Boots Boots { get => boots; }
        public float DistOfItemTaking { get => distOfItemTaking; }
    }
}
