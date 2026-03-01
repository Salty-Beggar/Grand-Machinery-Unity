

using UnityEngine;

namespace Entities
{
    public class NodeGeneratorScript : EntityScript
    {
        protected override void DefineSpecies() => species = EntitySpecies.NodeGenerator;
        public override void ReceiveEntityLinkParameters(EntityParamethers parameters)
        {
            target = (parameters as NodeGeneratorParameters).target;
        }

        private EnergyInterface energyInterface;
        public EntityScript target;

        private readonly int __DEBUG_delay = 240;
        private int __DEBUG_delayCur = 50;

        protected override void Start() {
            base.Start();
            energyInterface = new EnergyInterface(new(0, 10));
            Game.InterfaceSubmanager.AssignInterface(energyInterface, this);
        }

        void Update() {
            __DEBUG_delayCur--;
            if (__DEBUG_delayCur <= 0) {
                energyInterface.value = 1;
                __DEBUG_delayCur = __DEBUG_delay;
            }
            if (energyInterface.value != 0) {
                EnergyInterface @interface = Game.InterfaceSubmanager.GetInterface(target, InterfaceSpecies.Energy) as EnergyInterface;
                @interface.value++;
                energyInterface.value = 0;
            }
        }
    }
}