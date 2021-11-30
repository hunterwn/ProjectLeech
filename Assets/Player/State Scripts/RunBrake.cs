using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunBrake : PlayerState {
  int faceDir;
  void OnEnable() {
    initializeState("runbrake");
    player.movementDisabled = true;
    faceDir = GetFacingDirection();
  }

  private void OnDisable() {
    player.movementDisabled = false;
  }
  void Update() {
    if(!player.controller.collisions.below)
    {
      EnterFall();
      return;
    }

    if(CheckAnimationFinished())
    {
      if (player.directionalInput.x != 0)
      {
        EnterWalk();
      } else {
        EnterIdle();
      }
      return;
    }
  }
}