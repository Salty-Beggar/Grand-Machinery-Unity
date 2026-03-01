

using Entities;
using UnityEditor.Experimental.Licensing;
using UnityEngine;

public class Hitscan : ActionAsset { // OBSERVATION008: Step additions and sizes have hardcoded values, so replace the hardcoding with variables for those.
    private Vector2 direction;
    private RectCollisionMask collisionMask;
    public bool hasEnded = false;
    public static Hitscan Start(Vector2 initialPos, Vector2 initialDir) {
        Hitscan newHitscan = new(initialPos, initialDir);
        return newHitscan;
    }

    public void Update() {
        int stepSize = 160;
        int stepAdd = 16;
        Vector2 initialPos = collisionMask.position;
        for (int i = 0; i < stepSize; i += stepAdd) {
            foreach (EntityScript entity in Game.EntityAssetSubmanager.GetEntityList(EntitySpecies.ShooterDrone)) {
                if (collisionMask.IsPlaceMeeting(Vector2.zero, entity.collisionMask, true)) NotifyEntityCollision(entity);
            }
            if (hasEnded) {
                NotifyEnd(initialPos, collisionMask.position);
                break;
            }
            collisionMask.position += stepAdd*direction;
            initialPos += stepAdd*direction;
        }
        NotifyStepEnd(initialPos, collisionMask.position);
    }

    private void NotifyStepEnd(Vector2 initialPos, Vector2 currentPos) {

    }

    private void NotifyEntityCollision(EntityScript entity) {
        if (Game.InterfaceSubmanager.EntityHasInterface(entity, InterfaceSpecies.Energy)) {
            EnergyInterface curInterface = Game.InterfaceSubmanager.GetInterface(entity, InterfaceSpecies.Energy) as EnergyInterface;
            curInterface.value += 1;
            hasEnded = true;
        }
    }

    private void NotifyEnd(Vector2 initialPos, Vector2 finalPos) {

    }

    private Hitscan(Vector2 initialPos, Vector2 initialDir) {
        this.direction = initialDir;

        Vector2 colMaskStart = Vector2.zero;
        Vector2 colMaskEnd = Vector2.zero;

        if (initialDir == Vector2.left) colMaskStart = Vector2.left*16;
        else if (initialDir == Vector2.down) colMaskStart = Vector2.down*16;
        else if (initialDir == Vector2.right) colMaskEnd = Vector2.right*16;
        else if (initialDir == Vector2.up) colMaskEnd = Vector2.up*16;

        this.collisionMask = new(colMaskStart, colMaskEnd, initialPos);
    }
}