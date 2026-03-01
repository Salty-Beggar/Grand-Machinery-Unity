

public class HealthInterfaceParameters : InterfaceParamethers
{
    public override void DefineSpecies() => species = InterfaceSpecies.Health;
    
    public int value;
    public int valueMax;

    public HealthInterfaceParameters(int value, int valueMax) {
        this.value = value;
        this.valueMax = valueMax;
    }
}