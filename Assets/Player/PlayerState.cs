using System.Collections;
using System.Collections.Generic;
using UnityEngine;

  public class PlayerState : MonoBehaviour {
    public string animid;
    public Animator animator;
    public Player player;
    public Controller2D controller;
    public HitboxController hitboxController;
    PlayerInput playerInput;

    public int currentFrame;

    public void initializeState(string animid) {
      this.animid = animid;
      this.player = GetComponent<Player>();
      this.controller = GetComponent<Controller2D>();
      this.animator = GetComponent<Animator>();
      this.playerInput = GetComponent<PlayerInput>();
      this.hitboxController = GetComponent<HitboxController>();
      this.currentFrame = 0;
      animator.SetBool(this.animid, true);
      player.state = this;
    }

    public bool CheckAnimationFinished() {
      return (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0));
    }

    public bool CheckAnimationTransition() {
      AnimatorTransitionInfo currentTransition = animator.GetAnimatorTransitionInfo(0);
      return (currentTransition.duration > 0);
    }

    public void SetFacingDirection(int faceDir = 0) {
      if(faceDir != 0)
      {
        animator.SetInteger("facing_direction", faceDir);
      } else if(player.directionalInput.x != 0)
      {
        animator.SetInteger("facing_direction", (int)player.directionalInput.x);
      }
    }
    public int GetFacingDirection() {
        return animator.GetInteger("facing_direction");
    }

    public void EnterIdle() {
      
      this.enabled = false;
      animator.SetBool(this.animid, false);
      GetComponent<Idle>().enabled = true;
    }
    public void EnterAttack1() {
      this.enabled = false;
      animator.SetBool(this.animid, false);
      GetComponent<Attack1>().enabled = true;
    }
    public void EnterAttack2() {
      
      this.enabled = false;
      animator.SetBool(this.animid, false);
      GetComponent<Attack2>().enabled = true;
    }
    public void EnterAttack3() {
      
      this.enabled = false;
      animator.SetBool(this.animid, false);
      GetComponent<Attack3>().enabled = true;
    }
    public void EnterAirKick() {
      this.enabled = false;
      animator.SetBool(this.animid, false);
      GetComponent<AirKick>().enabled = true;
    }
    public void EnterFall() {
      
      SetFacingDirection();
      this.enabled = false;
      animator.SetBool(this.animid, false);
      GetComponent<Fall>().enabled = true;
    }
    public void EnterWalk() {
      
      SetFacingDirection();
      this.enabled = false;
      animator.SetBool(this.animid, false);
      GetComponent<Walk>().enabled = true;
    }
    public void EnterLanding() {
      
      this.enabled = false;
      animator.SetBool(this.animid, false);
      GetComponent<Landing>().enabled = true;
    }
    public void EnterWallSlide() {
      
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

    public void EnterDeath() {
      this.enabled = false;
      animator.SetBool(this.animid, false);
      GetComponent<Death>().enabled = true;
    }
  }