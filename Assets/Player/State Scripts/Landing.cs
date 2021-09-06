using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
  public class Landing : PlayerState {
    void OnEnable() {
      initializeState("landing");
    }
    void Update() {
      PhysicsHandler();
      CollisionHandler();
      InputHandler();
    }

    void PhysicsHandler() {
      ApplyHorizontalFriction(player.ground_friction);
    }

    void CollisionHandler() {

    }

    void InputHandler() {
      int inputDir = GetDirectionHeld();
      int facing_dir = animator.GetInteger("facing_direction");

      if (CheckAnimationFinished()) {
        if (inputDir == facing_dir * -1) {
          ReverseFacingDirection();
        }

        if (inputDir == 0) {
          EnterIdle();
          return;
        } else if (CheckRunInput()) {
          EnterRun();
        } else {
          EnterWalk();
        }
      }
    }
  }
}