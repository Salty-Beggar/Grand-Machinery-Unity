

namespace Entities {
    public interface IEntitySerializer<T> {
        public abstract T SerializeBlueprint(EntityBlueprint blueprint);
        public abstract EntityBlueprint ParseBlueprintPB(T blueprintPB);
    }
}