

using UnityEngine;
using Entities;

public abstract class Interface {
    public InterfaceSpecies species;
    public EntityScript entity;

    protected abstract void DefineSpecies();
    protected abstract void ReceiveParamethers(InterfaceParamethers paramethers);

    protected Interface(InterfaceParamethers paramethers) {
        DefineSpecies();
        ReceiveParamethers(paramethers);
    }
}