using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    public class Run : MonoBehaviour
    {
        string animid = "run";
        void OnEnable() {
            GetComponent<Animator>().SetBool(this.animid, true);
        }
        void OnDisable() {
            GetComponent<Animator>().SetBool(this.animid, false);
        }
        void Update()
        {
            if(!PlayerFunctions.CheckRunInput())
            {
                this.enabled = false;
                if(PlayerFunctions.GetDirectionHeld() == 0)
                {
                    GetComponent<Idle>().enabled = true;
                } else {
                    GetComponent<RunBrake>().enabled = true;
                }
            }
        }
    }
}