using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KeepInbounds : MonoBehaviour
{
    public CapsuleCollider coll;
    public Player player;
    private Vector3 pushDirection;
    private float pushForce = 2.0f;
    private float limit = 4.0f;
    private Collider current_collider;
    private void OnCollisionEnter(Collision collision) {
        //check for grates
        if(collision.gameObject.tag == "Grate")
        {
            return;
        }
        if(collision.gameObject.layer == 8)
        {
            player.movementDisabled = false;
            this.current_collider = collision.collider;
            this.pushDirection = player.velocity * -1.0f;
            player.addedVelocity = pushDirection * pushForce;
        }
    }

    private void OnCollisionStay(Collision collision) {
        if(collision.collider == this.current_collider)
        {
            if(player.velocity.magnitude < limit)
            {
                player.addedVelocity = pushDirection * pushForce;
            }
        }
    }

    private void OnCollisionExit(Collision collision) {
        if(collision.collider == this.current_collider)
        {
            player.movementDisabled = false;
            this.current_collider = null;
        }
    }
}