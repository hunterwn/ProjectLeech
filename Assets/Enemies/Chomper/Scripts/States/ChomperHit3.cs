using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChomperHit3 : ChomperState {

  public Material dissolve;
  public SkinnedMeshRenderer meshrenderer;

  public Transform healthOrb;
  private bool materialApplied = false;

  public int stateLength;
  void OnEnable() {
    initializeState("hit3");
    chomperController.agent.isStopped = true;
    chomperController.dead = true;
    chomperController.deathSFX.Play();
    materialApplied = false;
  }
  void Update() {
    if(!materialApplied && CheckAnimationFinished())
    {
      materialApplied = true;
      chomperController.damageController.hurtbox.enabled = false;
      meshrenderer.material = dissolve;
      StartCoroutine(DelayedDeath());
    }
  }

  IEnumerator DelayedDeath()
  {
      yield return new WaitForSeconds(1.5f);

      Vector3 orbSpawnPosition = transform.position;
      orbSpawnPosition.y += 1.2f;

      Transform healthOrbTransform = Instantiate(healthOrb, orbSpawnPosition, Quaternion.identity);
      StartCoroutine(DelayedDeath());

      yield return new WaitForSeconds(1.5f);

      gameObject.SetActive(false);
  }

  public override void OnTakeDamage()
  {
    return;
  }
}