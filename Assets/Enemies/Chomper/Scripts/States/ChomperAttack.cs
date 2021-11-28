using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChomperAttack : ChomperState {
  void OnEnable() {
    initializeState("attack");
  }
  void Update() {
    if(CheckAnimationFinished())
    {
      EnterIdle();
    }
  }
}