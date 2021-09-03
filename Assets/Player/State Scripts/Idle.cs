using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    public class Idle : MonoBehaviour
    {
        string animid = "idle";
        void OnEnable() {
            GetComponent<Animator>().SetBool(this.animid, true);
        }
        void OnDisable() {
            GetComponent<Animator>().SetBool(this.animid, false);
        }
        // Update is called once per frame
        void Update() {
            print(transform.rotation.y);
            int inputDir = PlayerFunctions.GetDirectionHeld();
            int facing_direction = GetComponent<Animator>().GetInteger("facing_direction");

            if (!GetComponent<CharacterController>().isGrounded) {
                //this.enabled = false;
                //GetComponent<Falling>().enabled = true;
            } else if (inputDir != 0) {
                if(inputDir != facing_direction)
                {
                    facing_direction *= -1;
                    GetComponent<Animator>().SetInteger("facing_direction", facing_direction);
                } else {
                    this.enabled = false;
                    GetComponent<Walk>().enabled = true;
                }
            }
        }
    }
}