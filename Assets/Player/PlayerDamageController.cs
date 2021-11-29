using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerDamageController : MonoBehaviour
{
    public BoxCollider hurtbox;
    public Player player;
    private IEnumerator damageflash;

    private void OnCollisionEnter(Collision collision) {
        if(collision.collider.tag == "hurtbox")
        {
            if(!player.invincible)
            {
                Vector3 pushDirection = transform.position - collision.transform.position;
                player.addedVelocity = pushDirection * 20;
                
                damageflash = player.DamageFlash(Color.white, 1.0f, 0.05f);
                player.invincible = true;
                StartCoroutine(damageflash);
            }
        }
    }
}