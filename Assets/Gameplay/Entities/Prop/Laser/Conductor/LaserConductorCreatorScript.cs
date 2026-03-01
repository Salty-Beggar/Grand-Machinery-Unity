


namespace Entities
{
    public class LaserConductorCreatorScript : EntityCreatorScript
    {
        protected override void DefineParameters() {
            paramethers = new LaserConductorParameters(transform.position);
        }

        protected override void DefineSpecies() => species = EntitySpecies.LaserConductor;
    }
}