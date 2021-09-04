using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    public class Entry : PlayerState
    {
        void OnEnable() {
            initializeState("entry");

            EnterIdle(); //TODO: make entry animation and remove this line
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