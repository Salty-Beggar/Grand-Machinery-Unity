

using UnityEngine;
using UnityEngine.UI;

namespace Entities
{
    public class TryanCreatorScript : EntityCreatorScript
    {
        protected override void DefineSpecies() => species = EntitySpecies.Tryan;
        protected override void DefineParameters()
        {
            paramethers = new TryanParamethers(transform.position);
        }
    }
}