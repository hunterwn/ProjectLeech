using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
  public class RunBrake : PlayerState {
    void OnEnable() {
      initializeState("runbrake");
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
      if (!controller.isGrounded) {
        EnterFall();
      }
    }

    void InputHandler() {
      int facing_direction = animator.GetInteger("facing_direction");
      int inputDir = GetDirectionHeld();

      if (CheckJumpInput()) {
        EnterJumpSquat();
        return;
      }

      if (CheckAnimationFinished()) {
        if (inputDir == 0) {
          EnterIdle();
          return;
        } else if (inputDir == facing_direction * -1) {
          facing_direction *= -1;
          animator.SetInteger("facing_direction", facing_direction);
        }
        if (CheckRunInput()) {
          EnterRun();
        } else {
          EnterWalk();
        }
      }
    }
  }
}