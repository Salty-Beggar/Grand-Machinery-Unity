

using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Scripting;

namespace Entities
{
    [System.Serializable]
    public abstract class EntityParamethers {
        public EntitySpecies species;
        protected abstract void DefineSpecies();
        public Vector2 initialPosition;

        public EntityParamethers() {
            DefineSpecies();
        }
        public EntityParamethers(Vector2 initialPosition) {
            DefineSpecies();
            this.initialPosition = initialPosition;
        }
        public virtual void ConvertEntityLinkParameters() {}
    }
}