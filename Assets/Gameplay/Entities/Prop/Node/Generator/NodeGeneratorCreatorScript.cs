


namespace Entities
{
    public class NodeGeneratorCreatorScript : EntityCreatorScript
    {
        public EntityCreatorScript targets;
        protected override void DefineSpecies() => species = EntitySpecies.NodeGenerator;

        protected override void DefineParameters()
        {
            paramethers = new NodeGeneratorParameters(transform.position, null);
        }
        public override void DefineEntityLinks()
        {
            NodeGeneratorParameters paramethers2 = paramethers as NodeGeneratorParameters;
            paramethers2.target.SetIndex(targets.blueprintID, Game.BlueprintSubmanager.roomBlueprint.entityBlueprints);
        }
    }
}