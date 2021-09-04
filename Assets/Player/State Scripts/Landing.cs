using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    public class Landing : PlayerState
    {
        Animator animator;
        PlayerController player;
        int frameCount;
        void OnEnable() {
            this.animid = "landing";
            player = GetComponent<PlayerController>();
            animator = GetComponent<Animator>();
            animator.SetBool(this.animid, true);
            frameCount = 0;
        }
        void Update() {
            PhysicsHandler();
            CollisionHandler();
            InputHandler();

            frameCount += 1;
        }

        void PhysicsHandler() {
            ApplyHorizontalFriction(player.ground_friction);
        }

        void CollisionHandler() {

        }

        void InputHandler() {
            int inputDir = GetDirectionHeld();
            int facing_dir = animator.GetInteger("facing_direction");

            if(CheckAnimationFinished())
            {   
                if(inputDir == facing_dir * -1)
                {
                    ReverseFacingDirection();
                }

                if(inputDir == 0)
                {
                    EnterIdle();
                    return;
                } else if(CheckRunInput())
                {
                    EnterRun();
                } else {
                    EnterWalk();
                }
            }
        }
    }
}