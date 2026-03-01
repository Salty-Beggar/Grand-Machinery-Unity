

public class EnergyInterface : Interface
{
    protected override void DefineSpecies() => species = InterfaceSpecies.Energy;

    protected override void ReceiveParamethers(InterfaceParamethers paramethers)
    {
        value = ((EnergyInterfaceParamethers) paramethers).value;
    }

    public int value;
    public int valueMax;

    public EnergyInterface(EnergyInterfaceParamethers paramethers) : base(paramethers) {}
}