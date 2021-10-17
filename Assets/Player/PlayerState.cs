using System.Collections;
using System.Collections.Generic;
using UnityEngine;

  public class PlayerState : MonoBehaviour {
    public string animid;
    public Animator animator;
    public Player player;
    public Controller2D controller;
    PlayerInput playerInput;



    public void initializeState(string animid) {
      this.animid = animid;
      this.player = GetComponent<Player>();
      this.controller = GetComponent<Controller2D>();
      this.animator = GetComponent<Animator>();
      this.playerInput = GetComponent<PlayerInput>();
      animator.SetBool(this.animid, true);
    }

    // public bool CheckRunInput() {
    //   return Input.GetKey("left shift");
    // }

    // public bool CheckJumpInput() {
    //   return Input.GetKey("space");
    // }

    // public bool CheckJumpAerialInput() {
    //   return Input.GetKeyDown("space");
    // }

    // public int GetDirectionHeld() {
    //   if (Input.GetKey("a")) {
    //     return -1;
    //   } else if (Input.GetKey("d")) {
    //     return 1;
    //   } else {
    //     return 0;
    //   }
    // }

    public bool CheckAnimationFinished() {
      Animator animator = GetComponent<Animator>();
      return (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0));
    }

    public bool CheckAnimationTransition() {
      Animator animator = GetComponent<Animator>();
      AnimatorTransitionInfo currentTransition = animator.GetAnimatorTransitionInfo(0);
      return (currentTransition.duration > 0);
    }

    // public bool RayCastGround() {
    //   PlayerAnimController player = GetComponent<PlayerAnimController>();
    //   CapsuleCollider collider = GetComponent<CapsuleCollider>();
    //   float offset = 0.1f;
    //   Debug.DrawRay(collider.bounds.center, Vector3.down * (collider.bounds.extents.y + offset));
    //   return (Physics.Raycast(collider.bounds.center, Vector3.down, collider.bounds.extents.y + offset));
    // }

    // public void ReverseFacingDirection() {
    //   Animator animator = GetComponent<Animator>();
    //   int facing_dir = animator.GetInteger("facing_direction");
    //   facing_dir *= -1;
    //   animator.SetInteger("facing_direction", facing_dir);
    // }

    public void SetFacingDirection(int faceDir = 0) {
      Animator animator = GetComponent<Animator>();
      if(faceDir != 0)
      {
        animator.SetInteger("facing_direction", faceDir);
      } else if(player.directionalInput.x != 0)
      {
        animator.SetInteger("facing_direction", (int)player.directionalInput.x);
      }
    }

    // public void ApplyHorizontalFriction(float friction) {
    //   PlayerAnimController player = GetComponent<PlayerAnimController>();
    //   int movement_dir = (player.current_speed_h > 0) ? 1 : -1;
    //   if (Mathf.Abs(player.current_speed_h) > 0) {
    //     float newSpeed = player.current_speed_h - friction * Time.deltaTime * movement_dir;

    //     if (newSpeed > 0 && movement_dir > 0 || newSpeed < 0 && movement_dir < 0) {
    //       player.current_speed_h = newSpeed;
    //     }
    //   }
    // }

    // public void ApplyAerialDrift(float drift) {
    //   PlayerAnimController player = GetComponent<PlayerAnimController>();
    //   Animator animator = GetComponent<Animator>();
    //   int inputDir = GetDirectionHeld();
    //   if (inputDir != 0) {
    //     float newSpeed = player.current_speed_h + drift * Time.deltaTime * inputDir;
    //     player.current_speed_h = newSpeed;
    //     if (Mathf.Abs(newSpeed) < player.max_airdriftspeed) {
    //       player.current_speed_h = newSpeed;
    //     } else if (Mathf.Abs(newSpeed) > player.max_airdriftspeed) {
    //       if (newSpeed > 0) {
    //         player.current_speed_h = player.max_airdriftspeed;
    //       } else {
    //         player.current_speed_h = -1 * player.max_airdriftspeed;
    //       }
    //     }
    //   }
    // }

    // public void ApplyGravity(float gravity) {
    //   PlayerAnimController player = GetComponent<PlayerAnimController>();
    //   player.current_speed_v -= gravity * Time.deltaTime;
    // }

    // public int GetMovementDirection() {
    //   PlayerAnimController player = GetComponent<PlayerAnimController>();
    //   int movement_dir = (player.current_speed_h > 0) ? 1 : -1;
    //   return movement_dir;
    // }

    public void EnterIdle() {
      Animator animator = GetComponent<Animator>();
      this.enabled = false;
      animator.SetBool(this.animid, false);
      GetComponent<Idle>().enabled = true;
    }
    public void EnterFall() {
      Animator animator = GetComponent<Animator>();
      SetFacingDirection();
      this.enabled = false;
      animator.SetBool(this.animid, false);
      GetComponent<Fall>().enabled = true;
    }
    public void EnterWalk() {
      Animator animator = GetComponent<Animator>();
      SetFacingDirection();
      this.enabled = false;
      animator.SetBool(this.animid, false);
      GetComponent<Walk>().enabled = true;
    }
    // public void EnterRun() {
    //   Animator animator = GetComponent<Animator>();
    //   this.enabled = false;
    //   animator.SetBool(this.animid, false);
    //   GetComponent<Run>().enabled = true;
    // }
    // public void EnterRunBrake() {
    //   Animator animator = GetComponent<Animator>();
    //   this.enabled = false;
    //   animator.SetBool(this.animid, false);
    //   GetComponent<RunBrake>().enabled = true;
    // }
    public void EnterLanding() {
      Animator animator = GetComponent<Animator>();
      this.enabled = false;
      animator.SetBool(this.animid, false);
      GetComponent<Landing>().enabled = true;
    }
    public void EnterWallSlide() {
      Animator animator = GetComponent<Animator>();
      this.enabled = false;
      if(player.controller.collisions.left)
      {
        SetFacingDirection(1);
      } else {
        SetFacingDirection(-1);
      }
      animator.SetBool(this.animid, false);
      GetComponent<WallSlide>().enabled = true;
    }

    // public void EnterJumpSquat() {
    //   Animator animator = GetComponent<Animator>();
    //   this.enabled = false;
    //   animator.SetBool(this.animid, false);
    //   GetComponent<JumpSquat>().enabled = true;
    // }

    // public void EnterJump() {
    //   PlayerAnimController player = GetComponent<PlayerAnimController>();
    //   Animator animator = GetComponent<Animator>();
    //   if (player.jumps_left == 0) {
    //     return;
    //   }
    //   player.jumps_left -= 1;
    //   player.airstate = true;
    //   this.enabled = false;
    //   animator.SetBool(this.animid, false);
    //   GetComponent<Jump>().enabled = true;

    // }

    // public void EnterJumpAerial() {
    //   PlayerAnimController player = GetComponent<PlayerAnimController>();
    //   Animator animator = GetComponent<Animator>();
    //   if (player.jumps_left == 0) {
    //     return;
    //   }
    //   player.jumps_left -= 1;
    //   this.enabled = false;
    //   animator.SetBool(this.animid, false);
    //   GetComponent<JumpAerial>().enabled = true;

    // }
  }