using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Player {
    public class Walk : MonoBehaviour
    {
        string animid = "walk";
        void OnEnable() {
            GetComponent<Animator>().SetBool(this.animid, true);
        }
        void OnDisable() {
            GetComponent<Animator>().SetBool(this.animid, false);
        }
        void Update() {
            if(PlayerFunctions.CheckRunInput())
            {
                this.enabled = false;
                GetComponent<Run>().enabled = true;
            }
            else if (PlayerFunctions.GetDirectionHeld() == 0)
            {
                this.enabled = false;
                GetComponent<Idle>().enabled = true;
            }
        }
    }
}