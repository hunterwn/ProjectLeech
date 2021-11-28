using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChomperHit3 : ChomperState {

  public int stateLength;
  void OnEnable() {
    initializeState("hit3");
    chomperController.agent.isStopped = true;
    chomperController.dead = true;
  }
  void Update() {
    if(CheckAnimationFinished())
    {
      chomperController.damageController.hurtbox.enabled = false;
    }
  }

  public override void OnTakeDamage()
  {
    return;
  }
}