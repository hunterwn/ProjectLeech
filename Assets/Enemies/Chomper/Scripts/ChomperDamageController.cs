using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChomperDamageController : MonoBehaviour
{
    public CapsuleCollider hurtbox;
    public ChomperController chomperController;
    private Collider last_collider;
    public Transform hitEffect;
    public AudioSource hitSFX;

    private void OnTriggerEnter(Collider collider) {
        if(last_collider != collider)
        {
            if(collider.gameObject.CompareTag("hitbox"))
            {
                Transform hitEffectTransform = Instantiate(hitEffect, collider.transform.position, Quaternion.identity);
                hitSFX.Play();
                this.last_collider = collider;
                chomperController.state.OnTakeDamage();
            }
        }
    }
}