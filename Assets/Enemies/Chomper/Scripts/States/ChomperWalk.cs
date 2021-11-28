using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChomperWalk : ChomperState {
  void OnEnable() {
    initializeState("walk");
  }
  void Update() {
    if(chomperController.velocity < chomperController.minWalkVelocity)
    {
      EnterIdle();
    }
  }
}