using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class BossDamage : MonoBehaviour
{
    [SerializeField]
    private Boss boss;
    public Transform hitEffect;
    private void OnTriggerEnter(Collider coll) {
        if(coll.tag == "hitbox")
        {
            Transform hitEffectTransform = Instantiate(hitEffect, coll.transform.position, Quaternion.identity);
            boss.EnterState(boss.hurt);
            boss.agent.isStopped = true;
        }
    }
}