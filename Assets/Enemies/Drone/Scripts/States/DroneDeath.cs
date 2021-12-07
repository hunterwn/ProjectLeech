using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneDeath : DroneState {

  public Material dissolve;
  public MeshRenderer meshrenderer;
  public Transform healthOrb;

  public int stateLength;
  void OnEnable() {
    initializeState("death");
    droneController.agent.isStopped = true;
    droneController.dead = true;
    //droneController.deathSFX.Play();
  }
  void Update() {
    if(droneController.dead && CheckAnimationFinished())
    {
      droneController.dead = false;
      droneController.damageController.hurtbox.enabled = false;
      meshrenderer.material = dissolve;

      Vector3 orbSpawnPosition = transform.position;
      orbSpawnPosition.y += 1.2f;
      Transform healthOrbTransform = Instantiate(healthOrb, transform.position, Quaternion.identity);
      StartCoroutine(DelayedDeath());
    }

    if(droneController.agent.baseOffset > 0.2f)
    {
      droneController.agent.baseOffset -= 0.02f;
    }
  }

  IEnumerator DelayedDeath()
  {
      yield return new WaitForSeconds(3.0f);
      gameObject.SetActive(false);
  }

  public override void OnTakeDamage()
  {
    return;
  }
}