

using UnityEngine;

namespace Entities
{
    public class ShooterDroneParameters : EntityParamethers
    {
        protected override void DefineSpecies() => species = EntitySpecies.ShooterDrone;

        public ShooterDroneParameters(Vector2 initialPosition) : base(initialPosition) {
        
        }
    }
}