

using System.Collections.Generic;
using UnityEngine;

public class InterfaceData {
    public InterfaceSpecies species;
    public int amount;
    public Dictionary<Entities.EntityScript, Interface> interfaces = new();

    public InterfaceData(InterfaceSpecies species) {
        this.species = species;
    }
}