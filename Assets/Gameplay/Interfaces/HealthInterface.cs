

public class HealthInterface : Interface
{
    protected override void DefineSpecies() => species = InterfaceSpecies.Health;

    protected override void ReceiveParamethers(InterfaceParamethers parameters)
    {
        value = ((HealthInterfaceParameters) parameters).value;
    }

    public int value;
    public int valueMax;

    public HealthInterface(HealthInterfaceParameters parameters) : base(parameters) {}
}