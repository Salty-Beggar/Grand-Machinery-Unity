

using System.Collections.Generic;
using System.IO;
using Entities;
using UnityEngine;
using Google.Protobuf;

[System.Serializable]
public class BlueprintSubmanager : Manager {
    public readonly RoomBlueprint roomBlueprint = new();
    public override void Start_Event()
    {
        Game.EntityAssetSubmanager.ExecuteEntityCreators(); // OBSERVATION_BLUEPRINT001: Entity creator deactivation could happen here instead.
        ExecuteRoomBlueprint();
    }

    public override void Update_Event()
    {
        base.Update_Event();
    }

    public void ExecuteRoomBlueprint() {
        Game.EntityAssetSubmanager.ExecuteBlueprints(roomBlueprint.entityBlueprints.ToArray());
    }

    #region DEBUG
    public void __DEBUG_SaveEntityBlueprints() {
        string filePath = Application.persistentDataPath+"/Save.cock";
        FileStream file = File.Create(filePath);

        TryanClass tryanClass = new();
        BlueprintPB blueprintPB = new();
        foreach (var blueprint in roomBlueprint.entityBlueprints) {
            if (blueprint.species == EntitySpecies.Tryan) blueprintPB.EntitiesTryan.Add(tryanClass.SerializeBlueprint(blueprint));
        }

        blueprintPB.WriteTo(file);
        file.Close();
    }

    public List<EntityBlueprint> __DEBUG_LoadEntityBlueprint() {
        string filePath = Application.persistentDataPath+"/Save.cock";
        FileStream file = File.OpenRead(filePath);
        BlueprintPB blueprintPB = BlueprintPB.Parser.ParseFrom(file);

        TryanClass tryanClass = new();
        List<EntityBlueprint> loadedEntityBlueprint = new();
        foreach (var blueprint in blueprintPB.EntitiesTryan) {
            loadedEntityBlueprint.Add(tryanClass.ParseBlueprintPB(blueprint));
        }

        file.Close();
        return loadedEntityBlueprint;
    }
    #endregion

}