using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChomperHit2 : ChomperState {

  public int stateLength;
  void OnEnable() {
    initializeState("hit2");
    chomperController.agent.isStopped = true;
    chomperController.damage2SFX.Play();
  }
  void OnDisable() {
    chomperController.agent.isStopped = false;
  }
  void Update() {
    if(CheckAnimationFinished())
    {
      EnterIdle();
    }
  }
}