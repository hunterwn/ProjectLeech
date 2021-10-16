using System.Collections;
using System.Collections.Generic;
using UnityEngine;

  public class Idle : PlayerState {
    void OnEnable() {
      initializeState("idle");
    }

    void Update() {
      if(!player.controller.collisions.below)
      {
        EnterFall();
      }
      if(player.directionalInput.x != 0)
      {
        EnterWalk();
      }
    }

    // void CollisionHandler() {
    //   if (!this.controller.isGrounded) {
    //     EnterFall();
    //   }
    // }

    // void InputHandler() {
    //   int inputDir = GetDirectionHeld();
    //   int facing_direction = animator.GetInteger("facing_direction");

    //   if (CheckJumpInput()) {
    //     EnterJumpSquat();
    //     return;
    //   }

    //   if (inputDir == facing_direction * -1) {
    //     ReverseFacingDirection();
    //   } else if (inputDir == facing_direction) {
    //     if (CheckRunInput()) {
    //       EnterRun();
    //     } else {
    //       EnterWalk();
    //     }
    //   }
    // }
  }