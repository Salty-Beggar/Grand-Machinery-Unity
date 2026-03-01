

using UnityEngine;

namespace Entities
{
    public class NodeGeneratorParameters : EntityParamethers
    {
        protected override void DefineSpecies() => species = EntitySpecies.NodeGenerator;

        public EntityLink target;

        public NodeGeneratorParameters(Vector2 initialPosition, EntityLink target) : base(initialPosition) {
            this.target = target;
        }

        public override void ConvertEntityLinkParameters()
        {
            target.ConvertToEntity();
        }
    }
}