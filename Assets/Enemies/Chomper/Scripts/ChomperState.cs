using System.Collections;
using System.Collections.Generic;
using UnityEngine;

  public class ChomperState : MonoBehaviour {
    public string animid;
    public Animator animator;
    public HitboxController hitboxController;
    public ChomperController chomperController;
    public int currentFrame;

    public void initializeState(string animid) {
      this.animid = animid;
      this.animator = GetComponent<Animator>();
      this.hitboxController = GetComponent<HitboxController>();
      this.chomperController = GetComponent<ChomperController>();
      this.currentFrame = 0;
      animator.SetBool(this.animid, true);
      chomperController.state = this;
    }

    public virtual void OnTakeDamage()
    {
        if(chomperController.hp == 3)
        {
            EnterHit1();
        } else if (chomperController.hp == 2)
        {
            EnterHit2();
        } else if (chomperController.hp == 1) 
        {
            EnterHit3();
        }

        chomperController.hp = (chomperController.hp == 1) ? 3 : chomperController.hp - 1;
    }

    public bool CheckAnimationFinished() {
      return (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0));
    }

    public bool CheckAnimationTransition() {
      AnimatorTransitionInfo currentTransition = animator.GetAnimatorTransitionInfo(0);
      return (currentTransition.duration > 0);
    }
    public void EnterIdle() {
      this.enabled = false;
      animator.SetBool(this.animid, false);
      GetComponent<ChomperIdle>().enabled = true;
    }

    public void EnterWalk() {
      this.enabled = false;
      animator.SetBool(this.animid, false);
      GetComponent<ChomperWalk>().enabled = true;
    }

    public void EnterAttack() {
      this.enabled = false;
      animator.SetBool(this.animid, false);
      GetComponent<ChomperAttack>().enabled = true;
    }

    public void EnterHit1() {
      this.enabled = false;
      animator.SetBool(this.animid, false);
      GetComponent<ChomperHit1>().enabled = true;
    }

    public void EnterHit2() {
      this.enabled = false;
      animator.SetBool(this.animid, false);
      GetComponent<ChomperHit2>().enabled = true;
    }

    public void EnterHit3() {
      this.enabled = false;
      animator.SetBool(this.animid, false);
      GetComponent<ChomperHit3>().enabled = true;
    }
  }