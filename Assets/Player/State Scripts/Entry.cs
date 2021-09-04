using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    public class Entry : PlayerState
    {
        Animator animator;
        PlayerController player;
        void OnEnable() {
            this.animid = "entry";
            //animator.SetBool(this.animid, true);
            EnterIdle();
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