

public class EnergyInterfaceParamethers : InterfaceParamethers
{
    public override void DefineSpecies() => species = InterfaceSpecies.Energy;

    public int value;
    public int valueMax;

    public EnergyInterfaceParamethers(int value, int valueMax) {
        this.value = value;
        this.valueMax = valueMax;
    }
}