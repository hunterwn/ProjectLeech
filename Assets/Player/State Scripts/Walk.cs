using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    public class Walk : MonoBehaviour
    {
        string animid = "walk";
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
        void WalkR_Phys() {
            if(player.current_speed < player.walk_maxspeed)
            {
                player.current_speed += player.walk_acceleration * Time.deltaTime;
            }
            if(player.current_speed > player.walk_maxspeed)
            {
                player.current_speed = player.walk_maxspeed;
            }
        }
        void WalkL_Phys() {
            if(player.current_speed > player.walk_maxspeed * -1)
            {
                player.current_speed -= player.walk_acceleration * Time.deltaTime;
            }
            if(player.current_speed < player.walk_maxspeed * -1)
            {
                player.current_speed = player.walk_maxspeed * -1;
            }
        }
        
        void Update() {
            int inputDir = PlayerFunctions.GetDirectionHeld();
            int facing_dir = animator.GetInteger("facing_direction");

            if(facing_dir > 0)
            {
                WalkR_Phys();
            } else {
                WalkL_Phys();
            }



            if(PlayerFunctions.CheckRunInput())
            {
                this.enabled = false;
                GetComponent<Run>().enabled = true;
            }
            else if (PlayerFunctions.GetDirectionHeld() == 0)
            {
                this.enabled = false;
                GetComponent<Idle>().enabled = true;
            } else if (inputDir != facing_dir)
            {
                facing_dir *= -1;
                animator.SetInteger("facing_direction", facing_dir);
            }
        }
    }
}