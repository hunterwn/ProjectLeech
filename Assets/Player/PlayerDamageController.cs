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
        if(collision.collider.tag == "Enemy")
        {
            if(!player.invincible)
            {
                Vector3 pushDirection = transform.position - collision.transform.position;
                player.addedVelocity = pushDirection * 20;
                
                player.TakeDamage(1, 1.5f);
            }
        }
    }

    private void OnTriggerStay(Collider trigger) 
    {
        if (trigger.tag == "trap")
        {
            if(!player.invincible)
            {
                player.TakeDamage(1, 1.5f);
            }
        }
    }
}