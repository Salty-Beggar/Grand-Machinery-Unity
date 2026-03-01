
namespace Entities {
    public class TryanClass : IEntitySerializer<TryanPB>
    {

        public TryanPB SerializeBlueprint(EntityBlueprint blueprint)
        {
            TryanParamethers parameters = blueprint.paramethers as TryanParamethers;
            return new TryanPB() {
                BaseEntityParameters = new() {
                    InitialPosition = new() {
                        X = blueprint.entity.transform.position.x,
                        Y = blueprint.entity.transform.position.y
                    }
                }
            };
        }
        public EntityBlueprint ParseBlueprintPB(TryanPB blueprintPB)
        {
            return new EntityBlueprint(EntitySpecies.Tryan, new TryanParamethers(
                EntityClass.ParseVector2(blueprintPB.BaseEntityParameters.InitialPosition)
            ));
        }
    }
}