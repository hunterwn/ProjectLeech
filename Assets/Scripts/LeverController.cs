using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour {
  [SerializeField]
  private float activationDuration = 2f;
  [SerializeField, SerializeReference]
  private Component leverActionScript;
  private LeverAction leverAction;

  private bool isPlayerInRange;
  private HingeJoint leverJoint;
  private AudioSource leverSound;

  private void Start() {
    this.isPlayerInRange = false;
    this.leverJoint = GetComponentInChildren<HingeJoint>();
    this.leverSound = GetComponent<AudioSource>();
    this.leverAction = (LeverAction)leverActionScript;
  }

  private void Update() {
    if (this.isPlayerInRange && Input.GetKeyDown(KeyCode.E)) {
      StartCoroutine(ActivateLever());
    }
  }

  private IEnumerator ActivateLever() {
    this.leverJoint.useMotor = true;
    this.leverSound.Play();
    this.leverAction.ExecuteAction();

    yield return new WaitForSeconds(this.activationDuration);
    this.leverJoint.useMotor = false;
  } 

  private void OnTriggerEnter(Collider other) {
    if (other.tag == "Player") {
      this.isPlayerInRange = true;
    }
  }

  private void OnTriggerExit(Collider other) {
    if (other.tag == "Player") {
      this.isPlayerInRange = false;
    }
  }
}
