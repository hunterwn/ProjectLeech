using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAttack : DroneState {
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