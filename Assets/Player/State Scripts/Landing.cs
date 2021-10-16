using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landing : PlayerState {
  void OnEnable() {
    initializeState("landing");
  }
  void Update() {
    if (CheckAnimationFinished()) {
      EnterIdle();
    }
  }

  // void InputHandler() {
  //   int inputDir = GetDirectionHeld();
  //   int facing_dir = animator.GetInteger("facing_direction");

  //   if (CheckAnimationFinished()) {
  //     if (inputDir == facing_dir * -1) {
  //       ReverseFacingDirection();
  //     }

  //     if (inputDir == 0) {
  //       EnterIdle();
  //       return;
  //     } else if (CheckRunInput()) {
  //       EnterRun();
  //     } else {
  //       EnterWalk();
  //     }
  //   }
  // }
}