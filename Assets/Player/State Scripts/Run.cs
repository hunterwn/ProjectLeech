using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    public class Run : PlayerState
    {
        public string animid = "run";
        Animator animator;
        PlayerController player;
        CharacterController controller;
        int facing_direction;
        void OnEnable() {
            controller = GetComponent<CharacterController>();
            player = GetComponent<PlayerController>();
            animator = GetComponent<Animator>();
            animator.SetBool(this.animid, true);

            facing_direction = animator.GetInteger("facing_direction");
            if(animator.GetCurrentAnimatorStateInfo(0).IsName("RunR"))
            {
                facing_direction = 1;
            } else if (animator.GetCurrentAnimatorStateInfo(0).IsName("RunL")) {
                facing_direction = -1;
            }
            animator.SetInteger("facing_direction", facing_direction);
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
            if(CheckAnimationTransition())
            {
                ApplyHorizontalFriction(player.ground_friction);
            }

            if(Mathf.Abs(player.current_speed_h) < player.run_maxspeed)
            {
                player.current_speed_h += player.run_acceleration * Time.deltaTime * facing_direction;
            }

            if(Mathf.Abs(player.current_speed_h) >  player.run_maxspeed)
            {
                player.current_speed_h = player.run_maxspeed * facing_direction;
            }
        }

        void CollisionHandler() {
            if(!controller.isGrounded)
            {
                EnterFall();
            }
        }

        void InputHandler() {
            int inputDir = GetDirectionHeld();

            if(CheckJumpInput())
            {
                EnterJumpSquat();
                return;
            }

            if(!CheckRunInput() || 
                (inputDir == facing_direction * -1))
            {
                EnterRunBrake();
            }
        }
    }
}