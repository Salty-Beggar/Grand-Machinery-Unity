
using UnityEngine;

namespace Entities
{
    public abstract class EntityClass {
        public EntitySpecies species;
        public static Vector2 ParseVector2(Vector2PB vector2PB) {
            return new(vector2PB.X, vector2PB.Y);
        }
    }
}