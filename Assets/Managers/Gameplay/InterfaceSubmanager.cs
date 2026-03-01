

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Entities;

public enum InterfaceSpecies {
    Health,
    Energy,
    Physics,
    StatsHUD,
    Target
}

public class InterfaceSubmanager : Manager {
    private InterfaceSpecies[] species = new InterfaceSpecies[]{
        InterfaceSpecies.Health,
        InterfaceSpecies.Energy,
        InterfaceSpecies.Physics,
        InterfaceSpecies.StatsHUD,
        InterfaceSpecies.Target
    };
    private Dictionary<InterfaceSpecies, InterfaceData> interfaceData = new();

    public InterfaceSubmanager() {
        foreach (var species in species) interfaceData.Add(species, new InterfaceData(species));
    }

    public void AssignInterface(Interface @interface, EntityScript entity) { // OBSERVATION_INTERFACE001 - Remove this overload.
        interfaceData[@interface.species].interfaces.Add(entity, @interface);
        @interface.entity = entity;
    }

    public Interface AssignInterface(InterfaceSpecies species, InterfaceParamethers interfaceParamethers, EntityScript entity) {
        Interface newInterface = null;
        switch (species) {
            case InterfaceSpecies.Health: newInterface = new HealthInterface((HealthInterfaceParameters)interfaceParamethers); break;
            case InterfaceSpecies.Energy: newInterface = new EnergyInterface((EnergyInterfaceParamethers)interfaceParamethers); break;
            case InterfaceSpecies.Physics: newInterface = new PhysicsInterface((PhysicsInterfaceParamethers)interfaceParamethers); break;
        }
        interfaceData[species].interfaces.Add(entity, newInterface);
        newInterface.entity = entity;
        return newInterface;
    }

    public void DeassignInterface(Interface @interface) {
        interfaceData[@interface.species].interfaces.Remove(@interface.entity);
    }

    public EntityScript[] GetEntities(InterfaceSpecies species) {
        return interfaceData[species].interfaces.Keys.ToArray();
    }

    public bool EntityHasInterface(EntityScript entity, InterfaceSpecies species) {
        return interfaceData[species].interfaces.ContainsKey(entity);
    }

    public Interface GetInterface(EntityScript entity, InterfaceSpecies species) {
        return interfaceData[species].interfaces[entity];
    }

    public Dictionary<EntityScript, Interface>.ValueCollection GetInterfaces(InterfaceSpecies species) {
        return interfaceData[species].interfaces.Values;
    }

    public override void LateUpdate_Event()
    {
        base.LateUpdate_Event();
        PhysicsInterface.UpdateAll();
    }
}