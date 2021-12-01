using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DroneIdle : DroneState {
  void OnEnable() {
    initializeState("idle");
  }
  void Update() {
    if(droneController.velocity > droneController.minWalkVelocity)
    {
      EnterMoveForward();
    }
  }
}