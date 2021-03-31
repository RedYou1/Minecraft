using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using BiblioMinecraft;
using BiblioMinecraft.Entities;
using BiblioMinecraft.World_System;

namespace BiblioMinecraft
{
    public class Model3DHandeler
    {
        private Model3DGroup group;
        private List<KeyValuePair<Block, int>> blocks = new List<KeyValuePair<Block, int>>();
        private List<KeyValuePair<Entity, int>> entities = new List<KeyValuePair<Entity, int>>();

        public Model3DHandeler(Model3DGroup group)
        {
            this.group = group;
        }

        /*
        public void UpdateBlock(Block block)
        {
            foreach (KeyValuePair<Block, int> bl in blocks)
            {
                if (bl.Key.Equal(block))
                {
                    group.Children[bl.Value] = Helper.Model(block.Model());
                }
            }
        }

        public void UpdateEntity(Entity entity)
        {
            foreach (KeyValuePair<Entity, int> bl in entities)
            {
                if (bl.Key.Equal(entity))
                {
                    group.Children[bl.Value] = Helper.Model(entity.Model());
                }
            }
        }
        */

        public void AddBlock(Block block)
        {
            blocks.Add(new KeyValuePair<Block, int>(block,group.Children.Count));
            group.Children.Add(Helper.Model(block.Model()));
        }
        public void AddEntity(Entity entity)
        {
            entities.Add(new KeyValuePair<Entity, int>(entity, group.Children.Count));
            group.Children.Add(Helper.Model(entity.Model()));
        }

        public void RemoveBlock(Block block)
        {
            int i = -1;
            for (int j = 0;j<blocks.Count;j++)
            {
                if (blocks[j].Key.Equals(block))
                {
                    i = blocks[j].Value;
                    blocks.RemoveAt(j);
                    break;
                }
            }
            if (i != -1) {
                group.Children.RemoveAt(i);
                for (int a = 0; a < blocks.Count;a++)
                {
                    if (blocks[a].Value > i)
                    {
                        blocks[a] = new KeyValuePair<Block, int>(blocks[a].Key, blocks[a].Value-1);
                    }
                }
                for (int a = 0; a < entities.Count; a++)
                {
                    if (entities[a].Value > i)
                    {
                        entities[a] = new KeyValuePair<Entity, int>(entities[a].Key, entities[a].Value - 1);
                    }
                }
            }
        }

        public void RemoveEntity(Entity entity)
        {
            int i = -1;
            for (int j = 0; j < entities.Count; j++)
            {
                if (entities[j].Key.Equals(entity))
                {
                    i = entities[j].Value;
                    entities.RemoveAt(j);
                    break;
                }
            }
            if (i != -1)
            {
                group.Children.RemoveAt(i);
                for (int a = 0; a < blocks.Count; a++)
                {
                    if (blocks[a].Value > i)
                    {
                        blocks[a] = new KeyValuePair<Block, int>(blocks[a].Key, blocks[a].Value - 1);
                    }
                }
                for (int a = 0; a < entities.Count; a++)
                {
                    if (entities[a].Value > i)
                    {
                        entities[a] = new KeyValuePair<Entity, int>(entities[a].Key, entities[a].Value - 1);
                    }
                }
            }
        }

        public void Clear()
        {
            group.Children.Clear();
            blocks = new List<KeyValuePair<Block, int>>();
            entities = new List<KeyValuePair<Entity, int>>();
        }
    }
}
