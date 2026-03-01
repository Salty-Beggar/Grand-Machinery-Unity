using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;
using Entities;
using System.IO;

public class CollisionMaskScr : MonoBehaviour
{
    public Vector2 CollisionMaskStart;
    public Vector2 CollisionMaskEnd;
    public EntityScript collisionMaskEntity;
    public CollisionMask CollisionMask;
    public bool followsObject = true;
    void Awake() {
        if (followsObject) CollisionMask = new RectCollisionMask(CollisionMaskStart, CollisionMaskEnd, collisionMaskEntity);
        else CollisionMask = new RectCollisionMask(CollisionMaskStart, CollisionMaskEnd, transform.position);
    }
    void Start() {
        RectCollisionMask CurCollisionMask = (RectCollisionMask) CollisionMask;
        CurCollisionMask.position = GetComponent<Transform>().position;
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Vector2 Position = (Vector2)GetComponent<Transform>().position;
        Gizmos.DrawWireCube(Position+(CollisionMaskEnd-CollisionMaskStart)/2+CollisionMaskStart, CollisionMaskEnd-CollisionMaskStart);
    }
}

[CustomEditor(typeof(CollisionMaskScr))]
public class CollisionMaskEditor : Editor {
    public void OnSceneGUI() {
        //Handles.BeginGUI();
        CollisionMaskScr example = (CollisionMaskScr) target;

        Vector2 newCollisionMaskStart = Handles.FreeMoveHandle(example.CollisionMaskStart, 3, new Vector2(10f, 10f), Handles.RectangleHandleCap);
        example.CollisionMaskStart = new((int)newCollisionMaskStart.x, (int) newCollisionMaskStart.y);
        
        Vector2 newCollisionMaskEnd = Handles.FreeMoveHandle(example.CollisionMaskEnd, 3, new Vector2(10f, 10f), Handles.RectangleHandleCap);
        example.CollisionMaskEnd = new((int)newCollisionMaskEnd.x, (int) newCollisionMaskEnd.y);
        
    }
}