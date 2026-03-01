

using UnityEngine;

namespace Entities {
    public class EnergyCondenseParameters : EntityParamethers
    {
        protected override void DefineSpecies() => species = EntitySpecies.EnergyCondense;

        public EnergyCondenseParameters(Vector2 initialPosition) : base(initialPosition) {

        }
    }
}