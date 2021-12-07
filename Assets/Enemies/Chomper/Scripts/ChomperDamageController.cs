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
                //Transform hitEffectTransform = Instantiate(hitEffect, collider.transform.position, Quaternion.identity);
                StartCoroutine(SpawnHitEffect(collider.transform.position, 0.75f));
                hitSFX.Play();
                this.last_collider = collider;
                chomperController.state.OnTakeDamage();
            }
        }
    }

    IEnumerator SpawnHitEffect (Vector3 position, float lifetime)
    {
        Transform hitEffectTransform = Instantiate(hitEffect, position, Quaternion.identity);
        yield return new WaitForSeconds(lifetime);
        Destroy(hitEffectTransform.gameObject);
    }
}