using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    public class Fall : MonoBehaviour
    {
        string animid = "fall";
        Animator animator;
        PlayerController player;
        void OnEnable() {
            player = GetComponent<PlayerController>();
            animator = GetComponent<Animator>();
            animator.SetBool(this.animid, true);
        }
        void OnDisable() {
            animator.SetBool(this.animid, false);
        }
    }
}