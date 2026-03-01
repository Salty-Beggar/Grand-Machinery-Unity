

using Entities;
using UnityEngine;

public class EnergyCondenseScript : EntityScript
{
    protected override void DefineSpecies() => species = EntitySpecies.EnergyCondense;

    void Update() {

        collisionMask.position += Vector2.right;

        foreach (var entity in Game.InterfaceSubmanager.GetEntities(InterfaceSpecies.Energy)) {
            if (entity.species != EntitySpecies.ShooterDrone && collisionMask.IsPlaceMeeting(Vector2.zero, entity.collisionMask)) {
                var @interface = Game.InterfaceSubmanager.GetInterface(entity, InterfaceSpecies.Energy) as EnergyInterface;
                @interface.value++;
                Game.EntityAssetSubmanager.DeleteEntity(this);
            }
        }

        GetComponent<Transform>().position = collisionMask.position;
    }
}