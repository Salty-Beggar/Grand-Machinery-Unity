
using UnityEngine;

namespace Entities
{
    [System.Serializable]
    public class TryanParamethers : EntityParamethers
    {
        protected override void DefineSpecies() => species = EntitySpecies.Tryan;

        public TryanParamethers(Vector2 initialPosition) : base(initialPosition) {
            
        }
    }
}