

using UnityEngine;

namespace Entities
{
    public class LaserConductorParameters : EntityParamethers
    {
        protected override void DefineSpecies() => species =  EntitySpecies.LaserConductor;

        public LaserConductorParameters(Vector2 initialPosition) : base(initialPosition) {

        }
    }
}