

using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    [Icon("Assets/Gameplay/EntityScriptLogo.png")]
    public abstract class EntityScript : MonoBehaviour {
        [HideInInspector] public EntitySpecies species;
        public CollisionMask collisionMask;
        public LinkedListNode<EntityScript> node; // OBSERVATION009: Put a better name for these variables.
        public LinkedListNode<EntityScript> nodeEntities; // OBSERVATION009: Put a better name for these variables.

        // OBSERVATION_ENTITY001: Organize the functions better.
        protected abstract void DefineSpecies();
        public virtual void ReceiveParamethers(EntityParamethers paramethers) {
            transform.position = paramethers.initialPosition;
        }
        public virtual void ReceiveEntityLinkParameters(EntityParamethers parameters) {}
        public virtual void Delete_EEvent() {}
        public virtual void Create_EEvent() {}
        
        protected virtual void Awake() {
            DefineSpecies();
        }

        protected virtual void Start() { // OBSERVATION012 - Make it so you can choose which collision mask to take from the script, since multiple collision mask support will be added.
            collisionMask = GetComponent<CollisionMaskScr>().CollisionMask;
        }
    }
}