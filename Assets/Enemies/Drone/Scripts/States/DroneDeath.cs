using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneDeath : DroneState {

  public Material dissolve;
  public Material PA_Drone;
  public MeshRenderer meshrenderer;
  public Transform healthOrb;
  public GameObject Drone;
  public float rTime = 6;

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
      droneController.agent.isStopped = false;
      droneController.hp = 3;
      droneController.gameObject.transform.position = droneController.FOVCone.path.transform.position;
      droneController.FOVCone.viewedFlag = false;
      droneController.dead = false;
      meshrenderer.material = PA_Drone;
      droneController.state.EnterIdle();
      Instantiate(Drone, droneController.FOVCone.path.transform.position, Quaternion.identity);
      Destroy(this.gameObject);

  }

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
      gameObject.transform.position = new Vector3(1000.0f,0.0f,0.0f);
      StartCoroutine(RespawnTimer(rTime));
  }

  public override void OnTakeDamage()
  {
    return;
  }
}