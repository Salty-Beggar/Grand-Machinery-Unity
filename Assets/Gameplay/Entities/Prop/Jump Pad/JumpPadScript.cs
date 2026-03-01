


namespace Entities
{
    public class JumpPadScript : EntityScript {
        public override void ReceiveParamethers(EntityParamethers paramethers)
        {
            base.ReceiveParamethers(paramethers);
        }

        protected override void DefineSpecies() => species = EntitySpecies.JumpPad;
    }
}