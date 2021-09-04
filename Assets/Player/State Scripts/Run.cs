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
            int facing_direction = animator.GetInteger("facing_direction");

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
            int facing_direction = animator.GetInteger("facing_direction");

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