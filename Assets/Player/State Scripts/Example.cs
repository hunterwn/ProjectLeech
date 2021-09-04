using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    public class Example : PlayerState
    {
        Animator animator;
        PlayerController player;
        void OnEnable() {
            this.animid = "example";
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

        }

        void CollisionHandler() {

        }

        void InputHandler() {

        }
    }
}