using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneDeath : DroneState {

  public int stateLength;
  void OnEnable() {
    initializeState("death");
    droneController.agent.isStopped = true;
    droneController.dead = true;
    //droneController.deathSFX.Play();
  }
  void Update() {
    if(CheckAnimationFinished())
    {
      droneController.damageController.hurtbox.enabled = false;
      this.enabled = false;
      return;
    }

    if(droneController.dead && droneController.agent.baseOffset > 0.2f)
    {
      droneController.agent.baseOffset -= 0.02f;
    }
  }

  public override void OnTakeDamage()
  {
    return;
  }
}