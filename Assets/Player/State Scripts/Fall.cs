using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
  public class Fall : PlayerState {
    void OnEnable() {
      initializeState("fall");
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
      if (controller.isGrounded) {
        player.current_speed_v = 0;
        EnterLanding();
      }
    }

    void InputHandler() {
      int inputDir = GetDirectionHeld();
      int facing_dir = animator.GetInteger("facing_direction");

      if (CheckJumpAerialInput()) {
        EnterJumpAerial();
        return;
      }

      if (!CheckAnimationTransition() && inputDir == facing_dir * -1) {
        ReverseFacingDirection();
      }
    }
  }
}