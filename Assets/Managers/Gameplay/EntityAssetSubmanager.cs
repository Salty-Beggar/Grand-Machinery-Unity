
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

using Entities;

public enum EntitySpecies {
    Tryan,
    JumpPad,
    LaserConductor,
    NodeGenerator,
    SlicerDrone,
    ShooterDrone,
    EnergyCondense
}

public class EntityAssetSubmanager : Manager {
    private EntitySpecies[] species = new EntitySpecies[]{
        EntitySpecies.Tryan,
        EntitySpecies.JumpPad,
        EntitySpecies.SlicerDrone,
        EntitySpecies.ShooterDrone,
        EntitySpecies.EnergyCondense,
        EntitySpecies.LaserConductor,
        EntitySpecies.NodeGenerator
    };
    private readonly Dictionary<EntitySpecies, EntityData> entityData = new();
    private readonly LinkedList<EntityScript> entities = new();

    public EntityAssetSubmanager() {
        foreach (EntitySpecies species in species) entityData.Add(species, new EntityData());
    }

    #region Events

    public override void Start_Event()
    {
        base.Start_Event();
    }

    #endregion

    public LinkedList<EntityScript> GetEntityList(EntitySpecies species) {
        return entityData[species].entities;
    }

    public LinkedList<EntityScript> GetEntityList() { // OBSERVATION_ENTITY002: This is the entity retrieval function in which the list of entities retrived disregard their species, thus being one of the (if not the only) function(s) which forces all entities to be in the same DS. If it isn't ever used, then that's progress in making entities inherit typed generic objects.
        return entities;
    }

    public void RecordEntity(EntityScript entityAsset) { // Records an already existing entity asset into the entityData dictionary.
        EntityData curData = entityData[entityAsset.species];
        entityAsset.node = curData.entities.AddLast(entityAsset);
        entityAsset.nodeEntities = entities.AddLast(entityAsset);
    }

    public void DeleteEntity(EntityScript entity) {
        entity.Delete_EEvent();
        entityData[entity.species].entities.Remove(entity.node);
        entities.Remove(entity.nodeEntities);
        Object.Destroy(entity.gameObject);
    }

    public void DeleteAllEntities(EntitySpecies species) {
        while (entityData[species].entities.Count != 0) {
            DeleteEntity(entityData[species].entities.First.Value);
        }
    }

    public void DeleteAllEntities() {
        while (entities.Count != 0) {
            DeleteEntity(entities.First.Value);
        }
    }

    public EntityScript InstantiateEntity(EntitySpecies species, EntityParamethers paramethers) {
        EntityScript entity = Object.Instantiate(entityData[species].prefab).GetComponent<EntityScript>();
        Game.EntityAssetSubmanager.RecordEntity(entity);
        entity.ReceiveParamethers(paramethers);
        entity.Create_EEvent();
        return entity;
    }

    public void SetEntitiesPrefabs(GameObject[] gameObjects) { // The order it defines game objects is by the enum item's order, not by the species array of the manager!
        for (int i = 0; i < gameObjects.Length; i++) {
            entityData[(EntitySpecies)i].prefab = gameObjects[i];
        }
    }

    #region Creator scripts

    private readonly LinkedList<EntityCreatorScript> entityCreators = new();
    public void NotifyEntityCreator(EntityCreatorScript entityCreator) {
        entityCreators.AddLast(entityCreator);
    }

    public void ExecuteEntityCreators() {
        foreach (var entityCreator in entityCreators) {
            entityCreator.InstantiateBlueprint();
        }
        foreach (var entityCreator in entityCreators) {
            entityCreator.DefineEntityLinks();
        }
        foreach (var entityCreator in entityCreators) {
            Object.Destroy(entityCreator.gameObject);
        }
        entityCreators.Clear();
    }

    #endregion

    #region Blueprints

    public void ExecuteBlueprints(EntityBlueprint[] entityBlueprints) {
        foreach (var entityBlueprint in entityBlueprints) {
            entityBlueprint.InstantiateEntity();
        }
        foreach (var entityBlueprint in entityBlueprints) {
            entityBlueprint.ConvertEntityLinks();
        }
    }

    #endregion
}