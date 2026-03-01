

using System.Collections.Generic;

namespace Entities {
    public struct EntityLink {
        public int entityBlueprintIndex;
        public EntityScript entity;
        public List<EntityBlueprint> blueprints;

        public static EntityLink Construct(int index, List<EntityBlueprint> blueprints) {
            EntityLink newLink = new();
            newLink.SetIndex(index, blueprints);
            return newLink;
        }

        public void SetIndex(int index, List<EntityBlueprint> blueprints) {
            entityBlueprintIndex = index;
            this.blueprints = blueprints;
        }
        public void ConvertToEntity() {
            entity = blueprints[entityBlueprintIndex].entity;
        }
        public static implicit operator EntityLink(EntityScript entity) => new() { entity = entity };
        public static implicit operator EntityScript(EntityLink entityLink) => entityLink.entity;
        public static implicit operator int(EntityLink entityLink) => entityLink.entityBlueprintIndex;
    }
}