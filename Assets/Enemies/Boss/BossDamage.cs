using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class BossDamage : MonoBehaviour
{
    [SerializeField]
    private Boss boss;
    private int health = 15;
    public Transform hitEffect;
    private void OnTriggerEnter(Collider coll) {
        if(boss.dead)
        {
            return;
        }

        if(coll.tag == "hitbox")
        {
            Transform hitEffectTransform = Instantiate(hitEffect, coll.transform.position, Quaternion.identity);
            boss.EnterState(boss.hurt);
            boss.agent.isStopped = true;
            health -= 1;

            if(health <= 0)
            {
                boss.dead = true;
                boss.EnterState(boss.death);
                boss.agent.isStopped = true;
                gameObject.SetActive(false);

                // foreach(Transform child in transform)
                // {
                //     foreach(Collider c in child.GetComponents<Collider> ()) {
                //         c.enabled = false
                //     }
                // }
            }
        }
    }
}