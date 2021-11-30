using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneHit1 : DroneState {
  private AudioSource sfx;
  public int stateLength;
  void OnEnable() {
    initializeState("hit1");
    droneController.agent.isStopped = true;
    droneController.damage1SFX.Play();
  }
  void OnDisable() {
    droneController.agent.isStopped = false;
  }
  void Update() {
    if(CheckAnimationFinished())
    {
      EnterIdle();
    }
  }
}