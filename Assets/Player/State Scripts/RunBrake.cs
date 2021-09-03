using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    public class RunBrake : PlayerState
    {
        public string animid = "runbrake";
        Animator animator;
        PlayerController player;
        CharacterController controller;
        void OnEnable() {
            controller = GetComponent<CharacterController>();
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
            ApplyHorizontalFriction(player.ground_friction);
        }

        void CollisionHandler() {
            if(!controller.isGrounded)
            {
                EnterFall();
            }
        }

        void InputHandler() {
            int facing_direction = animator.GetInteger("facing_direction");
            int inputDir = GetDirectionHeld();

            if (CheckAnimationFinished())
            {
                this.enabled = false;
                if(inputDir == 0)
                {
                    EnterIdle();
                    return;
                } else if (inputDir == facing_direction * -1)
                {
                    facing_direction *= -1;
                    animator.SetInteger("facing_direction", facing_direction);
                }
                if(CheckRunInput())
                {
                    GetComponent<Run>().enabled = true;
                } else {
                    GetComponent<Walk>().enabled = true;
                }
            }
        }
    }
}