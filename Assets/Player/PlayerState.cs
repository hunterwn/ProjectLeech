using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    public class PlayerState : MonoBehaviour
    {
        public string animid;
        public PlayerController player;
        public Animator animator;
        public CapsuleCollider coll;
        public CharacterController controller;

        public void initializeState(string animid)
        {
            this.animid = animid;
            this.player = player = GetComponent<PlayerController>();
            this.animator = GetComponent<Animator>();
            this.coll = GetComponent<CapsuleCollider>();
            this.controller = GetComponent<CharacterController>();
            animator.SetBool(this.animid, true);
        }
        
        public bool CheckRunInput()
        {
            return Input.GetKey("left shift");
        }

        public bool CheckJumpInput()
        {
            return Input.GetKey("space");
        }

        public bool CheckJumpAerialInput()
        {
            return Input.GetKeyDown("space");
        }

        public int GetDirectionHeld()
        {
            if(Input.GetKey("a"))
            {
                return -1;
            } else if (Input.GetKey("d")){
                return 1;
            } else {
                return 0;
            }
        }

        public bool CheckAnimationFinished()
        {
            Animator animator = GetComponent<Animator>();
            return (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0));
        }

        public bool CheckAnimationTransition()
        {
            Animator animator = GetComponent<Animator>();
            AnimatorTransitionInfo currentTransition = animator.GetAnimatorTransitionInfo(0);
            return (currentTransition.duration > 0);
        }

        public bool RayCastGround()
        {
            PlayerController player = GetComponent<PlayerController>();
            CapsuleCollider collider = GetComponent<CapsuleCollider>();
            float offset = 0.1f;
            Debug.DrawRay(collider.bounds.center, Vector3.down * (collider.bounds.extents.y + offset));
            return (Physics.Raycast(collider.bounds.center, Vector3.down, collider.bounds.extents.y + offset));
        }

        public void ReverseFacingDirection()
        {
            Animator animator = GetComponent<Animator>();
            int facing_dir = animator.GetInteger("facing_direction");
            facing_dir *= -1;
            animator.SetInteger("facing_direction", facing_dir);
        }

        public void ApplyHorizontalFriction(float friction)
        {
            PlayerController player = GetComponent<PlayerController>();
            int movement_dir = (player.current_speed_h > 0) ? 1 : -1;
            if(Mathf.Abs(player.current_speed_h) > 0)
            {
                float newSpeed = player.current_speed_h - friction * Time.deltaTime * movement_dir;
                
                if(newSpeed > 0 && movement_dir > 0 || newSpeed < 0 && movement_dir < 0)
                {
                    player.current_speed_h = newSpeed;
                }
            }
        }

        public void ApplyAerialDrift(float drift)
        {
            PlayerController player = GetComponent<PlayerController>();
            Animator animator = GetComponent<Animator>();
            int inputDir = GetDirectionHeld();
            if(inputDir != 0)
            {
                float newSpeed = player.current_speed_h + drift * Time.deltaTime * inputDir;
                player.current_speed_h = newSpeed;
                if(Mathf.Abs(newSpeed) < player.max_airdriftspeed)
                {
                    player.current_speed_h = newSpeed;
                } else if (Mathf.Abs(newSpeed) > player.max_airdriftspeed)
                {
                    if(newSpeed > 0)
                    {
                        player.current_speed_h = player.max_airdriftspeed;
                    } else {
                        player.current_speed_h = -1*player.max_airdriftspeed;
                    }
                }
            }
        }

        public void ApplyGravity(float gravity)
        {
            PlayerController player = GetComponent<PlayerController>();
            player.current_speed_v -= gravity * Time.deltaTime; 
        }

        public int GetMovementDirection()
        {
            PlayerController player = GetComponent<PlayerController>();
            int movement_dir = (player.current_speed_h > 0) ? 1 : -1;
            return movement_dir;
        }

        public void EnterIdle()
        {
            Animator animator = GetComponent<Animator>();
            this.enabled = false;
            animator.SetBool(this.animid, false);
            GetComponent<Idle>().enabled = true;
        }
        public void EnterFall()
        {
            PlayerController player = GetComponent<PlayerController>();
            Animator animator = GetComponent<Animator>();
            if(!player.airstate) {
                player.jumps_left -= 1;
                player.airstate = true;
            }
            this.enabled = false;
            animator.SetBool(this.animid, false);
            GetComponent<Fall>().enabled = true;
        }
        public void EnterWalk()
        {
            Animator animator = GetComponent<Animator>();
            this.enabled = false;
            animator.SetBool(this.animid, false);
            GetComponent<Walk>().enabled = true;
        }
        public void EnterRun()
        {
            Animator animator = GetComponent<Animator>();
            this.enabled = false;
            animator.SetBool(this.animid, false);
            GetComponent<Run>().enabled = true;
        }
        public void EnterRunBrake()
        {
            Animator animator = GetComponent<Animator>();
            this.enabled = false;
            animator.SetBool(this.animid, false);
            GetComponent<RunBrake>().enabled = true;
        }
        public void EnterLanding()
        {
            PlayerController player = GetComponent<PlayerController>();
            player.jumps_left = player.max_jumps;
            player.airstate = false;
            Animator animator = GetComponent<Animator>();
            this.enabled = false;
            animator.SetBool(this.animid, false);
            GetComponent<Landing>().enabled = true;
        }

        public void EnterJumpSquat()
        {
            Animator animator = GetComponent<Animator>();
            this.enabled = false;
            animator.SetBool(this.animid, false);
            GetComponent<JumpSquat>().enabled = true;
        }

        public void EnterJump()
        {
            PlayerController player = GetComponent<PlayerController>();
            Animator animator = GetComponent<Animator>();
            if(player.jumps_left == 0) {
                return;
            }
            player.jumps_left -= 1;
            player.airstate = true;
            this.enabled = false;
            animator.SetBool(this.animid, false);
            GetComponent<Jump>().enabled = true;
            
        }

        public void EnterJumpAerial()
        {
            PlayerController player = GetComponent<PlayerController>();
            Animator animator = GetComponent<Animator>();
            if(player.jumps_left == 0) {
                return;
            }
            player.jumps_left -= 1;
            this.enabled = false;
            animator.SetBool(this.animid, false);
            GetComponent<JumpAerial>().enabled = true;
            
        }
    }
}