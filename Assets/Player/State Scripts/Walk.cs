using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    public class Walk : PlayerState
    {
        Animator animator;
        PlayerController player;
        CharacterController controller;
        void OnEnable() {
            this.animid = "walk";
            controller = GetComponent<CharacterController>();
            player = GetComponent<PlayerController>();
            animator = GetComponent<Animator>();
            animator.SetBool(this.animid, true);
        }
        void Update() {
            PhysicsHandler();
            CollisionHandler();
            InputHandler();
        }

        void PhysicsHandler() {
            int facing_dir = animator.GetInteger("facing_direction");

            if(CheckAnimationTransition())
            {
                ApplyHorizontalFriction(player.ground_friction);
            }

            if(facing_dir > 0)
            {
                if(player.current_speed_h < player.walk_maxspeed)
                {
                    player.current_speed_h += player.walk_acceleration * Time.deltaTime;
                }
                if(player.current_speed_h > player.walk_maxspeed)
                {
                    player.current_speed_h = player.walk_maxspeed;
                }
            } else {
                if(player.current_speed_h > player.walk_maxspeed * -1)
                {
                    player.current_speed_h -= player.walk_acceleration * Time.deltaTime;
                }
                if(player.current_speed_h < player.walk_maxspeed * -1)
                {
                    player.current_speed_h = player.walk_maxspeed * -1;
                }
            }
        }

        void CollisionHandler() {
            if(!controller.isGrounded)
            {
                EnterFall();
            }
        }

        void InputHandler() {
            int facing_dir = animator.GetInteger("facing_direction");
            int inputDir = GetDirectionHeld();

            if(CheckJumpInput())
            {
                EnterJumpSquat();
                return;
            }

            if(CheckRunInput())
            {
                EnterRun();
            }

            else if (GetDirectionHeld() == 0)
            {
                EnterIdle();
            } else if (inputDir != facing_dir)
            {
                ReverseFacingDirection();
            }
        }
    }
}