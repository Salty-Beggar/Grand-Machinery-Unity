

using System;
using UnityEngine;
using Entities;

public enum CollisionMaskType {
    Rect, Circle, Grid
}

public abstract class CollisionMask {
    public CollisionMaskType Type;
    public Vector2 position;
    public bool followsEntity = false;
    public EntityScript entity;
    public Vector2 flip = Vector2.one;
    public int rotation = 0;

    protected CollisionMask(EntityScript entity) {
        this.followsEntity = true;
        this.entity = entity;
    }

    protected CollisionMask(Vector2 position) {
        this.position = position;
    }

    public bool IsPlaceMeeting(Vector2 offset, CollisionMask CollisionMask, bool isInclusive) {
        switch (CollisionMask.Type) {
            case CollisionMaskType.Rect: return IsPlaceMeeting(offset, (RectCollisionMask) CollisionMask, isInclusive);
            default: return false;
        }
    }
    public bool IsPlaceMeeting(Vector2 offset, CollisionMask CollisionMask) {
        switch (CollisionMask.Type) {
            case CollisionMaskType.Rect: return IsPlaceMeeting(offset, (RectCollisionMask) CollisionMask, false);
            default: return false;
        }
    }
    public abstract bool IsPlaceMeeting(Vector2 offset, RectCollisionMask CollisionMask, bool isInclusive);
}