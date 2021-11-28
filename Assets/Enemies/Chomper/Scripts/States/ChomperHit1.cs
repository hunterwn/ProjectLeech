using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChomperHit1 : ChomperState {

  public int stateLength;
  void OnEnable() {
    initializeState("hit1");
    chomperController.agent.isStopped = true;
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