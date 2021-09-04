using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    public class JumpSquat : PlayerState
    {
        bool facing_dir_set;
        int jumpSquatFramesLeft;
        void OnEnable() {
            initializeState("jumpsquat");

            jumpSquatFramesLeft = player.jumpSquatFrames;
            facing_dir_set = false;
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

            jumpSquatFramesLeft -= 1;

            if(jumpSquatFramesLeft == 0)
            {
                EnterJump();
            }

            if(inputDir == facing_dir * -1 && !facing_dir_set)
            {
                facing_dir_set = true;
                ReverseFacingDirection();
            }
        }
    }
}