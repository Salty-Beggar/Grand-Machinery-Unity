

using System.IO;
using Unity.VisualScripting;
using UnityEngine;

namespace Entities
{
    public class ShooterDroneScript : EntityScript
    {
        protected override void DefineSpecies() => species = EntitySpecies.ShooterDrone;
        public override void ReceiveParamethers(EntityParamethers paramethers) {
            base.ReceiveParamethers(paramethers);
        }

        private readonly int shotCooldownFrames = 100;
        private int shotCooldownFramesCurrent = 100;
        private EnergyInterface energyInterface;
        private HealthInterface healthInterface;

        override protected void Awake() {
            base.Awake();
            energyInterface = Game.InterfaceSubmanager.AssignInterface(InterfaceSpecies.Energy, new EnergyInterfaceParamethers(3, 3), this) as EnergyInterface;
            healthInterface = Game.InterfaceSubmanager.AssignInterface(InterfaceSpecies.Health, new HealthInterfaceParameters(3, 3), this) as HealthInterface;
        }

        override public void Delete_EEvent() {
            Game.InterfaceSubmanager.DeassignInterface(energyInterface);
            Game.InterfaceSubmanager.DeassignInterface(healthInterface);
        }

        void FixedUpdate() {
            if (shotCooldownFramesCurrent > 0) shotCooldownFramesCurrent--;
            if (shotCooldownFramesCurrent <= 0 && energyInterface.value != 0) {
                energyInterface.value--;
                shotCooldownFramesCurrent = shotCooldownFrames;
                Game.EntityAssetSubmanager.InstantiateEntity(EntitySpecies.EnergyCondense, new EnergyCondenseParameters(transform.position));
            }
        }
    }
}