using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    public class Example : PlayerState
    {
        public string animid = "example";
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

        }

        void CollisionHandler() {

        }

        void InputHandler() {

        }
    }
}