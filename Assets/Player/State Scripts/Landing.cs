using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    public class Landing : PlayerState
    {
        public string animid = "landing";
        Animator animator;
        PlayerController player;
        bool facing_dir_set;
        int frameCount;
        void OnEnable() {
            player = GetComponent<PlayerController>();
            animator = GetComponent<Animator>();
            animator.SetBool(this.animid, true);
            facing_dir_set = false;
            frameCount = 0;
        }
        void OnDisable() {
            animator.SetBool(this.animid, false);
        }
        void Update() {
            PhysicsHandler();
            CollisionHandler();
            InputHandler();

            frameCount += 1;
            print("frameCount: " + frameCount);
        }

        void PhysicsHandler() {
            ApplyHorizontalFriction(player.ground_friction);
        }

        void CollisionHandler() {

        }

        void InputHandler() {
            if(CheckAnimationFinished())
            {
                int inputDir = GetDirectionHeld();
                int facing_dir = animator.GetInteger("facing_direction");
                if(inputDir == 0)
                {
                    EnterIdle();
                } else {
                    if(!CheckAnimationTransition())
                    {
                        if(!facing_dir_set && inputDir == facing_dir * -1)
                        {
                            facing_dir_set = true;
                            ReverseFacingDirection();
                        }

                        if(CheckRunInput())
                        {
                            EnterRun();
                        } else {
                            EnterWalk();
                        }
                    }
                }
            }
        }
    }
}