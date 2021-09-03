using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    public class RunBrake : MonoBehaviour
    {
        string animid = "runbrake";
        Animator animator;
        void OnEnable() {
            animator = GetComponent<Animator>();
            animator.SetBool(this.animid, true);
        }
        void OnDisable() {
            animator.SetBool(this.animid, false);
        }
        void Update()
        {
             if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0))
             {
                 this.enabled = false;
                 if(PlayerFunctions.GetDirectionHeld() == 0)
                 {
                    GetComponent<Idle>().enabled = true;
                 } else {
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
}