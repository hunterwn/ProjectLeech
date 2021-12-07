using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChomperDamageController : MonoBehaviour
{
    public CapsuleCollider hurtbox;
    public ChomperController chomperController;
    private Collider last_collider;

    private void OnTriggerEnter(Collider collider) {
        if(last_collider != collider)
        {
            if(collider.gameObject.CompareTag("hitbox"))
            {
                this.last_collider = collider;
                chomperController.state.OnTakeDamage();
            }
        }
    }
}