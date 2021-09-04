using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    public class JumpSquat : PlayerState
    {
        public string animid = "jumpsquat";
        Animator animator;
        PlayerController player;
        bool facing_dir_set;
        void OnEnable() {
            player = GetComponent<PlayerController>();
            animator = GetComponent<Animator>();
            animator.SetBool(this.animid, true);
            facing_dir_set = false;
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

        }

        void CollisionHandler() {

        }

        void InputHandler() {
            int inputDir = GetDirectionHeld();
            int facing_dir = animator.GetInteger("facing_direction");

            if(inputDir == facing_dir * -1 && !facing_dir_set)
            {
                facing_dir_set = true;
                ReverseFacingDirection();
            }

            if(CheckAnimationFinished())
            {
                EnterJump();
            }
        }
    }
}