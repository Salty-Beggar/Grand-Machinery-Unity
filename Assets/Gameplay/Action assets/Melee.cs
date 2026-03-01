

using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Entities;

public abstract class Melee : ActionAsset { // TODO - Replace LinkedList with a HashSet when the code becomes adapted to the instance collision grid.
    public CollisionMask collisionMask;
    public EntityScript starterEntity;
    public LinkedList<EntityScript> targets;

    public bool isDeleted;

    protected Melee(CollisionMask collisionMask, EntityScript starterEntity, LinkedList<EntityScript> targets) {
        this.collisionMask = collisionMask;
        this.starterEntity = starterEntity;
        this.targets = targets;
    }

    public void Update() {
        foreach (EntityScript entity in targets) {
            if (collisionMask.IsPlaceMeeting(Vector2.zero, entity.collisionMask)) {
                NotifyHit(entity);
            }
        }
    }

    protected abstract void NotifyHit(EntityScript entity);
}