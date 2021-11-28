using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChomperDamageController : MonoBehaviour
{
    public CapsuleCollider hurtbox;
    public ChomperController chomperController;

    private void OnTriggerEnter(Collider trigger) {
        if(trigger.gameObject.CompareTag("hitbox"))
        {
            chomperController.OnTakeDamage();
        }
    }
}