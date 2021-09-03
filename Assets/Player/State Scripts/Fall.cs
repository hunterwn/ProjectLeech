using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    public class Fall : PlayerState
    {
        public string animid = "fall";
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
            ApplyHorizontalFriction(player.air_friction);
            ApplyAerialDrift(player.aerial_drift);
            ApplyGravity(player.gravity);
        }

        void CollisionHandler() {
            if(controller.isGrounded)
            {
                player.current_speed_v = 0;
                EnterLanding();
            }
        }

        void InputHandler() {

        }
    }
}