

using Unity.VisualScripting;
using UnityEngine;

namespace Entities
{
    [Icon("Assets/Gameplay/CreatorEntityScriptLogo.png")]
    public abstract class EntityCreatorScript : MonoBehaviour {
        [HideInInspector] public EntitySpecies species;
        [SerializeField] private Sprite displaySpr;
        protected EntityParamethers paramethers;
        public EntityScript entity;

        [HideInInspector] public int blueprintID;
        protected abstract void DefineSpecies();

        protected abstract void DefineParameters();
        public void InstantiateBlueprint() {
            blueprintID = Game.BlueprintSubmanager.roomBlueprint.entityBlueprints.Count;
            Game.BlueprintSubmanager.roomBlueprint.entityBlueprints.Add(new(species, paramethers));
        }

        public virtual void DefineEntityLinks() {}

        void Awake() {
            DefineSpecies();
            if (!Game.GameSupermanager.parentObject.GetComponent<BlueprintScript>().executeCreators) return;

            DefineParameters();
            Game.EntityAssetSubmanager.NotifyEntityCreator(this);
        }

        void OnDrawGizmos() {
            Game.AestheticsSubmanager.__DEBUG_DrawSprite(transform.position, displaySpr);
        }
    }
}