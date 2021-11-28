using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChomperHit3 : ChomperState {

  public int stateLength;
  void OnEnable() {
    initializeState("hit3");
    Debug.Log("hit3");
  }
  void Update() {
    if(CheckAnimationFinished())
    {
      EnterIdle();
    }
  }
}