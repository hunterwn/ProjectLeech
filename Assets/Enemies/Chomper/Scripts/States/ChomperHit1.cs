using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChomperHit1 : ChomperState {
  void OnEnable() {
    initializeState("hit1");
  }
  void Update() {
    if(CheckAnimationFinished())
    {
      EnterIdle();
    }
  }
}