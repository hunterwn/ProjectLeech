using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    public class Idle : MonoBehaviour
    {
        string animid = "idle";
        Animator animator;
        void OnEnable() {
            animator = GetComponent<Animator>();
            animator.SetBool(this.animid, true);
        }
        void OnDisable() {
            animator.SetBool(this.animid, false);
        }
        void Update() {
            PlayerController player = GetComponent<PlayerController>();
            int inputDir = PlayerFunctions.GetDirectionHeld();
            int facing_direction = animator.GetInteger("facing_direction");

            player.current_speed = 0;

            if(inputDir == facing_direction * -1) 
            {
                facing_direction *= -1;
                animator.SetInteger("facing_direction", facing_direction);
            } else if(inputDir == facing_direction) 
            {
                this.enabled = false;
                if(PlayerFunctions.CheckRunInput())
                {
                    GetComponent<Run>().enabled = true;
                } else {
                    GetComponent<Walk>().enabled = true;
                }
            }
        }
    }
}