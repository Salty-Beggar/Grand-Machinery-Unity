

namespace Entities
{
    public class JumpPadCreatorScript : EntityCreatorScript
    {
        protected override void DefineParameters() => paramethers = new JumpPadParameters(transform.position);
        protected override void DefineSpecies() => species = EntitySpecies.JumpPad;
    }
}