

using UnityEngine;

namespace Entities
{
    public class LaserConductorScript : EntityScript
    {
        protected override void DefineSpecies() => species = EntitySpecies.LaserConductor;
        private EnergyInterface energyInterface;
        private Hitscan hitscan;
        private bool hasHitscan = false;

        protected override void Start() {
            base.Start();
            energyInterface = new EnergyInterface(new(0, 10));
            Game.InterfaceSubmanager.AssignInterface(energyInterface, this);
        }

        void Update() {
            if (energyInterface.value != 0) {
                hitscan = Hitscan.Start(GetComponent<Transform>().position, Vector2.up);
                energyInterface.value = 0;
                hasHitscan = true;
            }
            if (hasHitscan) {
                hitscan.Update();
                if (hitscan.hasEnded) hasHitscan = false;
            }
        }
    }
}