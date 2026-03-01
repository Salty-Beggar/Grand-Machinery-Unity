using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlicerDroneScript : MonoBehaviour
{
    public float HSpd = 0;
    public float VSpd = 0;
    public float Speed = 2;
    public float Gravity = 0.6f;
    public float JumpForce = 8;
    public RectCollisionMask CollisionMask; 
    
    void Start()
    {
        CollisionMask = (RectCollisionMask) GetComponent<CollisionMaskScr>().CollisionMask;
    }
    void Update()
    {
        Transform Transform = gameObject.transform;

        VSpd -= Gravity;

        CollisionMask.position = Transform.position;

        foreach (GameObject CurrentCollision in GameObject.FindGameObjectsWithTag("Collision")) {
            CollisionMask CurCollisionMask = CurrentCollision.GetComponent<CollisionMaskScr>().CollisionMask;
            if (CollisionMask.IsPlaceMeeting(Vector2.up*VSpd, CurCollisionMask)) {
                Transform.position = new Vector3(Transform.position.x, MathF.Ceiling(Transform.position.y), 0); // OBSERVATION004 - Rounding up or down should depend on speed's sign.
                while (!CollisionMask.IsPlaceMeeting(Vector2.up*MathF.Sign(VSpd), CurCollisionMask)) {
                    Transform.position += Vector3.up * MathF.Sign(VSpd);
                    CollisionMask.position = Transform.position;
                }
                VSpd = 0;
            }
            
            if (CollisionMask.IsPlaceMeeting(Vector2.right*HSpd, CurCollisionMask)) {
                Transform.position = new Vector3(MathF.Ceiling(Transform.position.x), Transform.position.y, 0); // OBSERVATION004 - Rounding up or down should depend on speed's sign.
                while (!CollisionMask.IsPlaceMeeting(Vector2.right*MathF.Sign(HSpd), CurCollisionMask)) {
                    Transform.position += Vector3.right * MathF.Sign(HSpd);
                    CollisionMask.position = Transform.position;
                }
                HSpd = 0;
            }
        }

        /*if (Transform.position.y+VSpd < 0) {
            Transform.position = Vector3.Scale(Transform.position, new Vector3(1, MathF.Ceiling(Transform.position.y), 1));
            VSpd = 0;
        }*/

        Transform.position += Vector3.right*HSpd+Vector3.up*VSpd;
    }
}
