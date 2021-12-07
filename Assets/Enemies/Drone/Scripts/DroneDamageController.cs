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
                StartCoroutine(SpawnHitEffect(collider.transform.position, 0.25f));
                hitSFX.Play();
                this.last_collider = collider;
                droneController.state.OnTakeDamage();
            }
        }
    }

    IEnumerator SpawnHitEffect (Vector3 position, float lifetime)
    {
        // Debug.Log("SpawnHitEffect Drone");
        Transform hitEffectTransform = Instantiate(hitEffect, position, Quaternion.identity);
        yield return new WaitForSeconds(lifetime);
        Destroy(hitEffectTransform.gameObject);
    }
}