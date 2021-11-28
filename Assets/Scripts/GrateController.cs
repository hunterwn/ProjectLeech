using System.Collections.Generic;
using UnityEngine;

public class GrateController : MonoBehaviour {

	private Animator animator;
	private bool PlayerInTrigger = false;
	private AudioSource sfx;

	private void Start() {
		animator = GetComponent<Animator>();
		sfx = GetComponent<AudioSource>();
	}

	private void Update() {
		if (PlayerInTrigger && Input.GetKeyDown(KeyCode.E)) {
			if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
				animator.SetTrigger("open");
				sfx.Play();
			}
		}
	}

	private void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			PlayerInTrigger = true;
		}
	}

	private void OnTriggerExit(Collider other) {
		if (other.tag == "Player") {
			PlayerInTrigger = false;
		}
	}
}
