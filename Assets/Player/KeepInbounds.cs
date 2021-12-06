using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KeepInbounds : MonoBehaviour
{
    public CapsuleCollider coll;
    public Player player;
    private Vector3 pushDirection;
    float pushForce = 2.0f;
    private void OnCollisionEnter(Collision collision) {
        //check for grates
        if(collision.gameObject.tag == "Grate")
        {
            return;
        }
        if(collision.gameObject.layer == 8)
        {
            pushDirection = player.velocity * -1.0f;
            player.addedVelocity = pushDirection * pushForce;
        }
    }
}