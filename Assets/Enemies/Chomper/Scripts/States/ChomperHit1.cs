using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChomperHit1 : ChomperState {

  public int stateLength;
  void OnEnable() {
    initializeState("hit1");
    Debug.Log("hit1");
  }
  void Update() {
    if(CheckAnimationFinished())
    {
      EnterIdle();
    }
  }
}