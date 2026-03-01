using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.Mathematics;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UIElements;

namespace Entities
{
    [RequireComponent(typeof(CollisionMaskScr))]
    public class TryanScript : EntityScript
    {
        private PhysicsInterface physicsInterface;
        private float acceleration = 1.1f;
        private float Speed = 2;
        private float Gravity = 0.6f;
        private int lastDirection = 1;

        // Jumping
        private bool isJumping = false;
        private bool isHoldingJump = false;
        private float JumpForce = 8;

        // Slashing
        private int delayFrames = 60;
        private int delayFramesCur = 0;
        private int slashFrames = 10;
        private RectCollisionMask slashColMask;
        private class DefaultSlash : Melee
        {
            public DefaultSlash(CollisionMask collisionMask, EntityScript starterEntity, LinkedList<EntityScript> targets) : base(collisionMask, starterEntity, targets) {

            }
            protected override void NotifyHit(EntityScript entity)
            {
                Game.GameplayManager.__DEBUG_actionQueue.Enqueue(() => {
                    Game.EntityAssetSubmanager.DeleteEntity(entity);
                });
            }
        }
        private DefaultSlash defaultSlash;
        private void slashHitFunction(EntityScript hitEntity) {
            Debug.Log("Hit entity");
            Game.GameplayManager.__DEBUG_actionQueue.Enqueue(() => {
                Game.EntityAssetSubmanager.DeleteEntity(hitEntity);
            });
        }

        #region Visual
        private int spriteCurrentFrame = 0;
        private int spriteSpeed = 5;
        private int spriteSpeedCurrent = 0;
        private bool spriteIsContinuous = true;
        private bool spriteIsRunning = true;
        private Sprite[] currentSprite;
        public Sprite[] idleSprite;
        public Sprite[] walkingSprite;
        public Sprite[] slashingSprite;
        private bool spriteIsMoving = false;
        private bool spriteIsSlashing = false;
        private void SpriteNotifyMovement() {
            if (!spriteIsMoving) {
                StartAnimation(walkingSprite);
                spriteIsMoving = true;
                spriteIsSlashing = false;
            }
        }
        private void SpriteNotifyStop() {
            if (spriteIsMoving) {
                StartAnimation(idleSprite);
                spriteIsMoving = false;
                spriteIsSlashing = false;
            }
        }
        private void SpriteNotifySlash() {
            if (!spriteIsSlashing) {
                spriteIsSlashing = true;
                StartAnimation(slashingSprite, false);
            }
        }

        private void StartAnimation(Sprite[] sprite, bool isContinuous = true) {
            currentSprite = sprite;
            spriteCurrentFrame = 0;
            spriteSpeedCurrent = 0;
            spriteIsContinuous = isContinuous;
            spriteIsRunning = true;
        }

        #endregion

        protected override void DefineSpecies() => species = EntitySpecies.Tryan;

        public override void ReceiveParamethers(EntityParamethers paramethers) {
            base.ReceiveParamethers(paramethers);
        }

        public override void Create_EEvent() {
            StartAnimation(idleSprite);
        }

        protected override void Start()
        {
            base.Start();
            physicsInterface = (PhysicsInterface) Game.InterfaceSubmanager.AssignInterface(InterfaceSpecies.Physics, new PhysicsInterfaceParamethers(), this);
            slashColMask = new RectCollisionMask(new(6, -5), new(20, 5), gameObject.transform.position);
            defaultSlash = new DefaultSlash(slashColMask, this, Game.EntityAssetSubmanager.GetEntityList(EntitySpecies.ShooterDrone));
        }

        void Update()
        {

            #region Attack

            if (Game.InputManager.InputBind_PrimaryAttack.IsActive) {
                SpriteNotifySlash();
                slashColMask.position = transform.position;
                slashColMask.flip = new(lastDirection, slashColMask.flip.y);
                defaultSlash.Update();
            }

            #endregion

            #region Movement

            // Horizontal movement
            InputBind_Movement InputBind_Movement = Game.InputManager.InputBind_Movement;
            Vector2 CurrentDirection = InputBind_Movement.IsActive ? InputBind_Movement.Value : Vector2.zero;
            if (CurrentDirection.x != 0) {
                lastDirection = MathF.Sign(CurrentDirection.x);
                SpriteNotifyMovement();
            }else SpriteNotifyStop();
            physicsInterface.TargetMaxHSpeed(CurrentDirection.x*Speed, acceleration);
            physicsInterface.TargetHSpeed(0, 0.7f);

            // Vertical movement
            physicsInterface.AddSpeed(Gravity*Vector2.down);

            if (physicsInterface.isCollidingDown) {
                if (!isHoldingJump && Game.InputManager.InputBind_Jump.IsActive) {
                    isJumping = true;
                    isHoldingJump = true;
                    physicsInterface.SetVSpeed(JumpForce);
                }else {
                    isJumping = false;
                }
            }

            if (physicsInterface.spd.y <= 0) isJumping = false;

            if (!Game.InputManager.InputBind_Jump.IsActive) {
                isHoldingJump = false;
                if (isJumping) {
                    physicsInterface.SetVSpeed(physicsInterface.spd.y/2);
                    isJumping = false;
                }
            }

            #endregion

            foreach (EntityScript CurrentCollision in Game.EntityAssetSubmanager.GetEntityList(EntitySpecies.JumpPad)) {
                CollisionMask CurCollisionMask = CurrentCollision.collisionMask;
                if (collisionMask.IsPlaceMeeting(physicsInterface.spd, CurCollisionMask)) {
                    physicsInterface.SetVSpeed(10);
                }
            }

            // Visual
            if (spriteIsRunning) {
                spriteSpeedCurrent++;
                if (spriteSpeedCurrent >= spriteSpeed) {
                    spriteCurrentFrame++;
                    spriteSpeedCurrent = 0;
                    if (spriteCurrentFrame == currentSprite.Length) {
                        if (spriteIsContinuous) spriteCurrentFrame = 0;
                        else {
                            spriteIsRunning = false;
                            spriteCurrentFrame = currentSprite.Length-1;
                        }
                    }
                }
            }
        }

        void OnRenderObject() {
            Game.AestheticsSubmanager.__DEBUG_DrawSprite(transform.position, currentSprite[spriteCurrentFrame], new(lastDirection, 1));
        }
    }
}
