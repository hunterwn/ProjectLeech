using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    public class Jump : PlayerState
    {
        public string animid = "jump";
        Animator animator;
        PlayerController player;
        CharacterController controller;
        private bool jumpVelocityApplied;
        void OnEnable() {
            controller = GetComponent<CharacterController>();
            player = GetComponent<PlayerController>();
            animator = GetComponent<Animator>();
            animator.SetBool(this.animid, true);
            jumpVelocityApplied = false;
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
            if(!jumpVelocityApplied)
            {
                jumpVelocityApplied = true;
                player.current_speed_v = player.jump_initial_velocity;
            } else {
                ApplyGravity(player.gravity);
                ApplyHorizontalFriction(player.air_friction);
                ApplyAerialDrift(player.aerial_drift);
            }
        }

        void CollisionHandler() {

        }

        void InputHandler() {
            int inputDir = GetDirectionHeld();
            int facing_dir = animator.GetInteger("facing_direction");

            if(!CheckAnimationTransition() && inputDir == facing_dir * -1)
            {
                ReverseFacingDirection();
            }

            if(CheckAnimationFinished())
            {
                EnterFall();
            }
        }
    }
}