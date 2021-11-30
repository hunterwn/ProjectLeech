using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : PlayerState {
  void OnEnable() {
    initializeState("walk");
  }
  void Update() {
    if(!player.controller.collisions.below)
    {
      EnterFall();
      return;
    }

    if(player.directionalInput.x == 0)
    {
      EnterIdle();
      return;
    }
    if(player.runHeld)
    {
      EnterRun();
      return;
    }
    if (player.directionalInput.x != player.input_prev.x)
    {
      EnterWalk();
      return;
    }
  }
}