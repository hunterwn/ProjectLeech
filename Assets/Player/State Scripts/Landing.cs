using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    public class Landing : PlayerState
    {
        public string animid = "landing";
        Animator animator;
        PlayerController player;
        void OnEnable() {
            player = GetComponent<PlayerController>();
            animator = GetComponent<Animator>();
            animator.SetBool(this.animid, true);
        }
        void OnDisable() {
            animator.SetBool(this.animid, false);
        }
        void Update() {
            PhysicsHandler();
            CollisionHandler();
            InputHandler();
        }

        void PhysicsHandler() {
            player.current_speed_v = 0;
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
                    if(inputDir == facing_dir * -1)
                    {
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