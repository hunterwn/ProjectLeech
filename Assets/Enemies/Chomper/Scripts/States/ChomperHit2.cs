using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChomperHit2 : ChomperState {

  public int stateLength;
  void OnEnable() {
    initializeState("hit2");
    Debug.Log("hit2");
  }
  void Update() {
    if(CheckAnimationFinished())
    {
      EnterIdle();
    }
  }
}