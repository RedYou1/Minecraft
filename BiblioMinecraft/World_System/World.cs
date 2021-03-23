using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiblioMinecraft.Entities;

namespace BiblioMinecraft.World_System
{
    public static class World
    {
        private static List<Chunk> chunks = new List<Chunk>();
        private static List<Entity> entities = new List<Entity>();

        public static void SpawnEntity(Entity entity)
        {
            entities.Add(entity);
        }

        public static void RemoveEntity(Entity entity)
        {
            entities.Remove(entity);
        }

        public static Chunk[] Chunks { get => chunks.ToArray(); }
        public static Entity[] Entities { get => entities.ToArray(); }
    }
}
