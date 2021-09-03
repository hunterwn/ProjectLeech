using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    public class Run : MonoBehaviour
    {
        string animid = "run";
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
            
            if(Mathf.Abs(player.current_speed) < player.run_maxspeed)
            {
                player.current_speed += player.run_acceleration * Time.deltaTime * facing_direction;
            }

            if(Mathf.Abs(player.current_speed) >  player.run_maxspeed)
            {
                player.current_speed = player.run_maxspeed * facing_direction;
            }

            if(!PlayerFunctions.CheckRunInput() || 
                (inputDir == facing_direction * -1))
            {
                this.enabled = false;
                GetComponent<RunBrake>().enabled = true;
            }
        }
    }
}