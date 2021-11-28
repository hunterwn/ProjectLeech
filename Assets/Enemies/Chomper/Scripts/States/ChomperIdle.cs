using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChomperIdle : ChomperState {
  void OnEnable() {
    initializeState("idle");
  }
  void Update() {
    if(chomperController.velocity > chomperController.minWalkVelocity)
    {
      EnterWalk();
    }
  }
}