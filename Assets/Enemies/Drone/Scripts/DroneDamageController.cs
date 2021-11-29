using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneDamageController : MonoBehaviour
{
    public CapsuleCollider hurtbox;
    public DroneController droneController;
        
    private void OnTriggerEnter(Collider trigger) 
    {
        if(trigger.gameObject.CompareTag("hitbox"))
        {
            droneController.state.OnTakeDamage();
        }
    }
}