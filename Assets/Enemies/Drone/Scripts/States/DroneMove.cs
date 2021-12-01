using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneMove : DroneState {
  void OnEnable() {
    initializeState("moveforward");
  }
  void Update() {
    if(droneController.velocity < droneController.minWalkVelocity)
    {
      EnterIdle();
    }
  }
}