using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class BossDetectPlayer : MonoBehaviour
{
    [SerializeField]
    private Player player;
    [SerializeField]
    private Boss boss;
    [HideInInspector]
    private NavMeshAgent agent;

    private void Start() {
        this.agent = boss.agent;
    }

    private void Update() {
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Vector3 position = transform.position;
        position.y += 3.0f;
        Debug.DrawRay(position, forward, Color.green);
    }
    private void OnTriggerEnter(Collider coll) {
        if(coll.tag == "Player")
        {
            if(!agent.isStopped)
            {
                agent.SetDestination(player.transform.position);
            }
        }
    }

    private void OnTriggerStay(Collider coll) {
        if(coll.tag == "Player")
        {
            if(!agent.isStopped)
            {
                agent.SetDestination(player.transform.position);
            }
        }
    }
}