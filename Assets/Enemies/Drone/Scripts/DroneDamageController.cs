using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneDamageController : MonoBehaviour
{
    public CapsuleCollider hurtbox;
    public DroneController droneController;
    private Collider last_collider;
    [SerializeField]
    public Transform hitEffect;
    public AudioSource hitSFX;
    private void OnTriggerEnter(Collider collider) 
    {
        if(last_collider != collider)
        {
            if(collider.gameObject.CompareTag("hitbox"))
            {
                Transform hitEffectTransform = Instantiate(hitEffect, collider.transform.position, Quaternion.identity);
                hitSFX.Play();
                this.last_collider = collider;
                droneController.state.OnTakeDamage();
            }
        }
    }
}