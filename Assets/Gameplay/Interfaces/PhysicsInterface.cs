

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class PhysicsInterface : Interface
{
    protected override void DefineSpecies() => species = InterfaceSpecies.Physics;

    protected override void ReceiveParamethers(InterfaceParamethers paramethers)
    {
        
    }

    public PhysicsInterface(PhysicsInterfaceParamethers paramethers) : base(paramethers) {}


    private Vector2 _spd;
    public Vector2 spd {
        get => _spd;
        private set => _spd = value;
    }

    #region Setting
        public void SetPosition(Vector2 position) { // OBSERVATION013 - Should this function be here?
            entity.transform.position = position;
        }
        public void SetSpeed(Vector2 spd) {
            this.spd = spd;
        }
        public void SetHSpeed(float hSpd) {
            spd = new(hSpd, spd.y);
        }
        public void SetVSpeed(float vSpd) {
            spd = new(spd.x, vSpd);
        }

    #endregion

    #region Addition
        public void AddPosition(Vector2 positionAdd) {
            SetPosition(entity.transform.position + (Vector3)positionAdd);
        }
        public void AddSpeed(Vector2 spdAdd) {
            SetSpeed(spd+spdAdd);
        }
        public void AddHSpeed(float spdAdd) {
            SetSpeed(new(spd.x+spdAdd, spd.y));
        }
    #endregion

    #region Targetting

        private struct SpeedTarget {
            public float target;
            public float addition;

            public SpeedTarget(float target, float addition) : this() {
                this.target = target;
                this.addition = addition;
            }
        }
        private Queue<SpeedTarget> targetHSpeedRequests = new();
        private bool hasHSpeedTargets = false;
        private float minimumHSpeedTarget = 0f;
        private float maximumHSpeedTarget = 0f;
        public void TargetHSpeed(float target, float addition) {
            targetHSpeedRequests.Enqueue(new SpeedTarget(target, addition));
            if (!hasHSpeedTargets) {
                hasHSpeedTargets = true;
                minimumHSpeedTarget = target;
                maximumHSpeedTarget = target;
            }else {
                if (target < minimumHSpeedTarget) minimumHSpeedTarget = target;
                else if (target > maximumHSpeedTarget) maximumHSpeedTarget = target;
            }
        }

        private bool hasMaxHSpeedTargets = false;
        private bool hasMinimumMaxHSpeed = false;
        private float minimumMaxHSpeedTarget;
        private bool hasMaximumMaxHSpeed = false;
        private float maximumMaxHSpeedTarget;
        private float maxHSpeedMinAddition = 0f;
        private float maxHSpeedMaxAddition = 0f;

        public void TargetMaxHSpeed(float target, float addition) {
            if (target == 0) return;
            hasMaxHSpeedTargets = true;
            if (target < 0) {
                maxHSpeedMinAddition += addition;
                if (!hasMinimumMaxHSpeed) {
                    minimumMaxHSpeedTarget = target;
                    hasMinimumMaxHSpeed = true;
                }else if (target < minimumMaxHSpeedTarget) minimumMaxHSpeedTarget = target;
            }else {
                maxHSpeedMaxAddition += addition;
                if (!hasMaximumMaxHSpeed) {
                    maximumMaxHSpeedTarget = target;
                    hasMaximumMaxHSpeed = true;
                }else if (target > maximumMaxHSpeedTarget) maximumMaxHSpeedTarget = target;
            }
        }


        private void ApplyTargetHSpeedTargets() {
            if (!hasHSpeedTargets || spd.x == minimumHSpeedTarget && spd.x == maximumHSpeedTarget) {
                minimumHSpeedTarget = 0f;
                maximumHSpeedTarget = 0f;
                return;
            }
            float prevHSpeed = spd.x;
            bool hasMin = true;
            bool hasMax = true;
            if (spd.x < minimumHSpeedTarget) hasMin = false;
            else if (spd.x > maximumHSpeedTarget) hasMax = false;
            while (targetHSpeedRequests.Count != 0) {
                SpeedTarget speedTarget = targetHSpeedRequests.Dequeue();
                if (Math.Abs(spd.x - speedTarget.target) <= speedTarget.addition) SetHSpeed(speedTarget.target);
                else AddHSpeed(speedTarget.addition*MathF.Sign(speedTarget.target-prevHSpeed));
            }
            if (prevHSpeed > maximumHSpeedTarget && hasMax) prevHSpeed = maximumHSpeedTarget;
            else if (prevHSpeed < minimumHSpeedTarget && hasMin) prevHSpeed = minimumHSpeedTarget;

            hasHSpeedTargets = false;
            minimumHSpeedTarget = 0f;
            maximumHSpeedTarget = 0f;
        }
        private void ApplyTargetMaxHSpeedTargets() {
            if (!hasMaxHSpeedTargets) {
                hasMinimumMaxHSpeed = false;
                hasMaximumMaxHSpeed = false;
                maxHSpeedMinAddition = 0f;
                maxHSpeedMaxAddition = 0f;
                return;
            }

            float prevHSpeed = spd.x;
            if (prevHSpeed > minimumMaxHSpeedTarget) AddHSpeed(-maxHSpeedMinAddition);
            if (prevHSpeed < maximumMaxHSpeedTarget) AddHSpeed(maxHSpeedMaxAddition);
            if (hasMinimumMaxHSpeed && prevHSpeed > minimumMaxHSpeedTarget && spd.x < minimumMaxHSpeedTarget) SetHSpeed(minimumMaxHSpeedTarget);
            else if (hasMaximumMaxHSpeed && prevHSpeed < maximumMaxHSpeedTarget && spd.x > maximumMaxHSpeedTarget) SetHSpeed(maximumMaxHSpeedTarget);
            
            hasMaxHSpeedTargets = false;
            hasMinimumMaxHSpeed = false;
            hasMaximumMaxHSpeed = false;
            maxHSpeedMinAddition = 0f;
            maxHSpeedMaxAddition = 0f;
        }

    #endregion

    #region Collision

        private bool _isCollidingDown = false;
        public bool isCollidingDown {
            get;
            private set;
        }
        public void ApplyCollision() {
            isCollidingDown = false;
            foreach (GameObject CurrentCollision in GameObject.FindGameObjectsWithTag("Collision")) {
                CollisionMask CurCollisionMask = CurrentCollision.GetComponent<CollisionMaskScr>().CollisionMask;
                CollisionMask collisionMask = entity.collisionMask;
                if (collisionMask.IsPlaceMeeting(Vector2.up*spd.y, CurCollisionMask)) {
                    entity.transform.position = new Vector2(entity.transform.position.x, MathF.Ceiling(entity.transform.position.y)); // OBSERVATION004 - Rounding up or down should depend on speed's sign.
                    while (!collisionMask.IsPlaceMeeting(Vector2.up*MathF.Sign(spd.y), CurCollisionMask)) {
                        entity.transform.position += (Vector3)Vector2.up * MathF.Sign(spd.y);
                    }
                    if (spd.y < 0) isCollidingDown = true;
                    spd = new Vector2(spd.x, 0);
                }
                
                if (collisionMask.IsPlaceMeeting(Vector2.right*spd.x, CurCollisionMask)) {
                    entity.transform.position = new Vector2(MathF.Ceiling(entity.transform.position.x), entity.transform.position.y); // OBSERVATION004 - Rounding up or down should depend on speed's sign.
                    while (!collisionMask.IsPlaceMeeting(Vector2.right*MathF.Sign(spd.x), CurCollisionMask)) {
                        entity.transform.position += (Vector3)Vector2.right * MathF.Sign(spd.x);
                    }
                    spd = new Vector2(0, spd.y);
                }
            }
        }

    #endregion

    #region Executions
        public void ExecuteSpeed() {
            AddPosition(spd);
        }
    #endregion

    public void Update() {
        ApplyTargetHSpeedTargets();
        ApplyTargetMaxHSpeedTargets();
        ApplyCollision();
        ExecuteSpeed();
    }

    public static void UpdateAll() {
        foreach (PhysicsInterface physicsInterface in Game.InterfaceSubmanager.GetInterfaces(InterfaceSpecies.Physics).Cast<PhysicsInterface>()) {
            physicsInterface.Update();
        }
    }
}