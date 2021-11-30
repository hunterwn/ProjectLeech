using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : PlayerState {
  float baseMoveSpeed;
  void OnEnable() {
    initializeState("run");
    baseMoveSpeed = player.moveSpeed;
    player.moveSpeed = player.runSpeed;
  }
  private void OnDisable() {
    player.moveSpeed = baseMoveSpeed;
  }
  void Update() {
    if(!player.controller.collisions.below)
    {
      EnterFall();
      return;
    }

    if(player.directionalInput.x == 0)
    {
      EnterRunBrake();
      return;
    }
    
    if (player.directionalInput.x != player.input_prev.x)
    {
      EnterRunBrake();
      return;
    }

    if(!player.runHeld)
    {
      EnterRunBrake();
      return;
    }
  }
}