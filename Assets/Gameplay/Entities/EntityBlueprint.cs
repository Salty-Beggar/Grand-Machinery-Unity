

using System;
using UnityEngine;

namespace Entities {

    [Serializable]
    public class EntityBlueprint {
        public EntitySpecies species;
        public EntityParamethers paramethers;
        public EntityScript entity;

        public EntityBlueprint(EntitySpecies species, EntityParamethers paramethers) {
            this.species = species;
            this.paramethers = paramethers;
        }

        public void InstantiateEntity() {
            entity = Game.EntityAssetSubmanager.InstantiateEntity(species, paramethers);
        }
        public void ConvertEntityLinks() {
            paramethers.ConvertEntityLinkParameters();
            entity.ReceiveEntityLinkParameters(paramethers);
        }
    }
}