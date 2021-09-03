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
            PlayerController player = GetComponent<PlayerController>();
            int facing_direction = animator.GetInteger("facing_direction");

            if(Mathf.Abs(player.current_speed) > 0)
            {
                player.current_speed -= player.ground_friction * Time.deltaTime * facing_direction;
            }

            if(player.current_speed < 0 && facing_direction > 0 ||
                player.current_speed > 0 && facing_direction < 0)
            {
                player.current_speed = 0;
            }

            if (PlayerFunctions.CheckAnimationFinished(animator))
            {
                this.enabled = false;
                int inputDir = PlayerFunctions.GetDirectionHeld();

                if(inputDir == 0)
                {
                    GetComponent<Idle>().enabled = true;
                    return;
                } else if (inputDir == facing_direction * -1)
                {
                    facing_direction *= -1;
                    animator.SetInteger("facing_direction", facing_direction);
                }
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