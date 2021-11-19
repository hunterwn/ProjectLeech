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
      if(player.attackInputDown)
      {
        EnterAttack1();
      }
    }
  }