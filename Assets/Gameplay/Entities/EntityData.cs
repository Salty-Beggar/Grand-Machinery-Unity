

using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    public class EntityData {
        public EntitySpecies species;
        
        public LinkedList<EntityScript> entities = new();
        public GameObject prefab;
    }
}