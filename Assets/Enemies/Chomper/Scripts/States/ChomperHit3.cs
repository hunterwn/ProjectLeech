using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChomperHit3 : ChomperState {

  public Material dissolve;
  public SkinnedMeshRenderer meshrenderer;
  public GameObject chomper;
  public Material chomperMat;
  public float rTime = 6;

  public Transform healthOrb;
  private bool materialApplied = false;

  public int stateLength;

  IEnumerator RespawnTimer (float rTime)
  {
      Debug.Log("Respawn!");      
      yield return new WaitForSeconds(rTime);
      Respawn();
  }

  void Respawn()
  {
      Debug.Log("Respawn!");
      chomperController.agent.isStopped = false;
      chomperController.hp = 3;
      chomperController.gameObject.transform.position = chomperController.FOVCone.path.transform.position;
      chomperController.FOVCone.viewedFlag = false;
      chomperController.dead = false;
      meshrenderer.material = chomperMat;
      chomperController.state.EnterIdle();
      Instantiate(chomper, chomperController.FOVCone.path.transform.position, Quaternion.identity);
      Destroy(this.gameObject);

  }

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

      Vector3 orbSpawnPosition = transform.position;
      orbSpawnPosition.y += 1.2f;

      Transform healthOrbTransform = Instantiate(healthOrb, orbSpawnPosition, Quaternion.identity);
      StartCoroutine(DelayedDeath());
    }
  }

  IEnumerator DelayedDeath()
  {
    yield return new WaitForSeconds(3.0f);
    gameObject.transform.position = new Vector3(1000.0f,0.0f,0.0f);
    StartCoroutine(RespawnTimer(rTime));
  }

  public override void OnTakeDamage()
  {
    return;
  }
}