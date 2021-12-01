using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChomperHit3 : ChomperState {

  public Material dissolve;
  public SkinnedMeshRenderer meshrenderer;

  public int stateLength;
  void OnEnable() {
    initializeState("hit3");
    chomperController.agent.isStopped = true;
    chomperController.dead = true;
    chomperController.deathSFX.Play();
  }
  void Update() {
    if(CheckAnimationFinished())
    {
      chomperController.damageController.hurtbox.enabled = false;
      meshrenderer.material = dissolve;
    }
  }

  public override void OnTakeDamage()
  {
    return;
  }
}