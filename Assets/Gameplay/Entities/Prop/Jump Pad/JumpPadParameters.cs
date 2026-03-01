

using UnityEngine;

namespace Entities
{
    [System.Serializable]
    public class JumpPadParameters : EntityParamethers
    {
        protected override void DefineSpecies() => species = EntitySpecies.JumpPad;

        public JumpPadParameters(Vector2 initialPosition) : base(initialPosition) {
        
        }

    }
}