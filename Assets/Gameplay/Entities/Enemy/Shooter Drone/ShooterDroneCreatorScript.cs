

using Unity.VisualScripting;

namespace Entities
{
    public class ShooterDroneCreatorScript : EntityCreatorScript
    {
        protected override void DefineSpecies() => species = EntitySpecies.ShooterDrone;
        protected override void DefineParameters()
        {
            paramethers = new ShooterDroneParameters(transform.position);
        }
    }
}